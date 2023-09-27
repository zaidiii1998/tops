using HUTOPS.Models;
using System.Web.Mvc;
using HUTOPS.Helper;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web.Helpers;
using System.Xml.Linq;

namespace HUTOPS.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application

        HUTOPSEntities DB = new HUTOPSEntities();
        public ActionResult Index()
        {
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
            List<Board> Boards = DB.Boards.ToList();
            List<BoardGroup> Groups = DB.BoardGroups.ToList();
            List<EducationalSubject> subjects = new List<EducationalSubject>();
            List<TestDate> testDate = DB.TestDates.ToList();
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
                Boards = Boards,
                Groups = Groups,
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

                if (Utility.GetAdminFromSession().Id != 0)
                {
                    var PersonalInformationErrors = Utility.ValidatePersonalInfo(applicationModel.PersonalInfo);
                    var EducationalError = Utility.ValidateEducation(applicationModel.Education);
                    if (PersonalInformationErrors.Count > 0 || EducationalError.Count > 0)
                    {
                        return Json(new { status = false, message = "Error Occured while validating your data", PersonalErrors = PersonalInformationErrors, EducationErrors = EducationalError, DocumentErrors = new List<string>() });
                    }
                    using (DbContextTransaction Transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            // Update Personal Information 
                            var person = DB.PersonalInformations.ToList().Where(x => x.Id == applicationModel.PersonalInfo.Id).FirstOrDefault();
                            if (person != null)
                            {
                                person.FirstName = applicationModel.PersonalInfo.FirstName;
                                person.MiddleName = applicationModel.PersonalInfo.MiddleName;
                                person.LastName = applicationModel.PersonalInfo.LastName;

                                person.FatherFirstName = applicationModel.PersonalInfo.FatherFirstName;
                                person.FatherMiddleName = applicationModel.PersonalInfo.MiddleName;
                                person.FatherLastName = applicationModel.PersonalInfo.FatherLastName;

                                person.CNIC = applicationModel.PersonalInfo.CNIC;
                                person.EmailAddress = applicationModel.PersonalInfo.EmailAddress;
                                person.AlterEmailAddress = applicationModel.PersonalInfo.AlterEmailAddress;
                                person.Gender = applicationModel.PersonalInfo.Gender;
                                person.DateOfBirth = applicationModel.PersonalInfo.DateOfBirth;

                                person.CellPhoneNumber = applicationModel.PersonalInfo.CellPhoneNumber;
                                person.WhatsAppNumber = applicationModel.PersonalInfo.WhatsAppNumber;
                                person.AlternateCellPhoneNumber = applicationModel.PersonalInfo.AlternateCellPhoneNumber;
                                person.HomePhoneNumber = applicationModel.PersonalInfo.HomePhoneNumber;
                                person.AlternateLandline = applicationModel.PersonalInfo.AlternateLandline;
                                person.GuardianCellPhoneNumber = applicationModel.PersonalInfo.GuardianCellPhoneNumber;
                                person.GuardianEmailAddress = applicationModel.PersonalInfo.GuardianEmailAddress;

                                person.ResidentialAddress = applicationModel.PersonalInfo.ResidentialAddress;
                                person.ResidentialCountry = applicationModel.PersonalInfo.ResidentialCountry;
                                person.ResidentialProvince = applicationModel.PersonalInfo.ResidentialProvince;
                                person.ResidentialCity = applicationModel.PersonalInfo.ResidentialCity;
                                person.ResidentialCityOther = applicationModel.PersonalInfo.ResidentialCityOther;
                                person.ResidentialPostalCode = applicationModel.PersonalInfo.ResidentialPostalCode;

                                person.PermanentAddress = applicationModel.PersonalInfo.PermanentAddress;
                                person.PermanentCountry = applicationModel.PersonalInfo.PermanentCountry;
                                person.PermanentProvince = applicationModel.PersonalInfo.PermanentProvince;
                                person.PermanentCity = applicationModel.PersonalInfo.PermanentCity;
                                person.PermanentCityOther = applicationModel.PersonalInfo.PermanentCityOther;
                                person.PermanentPostalCode = applicationModel.PersonalInfo.PermanentPostalCode;

                                person.HearAboutHU = applicationModel.PersonalInfo.HearAboutHU;
                                person.HearAboutHUOther = applicationModel.PersonalInfo.HearAboutHUOther;

                                DB.SaveChanges();
                            }
                            // Update Educational Information
                            var education = DB.Educationals.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                            if (education != null)
                            {
                                applicationModel.Education.Id = education.Id;

                                education.CurrentLevelOfEdu = applicationModel.Education.CurrentLevelOfEdu;
                                education.UniversityName = applicationModel.Education.UniversityName;
                                education.HSSCBoardId = applicationModel.Education.HSSCBoardId;
                                education.HSSCBoardName = applicationModel.Education.HSSCBoardName;
                                education.HSSCGroupId = applicationModel.Education.HSSCGroupId;
                                education.HSSCGroupName = applicationModel.Education.HSSCGroupName;

                                education.HSSCSchoolName = applicationModel.Education.HSSCSchoolName;
                                education.HSSCSchoolAddress = applicationModel.Education.HSSCSchoolAddress;
                                education.HSSCStartDate = applicationModel.Education.HSSCStartDate;
                                education.HSSCCompletionDate = applicationModel.Education.HSSCCompletionDate;
                                education.HSSCPercentage = applicationModel.Education.HSSCPercentage;

                                education.SSCSchoolName = applicationModel.Education.SSCSchoolName;
                                education.SSCSchoolAddress = applicationModel.Education.SSCSchoolAddress;
                                education.SSCPercentage = applicationModel.Education.SSCPercentage;

                                education.HUSchoolName = applicationModel.Education.HUSchoolName;
                                education.IntendedProgram = applicationModel.Education.IntendedProgram;

                                DB.SaveChanges();
                            }
                            // Delete Previous Educational Subjects
                            DB.EducationalSubjects.RemoveRange(DB.EducationalSubjects.ToList().Where(x => x.EducationalId == applicationModel.Education.Id));
                            DB.SaveChanges();

                            for (var i = 0; i < applicationModel.SubjectName[0].ToString().Split(',').Length; i++)
                            {
                                DB.EducationalSubjects.Add(new EducationalSubject()
                                {
                                    EducationalId = applicationModel.Education.Id,
                                    Name = applicationModel.SubjectName[0].ToString().Split(',')[i],
                                    ObtainMarks = int.Parse(applicationModel.SubjectObtain[0].ToString().Split(',')[i])
                                });
                                DB.SaveChanges();
                            }

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
                            }
                            if (applicationModel.SSCMarkSheet != null)
                            {
                                SSCPath = Path.Combine(UserDirectory, "SSC Mark Sheet" + Path.GetExtension(applicationModel.SSCMarkSheet.FileName));
                                applicationModel.SSCMarkSheet.SaveAs(SSCPath);
                                var doc = DB.Documents.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                                doc.SSCMarkSheet = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], SSCPath.Substring(SSCPath.IndexOf("Upload"))); ;
                                DB.SaveChanges();
                            }
                            if (applicationModel.HSSCMarkSheet != null)
                            {
                                HSSCPath = Path.Combine(UserDirectory, "HSSC Mark Sheet" + Path.GetExtension(applicationModel.HSSCMarkSheet.FileName));
                                applicationModel.HSSCMarkSheet.SaveAs(HSSCPath);
                                var doc = DB.Documents.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                                doc.HSSCMarkSheet = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], HSSCPath.Substring(HSSCPath.IndexOf("Upload"))); ;
                                DB.SaveChanges();
                            }
                            if (applicationModel.Photograph != null)
                            {
                                PhotographPath = Path.Combine(UserDirectory, "Photo" + Path.GetExtension(applicationModel.Photograph.FileName));
                                applicationModel.Photograph.SaveAs(PhotographPath);
                                var doc = DB.Documents.ToList().Where(x => x.UserId == applicationModel.PersonalInfo.Id).FirstOrDefault();
                                doc.Photograph = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["BaseURL"], PhotographPath.Substring(PhotographPath.IndexOf("Upload"))); ;
                                DB.SaveChanges();
                            }

                            Transaction.Commit();
                            return Json(new { status = true, message = "Application Updated Successfully" });
                        }
                        catch (System.Exception ex)
                        {
                            Transaction.Rollback();
                            return Json(new { status = false, message = "Form Submittion Failed " + ex.Message });

                        }

                    }
                }


                var PersonalInfoErrors = Utility.ValidatePersonalInfo(applicationModel.PersonalInfo);
                var EducationError = Utility.ValidateEducation(applicationModel.Education);

                List<string> DocumentError = new List<string>();
                if (applicationModel.SSCMarkSheet == null && applicationModel.Photograph == null)
                {
                    DocumentError.Add("SSC Mark sheet: is required");
                    DocumentError.Add("Passport size Photograph: is required");
                }

                if (PersonalInfoErrors.Count > 0 || EducationError.Count > 0 || DocumentError.Count > 0)
                {
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
                            DB.PersonalInformations.Add(applicationModel.PersonalInfo);
                            DB.SaveChanges();
                            
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
                                Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided Documents are validated.");
                                // Check if files are present
                                if (applicationModel.CNIC != null)
                                {
                                    CnicPath = Path.Combine(UserDirectory, "CNIC" + Path.GetExtension(applicationModel.CNIC.FileName));
                                    applicationModel.CNIC.SaveAs(CnicPath);
                                }
                                if (applicationModel.SSCMarkSheet != null)
                                {
                                    SSCPath = Path.Combine(UserDirectory, "SSC Mark Sheet" + Path.GetExtension(applicationModel.SSCMarkSheet.FileName));
                                    applicationModel.SSCMarkSheet.SaveAs(SSCPath);
                                }
                                if (applicationModel.HSSCMarkSheet != null)
                                {
                                    HSSCPath = Path.Combine(UserDirectory, "HSSC Mark Sheet" + Path.GetExtension(applicationModel.HSSCMarkSheet.FileName));
                                    applicationModel.HSSCMarkSheet.SaveAs(HSSCPath);
                                }
                                if (applicationModel.Photograph != null)
                                {
                                    PhotographPath = Path.Combine(UserDirectory, "Photo" + Path.GetExtension(applicationModel.Photograph.FileName));
                                    applicationModel.Photograph.SaveAs(PhotographPath);
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
                                Utility.AddLog(Constants.LogType.ActivityLog, $"User successfully submited Documents.");
}
                            else
                            {
                                DocumentError.Add("SSC Mark sheet: is required");
                                DocumentError.Add("Passport size Photograph: is required");
                            }
                            
                            applicationModel.Education.UserId = applicationModel.PersonalInfo.Id;
                            DB.Educationals.Add(applicationModel.Education);
                            DB.SaveChanges();


                            for (var i = 0; i < applicationModel.SubjectName[0].ToString().Split(',').Length; i++)
                            {
                                DB.EducationalSubjects.Add(new EducationalSubject()
                                {
                                    EducationalId = applicationModel.Education.Id,
                                    Name = applicationModel.SubjectName[0].ToString().Split(',')[i],
                                    ObtainMarks = int.Parse(applicationModel.SubjectObtain[0].ToString().Split(',')[i])
                                });
                                DB.SaveChanges();
                            }
                            

                            Transaction.Commit();
                            var EmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Id == 2).FirstOrDefault();
                            string EmailBody = EmailTemplate.Body;

                            EmailBody = EmailBody.Replace("{{Photo}}", applicationModel.Document.Photograph);
                            EmailBody = EmailBody.Replace("{{HUTOPSId}}", applicationModel.PersonalInfo.HUTopId);
                            EmailBody = EmailBody.Replace("{{Name}}", applicationModel.PersonalInfo.FirstName +" " + applicationModel.PersonalInfo.MiddleName + " " + applicationModel.PersonalInfo.LastName);
                            EmailBody = EmailBody.Replace("{{FatherName}}", applicationModel.PersonalInfo.FatherFirstName + " " + applicationModel.PersonalInfo.FatherMiddleName + " " + applicationModel.PersonalInfo.FatherLastName);

                            EmailBody = EmailBody.Replace("{{CNIC}}", applicationModel.PersonalInfo.CNIC );
                            EmailBody = EmailBody.Replace("{{Email}}", applicationModel.PersonalInfo.EmailAddress);
                            EmailBody = EmailBody.Replace("{{Gender}}", applicationModel.PersonalInfo.Gender );
                            EmailBody = EmailBody.Replace("{{Husband}}", applicationModel.PersonalInfo.HusbandName);
                            EmailBody = EmailBody.Replace("{{DOB}}", applicationModel.PersonalInfo.DateOfBirth.ToString() );
                            EmailBody = EmailBody.Replace("{{PhoneNumber}}", applicationModel.PersonalInfo.CellPhoneNumber);
                            EmailBody = EmailBody.Replace("{{Address}}", applicationModel.PersonalInfo.ResidentialAddress);

                            EmailBody = EmailBody.Replace("{{levelofEducation}}", applicationModel.Education.CurrentLevelOfEdu);
                            EmailBody = EmailBody.Replace("{{University}}", applicationModel.Education.UniversityName);
                            EmailBody = EmailBody.Replace("{{Board}}", applicationModel.Education.HSSCBoardName);
                            EmailBody = EmailBody.Replace("{{Group}}", applicationModel.Education.HSSCGroupName);
                            EmailBody = EmailBody.Replace("{{HSSCSchoolName}}", applicationModel.Education.HSSCSchoolName);
                            EmailBody = EmailBody.Replace("{{HSSCPercent}}", applicationModel.Education.HSSCPercentage);
                            EmailBody = EmailBody.Replace("{{StartingYear}}", applicationModel.Education.HSSCStartDate.ToString());
                            EmailBody = EmailBody.Replace("{{CompletionYear}}", applicationModel.Education.HSSCCompletionDate.ToString());
                            EmailBody = EmailBody.Replace("{{CollegeAddress}}", applicationModel.Education.HSSCSchoolAddress);

                            EmailBody = EmailBody.Replace("{{SSCSchoolName}}", applicationModel.Education.SSCSchoolName);
                            EmailBody = EmailBody.Replace("{{SSCPercent}}", applicationModel.Education.SSCPercentage);
                            EmailBody = EmailBody.Replace("{{SchoolAddress}}", applicationModel.Education.SSCSchoolAddress);



                            CPD.Framework.Core.EmailService.SendEmail(applicationModel.PersonalInfo.EmailAddress, null, EmailTemplate.Subject, EmailBody);



                            return Json(new { status = true, message = "Application Submited Successfully"});


                        }
                        catch (System.Exception ex)
                        {
                            Transaction.Rollback();
                            Directory.Delete(UserDirectory, true);
                            return Json(new { status = false, message = "Form Submittion Failed" + ex.Message });

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

                return Json(new { status = false, message = "Form Submittion Failed" + ex.Message });

            }


        }

        public ActionResult Success()
        {
            return View();
        }
    }
}