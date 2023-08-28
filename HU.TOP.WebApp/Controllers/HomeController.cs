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
            try
            {
                int userId = int.Parse(Session["UserId"].ToString());
                var user = DB.PersonalInformations.ToList();
                var LoginUser = user.Where(x => x.Id == userId).FirstOrDefault();
                return View(LoginUser);
            }
            catch (System.Exception)
            {
                return View();
            }
            
        }
    }
}