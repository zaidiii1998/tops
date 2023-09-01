using System.Linq;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        HU_TOPSEntities DB = new HU_TOPSEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                var result = DB.WEB_UserLogin(form["email"], form["psw"]).ToList().FirstOrDefault();
                if (result.Response != -1)
                {
                    var user = DB.PersonalInformations.ToList().Where(x => x.Id == result.Response).FirstOrDefault();
                    Session["UserId"] = result.Response.ToString();
                    Session["Name"] = user.FirstName + " " + user.LastName;
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
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection form)
        {
            try
            {
                if (form["password"] == form["cpassword"])
                {
                    var result = DB.WEB_CreateUser(form["fname"], form["mname"], form["lname"], form["cnic"], form["number"], form["email"], form["password"]).ToList().FirstOrDefault();
                    if (result != 0)
                    {
                        var user = DB.PersonalInformations.ToList().Where(x => x.Id ==  result).FirstOrDefault();
                        Session["UserId"] = result.ToString();
                        Session["Name"] = user.FirstName + " " + user.LastName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Result = "Registration Failed, Please try again later";
                    }
                    return View();
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
        public ActionResult LogOut()
        {
            Session.Remove("UserId");
            return View("Login");
        }
    }
}