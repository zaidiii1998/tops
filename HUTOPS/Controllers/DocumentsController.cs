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
            return View(documents == null? new Document() : documents);

        }
        public ActionResult View(int doc)
        {
            try
            {
                int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                var documents = DB.Documents.ToList().Where(x => x.UserId == userId).ToList().FirstOrDefault();
                var url = "";
                if(documents != null) { 
                if (doc == 1){url = documents.Photograph == "" ? "" : documents.Photograph.Substring(documents.Photograph.IndexOf("Upload"));}
                else if (doc == 2) { url = documents.SSCMarkSheet == "" ? "" : documents.SSCMarkSheet.Substring(documents.SSCMarkSheet.IndexOf("Upload")); }
                else if (doc == 3) { url = documents.HSSCMarkSheet == "" ? "" : documents.HSSCMarkSheet.Substring(documents.HSSCMarkSheet.IndexOf("Upload")); }
                else { url = documents.CNIC == "" ? "" : documents.CNIC.Substring(documents.CNIC.IndexOf("Upload")); }
                }
                ViewBag.Url = '/' + url;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}