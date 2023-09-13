using HUTOPS.Helper;
using HUTOPS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class EducationController : Controller
    {
        //HU_TOPSEntities DB = new HU_TOPSEntities(); // Local System DB
        HUTOPSEntities DB = new HUTOPSEntities(); // Server DB
        // GET: Education
        public ActionResult Index()
        {
            EducationPageModel educationPageModel = new EducationPageModel();
            var edu = DB.Educationals.ToList();
            var sub = DB.EducationalSubjects.ToList();
            educationPageModel.Education = Utility.GetEducationFromSession();
            educationPageModel.Boards = DB.Boards.ToList();
            educationPageModel.Groups = DB.BoardGroups.ToList();
            if (educationPageModel.Education != null)
            {
                educationPageModel.Subjects = sub.FindAll(x => x.EducationalId == (educationPageModel.Education == null ? 0 : educationPageModel.Education.Id)).ToList();
            }
            else
            {
                educationPageModel.Education = new Educational();
                educationPageModel.Subjects = new List<EducationalSubject>();
            }
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData["Result"].ToString();
            }
            ViewBag.Declaration = Utility.GetUserFromSession().Declaration;
            return View(educationPageModel);
        }
        [HttpPost]
        public ActionResult Save(Educational educational, string[] SubjectName, string[] SubjectObtain, string[] SubjectTotal, string[] SubjectGrade)
        {
            try
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"User-requested to Save Educational Information. Details: {JsonConvert.SerializeObject(educational)}");
                var user = Utility.GetUserFromSession();
                if (user.Declaration == 1)
                {
                    return Json(new { status = false, message = "You have already submited your application" });
                }

                educational.UserId = user.Id;
                var SubNames = Utility.ConvertArrayToCSV(SubjectName);
                var SubObtain = Utility.ConvertArrayToCSV(SubjectObtain);
                var SubTotal = Utility.ConvertArrayToCSV(SubjectTotal);
                var SubGrade = Utility.ConvertArrayToCSV(SubjectGrade);
                educational.IsCompleted = 0;
                var EducationalId = DB.WEB_InsertEducation(
                    educational.UserId, 
                    educational.CurrentLevelOfEdu, 
                    educational.CurrentCollege,
                    educational.CollegeAddress,
                    educational.CollegeStartDate.ToString(),
                    educational.CollegeCompletionDate.ToString(),
                    educational.HSSCPercentage,
                    educational.BoardOfEdu,
                    educational.BoardName,
                    educational.GroupOfStudy,
                    educational.GroupName,
                    educational.SchoolName,
                    educational.SchoolAddress,
                    educational.SSCPercentage,
                    educational.UniversityName,
                    educational.IntendedProgram,
                    educational.IsCompleted,
                    SubNames + ',',
                    SubObtain + ',',
                    SubTotal + ',',
                    SubGrade + ','
                    );
                Utility.SetSession(educational);
                Utility.AddLog(Constants.LogType.ActivityLog, $"User Successfully Save Educational Information. Details: {JsonConvert.SerializeObject(educational)}");
                return Json(new {status = true,message = "Educational Information Updated Successfully"});
            }
            catch (Exception ex)
            {
                Utility.AddLog(Constants.LogType.Exception, "Exception occurred during Saving educational Information." + ((ex.InnerException != null) ? ex.InnerException.Message : "") + "Model Details: " + JsonConvert.SerializeObject(educational));
                return Json(new { status = false, message = "Educational Information Updation Failed" });

            }
        }
        [HttpPost]
        public ActionResult Submit(Educational educational, string[] SubjectName, string[] SubjectObtain, string[] SubjectTotal, string[] SubjectGrade)
        {
            try
            {
                var IsValid = true;
                List<string> Err = new List<string>();
                Utility.AddLog(Constants.LogType.ActivityLog, $"User-requested to Submit Educational Information. Details: {JsonConvert.SerializeObject(educational)}");
                var user = Utility.GetUserFromSession();
                if (user.Declaration == 1)
                {
                    return Json(new { status = false, message = "You have already submited your application" });
                }

                
                var SubNames = Helper.Utility.ConvertArrayToCSV(SubjectName);
                var SubObtain = Helper.Utility.ConvertArrayToCSV(SubjectObtain);
                var SubTotal = Helper.Utility.ConvertArrayToCSV(SubjectTotal);
                var SubGrade = Helper.Utility.ConvertArrayToCSV(SubjectGrade);
                educational.BoardName = DB.Boards.ToList().Where(x => x.Id == (educational.BoardOfEdu == "" ? 0: educational.BoardOfEdu == null ? 0 : int.Parse(educational.BoardOfEdu))).ToList().FirstOrDefault().Name;
                educational.GroupName = DB.BoardGroups.ToList().Where(x => x.Id == (educational.GroupOfStudy == "" ? 0 : educational.GroupOfStudy == null ? 0 : int.Parse(educational.GroupOfStudy))).ToList().FirstOrDefault().Name;
                educational.IsCompleted = 1;
                if (string.IsNullOrEmpty(educational.CurrentLevelOfEdu))
                {
                    IsValid = false;
                    Err.Add("Your Current Level of Study: is required");
                }
                if (string.IsNullOrEmpty(educational.CollegeStartDate.ToString()))
                {
                    IsValid = false;
                    Err.Add("Starting Year: is required");
                }
                if (string.IsNullOrEmpty(educational.CollegeCompletionDate.ToString()))
                {
                    IsValid = false;
                    Err.Add("Completion Year: is required");
                }
                if (string.IsNullOrEmpty(educational.CurrentCollege))
                {
                    IsValid = false;
                    Err.Add("Current College/Last College Name: is required");
                }
                if (string.IsNullOrEmpty(educational.CollegeAddress))
                {
                    IsValid = false;
                    Err.Add("College Address with City Name: is required");
                }
                if (string.IsNullOrEmpty(educational.BoardOfEdu))
                {
                    IsValid = false;
                    Err.Add("Board Of Education: is required");
                }
                if (string.IsNullOrEmpty(educational.GroupOfStudy))
                {
                    IsValid = false;
                    Err.Add("Group Of Studies: is required");
                }
                if (string.IsNullOrEmpty(educational.HSSCPercentage))
                {
                    IsValid = false;
                    Err.Add("Overall HSSC Percentage: is required");
                }
                if (string.IsNullOrEmpty(educational.SchoolName))
                {
                    IsValid = false;
                    Err.Add("Secondary Education School Name: is required");
                }
                if (string.IsNullOrEmpty(educational.SchoolAddress))
                {
                    IsValid = false;
                    Err.Add("Secondary Education School Address with City Name: is required");
                }
                if (string.IsNullOrEmpty(educational.SSCPercentage))
                {
                    IsValid = false;
                    Err.Add("Overall SSC Percentage: is required");
                }
                if (string.IsNullOrEmpty(educational.IntendedProgram))
                {
                    IsValid = false;
                    Err.Add("Which Degree Program you are planning to pursue at Habib University?: is required");
                }
                if (IsValid) {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"User provided data is Valid for Submit Personal Information. Details: {JsonConvert.SerializeObject(educational)}");
                    var EducationalId = DB.WEB_InsertEducation(
                    user.Id,
                    educational.CurrentLevelOfEdu,
                    educational.CurrentCollege,
                    educational.CollegeAddress,
                    educational.CollegeStartDate.ToString(),
                    educational.CollegeCompletionDate.ToString(),
                    educational.HSSCPercentage,
                    educational.BoardOfEdu,
                    educational.BoardName,
                    educational.GroupOfStudy,
                    educational.GroupName,
                    educational.SchoolName,
                    educational.SchoolAddress,
                    educational.SSCPercentage,
                    educational.UniversityName,
                    educational.IntendedProgram,
                    educational.IsCompleted,
                    SubNames + ',',
                    SubObtain + ',',
                    SubTotal + ',',
                    SubGrade + ','
                    );
                    Utility.SetSession(educational);
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Educational Information Submited Successfully. Details: {JsonConvert.SerializeObject(educational)}");
                    return Json(new { status = true, message = "Educational Information Submited Successfully" });
                }
                else
                {
                    
                    EducationPageModel mymodel = new EducationPageModel();
                    var edu = DB.Educationals.ToList();
                    var sub = DB.EducationalSubjects.ToList();
                    mymodel.Education = Utility.GetEducationFromSession();
                    mymodel.Boards = DB.Boards.ToList();
                    mymodel.Groups = DB.BoardGroups.ToList();
                    if (mymodel.Education != null)
                    {
                        mymodel.Subjects = sub.FindAll(x => x.EducationalId == (mymodel.Education == null ? 0 : mymodel.Education.Id)).ToList();
                    }
                    else
                    {
                        mymodel.Education = new Educational();
                        mymodel.Subjects = new List<EducationalSubject>();
                    }
                    return Json(new { status = false, message = "Educational Information Submition Failed", error = Err });
                }
            } 
            catch (Exception ex)
            {
                Helper.Utility.AddLog(Constants.LogType.Exception, "Exception occurred during Submiting educational Information." + ((ex.InnerException != null) ? ex.InnerException.Message : "") + "Model Details: " + JsonConvert.SerializeObject(educational));
                return Json(new { status = false, message = "Educational Information Submition Failed" });

            }
        }

    }
}