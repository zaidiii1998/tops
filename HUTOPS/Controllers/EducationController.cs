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
        HU_TOPSEntities DB = new HU_TOPSEntities();
        // GET: Education
        public ActionResult Index()
        {
            int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
            EducationPageModel mymodel = new EducationPageModel();
            var edu = DB.Educationals.ToList();
            var sub = DB.EducationalSubjects.ToList();
            mymodel.Education = edu.Where(x => x.UserId == userId).ToList().FirstOrDefault();
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
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData["Result"].ToString();
            }
            return View(mymodel);
        }
        [HttpPost]
        public ActionResult Save(Educational educational, string[] SubjectName, string[] SubjectObtain, string[] SubjectTotal, string[] SubjectGrade)
        {
            try
            {
                var userId = int.Parse(Session["UserId"].ToString());
                var SubNames = Helper.Helper.ConvertArrayToCSV(SubjectName);
                var SubObtain = Helper.Helper.ConvertArrayToCSV(SubjectObtain);
                var SubTotal = Helper.Helper.ConvertArrayToCSV(SubjectTotal);
                var SubGrade = Helper.Helper.ConvertArrayToCSV(SubjectGrade);
                educational.BoardName = DB.Boards.ToList().Where(x => x.Id == (educational.BoardOfEdu == "" ? 0 : int.Parse(educational.BoardOfEdu))).ToList().FirstOrDefault().Name;
                educational.GroupName = DB.BoardGroups.ToList().Where(x => x.Id == (educational.GroupOfStudy == "" ? 0 : int.Parse(educational.GroupOfStudy))).ToList().FirstOrDefault().Name;
                var EducationalId = DB.WEB_InsertEducation(
                    userId, 
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
                    SubNames + ',',
                    SubObtain + ',',
                    SubTotal + ',',
                    SubGrade + ','
                    );

                return Json(new {status = true,message = "Educational Information Updated Successfully"});
            }
            catch (Exception)
            {
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
                
                var userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                var SubNames = Helper.Helper.ConvertArrayToCSV(SubjectName);
                var SubObtain = Helper.Helper.ConvertArrayToCSV(SubjectObtain);
                var SubTotal = Helper.Helper.ConvertArrayToCSV(SubjectTotal);
                var SubGrade = Helper.Helper.ConvertArrayToCSV(SubjectGrade);
                educational.BoardName = DB.Boards.ToList().Where(x => x.Id == (educational.BoardOfEdu == "" ? 0: educational.BoardOfEdu == null ? 0 : int.Parse(educational.BoardOfEdu))).ToList().FirstOrDefault().Name;
                educational.GroupName = DB.BoardGroups.ToList().Where(x => x.Id == (educational.GroupOfStudy == "" ? 0 : educational.GroupOfStudy == null ? 0 : int.Parse(educational.GroupOfStudy))).ToList().FirstOrDefault().Name;

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
                var EducationalId = DB.WEB_InsertEducation(
                    userId,
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
                    SubNames + ',',
                    SubObtain + ',',
                    SubTotal + ',',
                    SubGrade + ','
                    );
                    TempData["Result"] = "Educational Information Submited Successfully";
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"Educational Information Submited Successfully. Details: {JsonConvert.SerializeObject(educational)}");
                    return Json(new { status = true, message = "Educational Information Submited Successfully" });
                }
                else
                {
                    
                    EducationPageModel mymodel = new EducationPageModel();
                    var edu = DB.Educationals.ToList();
                    var sub = DB.EducationalSubjects.ToList();
                    mymodel.Education = edu.Where(x => x.UserId == userId).ToList().FirstOrDefault();
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
            catch (Exception)
            {
                return Json(new { status = false, message = "Educational Information Submition Failed" });

            }
        }

    }
}