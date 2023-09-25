using HUTOPS.Models;
using System.Web.Mvc;
using HUTOPS.Helper;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;

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
                Subjects = subjects
                 
        };

            return View(applicationModel);
        }
        [HttpPost]
        public ActionResult Submit(ApplicationModel applicationModel)
        {
            try
            {
                var PersonalInfoErrors = Utility.ValidatePersonalInfo(applicationModel.PersonalInfo);
                var EducationError = Utility.ValidateEducation(applicationModel.Education);
                List<string> DocumentError = new List<string>(); 
                if (PersonalInfoErrors.Count > 0 || EducationError.Count > 0)
                {
                    return Json(new { status = false, message = "Error Occured while validating your data", PersonalErrors = PersonalInfoErrors, EducationErrors = EducationError });
                }
                else
                {
                    var SubNames = Utility.ConvertArrayToCSV(applicationModel.SubjectName);
                    var SubObtain = Utility.ConvertArrayToCSV(applicationModel.SubjectObtain);

                    using (DbContextTransaction Transaction = DB.Database.BeginTransaction())
                    {
                        try
                        {
                            DB.PersonalInformations.Add(applicationModel.PersonalInfo);
                            DB.SaveChanges();
                            applicationModel.Education.UserId = applicationModel.PersonalInfo.Id;

                            var isSuccess = true;
                            string CnicFile = Path.GetFileName(applicationModel.CNIC == null? "": applicationModel.CNIC.FileName);
                            string Photograph = Path.GetFileName(applicationModel.Photograph == null? "" : applicationModel.Photograph.FileName);
                            string SSC = Path.GetFileName(applicationModel.SSCMarkSheet == null? "" : applicationModel.SSCMarkSheet.FileName);
                            string HSSC = Path.GetFileName(applicationModel.HSSCMarkSheet == null? "" : applicationModel.HSSCMarkSheet.FileName);

                            string CnicPath = "";
                            string SSCPath = "";
                            string HSSCPath = "";
                            string PhotographPath = "";


                            string uploadDirectory = HttpContext.Server.MapPath("~/UploadedFiles");
                            string UserDirectory = HttpContext.Server.MapPath("~/UploadedFiles/" + applicationModel.PersonalInfo.Id);
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
                                    CnicPath = Path.Combine(UserDirectory, "CNIC.jpeg");
                                    applicationModel.CNIC.SaveAs(CnicPath);
                                }
                                if (applicationModel.SSCMarkSheet != null)
                                {
                                    SSCPath = Path.Combine(UserDirectory, "SSC Mark Sheet.jpeg");
                                    applicationModel.SSCMarkSheet.SaveAs(SSCPath);
                                }
                                if (applicationModel.HSSCMarkSheet != null)
                                {
                                    HSSCPath = Path.Combine(UserDirectory, "HSSC Mark Sheet.jpeg");
                                    applicationModel.HSSCMarkSheet.SaveAs(HSSCPath);
                                }
                                if (applicationModel.Photograph != null)
                                {
                                    PhotographPath = Path.Combine(UserDirectory, "Photo.jpeg");
                                    applicationModel.Photograph.SaveAs(PhotographPath);
                                }
                                var document = DB.Documents.Where(x => x.UserId == applicationModel.PersonalInfo.Id).ToList().FirstOrDefault();
                                if (document != null)
                                {
                                    document.CNIC = CnicPath;
                                    document.Photograph = PhotographPath;
                                    document.SSCMarkSheet = SSCPath;
                                    document.HSSCMarkSheet = HSSCPath;
                                    document.IsCompleted = 1;
                                    DB.SaveChanges();
                                }
                                else
                                {
                                    DB.Documents.Add(new Document
                                    {
                                        UserId = applicationModel.PersonalInfo.Id,
                                        CNIC = CnicPath,
                                        Photograph = PhotographPath,
                                        SSCMarkSheet = SSCPath,
                                        HSSCMarkSheet = HSSCPath,
                                        IsCompleted = 1
                                    });
                                    DB.SaveChanges();
                                }
                                Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User successfully submited Documents.");
}
                            else
                            {
                                DocumentError.Add("SSC Mark sheet: is required");
                                DocumentError.Add("Passport size Photograph: is required");
                            }

                            DB.Educationals.Add(applicationModel.Education);
                            DB.SaveChanges();


                            for(var i = 0; i<applicationModel.SubjectName.Length - 1; i++)
                            {
                                DB.EducationalSubjects.Add(new EducationalSubject()
                                {
                                    EducationalId = applicationModel.Education.Id,
                                    Name = applicationModel.SubjectName[i],
                                    ObtainMarks = int.Parse(applicationModel.SubjectObtain[i])
                                });
                                DB.SaveChanges();
                            }
                            

                            Transaction.Commit();
                            return Json(new { status = true, message = "Application Submited Successfully"});


                        }
                        catch (System.Exception ex)
                        {
                            Transaction.Rollback();
                            return Json(new { status = false, message = "Form Submittion Failed" + ex.Message });

                        }
                    }



                    //    if (applicationModel.PersonalInfo.Id != 0)
                    //{
                    //    // Update Information 
                    //    var result =
                    //    DB.SP_UpdateApplication(
                    //            applicationModel.PersonalInfo.Id,
                    //            applicationModel.PersonalInfo.FirstName = Utility.ToCamelCase(applicationModel.PersonalInfo.FirstName),
                    //            applicationModel.PersonalInfo.MiddleName = Utility.ToCamelCase(applicationModel.PersonalInfo.MiddleName),
                    //            applicationModel.PersonalInfo.LastName = Utility.ToCamelCase(applicationModel.PersonalInfo.LastName),
                    //            applicationModel.PersonalInfo.FatherFirstName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherFirstName),
                    //            applicationModel.PersonalInfo.FatherMiddleName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherMiddleName),
                    //            applicationModel.PersonalInfo.FatherLastName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherLastName),
                    //            applicationModel.PersonalInfo.Gender.ToString(),
                    //            applicationModel.PersonalInfo.HusbandName,
                    //            applicationModel.PersonalInfo.DateOfBirth.ToString(),
                    //            applicationModel.PersonalInfo.CNIC,
                    //            applicationModel.PersonalInfo.EmailAddress,
                    //            applicationModel.PersonalInfo.AlterEmailAddress,
                    //            // Contact
                    //            applicationModel.PersonalInfo.CellPhoneNumber,
                    //            applicationModel.PersonalInfo.WhatsAppNumber,
                    //            applicationModel.PersonalInfo.AlternateCellPhoneNumber,
                    //            applicationModel.PersonalInfo.HomePhoneNumber,
                    //            applicationModel.PersonalInfo.AlternateLandline,
                    //            applicationModel.PersonalInfo.GuardianCellPhoneNumber,
                    //            applicationModel.PersonalInfo.GuardianEmailAddress,
                    //            // Address
                    //            applicationModel.PersonalInfo.ResidentialAddress,
                    //            applicationModel.PersonalInfo.ResidentialCountry,
                    //            applicationModel.PersonalInfo.ResidentialProvince,
                    //            applicationModel.PersonalInfo.ResidentialCity,
                    //            applicationModel.PersonalInfo.ResidentialCityOther,
                    //            applicationModel.PersonalInfo.ResidentialPostalCode,
                    //            applicationModel.PersonalInfo.PermanentAddress,
                    //            applicationModel.PersonalInfo.PermanentCountry,
                    //            applicationModel.PersonalInfo.PermanentProvince,
                    //            applicationModel.PersonalInfo.PermanentCity,
                    //            applicationModel.PersonalInfo.PermanentCityOther,
                    //            applicationModel.PersonalInfo.PermanentPostalCode,
                    //            applicationModel.PersonalInfo.HearAboutHU,
                    //            applicationModel.PersonalInfo.HearAboutHUOther,

                    //            // Education Info
                    //            applicationModel.Education.CurrentLevelOfEdu,
                    //            applicationModel.Education.HSSCSchoolName,
                    //            applicationModel.Education.HSSCSchoolAddress,
                    //            applicationModel.Education.HSSCStartDate.ToString(),
                    //            applicationModel.Education.HSSCCompletionDate.ToString(),
                    //            applicationModel.Education.HSSCPercentage,
                    //            applicationModel.Education.HSSCBoardId,
                    //            applicationModel.Education.HSSCBoardName,
                    //            applicationModel.Education.HSSCGroupId,
                    //            applicationModel.Education.HSSCGroupName,
                    //            applicationModel.Education.SSCSchoolName,
                    //            applicationModel.Education.SSCSchoolAddress,
                    //            applicationModel.Education.SSCPercentage,
                    //            applicationModel.Education.UniversityName,
                    //            applicationModel.Education.IntendedProgram,
                    //            applicationModel.Education.HUSchoolName,
                    //            SubNames + ',',
                    //            SubObtain + ','
                    //        ).FirstOrDefault();
                    //    if (result.Response != 0)
                    //    {
                    //        return Json(new { status = true, message = "Application Updated Successfully", userId = result.Response });
                    //    }
                    //    else
                    //    {
                    //        return Json(new { status = false, message = "Form Submittion Failed" + result.Reason.ToString() });
                    //    }
                    //}
                    //else
                    //{
                    //    // Insertion INTO DB

                    //    var result =
                    //    DB.SP_InsertApplication(
                    //            applicationModel.PersonalInfo.FirstName = Utility.ToCamelCase(applicationModel.PersonalInfo.FirstName),
                    //            applicationModel.PersonalInfo.MiddleName = Utility.ToCamelCase(applicationModel.PersonalInfo.MiddleName),
                    //            applicationModel.PersonalInfo.LastName = Utility.ToCamelCase(applicationModel.PersonalInfo.LastName),
                    //            applicationModel.PersonalInfo.FatherFirstName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherFirstName),
                    //            applicationModel.PersonalInfo.FatherMiddleName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherMiddleName),
                    //            applicationModel.PersonalInfo.FatherLastName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherLastName),
                    //            applicationModel.PersonalInfo.Gender.ToString(),
                    //            applicationModel.PersonalInfo.HusbandName,
                    //            applicationModel.PersonalInfo.DateOfBirth.ToString(),
                    //            applicationModel.PersonalInfo.CNIC,
                    //            applicationModel.PersonalInfo.EmailAddress,
                    //            applicationModel.PersonalInfo.AlterEmailAddress,
                    //            // Contact
                    //            applicationModel.PersonalInfo.CellPhoneNumber,
                    //            applicationModel.PersonalInfo.WhatsAppNumber,
                    //            applicationModel.PersonalInfo.AlternateCellPhoneNumber,
                    //            applicationModel.PersonalInfo.HomePhoneNumber,
                    //            applicationModel.PersonalInfo.AlternateLandline,
                    //            applicationModel.PersonalInfo.GuardianCellPhoneNumber,
                    //            applicationModel.PersonalInfo.GuardianEmailAddress,
                    //            // Address
                    //            applicationModel.PersonalInfo.ResidentialAddress,
                    //            applicationModel.PersonalInfo.ResidentialCountry,
                    //            applicationModel.PersonalInfo.ResidentialProvince,
                    //            applicationModel.PersonalInfo.ResidentialCity,
                    //            applicationModel.PersonalInfo.ResidentialCityOther,
                    //            applicationModel.PersonalInfo.ResidentialPostalCode,
                    //            applicationModel.PersonalInfo.PermanentAddress,
                    //            applicationModel.PersonalInfo.PermanentCountry,
                    //            applicationModel.PersonalInfo.PermanentProvince,
                    //            applicationModel.PersonalInfo.PermanentCity,
                    //            applicationModel.PersonalInfo.PermanentCityOther,
                    //            applicationModel.PersonalInfo.PermanentPostalCode,
                    //            applicationModel.PersonalInfo.HearAboutHU,
                    //            applicationModel.PersonalInfo.HearAboutHUOther,

                    //            // Education Info
                    //            applicationModel.Education.CurrentLevelOfEdu,
                    //            applicationModel.Education.HSSCSchoolName,
                    //            applicationModel.Education.HSSCSchoolAddress,
                    //            applicationModel.Education.HSSCStartDate.ToString(),
                    //            applicationModel.Education.HSSCCompletionDate.ToString(),
                    //            applicationModel.Education.HSSCPercentage,
                    //            applicationModel.Education.HSSCBoardId,
                    //            applicationModel.Education.HSSCBoardName,
                    //            applicationModel.Education.HSSCGroupId,
                    //            applicationModel.Education.HSSCGroupName,
                    //            applicationModel.Education.SSCSchoolName,
                    //            applicationModel.Education.SSCSchoolAddress,
                    //            applicationModel.Education.SSCPercentage,
                    //            applicationModel.Education.UniversityName,
                    //            applicationModel.Education.IntendedProgram,
                    //            applicationModel.Education.HUSchoolName,
                    //            SubNames + ',',
                    //            SubObtain + ','
                    //        ).FirstOrDefault();
                    //    if (result.Response != 0)
                    //    {
                    //        return Json(new { status = true, message = "Successfully Submitted", userId = result.Response });
                    //    }
                    //    else
                    //    {
                    //        return Json(new { status = false, message = "Form Submittion Failed" + result.Reason.ToString() });
                    //    }
                    //}
                }
            }
            catch (System.Exception ex)
            {

                return Json(new { status = false, message = "Form Submittion Failed" + ex.Message });

            }


        }
    }
}