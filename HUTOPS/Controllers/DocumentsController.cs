using HUTOPS.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class DocumentsController : Controller
    {
        // GET: Documents
        //HU_TOPSEntities DB = new HU_TOPSEntities(); // Local System DB
        HUTOPSEntities DB = new HUTOPSEntities(); // Server DB
        public ActionResult Index()
        {

            var personalInfo = Utility.GetUserFromSession();
            var documents = DB.Documents.ToList().Where(x => x.UserId == personalInfo.Id).FirstOrDefault();
            ViewBag.Declaration = Utility.GetUserFromSession().Declaration;
            return View(documents == null? new Document() : documents);

        }
        public ActionResult View(int doc)
        {
            try
            {
                var personalInfo = Utility.GetUserFromSession();
                var documents = DB.Documents.ToList().Where(x => x.UserId == personalInfo.Id).FirstOrDefault();
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