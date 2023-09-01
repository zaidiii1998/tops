using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    public class DocumentsController : Controller
    {
        // GET: Documents
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            try
            {
                // Get the uploaded files
                HttpPostedFileBase bFormCnicFile = Request.Files["bFormCnic"];

                // Check if files are present
                if (bFormCnicFile != null && bFormCnicFile.ContentLength > 0)
                {
                    // Save the file to a server directory
                    string bFormCnicPath = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(bFormCnicFile.FileName));
                    bFormCnicFile.SaveAs(bFormCnicPath);

                    // Process other files similarly
                }

                // Handle success response
                return Json(new { success = true, message = "Files uploaded successfully." });
            }
            catch (Exception ex)
            {
                // Handle error
                return Json(new { success = false, message = "An error occurred while uploading files." });
            }
        }
    }
}