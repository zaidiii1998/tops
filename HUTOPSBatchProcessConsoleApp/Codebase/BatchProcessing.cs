using HUTOPSBatchProcessConsoleApp.EAppDBModel;
using HUTOPSBatchProcessConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace HUTOPSBatchProcessConsoleApp.Codebase
{
    public static class BatchProcessing
    {
        static HUTOPSEntities DB = new HUTOPSEntities();
        public static List<ExcelData> GenerateAdmitCard(List<ExcelData> records, string shift, string venue, string testDate)
        {
            try
            {
                foreach (var record in records)
                {
                    try
                    {
                        var isValid = true;

                        Helper.AddLog(Constants.LogType.ActivityLog, $"Service request to fetch record against HUTOPSId : {record.HUTOPSIds}");

                        var personalInformation = DB.PersonalInformations.ToList().Where(x => x.HUTopId.Trim() == record.HUTOPSIds.Trim()).FirstOrDefault();
                        var document = new Document();
                        var EducationalInfo = new Educational();
                        var EmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Id == 3).FirstOrDefault();
                        string EmailBody = EmailTemplate.Body;

                        if (personalInformation != null)
                        {
                            document = DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null? new Document() : DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                            EducationalInfo = DB.Educationals.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null? new Educational(): DB.Educationals.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();

                            EmailBody = EmailBody.Replace("{{HUTOPSId}}", personalInformation.HUTopId);
                            EmailBody = EmailBody.Replace("{{Name}}", personalInformation.FirstName + " " + personalInformation.MiddleName + " " + personalInformation.LastName);
                            EmailBody = EmailBody.Replace("{{CNIC}}", personalInformation.CNIC);
                        }
                        else
                        {
                            record.Status = "Record not found "; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
                            continue;
                        }
                        if (EducationalInfo.Id != 0)
                        {
                            EmailBody = EmailBody.Replace("{{HUSchool}}", EducationalInfo.HUSchoolName == "SE" ? Constants.SchoolName.SE : Constants.SchoolName.SA);
                        }
                        else
                        {
                            record.Status = "Educational Information not Found"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Educational Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
                            continue;
                        }
                        if (document.Id != 0)
                        {
                            EmailBody = EmailBody.Replace("{{Photo}}", document.Photograph);
                        }
                        else
                        {
                            record.Status = "Documents not found against this HUTOPS Id"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Documents Record not found against HUTOPS Id: {record.HUTOPSIds}");
                            continue;
                        }
                        EmailBody = EmailBody.Replace("{{TestDate}}", testDate);
                        EmailBody = EmailBody.Replace("{{ApproxDatetime}}", shift == "1" ? Constants.Shift.FirstShift : shift == "2" ? Constants.Shift.SecondShift : Constants.Shift.ThirtShift);
                        EmailBody = EmailBody.Replace("{{ReportingTime}}", shift == "1" ? Constants.ReportingTime.FirstShift : shift == "2" ? Constants.ReportingTime.SecondShift : Constants.ReportingTime.ThirtShift);
                        EmailBody = EmailBody.Replace("{{Vanue}}", venue == "Karachi" ? Constants.Vanue.Karachi : Constants.Vanue.Islamabad);

                        // Save File to server
                        if (isValid)
                        {
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Service fetched user Inforation and mapped against HUTOPSId: {record.HUTOPSIds}");

                            string baseFilePath = System.Configuration.ConfigurationSettings.AppSettings["PhysicalPath"].ToString() + personalInformation.Id;
                            var filePath = HU_HTML_TO_PDF.Converter.ConvertHTML_TO_PDF(EmailBody, baseFilePath, "A4", "Portrait");
                            // Update Admit Card Status in perssonal Info Table

                            Helper.AddLog(Constants.LogType.ActivityLog, $"Admit Card genrated against HUTOPSId : {record.HUTOPSIds}");

                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                                personalInfo.IsAdmitCardGenerated = 1;
                                personalInfo.AdmitCardGeneratedOn = DateTime.Now;
                                tempDB.SaveChanges();
                            }
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Update Admit Card Status in the personal Information Table Against HUTOPSId : {record.HUTOPSIds}");
                            // Update Admit Card Path in Documents Table
                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var documents = tempDB.Documents.ToList().Where(x => x.Id == document.Id).FirstOrDefault();
                                documents.AdmitCard = filePath;
                                tempDB.SaveChanges();
                            }
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Update Admit card Path in the documents table against HUTOPSId : {record.HUTOPSIds}");
                            // Send Email
                            
                            record.Status = "Admit Card Genrated Successfully";
                        }
                        else
                        {
                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.HUTopId == record.HUTOPSIds).FirstOrDefault();
                                if ( personalInfo != null)
                                {
                                    personalInfo.IsAdmitCardGenerated = 0;
                                    tempDB.SaveChanges();
                                }
                                
                            }
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Admit Card genration failed against HUTOPSId : {record.HUTOPSIds}");
                        }
                    }
                    catch (Exception ex)
                    {
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.HUTopId == record.HUTOPSIds).FirstOrDefault();
                            personalInfo.IsAdmitCardGenerated = 0;
                            tempDB.SaveChanges();
                        }
                        Helper.AddLog(Constants.LogType.Exception, $"Error Occured while processing Batch Record against HUTOPSId : {record.HUTOPSIds} Error Details: {ex.Message}");
                        record.Status = "Failed" + ex.Message;
                    }

                }
                return records;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<ExcelData> SendAdmitCard(List<ExcelData> records)
        {
            try
            {
                foreach (var record in records)
                {
                    try
                    {
                        var isValid = true;

                        Helper.AddLog(Constants.LogType.ActivityLog, $"Service request to fetch record against HUTOPSId : {record.HUTOPSIds}");


                        var personalInformation = DB.PersonalInformations.ToList().Where(x => x.HUTopId.Trim() == record.HUTOPSIds.Trim()).FirstOrDefault();
                        var document = new Document();
                        var EmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Id == 4).FirstOrDefault();
                        if (personalInformation == null)
                        {
                            record.Status = "Record not found "; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found against HUTOPS Id: {record.HUTOPSIds.ToString()}");
                            continue;
                        }
                        else
                        {
                            document = DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null ? new Document() : DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                        }

                        if (document.Id == 0)
                        {
                            record.Status = "Documents not found against this HUTOPS Id"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Documents Record not found against HUTOPS Id: {record.HUTOPSIds.ToString()}");
                            continue;
                        }

                        // Save File to server
                        if (isValid)
                        {
                            var admitCardPath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), document.AdmitCard.Substring(document.AdmitCard.IndexOf("Upload")));

                            if (File.Exists(admitCardPath))
                            {
                                // Send Email
                                CPD.Framework.Core.EmailService.SendEmail(personalInformation.EmailAddress, null, null, EmailTemplate.Subject, EmailTemplate.Body, admitCardPath, "tops@habib.edu.pk", null);

                                Helper.AddLog(Constants.LogType.ActivityLog, $"Email has been sent to Applicant against HUTOPSId : {record.HUTOPSIds} ");
                                // Update sent Admit Card Status in perssonal Info Table
                                using (HUTOPSEntities tempDB = new HUTOPSEntities())
                                {
                                    var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                                    personalInfo.IsAdmitCardSent = 1;
                                    personalInfo.AdmitCardSentOn = DateTime.Now;
                                    tempDB.SaveChanges();
                                }
                                Helper.AddLog(Constants.LogType.ActivityLog, $"Update Email sent status in the personal Information table against HUTOPSId : {record.HUTOPSIds}");

                                record.Status = "Admit Card Sent successfully";
                            }
                            else {
                                Helper.AddLog(Constants.LogType.ActivityLog, $"Admit Card not found against HUTOPSId : {record.HUTOPSIds}");

                                record.Status = "Admit Card not Exist";
                                using (HUTOPSEntities tempDB = new HUTOPSEntities())
                                {
                                    var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                                    personalInfo.IsAdmitCardSent = 0;
                                    tempDB.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                                personalInfo.IsAdmitCardSent = 0;
                                tempDB.SaveChanges();
                            }
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Admit Card sending failed against HUTOPSId : {record.HUTOPSIds}");
                        }
                    }
                    catch (Exception ex)
                    {
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.HUTopId == record.HUTOPSIds).FirstOrDefault();
                            if (personalInfo != null)
                            {
                                personalInfo.IsAdmitCardSent = 0;
                                tempDB.SaveChanges();
                            }
                        }
                        Helper.AddLog(Constants.LogType.Exception, $"Error Occured while processing Batch Record against HUTOPSId : {record.HUTOPSIds} Error Details: {ex.Message}");
                        record.Status = "Failed" + ex.Message;
                    }

                }
                return records;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<ExcelData> GenerateSendAdmitCard(List<ExcelData> records, string shift, string venue, string testDate)
        {
            try
            {
                foreach (var record in records)
                {
                    try
                    {
                        var isValid = true;

                        Helper.AddLog(Constants.LogType.ActivityLog, $"Service request to fetch record against HUTOPSId : {record.HUTOPSIds}");


                        var personalInformation = DB.PersonalInformations.ToList().Where(x => x.HUTopId.Trim() == record.HUTOPSIds.Trim()).FirstOrDefault();
                        var document = new Document();
                        var EducationalInfo = new Educational();
                        var AdmmitCardEmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Id == 3).FirstOrDefault();
                        var EmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Id == 4).FirstOrDefault();
                        string EmailBody = AdmmitCardEmailTemplate.Body;

                        if (personalInformation != null)
                        {
                            document = DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null ? new Document() : DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                            EducationalInfo = DB.Educationals.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null ? new Educational() : DB.Educationals.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();

                            EmailBody = EmailBody.Replace("{{HUTOPSId}}", personalInformation.HUTopId);
                            EmailBody = EmailBody.Replace("{{Name}}", personalInformation.FirstName + " " + personalInformation.MiddleName + " " + personalInformation.LastName);
                            EmailBody = EmailBody.Replace("{{CNIC}}", personalInformation.CNIC);
                        }
                        else
                        {
                            record.Status = "Record not found "; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
                            continue;
                        }
                        if (EducationalInfo.Id != 0)
                        {
                            EmailBody = EmailBody.Replace("{{HUSchool}}", EducationalInfo.HUSchoolName == "SE" ? Constants.SchoolName.SE : Constants.SchoolName.SA);
                        }
                        else
                        {
                            record.Status = "Educational Information not Found"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Educational Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
                            continue;
                        }
                        if (document.Id != 0)
                        {
                            EmailBody = EmailBody.Replace("{{Photo}}", document.Photograph);
                        }
                        else
                        {
                            record.Status = "Documents not found against this HUTOPS Id"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Documents Record not found against HUTOPS Id: {record.HUTOPSIds}");
                            continue;
                        }
                        EmailBody = EmailBody.Replace("{{TestDate}}", testDate);
                        EmailBody = EmailBody.Replace("{{ApproxDatetime}}", shift == "1" ? Constants.Shift.FirstShift : shift == "2" ? Constants.Shift.SecondShift : Constants.Shift.ThirtShift);
                        EmailBody = EmailBody.Replace("{{ReportingTime}}", shift == "1" ? Constants.ReportingTime.FirstShift : shift == "2" ? Constants.ReportingTime.SecondShift : Constants.ReportingTime.ThirtShift);
                        EmailBody = EmailBody.Replace("{{Vanue}}", venue == "Karachi" ? Constants.Vanue.Karachi : Constants.Vanue.Islamabad);

                        // Save File to server
                        if (isValid)
                        {
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Service fetched user Inforation and mapped against HUTOPSId: {record.HUTOPSIds}");

                            string baseFilePath = System.Configuration.ConfigurationSettings.AppSettings["PhysicalPath"].ToString() + personalInformation.Id;
                            var filePath = HU_HTML_TO_PDF.Converter.ConvertHTML_TO_PDF(EmailBody, baseFilePath, "A4", "Portrait");
                            // Update Admit Card Status in perssonal Info Table

                            Helper.AddLog(Constants.LogType.ActivityLog, $"Admit Card genrated against HUTOPSId : {record.HUTOPSIds}");

                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                                personalInfo.IsAdmitCardGenerated = 1;
                                personalInfo.AdmitCardGeneratedOn = DateTime.Now;
                                tempDB.SaveChanges();
                            }
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Update Admit Card Status in the personal Information Table Against HUTOPSId : {record.HUTOPSIds}");
                            // Update Admit Card Path in Documents Table

                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var documents = tempDB.Documents.ToList().Where(x => x.Id == document.Id).FirstOrDefault();
                                documents.AdmitCard = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], filePath.Substring(filePath.IndexOf("Upload")));
                                tempDB.SaveChanges();
                            }
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Update Admit card Path in the documents table against HUTOPSId : {record.HUTOPSIds}");
                            
                            // Send Email
                            CPD.Framework.Core.EmailService.SendEmail(personalInformation.EmailAddress, null, null, AdmmitCardEmailTemplate.Subject, EmailTemplate.Body, filePath, "tops@habib.edu.pk", null);

                            Helper.AddLog(Constants.LogType.ActivityLog, $"Email has been sent to Applicant against HUTOPSId : {record.HUTOPSIds} ");
                            // Update sent Admit Card Status in perssonal Info Table
                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                                personalInfo.IsAdmitCardSent = 1;
                                personalInfo.AdmitCardSentOn = DateTime.Now;
                                tempDB.SaveChanges();
                            }
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Update Email sent status in the personal Information table against HUTOPSId : {record.HUTOPSIds}");

                            record.Status = "Admit Card Genrated and Email Sent";
                        }
                        else
                        {
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Admit card generation failed against HUTOPSId : {record.HUTOPSIds}");

                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                                personalInfo.IsAdmitCardGenerated = 0;
                                tempDB.SaveChanges();
                            }
                            using (HUTOPSEntities tempDB = new HUTOPSEntities())
                            {
                                var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                                personalInfo.IsAdmitCardSent = 0;
                                tempDB.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Helper.AddLog(Constants.LogType.Exception, $"Error Occured while processing Batch Record against HUTOPSId : {record.HUTOPSIds} Error Details: {ex.Message}");
                        record.Status = "Failed" + ex.Message;
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.HUTopId == record.HUTOPSIds).FirstOrDefault();
                            personalInfo.IsAdmitCardGenerated = 0;
                            tempDB.SaveChanges();
                        }
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.HUTopId == record.HUTOPSIds).FirstOrDefault();
                            personalInfo.IsAdmitCardSent = 0;
                            tempDB.SaveChanges();
                        }
                    }

                }
                return records;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<ExcelData> UpdateResult(List<ExcelData> records, byte? result, byte? IsRecordSendToEApp)
        {
            try
            {
                foreach (var record in records)
                {
                    try
                    {
                        Helper.AddLog(Constants.LogType.ActivityLog, $"Service request to fetch record against HUTOPSId : {record.HUTOPSIds}");

                        var personalInformation = DB.PersonalInformations.ToList().Where(x => x.HUTopId.Trim() == record.HUTOPSIds.Trim()).FirstOrDefault();
                        var educationalInformation = new Educational();
                        if (personalInformation != null)
                        {
                            educationalInformation = DB.Educationals.Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                            personalInformation.Result = result;
                            DB.SaveChanges();
                            record.Status = "Result Updated Successfully";
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Result Updated against HUTOPS Id: {record.HUTOPSIds}");
                            
                            if(IsRecordSendToEApp == 1)
                            {
                                if (personalInformation.IsRecordMoveToEApp != 1)
                                {
                                    if (educationalInformation.Id != 0) {
                                        if (personalInformation.Result == 2)
                                        {
                                            var EApp_PersonalInformation = new EAppDBModel.PersonalInformation() {
                                                Appid = "",
                                                FirstName = personalInformation.FirstName,
                                                MiddleName = personalInformation.MiddleName,
                                                LastName = personalInformation.LastName,
                                                Gender = personalInformation.Gender,
                                                HusbandName = personalInformation.HusbandName,
                                                FatherFirstName = personalInformation.FatherFirstName,
                                                FatherMiddleName = personalInformation.FatherMiddleName,
                                                FatherLastName = personalInformation.FatherLastName,
                                                FatherName = personalInformation.FatherFirstName + personalInformation.FatherMiddleName + personalInformation.FatherLastName,
                                                DateofBirth = personalInformation.DateOfBirth,
                                                CountryAdd = null,
                                                PhoneNo = personalInformation.CellPhoneNumber,
                                                Tellus = null,
                                                CurrentAddress = personalInformation.ResidentialAddress,
                                                PostalAddress = personalInformation.ResidentialAddress,
                                                Country = personalInformation.ResidentialCountry,
                                                City = personalInformation.ResidentialCity,
                                                Provience = personalInformation.ResidentialProvince,
                                                Postal = null,
                                                Email = personalInformation.EmailAddress,
                                                AlterEmail = personalInformation.AlterEmailAddress,
                                                Password = personalInformation.Password,
                                                CPassword = personalInformation.Password,
                                                Savedate = personalInformation.CreatedDatetime,
                                                Updatedate = personalInformation.UpdateDate,
                                                AppStatus = 1,
                                                StudentStatus = "0",
                                                TempID = null,
                                                HereYou = personalInformation.HearAboutHU,
                                                HereOther = personalInformation.HearAboutHUOther,
                                                NewEmail = "",
                                                Discount = null,
                                                DiscountFee = null,
                                                Yourinterests = null,
                                                YourinterestsOther = null,

                                                Permanent_addres = personalInformation.PermanentAddress,
                                                Permanent_country = personalInformation.PermanentCountry,
                                                Permanent_provience = personalInformation.PermanentProvince,
                                                Permanent_city = personalInformation.PermanentCity,
                                                Permanent_cityother = personalInformation.PermanentCityOther,
                                                Permanent_postal = null,


                                                Postal_addres = personalInformation.ResidentialAddress,
                                                Postal_country = personalInformation.ResidentialCountry,
                                                Postal_provience = personalInformation.ResidentialProvince,
                                                Postal_city = personalInformation.ResidentialCity,
                                                Postal_cityother = personalInformation.ResidentialCityOther,
                                                Postal_postal = null,

                                                AppliedBefore = personalInformation.IsAppliedBefore == 0 ? "No" : "Yes",
                                                AppliedBeforeMonth = null,
                                                AppliedBeforeYear = personalInformation.AppliedBeforeYear,

                                                WhatsAppNumber = personalInformation.WhatsAppNumber,

                                                TestDate = personalInformation.TestDate,
                                                SubmissionDate = personalInformation.SubmissionDate,
                                                HUTopsCandidate = "Yes",
                                                HuTopsId = personalInformation.HUTopId,

                                                // Education 
                                                School = educationalInformation.HUSchoolName,
                                                SchoolName = "Other",
                                                Currentqualification = educationalInformation.CurrentLevelOfEdu,
                                                AlternateMobile = personalInformation.AlternateCellPhoneNumber,
                                                Alternatelandline = personalInformation.AlternateLandline,
                                                Cityother = personalInformation.ResidentialCityOther,
                                                OtherCurrentqualification = educationalInformation.HSSCSchoolName,
                                                Userid = null,
                                                chk = null,
                                                Modeofstudy = null,
                                                BoardofEducation = educationalInformation.HSSCBoardName,
                                                Intendedprogram = educationalInformation.IntendedProgram,
                                                Retake = false,
                                                CurrentHighSchoolOther = educationalInformation.HSSCSchoolName,
                                                CurrentHighSchoolCode_old = 0,
                                                BoardofEducationOther = "",
                                                CurrentHighSchoolCode = "-1"
                                            };

                                            using (EApplicationEntities EApp_DB = new EApplicationEntities())
                                            {
                                                EApp_DB.PersonalInformations.Add(EApp_PersonalInformation);
                                                EApp_DB.SaveChanges();
                                            }
                                            personalInformation.IsRecordMoveToEApp = 1;
                                            DB.SaveChanges();

                                            record.Status = " Result Updated and Record Move to E-Application Successfully";
                                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Result Updated and Record Move to E-Application against HUTOPS Id: {record.HUTOPSIds}");

                                        }
                                        else
                                        {
                                            record.Status = "Record Mark as Failed";
                                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already move to E-Application against HUTOPS Id: {record.HUTOPSIds}");
                                        }
                                    }
                                    else
                                    {
                                        record.Status = "Educational Information is not found";
                                        Helper.AddLog(Constants.LogType.ActivityLog, $"Educational Information not found against HUTOPS Id: {record.HUTOPSIds}");
                                    }

                                }
                                else
                                {
                                    record.Status = "Record already move to E-Application";
                                    Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already move to E-Application against HUTOPS Id: {record.HUTOPSIds}");
                                }
                            }
                        }
                        else
                        {
                            record.Status = "Record not found ";
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
                            continue;
                        }

                    }
                    catch (Exception ex)
                    {
                        Helper.AddLog(Constants.LogType.Exception, $"Error Occured while processing Batch Record against HUTOPSId : {record.HUTOPSIds} Error Details: {ex.Message}");
                        record.Status = "Failed" + ex.Message;
                    }

                }
                return records;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<ExcelData> ShiftRecordToEApp(List<ExcelData> records)
        {
            try
            {
                foreach (var record in records)
                {
                    try
                    {
                        Helper.AddLog(Constants.LogType.ActivityLog, $"Service request to fetch record against HUTOPSId : {record.HUTOPSIds}");


                        var personalInformation = DB.PersonalInformations.ToList().Where(x => x.HUTopId.Trim() == record.HUTOPSIds.Trim()).FirstOrDefault();
                        var educationalInformation = new Educational();
                        if (personalInformation != null)
                        {
                            educationalInformation = DB.Educationals.Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                            if (personalInformation.IsRecordMoveToEApp != 1)
                            {
                                if (educationalInformation.Id != 0)
                                {
                                    if (personalInformation.Result == 2)
                                    {
                                        var EApp_PersonalInformation = new EAppDBModel.PersonalInformation()
                                        {
                                            Appid = "",
                                            FirstName = personalInformation.FirstName,
                                            MiddleName = personalInformation.MiddleName,
                                            LastName = personalInformation.LastName,
                                            Gender = personalInformation.Gender,
                                            HusbandName = personalInformation.HusbandName,
                                            FatherFirstName = personalInformation.FatherFirstName,
                                            FatherMiddleName = personalInformation.FatherMiddleName,
                                            FatherLastName = personalInformation.FatherLastName,
                                            FatherName = personalInformation.FatherFirstName + personalInformation.FatherMiddleName + personalInformation.FatherLastName,
                                            DateofBirth = personalInformation.DateOfBirth,
                                            CountryAdd = null,
                                            PhoneNo = personalInformation.CellPhoneNumber,
                                            Tellus = null,
                                            CurrentAddress = personalInformation.ResidentialAddress,
                                            PostalAddress = personalInformation.ResidentialAddress,
                                            Country = personalInformation.ResidentialCountry,
                                            City = personalInformation.ResidentialCity,
                                            Provience = personalInformation.ResidentialProvince,
                                            Postal = null,
                                            Email = personalInformation.EmailAddress,
                                            AlterEmail = personalInformation.AlterEmailAddress,
                                            Password = personalInformation.Password,
                                            CPassword = personalInformation.Password,
                                            Savedate = personalInformation.CreatedDatetime,
                                            Updatedate = personalInformation.UpdateDate,
                                            AppStatus = 1,
                                            StudentStatus = "0",
                                            TempID = null,
                                            HereYou = personalInformation.HearAboutHU,
                                            HereOther = personalInformation.HearAboutHUOther,
                                            NewEmail = "",
                                            Discount = null,
                                            DiscountFee = null,
                                            Yourinterests = null,
                                            YourinterestsOther = null,

                                            Permanent_addres = personalInformation.PermanentAddress,
                                            Permanent_country = personalInformation.PermanentCountry,
                                            Permanent_provience = personalInformation.PermanentProvince,
                                            Permanent_city = personalInformation.PermanentCity,
                                            Permanent_cityother = personalInformation.PermanentCityOther,
                                            Permanent_postal = null,


                                            Postal_addres = personalInformation.ResidentialAddress,
                                            Postal_country = personalInformation.ResidentialCountry,
                                            Postal_provience = personalInformation.ResidentialProvince,
                                            Postal_city = personalInformation.ResidentialCity,
                                            Postal_cityother = personalInformation.ResidentialCityOther,
                                            Postal_postal = null,

                                            AppliedBefore = personalInformation.IsAppliedBefore == 0 ? "No" : "Yes",
                                            AppliedBeforeMonth = null,
                                            AppliedBeforeYear = personalInformation.AppliedBeforeYear,

                                            WhatsAppNumber = personalInformation.WhatsAppNumber,

                                            TestDate = personalInformation.TestDate,
                                            SubmissionDate = personalInformation.SubmissionDate,
                                            HUTopsCandidate = "Yes",
                                            HuTopsId = personalInformation.HUTopId,

                                            // Education 
                                            School = educationalInformation.HUSchoolName,
                                            SchoolName = "Other",
                                            Currentqualification = educationalInformation.CurrentLevelOfEdu,
                                            AlternateMobile = personalInformation.AlternateCellPhoneNumber,
                                            Alternatelandline = personalInformation.AlternateLandline,
                                            Cityother = personalInformation.ResidentialCityOther,
                                            OtherCurrentqualification = educationalInformation.HSSCSchoolName,
                                            Userid = null,
                                            chk = null,
                                            Modeofstudy = null,
                                            BoardofEducation = educationalInformation.HSSCBoardName,
                                            Intendedprogram = educationalInformation.IntendedProgram,
                                            Retake = false,
                                            CurrentHighSchoolOther = educationalInformation.HSSCSchoolName,
                                            CurrentHighSchoolCode_old = 0,
                                            BoardofEducationOther = "",
                                            CurrentHighSchoolCode = "-1"
                                        };

                                        using (EApplicationEntities EApp_DB = new EApplicationEntities())
                                        {
                                            EApp_DB.PersonalInformations.Add(EApp_PersonalInformation);
                                            EApp_DB.SaveChanges();
                                        }
                                        personalInformation.IsRecordMoveToEApp = 1;
                                        DB.SaveChanges();

                                        record.Status = "Record Move to E-Application Successfully";
                                        Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record Move to E-Application against HUTOPS Id: {record.HUTOPSIds}");

                                    }
                                    else
                                    {
                                        personalInformation.IsRecordMoveToEApp = 0;
                                        DB.SaveChanges();
                                        record.Status = "Record Mark as Failed";
                                        Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already move to E-Application against HUTOPS Id: {record.HUTOPSIds}");
                                        continue;
                                    }
                                }
                                else
                                {
                                    personalInformation.IsRecordMoveToEApp = 1;
                                    DB.SaveChanges();
                                    record.Status = "Educational Information is not found";
                                    Helper.AddLog(Constants.LogType.ActivityLog, $"Educational Information not found against HUTOPS Id: {record.HUTOPSIds}");
                                    continue;
                                }

                            }
                            else
                            {
                                record.Status = "Record already move to E-Application";
                                Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already move to E-Application against HUTOPS Id: {record.HUTOPSIds}");
                                continue;
                            }

                        }
                        else
                        {
                            record.Status = "Record not found ";
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
                            continue;
                        }

                    }
                    catch (Exception ex)
                    {
                        using (HUTOPSEntities DB = new HUTOPSEntities())
                        {
                            var personalInformation = DB.PersonalInformations.Where(x => x.HUTopId == record.HUTOPSIds).FirstOrDefault();
                            if (personalInformation != null)
                            {
                                personalInformation.IsRecordMoveToEApp = 1;
                                DB.SaveChanges();
                            }
                        }
                        Helper.AddLog(Constants.LogType.Exception, $"Error Occured while processing Batch Record against HUTOPSId : {record.HUTOPSIds} Error Details: {ex.Message}");
                        record.Status = "Failed" + ex.Message;
                    }

                }
                return records;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
