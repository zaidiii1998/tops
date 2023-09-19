using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        HUTOPSEntities DB = new HUTOPSEntities();
        public ActionResult Index()
        {
            return View(DB.EmailTemplates.ToList());
        }
    }
}