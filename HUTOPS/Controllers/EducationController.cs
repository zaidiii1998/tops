using HUTOPS.Helper;
using System;
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
            return View();
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