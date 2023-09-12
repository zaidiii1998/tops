using HUTOPS.Helper;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
            var personal = DB.PersonalInformations.ToList().Where(x => x.Id == int.Parse(Helper.Utility.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
            var edu = DB.Educationals.ToList().Where(x => x.UserId == int.Parse(Helper.Utility.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
            var docs = DB.Documents.ToList().Where(x => x.UserId == int.Parse(Helper.Utility.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
            ViewBag.Personal = personal == null ? "" : personal.IsCompleted.ToString();
            ViewBag.Edu = edu == null ? "" : edu.IsCompleted.ToString(); 
            ViewBag.Docs = docs == null ? "" :docs.IsCompleted.ToString();
            ViewBag.Declaration = personal.Declaration;
            return View();
        }
        public ActionResult Submit(bool check1, bool check2, bool check3, string HearHU, string HearHUOther)
        {
            try
            {
                Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User-requested to submit declarations:");
                var personal = DB.PersonalInformations.ToList().Where(x => x.Id == int.Parse(Helper.Utility.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
                var edu = DB.Educationals.ToList().Where(x => x.UserId == int.Parse(Helper.Utility.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
                var docs = DB.Documents.ToList().Where(x => x.UserId == int.Parse(Helper.Utility.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
                if (string.IsNullOrWhiteSpace(HearHU))
                {
                    return Json(new { status = false, message = "How did you first hear about Habib University : is required" });
                }
                
                if (personal.IsCompleted == 1 && edu.IsCompleted == 1 && docs.IsCompleted == 1)
                {
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User has completed All required Sections to submit declarations:");
                    if (check1 && check2 && check3)
                    {

                        var userId = int.Parse(Helper.Utility.GetSession(Constants.Session.UserId));
                        var user = DB.PersonalInformations.ToList().Where(x => x.Id == userId).ToList().FirstOrDefault();
                        user.Declaration = 1;
                        user.SubmissionDate = DateTime.Now;
                        user.HearAboutHU = HearHU;
                        user.HearAboutHUOther = HearHUOther;
                        DB.SaveChanges();

                        Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"Declarations and Aplication Submited Successfully");
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