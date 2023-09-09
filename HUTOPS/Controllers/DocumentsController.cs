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
        public ActionResult Index()
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData["Result"].ToString();
            }
            return View();

        }
    }
}