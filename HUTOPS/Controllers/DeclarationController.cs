using HUTOPS.Helper;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class DeclarationController : Controller
    {
        //HU_TOPSEntities DB = new HU_TOPSEntities(); // Local System DB
        HUTOPSEntities DB = new HUTOPSEntities(); // Server DB
        // GET: Declaration
        public ActionResult Index()
        {
            var personal = Utility.GetUserFromSession();
            var edu = Utility.GetEducationFromSession();
            var docs = DB.Documents.ToList().Where(x => x.UserId == personal.Id).FirstOrDefault();
            ViewBag.Personal = personal == null ? "" : personal.IsCompleted.ToString();
            ViewBag.Edu = edu == null ? "" : edu.IsCompleted.ToString(); 
            ViewBag.Docs = docs == null ? "" :docs.IsCompleted.ToString();
            ViewBag.User = personal;
            return View();
        }
        public ActionResult Submit(bool check1, bool check2, bool check3, int UserId)
        {
            try
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"User-requested to submit declarations:");
                var personal = Utility.GetUserFromSession();
                if (personal.Declaration == 1 && Utility.GetAdminFromSession().Name == null)
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"User has already submited the application");
                    return Json(new { status = false, message = "You have already submited your application" });
                }
                if (UserId != personal.Id)
                {
                    return Json(new { status = false, message = "Multi profile conflict occur while saving student record" });
                }
                var edu = Utility.GetEducationFromSession();
                var docs = DB.Documents.ToList().Where(x => x.UserId == personal.Id).FirstOrDefault();

                if (personal.IsCompleted == 1 && edu.IsCompleted == 1 && docs.IsCompleted == 1)
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"User has completed All required Sections to submit declarations:");
                    if (check1 && check2 && check3)
                    {
                        var person = DB.PersonalInformations.ToList().Where(x => x.Id == personal.Id).FirstOrDefault();
                        person.Declaration = 1;
                        person.SubmissionDate = DateTime.Now;
                        DB.SaveChanges();
                        Utility.SetSession(person);

                        Utility.AddLog(Constants.LogType.ActivityLog, $"Declarations and Aplication Submited Successfully");
                        return Json(new { status = true, message = "Aplication Submited Successfully" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "All checkboxes are required" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Please Complete All sections before submit your application" });
                }
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Aplication Submition Failed" });
            }
        }
        
    }
}