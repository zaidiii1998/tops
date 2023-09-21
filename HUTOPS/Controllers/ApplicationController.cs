using HUTOPS.Models;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace HUTOPS.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Submit(ApplicationModel applicationModel)
        {

            return Json(new { status = true, message = "Successfully Submitted" });
        }
    }
}