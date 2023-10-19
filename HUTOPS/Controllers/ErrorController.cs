using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}