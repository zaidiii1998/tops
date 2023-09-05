using HUTOPS.Helper;
using HUTOPS.Models;
using System;
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
            ViewModel mymodel = new ViewModel();
            var edu = DB.Educationals.ToList();
            var sub = DB.EducationalSubjects.ToList();
            mymodel.Education = edu.Where(x => x.UserId == userId).ToList();
            if (mymodel.Education != null)
            {
                mymodel.Subjects = sub.FindAll(x => x.EducationalId == mymodel.Education.FirstOrDefault().Id).ToList();
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

                var EducationalId = DB.WEB_InsertEducation(
                    userId, 
                    educational.CurrentLevelOfEdu, 
                    educational.CurrentCollege,
                    educational.CollegeAddress,
                    educational.CollegeStartDate.ToString(),
                    educational.CollegeCompletionDate.ToString(),
                    educational.HSSCPercentage,
                    educational.BoardOfEdu,
                    educational.GroupOfStudy,
                    educational.SchoolName,
                    educational.SchoolAddress,
                    educational.SSCPercentage,
                    educational.UniversityName,
                    educational.IntendedProgram,
                    SubNames,
                    SubObtain,
                    SubTotal,
                    SubGrade
                    );

                return Json(new {status = true,message = "Educational Information Updated Successfully"});
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Educational Information Updation Failed" });

            }
        }
    }
}