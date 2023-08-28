using System.Linq;
using System.Web.Mvc;

namespace HU.TOP.WebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        HU_TOPSEntities DB = new HU_TOPSEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                var result = DB.WEB_UserLogin(form["email"], form["psw"]).ToList().FirstOrDefault();
                if (result.Response != -1)
                {
                    Session["UserId"] = result.Response.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Result = "Invalid User";
                    return View();
                }
            }
            catch (System.Exception)
            {

                ViewBag.Result = "Invalid User";
                return View();
            }
            
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            try
            {
                if (form["password"] == form["cpassword"])
                {
                    var result = DB.WEB_CreateUser(form["fname"], form["mname"], form["lname"], form["cnic"], form["number"], form["email"], form["password"]).ToList().FirstOrDefault();
                    if (result != -1)
                    {
                        Session["UserId"] = result.ToString();
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ViewBag.Result = "Registration Failed, Please try again later";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Result = "Password not match";
                    return View();
                }
            }
            catch (System.Exception)
            {
                ViewBag.Result = "Registration Failed, Please try again later";
                return View();
            }
        }
    }
}