﻿using HUTOPS.Models;
using System.Web.Mvc;
using HUTOPS.Helper;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Data;

namespace HUTOPS.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application

        HUTOPSEntities DB = new HUTOPSEntities();

        public ActionResult TermAndConditions()
        {
            if (ConfigurationManager.AppSettings["FormStatus"].ToString() == "Disabled")
            {
                return RedirectToAction("Disabled");
            }
                
            return View();
        }
        public ActionResult Disabled()
        {
            return View();
        }
        public ActionResult Index()
        {
            if (ConfigurationManager.AppSettings["FormStatus"].ToLowerInvariant() == "disabled" && Utility.GetAdminFromSession().Id == 0)
            {
                return RedirectToAction("Disabled");
            }


            // Check and Create Unique Cookie Id (UCID)
            var UCID = Session[Constants.Session.UCID] == null? "" : Session[Constants.Session.UCID].ToString();
            if (string.IsNullOrEmpty(UCID)) Session[Constants.Session.UCID] = DateTime.Now.Ticks.ToString();

            var LoginUser = Utility.GetUserFromSession();
            var Education = Utility.GetEducationFromSession();
            var country = DB.Countries.ToList();
            List<City> city = new List<City>();
            var province = DB.States.ToList();
            if(LoginUser != null)
            {
                var UserProvince = province.Where(x => x.Name == LoginUser.ResidentialProvince).FirstOrDefault();
                city = DB.Cities.ToList().Where(x => x.StateId == (UserProvince == null ? 0 : UserProvince.Id)).ToList();
            }
            List<EducationalSubject> subjects = new List<EducationalSubject>();
            List<TestDate> testDate = DB.TestDates.ToList().Where(x => x.Visibility == 1 && x.DeadlineDate > DateTime.UtcNow + TimeSpan.FromHours(5)).ToList();
            if (Education != null)
            {
                subjects = DB.EducationalSubjects.ToList().FindAll(x => x.EducationalId == (Education == null ? 0 : Education.Id)).ToList();
            }


            ApplicationModel applicationModel = new ApplicationModel()
            {
                PersonalInfo = Utility.GetUserFromSession() == null? new PersonalInformation() : Utility.GetUserFromSession(),
                Education = Utility.GetEducationFromSession() == null ? new Educational() : Utility.GetEducationFromSession(),
                Document = Utility.GetDocumentFromSession() == null ? new Document() : Utility.GetDocumentFromSession(),
                // Required For Personal Info Form
                Country = country,
                City = city,
                Province = province,

                // Required for Education Form
                Subjects = subjects,
                // Required for TestDate
                TestDate = testDate
                 
        };

            return View(applicationModel);
        }
        [HttpPost]
        public ActionResult Submit(ApplicationModel applicationModel)
        {
            try
            {
                // Check and Create Unique Cookie Id (UCID)
                var UCID = Session[Constants.Session.UCID] == null ? "" : Session[Constants.Session.UCID].ToString();
                if (string.IsNullOrEmpty(UCID)) Session[Constants.Session.UCID] = DateTime.Now.Ticks.ToString();

                if (Utility.GetAdminFromSession().Id != 0)
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Admin request to Update User Application Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)} {JsonConvert.SerializeObject(applicationModel.Education)} {JsonConvert.SerializeObject(applicationModel.Document)}");


                    var userId = Utility.GetUserFromSession().Id;
                    if (applicationModel.PersonalInfo.Id == 0)
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} request to Add new Application as a applicant");
                        return Json(new { status = false, message = "Admin cannot Register new Application for new registration you have to logout" });
                    }
                    if (userId != applicationModel.PersonalInfo.Id)
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Multi Profile Error Occured while admit Updating Applicaiton");
                        return Json(new { status = false, message = "Multi profile conflict occur while saving student record" });
                    }
                    var PersonalInformationErrors = Utility.ValidatePersonalInfo(applicationModel.PersonalInfo);
                    var EducationalError = Utility.ValidateEducation(applicationModel.Education);
                    var DocumentErrors = Utility.ValidateDocuments(applicationModel.CNIC, applicationModel.Photograph, applicationModel.SSCMarkSheet, applicationModel.HSSCMarkSheet);
                    if (!string.IsNullOrEmpty(applicationModel.SubjectName))
                    {
                        var subjectslen = applicationModel.SubjectName.Split(',').Length;
                        if (subjectslen < 3)
                        {
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Error Occured while validating Subjects Name: missing subjects names");
                            EducationalError.Add("Please Enter Subjects name in Education section");
                        }
                    }
                    if (PersonalInformationErrors.Count > 0 || EducationalError.Count > 0 || DocumentErrors.Count > 0)
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Validation Failed while admit try to Update applicant record Details: {PersonalInformationErrors}{EducationalError}");
                        return Json(new { status = false, message = "Error Occured while validating your data", PersonalErrors = PersonalInformationErrors, EducationErrors = EducationalError, DocumentErrors = DocumentErrors });
                    }
                    using (DbContextTransaction Transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            // Update Personal Information 
                            var person = DB.PersonalInformations.ToList().Where(x => x.Id == applicationModel.PersonalInfo.Id).FirstOrDefault();
                            if (person != null)
                            {
                                person.FirstName = applicationModel.PersonalInfo.FirstName?.Trim();
                                person.MiddleName = applicationModel.PersonalInfo.MiddleName?.Trim();
                                person.LastName = applicationModel.PersonalInfo.LastName?.Trim();

                                person.FatherFirstName = applicationModel.PersonalInfo.FatherFirstName?.Trim();
                                person.FatherMiddleName = applicationModel.PersonalInfo.MiddleName?.Trim();
                                person.FatherLastName = applicationModel.PersonalInfo.FatherLastName?.Trim();

                                person.CNIC = applicationModel.PersonalInfo.CNIC?.Trim();
                                person.EmailAddress = applicationModel.PersonalInfo.EmailAddress?.Trim();
                                person.AlterEmailAddress = applicationModel.PersonalInfo.AlterEmailAddress?.Trim();
                                person.Gender = applicationModel.PersonalInfo.Gender?.Trim();
                                person.DateOfBirth = applicationModel.PersonalInfo.DateOfBirth;

                                person.CellPhoneNumber = applicationModel.PersonalInfo.CellPhoneNumber?.Trim();
                                person.WhatsAppNumber = applicationModel.PersonalInfo.WhatsAppNumber?.Trim();
                                person.AlternateCellPhoneNumber = applicationModel.PersonalInfo.AlternateCellPhoneNumber?.Trim();
                                person.HomePhoneNumber = applicationModel.PersonalInfo.HomePhoneNumber?.Trim();  
                                //person.AlternateLandline = applicationModel.PersonalInfo.AlternateLandline;
                                //person.GuardianCellPhoneNumber = applicationModel.PersonalInfo.GuardianCellPhoneNumber;
                                person.GuardianEmailAddress = applicationModel.PersonalInfo.GuardianEmailAddress?.Trim();

                                person.ResidentialAddress = applicationModel.PersonalInfo.ResidentialAddress?.Trim();
                                person.ResidentialCountry = applicationModel.PersonalInfo.ResidentialCountry?.Trim();
                                person.ResidentialProvince = applicationModel.PersonalInfo.ResidentialProvince?.Trim();
                                person.ResidentialCity = applicationModel.PersonalInfo.ResidentialCity?.Trim();
                                person.ResidentialCityOther = applicationModel.PersonalInfo.ResidentialCityOther?.Trim();
                                person.ResidentialPostalCode = applicationModel.PersonalInfo.ResidentialPostalCode;

                                person.PermanentAddress = applicationModel.PersonalInfo.PermanentAddress?.Trim();
                                person.PermanentCountry = applicationModel.PersonalInfo.PermanentCountry?.Trim();
                                person.PermanentProvince = applicationModel.PersonalInfo.PermanentProvince?.Trim();
                                person.PermanentCity = applicationModel.PersonalInfo.PermanentCity?.Trim();
                                person.PermanentCityOther = applicationModel.PersonalInfo.PermanentCityOther?.Trim();
                                person.PermanentPostalCode = applicationModel.PersonalInfo.PermanentPostalCode;

                                person.IsAppliedBefore = applicationModel.PersonalInfo.IsAppliedBefore;
                                person.AppliedBeforeYear = applicationModel.PersonalInfo.AppliedBeforeYear;
                                person.AppliedBeforeId = applicationModel.PersonalInfo.AppliedBeforeId;

                                person.HearAboutHU = applicationModel.PersonalInfo.HearAboutHU?.Trim();
                                person.HearAboutHUOther = applicationModel.PersonalInfo.HearAboutHUOther?.Trim();

                                person.TestDate = applicationModel.PersonalInfo.TestDate;

                                DB.SaveChanges();
                            }
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Updated by {Utility.GetAdminFromSession().Name} Against Applicant: {person}");
                            // Update Educational Information
                            var education = DB.Educationals.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                            if (education != null)
                            {
                                applicationModel.Education.Id = education.Id;

                                education.CurrentLevelOfEdu = applicationModel.Education.CurrentLevelOfEdu?.Trim();
                                education.UniversityName = applicationModel.Education.UniversityName?.Trim();
                                education.HSSCBoardId = applicationModel.Education.HSSCBoardId?.Trim();
                                education.HSSCBoardName = applicationModel.Education.HSSCBoardName?.Trim();
                                education.HSSCGroupId = applicationModel.Education.HSSCGroupId?.Trim();
                                education.HSSCGroupName = applicationModel.Education.HSSCGroupName?.Trim();

                                education.HSSCSchoolName = applicationModel.Education.HSSCSchoolName?.Trim();
                                education.HSSCSchoolAddress = applicationModel.Education.HSSCSchoolAddress?.Trim();
                                education.HSSCStartDate = applicationModel.Education.HSSCStartDate;
                                education.HSSCCompletionDate = applicationModel.Education.HSSCCompletionDate;
                                education.HSSCPercentage = applicationModel.Education.HSSCPercentage?.Trim();

                                education.SSCSchoolName = applicationModel.Education.SSCSchoolName?.Trim();
                                education.SSCSchoolAddress = applicationModel.Education.SSCSchoolAddress?.Trim();
                                education.SSCPercentage = applicationModel.Education.SSCPercentage?.Trim();

                                education.HUSchoolName = applicationModel.Education.HUSchoolName?.Trim();
                                education.IntendedProgram = applicationModel.Education.IntendedProgram?.Trim();

                                DB.SaveChanges();
                            }
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Educational Information Updated by {Utility.GetAdminFromSession().Name} Against Educational Info: {education}");

                            // Delete Previous Educational Subjects
                            DB.EducationalSubjects.RemoveRange(DB.EducationalSubjects.ToList().Where(x => x.EducationalId == applicationModel.Education.Id));
                            DB.SaveChanges();
                            if (!string.IsNullOrEmpty(applicationModel.SubjectName))
                            {
                                var SubjectNames = applicationModel.SubjectName.Split(',');
                                foreach (string _subjectName in SubjectNames)
                                {
                                    if (!string.IsNullOrEmpty(_subjectName))
                                    {
                                        string name = _subjectName.Substring(0, _subjectName.Length <= 50 ? _subjectName.Length : 50);
                                        DB.EducationalSubjects.Add(new EducationalSubject()
                                        {
                                            EducationalId = applicationModel.Education.Id,
                                            Name = name
                                        });
                                        DB.SaveChanges();
                                    }
                                }
                            }

                            Utility.AddLog(Constants.LogType.ActivityLog, $"Remove and Insert updated Educational Subjects Updated by {Utility.GetAdminFromSession().Name} Against Educational Id: {education.Id}");


                            string uploadDirectory = HttpContext.Server.MapPath("~/UploadedFiles");
                            string UserDirectory = Path.Combine(HttpContext.Server.MapPath("~/UploadedFiles/"), applicationModel.PersonalInfo.Id.ToString());

                            string CnicPath = "";
                            string SSCPath = "";
                            string HSSCPath = "";
                            string PhotographPath = "";


                            // Create Directories If not exists
                            if (!Directory.Exists(uploadDirectory))
                            {
                                Directory.CreateDirectory(uploadDirectory);
                            }
                            if (!Directory.Exists(UserDirectory))
                            {
                                Directory.CreateDirectory(UserDirectory);
                            }

                            // Update Files and save into DB
                            if (applicationModel.CNIC != null)
                            {

                                CnicPath = Path.Combine(UserDirectory, "CNIC" + Path.GetExtension(applicationModel.CNIC.FileName));
                                
                                applicationModel.CNIC.SaveAs(CnicPath);
                                var doc = DB.Documents.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                                doc.CNIC = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], CnicPath.Substring(CnicPath.IndexOf("Upload")));
                                
                                DB.SaveChanges();
                                Utility.AddLog(Constants.LogType.ActivityLog, $"CNIC/B-Form updated by {Utility.GetAdminFromSession().Name} Against applicant {applicationModel.PersonalInfo.Id}");
                            }
                            if (applicationModel.SSCMarkSheet != null)
                            {
                                SSCPath = Path.Combine(UserDirectory, "SSC Mark Sheet" + Path.GetExtension(applicationModel.SSCMarkSheet.FileName));
                                applicationModel.SSCMarkSheet.SaveAs(SSCPath);
                                var doc = DB.Documents.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                                doc.SSCMarkSheet = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], SSCPath.Substring(SSCPath.IndexOf("Upload"))); ;
                                DB.SaveChanges();
                                Utility.AddLog(Constants.LogType.ActivityLog, $"SSC Mark sheet updated by {Utility.GetAdminFromSession().Name} Against applicant {applicationModel.PersonalInfo.Id}");

                            }
                            if (applicationModel.HSSCMarkSheet != null)
                            {
                                HSSCPath = Path.Combine(UserDirectory, "HSSC Mark Sheet" + Path.GetExtension(applicationModel.HSSCMarkSheet.FileName));
                                applicationModel.HSSCMarkSheet.SaveAs(HSSCPath);
                                var doc = DB.Documents.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                                doc.HSSCMarkSheet = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], HSSCPath.Substring(HSSCPath.IndexOf("Upload"))); ;
                                DB.SaveChanges();
                                Utility.AddLog(Constants.LogType.ActivityLog, $"HSSC Mark sheet updated by {Utility.GetAdminFromSession().Name} Against applicant {applicationModel.PersonalInfo.Id}");

                            }
                            if (applicationModel.Photograph != null)
                            {
                                PhotographPath = Path.Combine(UserDirectory, "Photo" + Path.GetExtension(applicationModel.Photograph.FileName));
                                applicationModel.Photograph.SaveAs(PhotographPath);
                                var doc = DB.Documents.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                                doc.Photograph = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], PhotographPath.Substring(PhotographPath.IndexOf("Upload"))); ;
                                DB.SaveChanges();
                                Utility.AddLog(Constants.LogType.ActivityLog, $"Photograph updated by {Utility.GetAdminFromSession().Name} Against applicant {applicationModel.PersonalInfo.Id}");

                            }

                            Transaction.Commit();
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Transaction Commited by {Utility.GetAdminFromSession().Name} Against applicant {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");
                            return Json(new { status = true, message = "Application Updated Successfully" });
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var eve in ex.EntityValidationErrors)
                            {
                                Utility.AddLog(Constants.LogType.Exception, $"Entity of type \"{0}\" in state \"{1}\" has the following validation errors: {eve.Entry.Entity.GetType().Name}, {eve.Entry.State} UserDetails: {JsonConvert.SerializeObject(applicationModel)}");
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Utility.AddLog(Constants.LogType.Exception, $"Error: {ve.PropertyName}, {ve.ErrorMessage}  UserDetails: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");
                                }
                            }
                            Transaction.Rollback();
                            Utility.AddLog(Constants.LogType.Exception, $"Exception Occured while Updating Application by {Utility.GetAdminFromSession().Name} Error Details: {ex.Message}");

                            return Json(new { status = false, message = "Form Submittion Failed " + ex.Message });

                        }

                    }
                }

                Utility.AddLog(Constants.LogType.ActivityLog, $"Applicant request to Submit Application Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)} {JsonConvert.SerializeObject(applicationModel.Education)} {JsonConvert.SerializeObject(applicationModel.Document)}");

                var PersonalInfoErrors = Utility.ValidatePersonalInfo(applicationModel.PersonalInfo);
                var EducationError = Utility.ValidateEducation(applicationModel.Education);

                List<string> DocumentError = Utility.ValidateDocuments(applicationModel.CNIC, applicationModel.Photograph, applicationModel.SSCMarkSheet, applicationModel.HSSCMarkSheet);
                if (applicationModel.SSCMarkSheet == null)
                {
                    DocumentError.Add("SSC Mark sheet: is required");
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Error Occured while validating Documents: SSC Mark sheet = {applicationModel.SSCMarkSheet}");
                }
                if (applicationModel.Photograph == null)
                {
                    DocumentError.Add("Passport size Photograph: is required");
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Error Occured while validating Documents: Photograph = {applicationModel.Photograph}");
                }
                if (!string.IsNullOrEmpty(applicationModel.SubjectName))
                {
                    var subjectslength = applicationModel.SubjectName.Split(',').Length;
                    if (subjectslength < 3)
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Error Occured while validating Subjects Name: missing subjects names");
                        EducationError.Add("Please Enter Subjects name in Education section");
                    }
                }
                if (PersonalInfoErrors.Count > 0 || EducationError.Count > 0 || DocumentError.Count > 0)
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Validation Failed Details: {JsonConvert.SerializeObject(PersonalInfoErrors)}{JsonConvert.SerializeObject(EducationError)} {JsonConvert.SerializeObject(DocumentError)}");
                    return Json(new { status = false, message = "Error Occured while validating your data", PersonalErrors = PersonalInfoErrors, EducationErrors = EducationError, DocumentErrors  = DocumentError });
                }
                else
                {

                    string UserDirectory = "";
                    using (DbContextTransaction Transaction = DB.Database.BeginTransaction())
                    {
                        
                        try
                        {
                            applicationModel.PersonalInfo.Declaration = 1;

                            applicationModel.PersonalInfo.FirstName = Utility.ToCamelCase(applicationModel.PersonalInfo.FirstName)?.Trim();
                            applicationModel.PersonalInfo.MiddleName = Utility.ToCamelCase(applicationModel.PersonalInfo.MiddleName)?.Trim();
                            applicationModel.PersonalInfo.LastName = Utility.ToCamelCase(applicationModel.PersonalInfo.LastName)?.Trim();
                            applicationModel.PersonalInfo.FatherFirstName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherFirstName)?.Trim();
                            applicationModel.PersonalInfo.FatherMiddleName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherMiddleName)?.Trim();
                            applicationModel.PersonalInfo.FatherLastName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherLastName)?.Trim();
                            applicationModel.PersonalInfo.CreatedDatetime = DateTime.UtcNow + TimeSpan.FromHours(5);

                            DB.PersonalInformations.Add(applicationModel.PersonalInfo);
                            DB.SaveChanges();
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Table record Inserted : {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");



                            string CnicPath = "";
                            string SSCPath = "";
                            string HSSCPath = "";
                            string PhotographPath = "";

                            string uploadDirectory = HttpContext.Server.MapPath("~/UploadedFiles");
                            UserDirectory = Path.Combine(HttpContext.Server.MapPath("~/UploadedFiles/"), applicationModel.PersonalInfo.Id.ToString());
                            if (!Directory.Exists(uploadDirectory))
                            {
                                Directory.CreateDirectory(uploadDirectory);
                            }
                            if (!Directory.Exists(UserDirectory))
                            {
                                Directory.CreateDirectory(UserDirectory);
                            }

                            if (applicationModel.SSCMarkSheet != null && applicationModel.Photograph != null)
                            {
                                Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided Documents are validated. User Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");
                                // Check if files are present
                                if (applicationModel.CNIC != null)
                                {
                                    CnicPath = Path.Combine(UserDirectory, "CNIC" + Path.GetExtension(applicationModel.CNIC.FileName));
                                    applicationModel.CNIC.SaveAs(CnicPath);
                                    Utility.AddLog(Constants.LogType.ActivityLog, $"CNIC File uploaded to the server CNIC Path: {CnicPath} User Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");
                                }
                                if (applicationModel.SSCMarkSheet != null)
                                {
                                    SSCPath = Path.Combine(UserDirectory, "SSC Mark Sheet" + Path.GetExtension(applicationModel.SSCMarkSheet.FileName));
                                    applicationModel.SSCMarkSheet.SaveAs(SSCPath);
                                    Utility.AddLog(Constants.LogType.ActivityLog, $"SSC Mark sheet File uploaded to the server Path: {SSCPath} User Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");
                                }
                                if (applicationModel.HSSCMarkSheet != null)
                                {
                                    HSSCPath = Path.Combine(UserDirectory, "HSSC Mark Sheet" + Path.GetExtension(applicationModel.HSSCMarkSheet.FileName));
                                    applicationModel.HSSCMarkSheet.SaveAs(HSSCPath);
                                    Utility.AddLog(Constants.LogType.ActivityLog, $"HSSC Mark sheet File uploaded to the server Path: {HSSCPath} User Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");

                                }
                                if (applicationModel.Photograph != null)
                                {
                                    PhotographPath = Path.Combine(UserDirectory, "Photo" + Path.GetExtension(applicationModel.Photograph.FileName));
                                    applicationModel.Photograph.SaveAs(PhotographPath);
                                    Utility.AddLog(Constants.LogType.ActivityLog, $"Photograph File uploaded to the server Path: {PhotographPath} User Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");
                                }

                                DB.Documents.Add(new Document
                                {
                                    UserId = applicationModel.PersonalInfo.Id,
                                    CNIC = CnicPath == ""?"": Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], CnicPath.Substring(CnicPath.IndexOf("Upload"))),
                                    Photograph = PhotographPath == "" ? "" : Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], PhotographPath.Substring(PhotographPath.IndexOf("Upload"))),
                                    SSCMarkSheet = SSCPath == "" ? "" : Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], SSCPath.Substring(SSCPath.IndexOf("Upload"))),
                                    HSSCMarkSheet = HSSCPath == "" ? "" : Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], HSSCPath.Substring(HSSCPath.IndexOf("Upload"))),
                                    IsCompleted = 1
                                });
                                DB.SaveChanges();
                                Utility.AddLog(Constants.LogType.ActivityLog, $"Document Record inserted against applicant: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");
}
                            else
                            {
                                DocumentError.Add("SSC Mark sheet: is required");
                                DocumentError.Add("Passport size Photograph: is required");
                                Utility.AddLog(Constants.LogType.ActivityLog, $"Required Documents not provided by applicant: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");

                            }

                            applicationModel.Education.UserId = applicationModel.PersonalInfo.Id;
                            applicationModel.Education.SSCSchoolName = Utility.ToCamelCase(applicationModel.Education.SSCSchoolName)?.Trim();
                            applicationModel.Education.HSSCSchoolName = Utility.ToCamelCase(applicationModel.Education.HSSCSchoolName)?.Trim();

                            DB.Educationals.Add(applicationModel.Education);
                            DB.SaveChanges();
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Educational Table record Inserted Detials: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");

                            if (!string.IsNullOrEmpty(applicationModel.SubjectName))
                            {
                                var SubjectNames = applicationModel.SubjectName.Split(',');
                                foreach (string _subjectName in SubjectNames)
                                {
                                    if (!string.IsNullOrEmpty(_subjectName))
                                    {
                                        string name = _subjectName.Substring(0, _subjectName.Length <= 50 ? _subjectName.Length : 50);
                                        DB.EducationalSubjects.Add(new EducationalSubject()
                                        {
                                            EducationalId = applicationModel.Education.Id,
                                            Name = name
                                        });
                                        DB.SaveChanges();
                                    }
                                }
                            }
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Educational Subjects Inserted against Educational: {JsonConvert.SerializeObject(applicationModel.Education)}");


                            Transaction.Commit();

                            Utility.AddLog(Constants.LogType.ActivityLog, $"Transaction Commited against Applicant: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");

                            var EmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Id == 2).FirstOrDefault();

                            Utility.AddLog(Constants.LogType.ActivityLog, $"Get Email Template for Submition Eamil. User Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");

                            string EmailBody = EmailTemplate.Body;


                            var documents = DB.Documents.Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                            EmailBody = EmailBody.Replace("{{Photo}}", documents.Photograph);

                            EmailBody = EmailBody.Replace("{{HUTopId}}", applicationModel.PersonalInfo.HUTopId);
                            EmailBody = EmailBody.Replace("{{FirstName}}", applicationModel.PersonalInfo.FirstName);
                            EmailBody = EmailBody.Replace("{{MiddleName}}", applicationModel.PersonalInfo.MiddleName);
                            EmailBody = EmailBody.Replace("{{LastName}}", applicationModel.PersonalInfo.LastName);
                            EmailBody = EmailBody.Replace("{{FatherFirstName}}", applicationModel.PersonalInfo.FatherFirstName);
                            EmailBody = EmailBody.Replace("{{FatherMiddleName}}", applicationModel.PersonalInfo.FatherMiddleName);
                            EmailBody = EmailBody.Replace("{{FatherLastName}}", applicationModel.PersonalInfo.FatherLastName);
                            EmailBody = EmailBody.Replace("{{Gender}}", applicationModel.PersonalInfo.Gender);
                            EmailBody = EmailBody.Replace("{{DateOfBirth}}", applicationModel.PersonalInfo.DateOfBirth.Value.ToString("dd/MM/yyyy"));
                            EmailBody = EmailBody.Replace("{{CellPhoneNumber}}", applicationModel.PersonalInfo.CellPhoneNumber);
                            EmailBody = EmailBody.Replace("{{ResidentialAddress}}", applicationModel.PersonalInfo.ResidentialAddress);
                            EmailBody = EmailBody.Replace("{{ResidentialCity}}", applicationModel.PersonalInfo.ResidentialCity);
                            EmailBody = EmailBody.Replace("{{ResidentialCityOther}}", applicationModel.PersonalInfo.ResidentialCityOther);
                            EmailBody = EmailBody.Replace("{{ResidentialProvince}}", applicationModel.PersonalInfo.ResidentialProvince);
                            EmailBody = EmailBody.Replace("{{ResidentialCountry}}", applicationModel.PersonalInfo.ResidentialCountry);
                            EmailBody = EmailBody.Replace("{{PermanentAddress}}", applicationModel.PersonalInfo.PermanentAddress);
                            EmailBody = EmailBody.Replace("{{PermanentCity}}", applicationModel.PersonalInfo.PermanentCity);
                            EmailBody = EmailBody.Replace("{{PermanentCityOther}}", applicationModel.PersonalInfo.PermanentCityOther);
                            EmailBody = EmailBody.Replace("{{PermanentProvince}}", applicationModel.PersonalInfo.PermanentProvince);
                            EmailBody = EmailBody.Replace("{{PermanentCountry}}", applicationModel.PersonalInfo.PermanentCountry);
                            EmailBody = EmailBody.Replace("{{CNIC}}", applicationModel.PersonalInfo.CNIC);
                            EmailBody = EmailBody.Replace("{{EmailAddress}}", applicationModel.PersonalInfo.EmailAddress);

                            EmailBody = EmailBody.Replace("{{TestDate}}", DateTime.Parse(applicationModel.PersonalInfo.TestDate).ToString("dd/MM/yyyy"));
                            EmailBody = EmailBody.Replace("{{IsAppliedBefore}}", applicationModel.PersonalInfo.IsAppliedBefore.ToString());
                            EmailBody = EmailBody.Replace("{{AppliedBeforeId}}", applicationModel.PersonalInfo.AppliedBeforeId);
                            EmailBody = EmailBody.Replace("{{AppliedBeforeYear}}", applicationModel.PersonalInfo.AppliedBeforeYear.ToString());
                            EmailBody = EmailBody.Replace("{{HomePhoneNumber}}", applicationModel.PersonalInfo.HomePhoneNumber);
                            EmailBody = EmailBody.Replace("{{WhatsAppNumber}}", applicationModel.PersonalInfo.WhatsAppNumber);
                            EmailBody = EmailBody.Replace("{{GuardianCellPhoneNumber}}", applicationModel.PersonalInfo.GuardianCellPhoneNumber);
                            EmailBody = EmailBody.Replace("{{GuardianEmailAddress}}", applicationModel.PersonalInfo.GuardianEmailAddress);
                            EmailBody = EmailBody.Replace("{{HearAboutHU}}", applicationModel.PersonalInfo.HearAboutHU);
                            EmailBody = EmailBody.Replace("{{HearAboutHUOther}}", applicationModel.PersonalInfo.HearAboutHUOther);
                            EmailBody = EmailBody.Replace("{{CreatedDatetime}}", applicationModel.PersonalInfo.CreatedDatetime.ToString());
                            EmailBody = EmailBody.Replace("{{Declaration}}", applicationModel.PersonalInfo.Declaration == 0? "False" : "True");

                            // Education 
                            EmailBody = EmailBody.Replace("{{levelofEducation}}", applicationModel.Education.CurrentLevelOfEdu);
                            EmailBody = EmailBody.Replace("{{University}}", applicationModel.Education.UniversityName);
                            if (string.IsNullOrEmpty(applicationModel.Education.UniversityName))
                            {
                                EmailBody = EmailBody.Replace("<strong>University:</strong>", "");
                            }
                            EmailBody = EmailBody.Replace("{{Board}}", applicationModel.Education.HSSCBoardName == "IB" ? "Board Of Intermediate" : applicationModel.Education.HSSCBoardName == "FB" ? "Federal Board" : "Aga Khan University Examination Board");
                            EmailBody = EmailBody.Replace("{{Group}}", applicationModel.Education.HSSCGroupName);
                            EmailBody = EmailBody.Replace("{{HSSCSchoolName}}", applicationModel.Education.HSSCSchoolName);
                            EmailBody = EmailBody.Replace("{{HSSCPercent}}", applicationModel.Education.HSSCPercentage);
                            EmailBody = EmailBody.Replace("{{StartingYear}}", applicationModel.Education.HSSCStartDate.ToString());
                            EmailBody = EmailBody.Replace("{{CompletionYear}}", applicationModel.Education.HSSCCompletionDate.ToString());
                            EmailBody = EmailBody.Replace("{{HSSCSchoolAddress}}", applicationModel.Education.HSSCSchoolAddress);

                            EmailBody = EmailBody.Replace("{{SSCSchoolName}}", applicationModel.Education.SSCSchoolName);
                            EmailBody = EmailBody.Replace("{{SSCPercent}}", applicationModel.Education.SSCPercentage);
                            EmailBody = EmailBody.Replace("{{SSCSchoolAddress}}", applicationModel.Education.SSCSchoolAddress);
                            
                            EmailBody = EmailBody.Replace("{{IntendedProgram}}", applicationModel.Education.IntendedProgram);
                            EmailBody = EmailBody.Replace("{{HUSchoolName}}", applicationModel.Education.HUSchoolName == "SE"? "Dhanani School of Science and Engineering" : "School of Arts, Humanities and Social Sciences");
                            EmailBody = EmailBody.Replace("{{Subjects}}", applicationModel.SubjectName);


                            List<string> files = new List<string>();
                            if (!string.IsNullOrEmpty(SSCPath))
                                files.Add(SSCPath);
                            if (!string.IsNullOrEmpty(HSSCPath))
                                files.Add(HSSCPath);
                            if (!string.IsNullOrEmpty(PhotographPath))
                                files.Add(PhotographPath);
                            if (!string.IsNullOrEmpty(CnicPath))
                                files.Add(CnicPath);



                            CPD.Framework.Core.EmailService.SendEmailWithMultipleFiles(applicationModel.PersonalInfo.EmailAddress,new List<string> {"tops@habib.edu.pk"} , null, EmailTemplate.Subject, EmailBody,files, "tops@habib.edu.pk");

                            Utility.AddLog(Constants.LogType.ActivityLog, $"Email has been sent to applicant User Details: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");

                            Session.RemoveAll();
                            return Json(new { status = true, message = "Application Submited Successfully"});


                        }
                        catch (DbEntityValidationException ex)
                        {
                            string EmailBody = string.Empty;
                            EmailBody = EmailBody + "Entity Validation Error Occured on submition of HUTOPS Applicant form <br />";
                            EmailBody = EmailBody + $"Datetime: {DateTime.UtcNow + TimeSpan.FromHours(5)} <br />";
                            foreach (var eve in ex.EntityValidationErrors)
                            {
                                //Utility.AddLog(Constants.LogType.Exception, $"Entity of type \"{0}\" in state \"{1}\" has the following validation errors: {eve.Entry.Entity.GetType().Name}, {eve.Entry.State} UserDetails: {JsonConvert.SerializeObject(applicationModel)}");
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    EmailBody = EmailBody + $"<br /> <br /> <br />  Error: {ve.PropertyName} , {ve.ErrorMessage}";
                                    Utility.AddLog(Constants.LogType.Exception, $"Error: {ve.PropertyName}, {ve.ErrorMessage}  UserDetails: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");
                                }
                            }

                            Transaction.Rollback();
                            if(UserDirectory != "")
                            {
                                Directory.Delete(UserDirectory, true);
                            }
                            
                            Utility.AddLog(Constants.LogType.Exception, $"Transaction Rollback and Documents has been removed from the server");

                            EmailBody = EmailBody + $"<br /> <br /> <br /> Personal Information: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}";
                            EmailBody = EmailBody + $"<br /> <br /> <br /> Education Information: {JsonConvert.SerializeObject(applicationModel.Education)}";
                            CPD.Framework.Core.EmailService.SendEmail(ConfigurationManager.AppSettings["ExceptionEmailTo"], ConfigurationManager.AppSettings["ExceptionEmailCC"].ToString().Split(';').ToList(), null, "Exception HUTOPS", EmailBody, null, "tops@habib.edu.pk", null);


                            return Json(new { status = false, message = "Form Submittion Failed " + ex.Message });

                        }
                        catch (Exception ex)
                        {
                            string EmailBody = string.Empty;
                            EmailBody = EmailBody + "General Exception Error Occured on submition of HUTOPS Applicant form <br />";
                            EmailBody = EmailBody + $"Datetime: {DateTime.UtcNow + TimeSpan.FromHours(5)} <br />";
                            Utility.AddLog(Constants.LogType.Exception, $"Error Occured while Submitting Application Details: {Utility.GetInnerException(ex)} UserDetails: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");

                            Transaction.Rollback();
                            if (UserDirectory != "")
                            {
                                Directory.Delete(UserDirectory, true);
                            }
                            Utility.AddLog(Constants.LogType.Exception, $"Transaction Rollback and Documents has been removed from the server");

                            EmailBody = EmailBody + Utility.GetInnerException(ex);
                            EmailBody = EmailBody + $"<br /> <br /> <br /> Personal Information: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}";
                            EmailBody = EmailBody + $"<br /> <br /> <br /> Education Information: {JsonConvert.SerializeObject(applicationModel.Education)} <br />";

                            CPD.Framework.Core.EmailService.SendEmail(ConfigurationManager.AppSettings["ExceptionEmailTo"], ConfigurationManager.AppSettings["ExceptionEmailCC"].ToString().Split(';').ToList(), null, "Exception HUTOPS", EmailBody, null, "tops@habib.edu.pk", null);



                            return Json(new { status = false, message = "Form Submittion Failed " + Utility.GetInnerException(ex) });
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string EmailBody = string.Empty;
                EmailBody = EmailBody + "General Exception Error Occured on submition of HUTOPS Applicant form <br />";
                EmailBody = EmailBody + $"Datetime: {DateTime.UtcNow + TimeSpan.FromHours(5)} <br />";
                Utility.AddLog(Constants.LogType.Exception, $"Application Submition Failed Details: {Utility.GetInnerException(ex)}UserDetails: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)}");


                EmailBody = EmailBody + Utility.GetInnerException(ex);
                EmailBody = EmailBody + $"<br /> <br /> Personal Information: {JsonConvert.SerializeObject(applicationModel.PersonalInfo)} <br />";
                EmailBody = EmailBody + $"<br /> <br /> Education Information: {JsonConvert.SerializeObject(applicationModel.Education)}  <br />";
                CPD.Framework.Core.EmailService.SendEmail(ConfigurationManager.AppSettings["ExceptionEmailTo"], ConfigurationManager.AppSettings["ExceptionEmailCC"].ToString().Split(';').ToList(), null, "Exception HUTOPS", EmailBody, null, "tops@habib.edu.pk", null);

                return Json(new { status = false, message = "Form Submittion Failed " + Utility.GetInnerException(ex) });

            }
        }

        [HttpPost]
        public ActionResult Success(bool isSubmitted)
        {
            if (isSubmitted)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public FileResult View(string Name, int? Id = null )
        {
            try
            {
                var personalInfo = new PersonalInformation();
                if(Id == null) { 
                    personalInfo = Utility.GetUserFromSession();
                }
                else
                {
                    personalInfo = DB.PersonalInformations.Where(x => x.Id == Id).FirstOrDefault();
                }
                var documents = DB.Documents.ToList().Where(x => x.UserId == personalInfo.Id).FirstOrDefault();

                if (documents != null)
                {
                    switch (Name)
                    {
                        case "Photo":
                            Name = documents.Photograph == "" ? "" : documents.Photograph.Substring(documents.Photograph.IndexOf("" + personalInfo.Id + "")).Substring(personalInfo.Id.ToString().Length + 1);
                            break;
                        case "SSC Mark Sheet":
                            Name = documents.SSCMarkSheet == "" ? "" : documents.SSCMarkSheet.Substring(documents.SSCMarkSheet.IndexOf("" + personalInfo.Id + "")).Substring(personalInfo.Id.ToString().Length + 1);
                            break;
                        case "HSSC Mark Sheet":
                            Name = documents.HSSCMarkSheet == "" ? "" : documents.HSSCMarkSheet.Substring(documents.HSSCMarkSheet.IndexOf("" + personalInfo.Id + "")).Substring(personalInfo.Id.ToString().Length + 1);
                            break;
                        case "CNIC":
                            Name = documents.CNIC == "" ? "" : documents.CNIC.Substring(documents.CNIC.IndexOf("" + personalInfo.Id + "")).Substring(personalInfo.Id.ToString().Length + 1);
                            break;
                        default:
                            return File("", "application/octet-stream");
                    }
                }

                string contentType = "application/octet-stream";
                string extension = Path.GetExtension(Name);
                if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    contentType = "image/jpeg";
                else if (extension.ToLower() == ".pdf")
                    contentType = "application/pdf";
                return new FilePathResult(Server.MapPath("~/UploadedFiles/" + personalInfo.Id) + "/" + Name, contentType);
            }
            catch (Exception ex)
            {
                return File("", "application/octet-stream");
            }
            
        }

        [HttpPost]
        public ActionResult AddPersonalInformationLog(PersonalInformation personalInformation)
        {
            Utility.AddLog(Constants.LogType.ActivityLog, $"User move to next section from Personal Information {JsonConvert.SerializeObject(personalInformation)} ");
            return null;
        }
        [HttpPost]
        public ActionResult AddEducationLog(Educational educational)
        {
            Utility.AddLog(Constants.LogType.ActivityLog, $"User move to next section from Education Information {JsonConvert.SerializeObject(educational)} ");
            return null;
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