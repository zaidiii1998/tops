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
    public class DocumentsController : Controller
    {
        // GET: Documents
        HUTOPS.HU_TOPSEntities DB = new HU_TOPSEntities();
        public ActionResult Index()
        {
            int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
            var documents = DB.Documents.ToList().Where(x => x.UserId == userId).ToList().FirstOrDefault();
            ViewBag.Declaration = DB.PersonalInformations.ToList().Where(x => x.Id == userId).ToList().FirstOrDefault().Declaration;
            return View(documents);

        }
    }
}