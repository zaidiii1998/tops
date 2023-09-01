using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    public class DeclarationController : Controller
    {
        HU_TOPSEntities DB = new HU_TOPSEntities();
        // GET: Declaration
        public ActionResult Index()
        {
            return View();
        }
        
    }
}