using HUTOPSBatchProcessConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static HUTOPSBatchProcessConsoleApp.Model.Constants;

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
                        }
                        if (EducationalInfo.Id != 0)
                        {
                            EmailBody = EmailBody.Replace("{{HUSchool}}", EducationalInfo.HUSchoolName == "SE" ? Constants.SchoolName.SE : Constants.SchoolName.SA);
                        }
                        else
                        {
                            record.Status = "Educational Information not Found"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Educational Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
                        }
                        if (document.Id != 0)
                        {
                            EmailBody = EmailBody.Replace("{{Photo}}", document.Photograph);
                        }
                        else
                        {
                            record.Status = "Documents not found against this HUTOPS Id"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Documents Record not found against HUTOPS Id: {record.HUTOPSIds}");
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
                        }
                        else
                        {
                            document = DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null ? new Document() : DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                        }

                        if (document.Id == 0)
                        {
                            record.Status = "Documents not found against this HUTOPS Id"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Documents Record not found against HUTOPS Id: {record.HUTOPSIds.ToString()}");
                        }

                        // Save File to server
                        if (isValid)
                        {
                            if (File.Exists(document.AdmitCard))
                            {
                                // Send Email
                                CPD.Framework.Core.EmailService.SendEmail(personalInformation.EmailAddress, null, null, EmailTemplate.Subject, EmailTemplate.Body, document.AdmitCard, null, null);

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
                            }
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
                        }
                        if (EducationalInfo.Id != 0)
                        {
                            EmailBody = EmailBody.Replace("{{HUSchool}}", EducationalInfo.HUSchoolName == "SE" ? Constants.SchoolName.SE : Constants.SchoolName.SA);
                        }
                        else
                        {
                            record.Status = "Educational Information not Found"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Educational Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
                        }
                        if (document.Id != 0)
                        {
                            EmailBody = EmailBody.Replace("{{Photo}}", document.Photograph);
                        }
                        else
                        {
                            record.Status = "Documents not found against this HUTOPS Id"; isValid = false;
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Documents Record not found against HUTOPS Id: {record.HUTOPSIds}");
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
                            CPD.Framework.Core.EmailService.SendEmail(personalInformation.EmailAddress, null, null, AdmmitCardEmailTemplate.Subject, EmailTemplate.Body, filePath, null, null);

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

        public static List<ExcelData> UpdateResult(List<ExcelData> records, byte? result)
        {
            try
            {
                foreach (var record in records)
                {
                    try
                    {
                        Helper.AddLog(Constants.LogType.ActivityLog, $"Service request to fetch record against HUTOPSId : {record.HUTOPSIds}");


                        var personalInformation = DB.PersonalInformations.ToList().Where(x => x.HUTopId.Trim() == record.HUTOPSIds.Trim()).FirstOrDefault();
                        if (personalInformation != null)
                        {
                            personalInformation.Result = result;
                            DB.SaveChanges();
                            record.Status = "Result Updated Successfully";
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Result Updated against HUTOPS Id: {record.HUTOPSIds}");

                        }
                        else
                        {
                            record.Status = "Record not found ";
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found against HUTOPS Id: {record.HUTOPSIds}");
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
    }
}
