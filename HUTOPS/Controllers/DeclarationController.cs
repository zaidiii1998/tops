using HUTOPS.Helper;
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
        HU_TOPSEntities DB = new HU_TOPSEntities();
        // GET: Declaration
        public ActionResult Index()
        {
            var personal = DB.PersonalInformations.ToList().Where(x => x.Id == int.Parse(Helper.Helper.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
            var edu = DB.Educationals.ToList().Where(x => x.UserId == int.Parse(Helper.Helper.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
            var docs = DB.Documents.ToList().Where(x => x.UserId == int.Parse(Helper.Helper.GetSession(Helper.Constants.Session.UserId))).ToList().FirstOrDefault();
            ViewBag.Personal = personal.IsCompleted.ToString();
            ViewBag.Edu = edu.IsCompleted.ToString(); 
            ViewBag.Docs = docs.IsCompleted.ToString();
            return View();
        }
        
    }
}