﻿using ClosedXML.Excel;
using ClosedXML.Extensions;
using DocumentFormat.OpenXml.Vml.Office;
using HUTOPS.Codebase;
using HUTOPS.EAppDBModel;
using HUTOPS.Helper;
using HUTOPS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class StudentController : Controller
    {
        // GET: Student

        HUTOPSEntities DB = new HUTOPSEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Get(DTParameters param)
        {
            try
            {
                var HUTOPSId = Utility.GetParam(param, 1);
                var Name = Utility.GetParam(param, 2);
                var CNIC = Utility.GetParam(param, 3);
                var Phone = Utility.GetParam(param, 4);
                var Email = Utility.GetParam(param, 5);
                var AdmissionSession = Utility.GetParam(param, 6);
                var ApplicationStatus = Utility.GetParam(param, 7);

                DTResult<SP_GetStudents_Result> Result = new DTResult<SP_GetStudents_Result>();
                var Response = DB.SP_GetStudents(param.Start, param.Length,HUTOPSId, Name, CNIC, Phone, Email, AdmissionSession, ApplicationStatus);

                var Records = Response.ToList();
                var SingleRecord = Records.FirstOrDefault();
                int Total = 0;

                if (Records != null)
                {
                    foreach (var rec in Records.ToList())
                    {
                        Result.data.Add(rec);
                    }
                    Total = Records.Count;
                }
                Result.draw = param.Draw;
                Result.recordsTotal = Total == 0 ? 0 : int.Parse(SingleRecord.Count.ToString());
                Result.recordsFiltered = Total == 0 ? 0 : int.Parse(SingleRecord.Count.ToString());
                return Json(Result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult GetDocuments(int Id)
        {
            var documents = DB.Documents.Where(x => x.UserId == Id).FirstOrDefault();
            return Json(documents);
        }
        public ActionResult Download(DownloadFiltersModel model)
        {
            try
            {
                dynamic Records = DB.SP_GetStudentRecord(model.HUTOPSId, model.Name, model.CNIC, model.Phone, model.Email, model.AdmissionSession, model.ApplicationStatus);

                DataTable dtRecords = new DataTable();
                dtRecords.Columns.Add("HUTOPSId", typeof(string));
                dtRecords.Columns.Add("Application Status", typeof(string));
                dtRecords.Columns.Add("First Name", typeof(string));
                dtRecords.Columns.Add("Middle Name", typeof(string));
                dtRecords.Columns.Add("Last Name", typeof(string));
                dtRecords.Columns.Add("Father First Name", typeof(string));
                dtRecords.Columns.Add("Father Middle Name", typeof(string));
                dtRecords.Columns.Add("Father Last Name", typeof(string));
                dtRecords.Columns.Add("Gender", typeof(string));
                dtRecords.Columns.Add("Date Of Birth", typeof(string));
                dtRecords.Columns.Add("Cell Phone Number", typeof(string));
                dtRecords.Columns.Add("Alternate Cell Phone Number", typeof(string));
                dtRecords.Columns.Add("CNIC/B-Form", typeof(string));
                dtRecords.Columns.Add("Email Address", typeof(string));
                dtRecords.Columns.Add("Residential Address", typeof(string));
                dtRecords.Columns.Add("Residential Country", typeof(string));
                dtRecords.Columns.Add("Residential Province", typeof(string));
                dtRecords.Columns.Add("Residential City", typeof(string));
                dtRecords.Columns.Add("Residential City Other", typeof(string));
                dtRecords.Columns.Add("Permanent Address", typeof(string));
                dtRecords.Columns.Add("Permanent Country", typeof(string));
                dtRecords.Columns.Add("Permanent Province", typeof(string));
                dtRecords.Columns.Add("Permanent City", typeof(string));
                dtRecords.Columns.Add("Permanent City Other", typeof(string));
                dtRecords.Columns.Add("Test Date", typeof(string));
                dtRecords.Columns.Add("Is Applied Before", typeof(string));
                dtRecords.Columns.Add("Applied Before Year", typeof(string));
                dtRecords.Columns.Add("Home Phone Number", typeof(string));
                dtRecords.Columns.Add("WhatsApp Number", typeof(string));
                dtRecords.Columns.Add("Guardian Cell Phone Number", typeof(string));
                dtRecords.Columns.Add("Guardian Email Address", typeof(string));
                dtRecords.Columns.Add("Hear About HU", typeof(string));
                dtRecords.Columns.Add("Hear About HU Other", typeof(string));
                dtRecords.Columns.Add("Created Datetime", typeof(string));
                dtRecords.Columns.Add("Declaration", typeof(string));

                // Education 
                dtRecords.Columns.Add("Current Level Of Education", typeof(string));
                dtRecords.Columns.Add("HSSC School Name", typeof(string));
                dtRecords.Columns.Add("HSSC School Address", typeof(string));
                dtRecords.Columns.Add("HSSC Start Year", typeof(string));
                dtRecords.Columns.Add("HSSC Completion Year", typeof(string));
                dtRecords.Columns.Add("HSSC Percentage", typeof(string));
                dtRecords.Columns.Add("HSSC Board Name", typeof(string));
                dtRecords.Columns.Add("HSSC Group Name", typeof(string));
                dtRecords.Columns.Add("SSC School Name", typeof(string));
                dtRecords.Columns.Add("SSC School Address", typeof(string));
                dtRecords.Columns.Add("SSC Percentage", typeof(string));
                dtRecords.Columns.Add("University Name", typeof(string));
                dtRecords.Columns.Add("Intended Program", typeof(string));
                dtRecords.Columns.Add("HU School Name", typeof(string));
                
                
                // Documents
                dtRecords.Columns.Add("Photograph", typeof(string));
                dtRecords.Columns.Add("CNIC/B-Form Doc", typeof(string));
                dtRecords.Columns.Add("HSSC MarkSheet", typeof(string));
                dtRecords.Columns.Add("SSC MarkSheet", typeof(string));

                //Subjects
                dtRecords.Columns.Add("Subject 1", typeof(string));
                dtRecords.Columns.Add("Subject 2", typeof(string));
                dtRecords.Columns.Add("Subject 3", typeof(string));
                dtRecords.Columns.Add("Subject 4", typeof(string));
                dtRecords.Columns.Add("Subject 5", typeof(string));
                dtRecords.Columns.Add("Subject 6", typeof(string));
                dtRecords.Columns.Add("Subject 7", typeof(string));
                dtRecords.Columns.Add("Subject 8", typeof(string));
                dtRecords.Columns.Add("Subject 9", typeof(string));
                dtRecords.Columns.Add("Subject 10", typeof(string));


                if (Records != null)
                {
                    foreach (var item in Records)
                    {
                        dtRecords.Rows.Add(new object[] {
                        item.HUTopId == null ? "" : item.HUTopId.ToString(),
                        item.ApplicationStatus == null ? "" : item.ApplicationStatus.ToString(),
                        item.FirstName == null ? "" : item.FirstName.ToString(),
                        item.MiddleName == null ? "" : item.MiddleName.ToString(),
                        item.LastName == null ? "" : item.LastName.ToString(),
                        item.FatherFirstName == null ? "" : item.FatherFirstName.ToString(),
                        item.FatherMiddleName == null ? "" : item.FatherMiddleName.ToString(),
                        item.FatherLastName == null ? "" : item.FatherLastName.ToString(),
                        item.Gender == null ? "" : item.Gender.ToString(),
                        item.DateOfBirth == null ? "" : item.DateOfBirth.ToString("dd-MM-yyyy"),
                        item.CellPhoneNumber == null ? "" : item.CellPhoneNumber.ToString(),
                        item.AlternateCellPhoneNumber == null ? "" : item.AlternateCellPhoneNumber.ToString(),
                        item.CNIC == null ? "" : item.CNIC.ToString(),
                        item.EmailAddress == null ? "" : item.EmailAddress.ToString(),
                        item.ResidentialAddress == null ? "" : item.ResidentialAddress.ToString(),
                        item.ResidentialCountry == null ? "" : item.ResidentialCountry.ToString(),
                        item.ResidentialProvince == null ? "" : item.ResidentialProvince.ToString(),
                        item.ResidentialCity == null ? "" : item.ResidentialCity.ToString(),
                        item.ResidentialCityOther == null ? "" : item.ResidentialCityOther.ToString(),
                        item.PermanentAddress == null ? "" : item.PermanentAddress.ToString(),
                        item.PermanentCountry == null ? "" : item.PermanentCountry.ToString(),
                        item.PermanentProvince == null ? "" : item.PermanentProvince.ToString(),
                        item.PermanentCity == null ? "" : item.PermanentCity.ToString(),
                        item.PermanentCityOther == null ? "" : item.PermanentCityOther.ToString(),
                        item.TestDate == null ? "" : item.TestDate.ToString(),
                        item.IsAppliedBefore == null ? "" : item.IsAppliedBefore.ToString(),
                        item.AppliedBeforeYear == null ? "" : item.AppliedBeforeYear.ToString(),
                        item.HomePhoneNumber == null ? "" : item.HomePhoneNumber.ToString(),
                        item.WhatsAppNumber == null ? "" : item.WhatsAppNumber.ToString(),
                        item.GuardianCellPhoneNumber == null ? "" : item.GuardianCellPhoneNumber.ToString(),
                        item.GuardianEmailAddress == null ? "" : item.GuardianEmailAddress.ToString(),
                        item.HearAboutHU == null ? "" : item.HearAboutHU.ToString(),
                        item.HearAboutHUOther == null ? "" : item.HearAboutHUOther.ToString(),
                        item.CreatedDatetime == null ? "" : item.CreatedDatetime.ToString("dd-MM-yyyy HH:mm:ss"),
                        item.Declaration == 0 ? "False" : "True",

                        // Education 
                        item.CurrentLevelOfEdu == null ? "" : item.CurrentLevelOfEdu.ToString(),
                        item.HSSCSchoolName == null ? "" : item.HSSCSchoolName.ToString(),
                        item.HSSCSchoolAddress == null ? "" : item.HSSCSchoolAddress.ToString(),
                        item.HSSCStartDate == null ? "" : item.HSSCStartDate.ToString(),
                        item.HSSCCompletionDate == null ? "" : item.HSSCCompletionDate.ToString(),
                        item.HSSCPercentage == null ? "" : item.HSSCPercentage.ToString(),
                        item.HSSCBoardName == null ? "" : item.HSSCBoardName == "IB" ? "Board Of Intermediate" : item.HSSCBoardName == "FB" ? "Federal Board" : "Aga Khan University Examination Board",
                        item.HSSCGroupName == null ? "" : item.HSSCGroupName.ToString(),
                        item.SSCSchoolName == null ? "" : item.SSCSchoolName.ToString(),
                        item.SSCSchoolAddress == null ? "" : item.SSCSchoolAddress.ToString(),
                        item.SSCPercentage == null ? "" : item.SSCPercentage.ToString(),
                        item.UniversityName == null ? "" : item.UniversityName.ToString(),
                        item.IntendedProgram == null ? "" : item.IntendedProgram.ToString(),
                        item.HUSchoolName == null ? "" : item.HUSchoolName == "SE"? "Dhanani School of Science and Engineering" : "School of Arts, Humanities and Social Sciences",

                        // Documents
                        item.Photograph == null ? "Not Uploaded":item.Photograph == "" ? "Not Uploaded" : "Uploaded",
                        item.CNICDoc == null ? "Not Uploaded" :item.CNICDoc == "" ? "Not Uploaded" : "Uploaded",
                        item.HSSCMarkSheet == null ? "Not Uploaded" : item.HSSCMarkSheet == "" ? "Not Uploaded" : "Uploaded",
                        item.SSCMarkSheet == null ? "Not Uploaded" : item.SSCMarkSheet == "" ? "Not Uploaded" : "Uploaded",

                        // Subjects
                        item.SubjectName_1 == null ? "" : item.SubjectName_1.ToString(),
                        item.SubjectName_2 == null ? "" : item.SubjectName_2.ToString(),
                        item.SubjectName_3 == null ? "" : item.SubjectName_3.ToString(),
                        item.SubjectName_4 == null ? "" : item.SubjectName_4.ToString(),
                        item.SubjectName_5 == null ? "" : item.SubjectName_5.ToString(),
                        item.SubjectName_6 == null ? "" : item.SubjectName_6.ToString(),
                        item.SubjectName_7 == null ? "" : item.SubjectName_7.ToString(),
                        item.SubjectName_8 == null ? "" : item.SubjectName_8.ToString(),
                        item.SubjectName_9 == null ? "" : item.SubjectName_9.ToString(),
                        item.SubjectName_10 == null ? "" : item.SubjectName_10.ToString()
                    });
                    }

                }



                XLWorkbook excel = new XLWorkbook();
                var file_name = "Students_List" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                excel.Worksheets.Add(dtRecords, "Students");
                return excel.Deliver(file_name);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult UpdateSession(int? Id)
        {

            var currentSession = Utility.GetUserFromSession();
            if(currentSession.Id != 0)
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} has already open an applicant profile");

                return Json(new { status = false, message = "you have already open Student Profile. Kindly close the profile first" });
            }

            var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
            var education = DB.Educationals.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
            var document = DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();

            Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} Request to get Student Access For Updation. Student Details: {JsonConvert.SerializeObject(personalInformation)}");


            Utility.SetSession(personalInformation == null ? new PersonalInformation() : personalInformation);
            Utility.SetSession(education == null ? new Educational() : education);
            Utility.SetSession(document == null ? new Document() : document);
            return Json(new {status = true, message = "Session Updated Successfully"});
        }
        public ActionResult CloseSession()
        {

            Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} Request to close Current Student profile.");

            Utility.SetSession( new PersonalInformation());
            Utility.SetSession( new Educational());
            Utility.SetSession( new Document());

            return Json(new { status = true, message = "Student Profile Closed Successfully" });
        }

        public ActionResult UpdateApplicationStatus(UpdateApplicationStatusModel model)
        {
            try
            {
                var personalInfo = DB.PersonalInformations.FirstOrDefault(x => x.Id == model.Id);
                if(personalInfo != null)
                {
                    personalInfo.ApplicationStatus = model.Status;
                    DB.SaveChanges();
                }
                else
                {
                    return Json(new { status = false, message = "Record not found, please try again" });
                }
                return Json(new {status = true, message = "Application Status updated successfully"});
            }
            catch (Exception ex)
            {
                return Json(new { status = true, message = ex.Message });
            }
        } 

        [HttpPost]
        public ActionResult GenerateAdmitCard(AdmitCardModel model)
        {
            try
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"Admin request to Generate Admit Card Details: {JsonConvert.SerializeObject(model)}");

                if (ModelState.IsValid)
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Given model is valid");
                    var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == model.Id).FirstOrDefault();
                    var document = new Document();
                    var Educational = new Educational();
                    var EmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Id == 3).FirstOrDefault();
                    string EmailBody = EmailTemplate.Body;
                    string watermarkImage = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], "Content/Images/bg_logo_large.jpg");
                    if (personalInformation != null)
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Applicant record found for Admit Card Details: {JsonConvert.SerializeObject(personalInformation)}");
                        document = DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null ? new Document() : DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                        Educational = DB.Educationals.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null ? new Educational() : DB.Educationals.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                        EmailBody = EmailBody.Replace("{{HUTOPSId}}", personalInformation.HUTopId);
                        EmailBody = EmailBody.Replace("{{Name}}", personalInformation.FirstName + " " + personalInformation.MiddleName + " " + personalInformation.LastName);
                        EmailBody = EmailBody.Replace("{{CNIC}}", personalInformation.CNIC);
                    }
                    else
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Applicant record not found for Admit Card");
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                            personalInfo.IsAdmitCardGenerated = 0;
                            tempDB.SaveChanges();
                        }
                        return Json(new { status = false, message = "Record not found" });
                    }
                    if (Educational.Id != 0)
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Educational Information found against personal Information: {JsonConvert.SerializeObject(personalInformation)}");
                        EmailBody = EmailBody.Replace("{{HUSchool}}", Educational.HUSchoolName == "SE" ? Constants.SchoolName.SE : Constants.SchoolName.SA);
                    }
                    else
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Educational Information not found against personal Information: {JsonConvert.SerializeObject(personalInformation)}");
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                            personalInfo.IsAdmitCardGenerated = 0;
                            tempDB.SaveChanges();
                        }
                        return Json(new { status = false, message = "Educational Information not Found" });
                    }
                    if (document.Id != 0)
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Documents record found against personal Information: {JsonConvert.SerializeObject(personalInformation)}");
                        EmailBody = EmailBody.Replace("{{Photo}}", document.Photograph);
                    }
                    else
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Document record not found against personal Information: {JsonConvert.SerializeObject(personalInformation)}");
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                            personalInfo.IsAdmitCardGenerated = 0;
                            tempDB.SaveChanges();
                        }
                        return Json(new { status = false, message = $"Documents not found against this HUTOPS Id {personalInformation.HUTopId}" });
                    }

                    EmailBody = EmailBody.Replace("{{Watermark}}", watermarkImage);
                    EmailBody = EmailBody.Replace("{{TestDate}}", model.TestDate.ToString("dddd, dd MMMM yyyy"));
                    EmailBody = EmailBody.Replace("{{ApproxDatetime}}", model.Shift == "1" ? Constants.Shift.FirstShift : model.Shift == "2" ? Constants.Shift.SecondShift : Constants.Shift.ThirtShift);
                    EmailBody = EmailBody.Replace("{{ReportingTime}}", model.Shift == "1" ? Constants.ReportingTime.FirstShift : model.Shift == "2" ? Constants.ReportingTime.SecondShift : Constants.ReportingTime.ThirtShift);
                    EmailBody = EmailBody.Replace("{{Vanue}}", model.Venue == "Karachi" ? Constants.Vanue.Karachi : Constants.Vanue.Islamabad);
                        
                    // Save File to server
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Fetched user Inforation and mapped against HUTOPSId: {personalInformation.HUTopId}");
                    string uploadDirectory = HttpContext.Server.MapPath("~/UploadedFiles");
                    string UserDirectory = Path.Combine(HttpContext.Server.MapPath("~/UploadedFiles/"), personalInformation.Id.ToString());

                    var filePath = HU_HTML_TO_PDF.Converter.ConvertHTML_TO_PDF(EmailBody, UserDirectory, "A4", "Portrait");
                    // Update Admit Card Status in perssonal Info Table

                    Utility.AddLog(Constants.LogType.ActivityLog, $"Admit Card genrated against HUTOPSId : {personalInformation.HUTopId}");

                    using (HUTOPSEntities tempDB = new HUTOPSEntities())
                    {
                        var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                        personalInfo.IsAdmitCardGenerated = 1;
                        personalInfo.AdmitCardGeneratedOn = DateTime.UtcNow + TimeSpan.FromHours(5);
                        tempDB.SaveChanges();
                    }
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Update Admit Card Status in the personal Information Table Against HUTOPSId : {personalInformation.HUTopId}");
                    // Update Admit Card Path in Documents Table
                    using (HUTOPSEntities tempDB = new HUTOPSEntities())
                    {
                        var documents = tempDB.Documents.ToList().Where(x => x.Id == document.Id).FirstOrDefault();
                        documents.AdmitCard = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], filePath.Substring(filePath.IndexOf("Upload")));
                        tempDB.SaveChanges();
                    }
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Update Admit card Path in the documents table against HUTOPSId : {personalInformation.HUTopId}");
                    // Send Email

                    return Json(new { status = true, message = "Admit Card Genrated Successfully" });
                }
                else {
                    return Json(new { status = false, message = $"Please Enter in all required fields" });
                }
                
            
            }
            catch (Exception)
            {
                using (HUTOPSEntities tempDB = new HUTOPSEntities())
                {
                    var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == model.Id).FirstOrDefault();
                    personalInfo.IsAdmitCardGenerated = 0;
                    tempDB.SaveChanges();
                }
                return Json(new { status = false, message = $"An Error Occur while generating Admit Card" });
            }
        }

        public ActionResult SendAdmitCard(int Id)
        {
            try
            {
                var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
                var document = new Document();
                var EmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Id == 4).FirstOrDefault();
                if (personalInformation == null)
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found");
                    return Json(new { status = false, message = "Personal Information not Found" });
                }
                else
                {
                    document = DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault() == null ? new Document() : DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                }

                if (document.Id == 0)
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Documents Record not found against HUTOPS Id: {personalInformation.HUTopId}");
                    return Json(new { status = false, message = "Documents not Found" });
                }
                var admitCardPath = Path.Combine(HttpContext.Server.MapPath("~/"), document.AdmitCard.Substring(document.AdmitCard.IndexOf("Upload")));
                if (System.IO.File.Exists(admitCardPath))
                {
                       
                        // Send Email
                        CPD.Framework.Core.EmailService.SendEmail(personalInformation.EmailAddress, null, null, EmailTemplate.Subject, EmailTemplate.Body, admitCardPath, "tops@habib.edu.pk", null);

                        Utility.AddLog(Constants.LogType.ActivityLog, $"Email has been sent to Applicant against HUTOPSId : {personalInformation.HUTopId} ");
                        // Update sent Admit Card Status in perssonal Info Table
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                            personalInfo.IsAdmitCardSent = 1;
                            personalInfo.AdmitCardSentOn = DateTime.UtcNow + TimeSpan.FromHours(5);
                            tempDB.SaveChanges();
                        }
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Update Email sent status in the personal Information table against HUTOPSId : {personalInformation.HUTopId}");
                        return Json(new { status = true, message = "Admit Card Send Successfully" });
                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Admit Card not found against HUTOPSId : {personalInformation.HUTopId}");
                    using (HUTOPSEntities tempDB = new HUTOPSEntities())
                    {
                        var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                        personalInfo.IsAdmitCardSent = 0;
                        tempDB.SaveChanges();
                    }

                    return Json(new { status = false, message = "Admit Card not found" });
                }
            }
            catch (Exception ex)
            {
                using (HUTOPSEntities tempDB = new HUTOPSEntities())
                {
                    var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
                    personalInfo.IsAdmitCardSent = 0;
                    tempDB.SaveChanges();
                }
                Utility.AddLog(Constants.LogType.Exception, $"Error Occured while sending Admit Card Error Details: {ex.Message}");
                return Json(new { status = false, message = "Admit Card not found" });
            }
        }
        public ActionResult RecordMoveToEApp(int Id)
        {
            try
            {
                EApplicationEntities EAppDB = new EApplicationEntities();
                List<EAppDBModel.PersonalInformation> eappPInfo = new List<EAppDBModel.PersonalInformation>();
                eappPInfo = EAppDB.PersonalInformations.Where(p => p.ID > 194587).ToList();


                Utility.AddLog(Constants.LogType.ActivityLog, $"Admin request to move record to E-Application against Id : {Id}");


                var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
                if (personalInformation != null)
                {
                    if (!eappPInfo.Exists(p => p.HuTopsId == personalInformation.HUTopId))
                    {
                        if (personalInformation.IsRecordMoveToEApp != 1)
                        {
                            if (personalInformation.Result == 2)
                            {
                                var educationalInformation = DB.Educationals.Where(x => x.UserId == Id).FirstOrDefault();
                                if (educationalInformation != null)
                                {
                                    //For Moving record using Entity Framework
                                    if (RecordsProcessing.MoveRecordToEApp(personalInformation, educationalInformation))
                                    {
                                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                                        {
                                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
                                            personalInfo.IsRecordMoveToEApp = 1;
                                            //tempDB.Entry(personalInfo).State = EntityState.Modified;
                                            tempDB.SaveChanges();
                                        }
                                        Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record Move to E-Application against HUTOPS Id: {personalInformation.HUTopId}");
                                        return Json(new { status = true, message = "Record Move to E-Application Successfully" });
                                    }
                                    else
                                    {
                                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                                        {
                                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
                                            personalInfo.IsRecordMoveToEApp = 0;
                                            tempDB.Entry(personalInfo).State = EntityState.Modified;
                                            tempDB.SaveChanges();
                                        }
                                        return Json(new { status = false, message = "Error Occured while moving record to E-Application" });
                                    }
                                }
                                else
                                {
                                    Utility.AddLog(Constants.LogType.ActivityLog, $"Educational Informtion not found against HUTOPS Id: {personalInformation.HUTopId}");
                                    return Json(new { status = false, message = "Educational Informtion not found" });
                                }

                            }
                            else
                            {
                                Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record Result Mark as Failed against HUTOPS Id: {personalInformation.HUTopId}");
                                return Json(new { status = false, message = "Record Mark as Failed" });
                            }

                        }
                        else
                        {
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already move to E-Application against HUTOPS Id: {personalInformation.HUTopId}");
                            return Json(new { status = false, message = "Record already move to E-Application" });
                        }
                    }
                    else
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already exist in E-Application against HUTOPS Id: {personalInformation.HUTopId}");
                        return Json(new { status = false, message = "Record already exist in E-Application" });
                    }
                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found against HUTOPS Id: {personalInformation.HUTopId}");
                    return Json(new { status = false, message = "Record not found " });
                }

            }
            catch (Exception ex)
            {
                Utility.AddLog(Constants.LogType.Exception, $"Error Occured while Moving Record against PersonalInformation Id : {Id} Error Details: {ex.Message}");
                return Json(new { status = false, message = "Error Occured while Moving Record To E-Application" + ex.Message });
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}