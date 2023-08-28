using HUTOPS.Helper;
using System.Linq;
using System.Web.Mvc;

namespace HUTOPS.Controllers
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

        public ActionResult UpdatePersonalInfo(PersonalInformation model)
        {
            return Json(new {result = 1, msg = "Success"});
        }
    }
}