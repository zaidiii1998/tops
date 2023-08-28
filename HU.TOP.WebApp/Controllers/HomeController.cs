using HU.TOP.WebApp.Helper;
using System.Linq;
using System.Web.Mvc;

namespace HU.TOP.WebApp.Controllers
{
    [SessionValidatorActionFilter]
    public class HomeController : Controller
    {
        HU_TOPSEntities DB = new HU_TOPSEntities();
        public ActionResult Index()
        {
            return View(DB.PersonalInformations.ToList());
        }
    }
}