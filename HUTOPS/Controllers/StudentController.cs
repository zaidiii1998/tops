﻿using HUTOPS.Helper;
using HUTOPS.Models;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
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

                DTResult<PersonalInformation> Result = new DTResult<PersonalInformation>();
                var Response = DB.SP_GetStudents(param.Start, param.Length,HUTOPSId, Name, CNIC, Phone, Email);

                var Records = Response.ToList();
                var SingleRecord = Records.FirstOrDefault();
                int Total = 0;

                if (Records != null)
                {
                    foreach (var rec in Records.ToList())
                    {
                        Result.data.Add(new PersonalInformation()
                        {
                            Id = rec.Id,
                            HUTopId = rec.HUTopId,
                            FirstName = rec.FirstName,
                            LastName = rec.LastName,
                            CellPhoneNumber = rec.CellPhoneNumber,
                            EmailAddress = rec.EmailAddress,
                            CNIC = rec.CNIC,
                            IsAdmitCardGenerated = rec.IsAdmitCardGenerated,
                            AdmitCardGeneratedOn = rec.AdmitCardGeneratedOn,
                            IsAdmitCardSent = rec.IsAdmitCardSent,
                            AdmitCardSentOn = rec.AdmitCardSentOn,
                            Result = rec.Result,
                            IsRecordMoveToEApp = rec.IsRecordMoveToEApp
                        });
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
                        return Json(new { status = false, message = $"Documents not found against this HUTOPS Id {personalInformation.HUTopId}" });
                    }
                    EmailBody = EmailBody.Replace("{{TestDate}}", model.TestDate.ToString());
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
                        personalInfo.AdmitCardGeneratedOn = DateTime.Now;
                        tempDB.SaveChanges();
                    }
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Update Admit Card Status in the personal Information Table Against HUTOPSId : {personalInformation.HUTopId}");
                    // Update Admit Card Path in Documents Table
                    using (HUTOPSEntities tempDB = new HUTOPSEntities())
                    {
                        var documents = tempDB.Documents.ToList().Where(x => x.Id == document.Id).FirstOrDefault();
                        documents.AdmitCard = filePath;
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

                if (System.IO.File.Exists(document.AdmitCard))
                {
                        // Send Email
                        CPD.Framework.Core.EmailService.SendEmail(personalInformation.EmailAddress, null, null, EmailTemplate.Subject, EmailTemplate.Body, document.AdmitCard, null, null);

                        Utility.AddLog(Constants.LogType.ActivityLog, $"Email has been sent to Applicant against HUTOPSId : {personalInformation.HUTopId} ");
                        // Update sent Admit Card Status in perssonal Info Table
                        using (HUTOPSEntities tempDB = new HUTOPSEntities())
                        {
                            var personalInfo = tempDB.PersonalInformations.ToList().Where(x => x.Id == personalInformation.Id).FirstOrDefault();
                            personalInfo.IsAdmitCardSent = 1;
                            personalInfo.AdmitCardSentOn = DateTime.Now;
                            tempDB.SaveChanges();
                        }
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Update Email sent status in the personal Information table against HUTOPSId : {personalInformation.HUTopId}");
                        return Json(new { status = true, message = "Admit Card Send Successfully" });
                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Admit Card not found against HUTOPSId : {personalInformation.HUTopId}");

                    return Json(new { status = false, message = "Admit Card not found" });
                }
            }
            catch (Exception ex)
            {
                Utility.AddLog(Constants.LogType.Exception, $"Error Occured while sending Admit Card Error Details: {ex.Message}");
                return Json(new { status = false, message = "Admit Card not found" });
            }
        }
        public ActionResult RecordMoveToEApp(int Id)
        {
            try
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"Admin request to move record to E-Application against Id : {Id}");


                var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
                if (personalInformation != null)
                {
                    if (personalInformation.IsRecordMoveToEApp != 1)
                    {
                        if (personalInformation.Result == 2)
                        {
                            DB.SP_UserShiftToEApplication(personalInformation.Id);
                            personalInformation.IsRecordMoveToEApp = 1;
                            DB.SaveChanges();

                            Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record Move to E-Application against HUTOPS Id: {personalInformation.HUTopId}");
                            return Json(new { status = true, message = "Record Move to E-Application Successfully" });
                        }
                        else
                        {
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already move to E-Application against HUTOPS Id: {personalInformation.HUTopId}");
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
    }
}