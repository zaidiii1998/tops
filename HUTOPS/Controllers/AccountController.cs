using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using HUTOPS.Helper;

namespace HUTOPS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //HU_TOPSEntities DB = new HU_TOPSEntities(); // Local System DB
        HUTOPSEntities DB = new HUTOPSEntities(); // Server DB
        public ActionResult Login()
        {
            Helper.Utility.AddLog(Constants.LogType.ActivityLog, "User has navigated to the registration page.");
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
                    var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == result.Response).FirstOrDefault();
                    Utility.AddLog(Constants.LogType.ActivityLog, "User Successfully LogIn" + "UserID: " + result.Response + "UserName" + personalInformation.FirstName + " " + personalInformation.LastName);
                    Helper.Utility.SetSession(Constants.Session.UserId, result.Response.ToString());
                    Helper.Utility.SetSession(Constants.Session.UserName, personalInformation.FirstName + " " + personalInformation.LastName);
                    Helper.Utility.SetUserSession(personalInformation);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, "User LogIn Failed" + "Reason: " + result.Reason);
                    ViewBag.Result = "Invalid User";
                    return View();
                }
            }
            catch (System.Exception ex)
            {
                Helper.Utility.AddLog(Constants.LogType.ActivityLog, "Exception Occur While Login" + "Ex: " + ex.InnerException.Message);
                ViewBag.Result = "Invalid User";
                return View();
            }
            
        }
        public ActionResult Register()
        {
            Helper.Utility.AddLog(Constants.LogType.ActivityLog, "User has navigated to the registration page.");
            return View(new PersonalInformation());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection model)
        {
            PersonalInformation GivenModel = new PersonalInformation()
            {
                FirstName = model["FirstName"],
                MiddleName = model["MiddleName"],
                LastName = model["LastName"],
                EmailAddress = model["EmailAddress"],
                CNIC = model["CNIC"],
                CellPhoneNumber = model["CellPhoneNumber"]

            };
            try
            {
                
                var IsValid = true;
                var user = DB.PersonalInformations.ToList();
                var userEmail = user.Exists(x => x.EmailAddress == model["EmailAddress"]);
                var userCNIC = user.Where(x => x.CNIC == model["CNIC"]).ToList();
                var userPhone = user.Where(x => x.CellPhoneNumber == model["CellPhoneNumber"]).ToList();

                if (ModelState.IsValid)
                {
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, "Model Validation Is true");
                    if (model["Password"] != model["ConfirmPassword"])
                    {
                        IsValid = false;
                        ModelState.AddModelError("Password", "Password Confirmation does not match.");
                        ModelState.AddModelError("Id", "Password Confirmation does not match.");
                    }
                    if (string.IsNullOrEmpty(model["FirstName"]))
                    {
                        IsValid = false;
                        ModelState.AddModelError("FirstName", "First Name is required");
                    }
                    if (!string.IsNullOrEmpty(model["FirstName"]) && (model["FirstName"].Length < 3 || model["FirstName"].Length > 25))
                    {
                        IsValid = false;
                        ModelState.AddModelError("FirstName", "First Name length must be greater than 3 and less than 25 characters");

                    }
                    if (!string.IsNullOrEmpty(model["MiddleName"]) && (model["MiddleName"].Length < 3 || model["MiddleName"].Length > 25))
                    {
                        IsValid = false;
                        ModelState.AddModelError("MiddleName", "Middle Name length must be greater than 3 and less than 25 characters");

                    }
                    if (string.IsNullOrEmpty(model["LastName"]))
                    {
                        IsValid = false;
                        ModelState.AddModelError("LastName", "Last Name is required");
                    }
                    if (!string.IsNullOrEmpty(model["LastName"]) && (model["LastName"].Length < 3 || model["LastName"].Length > 25))
                    {
                        IsValid = false;
                        ModelState.AddModelError("LastName", "Last Name length must be greater than 3 and less than 25 characters");

                    }
                    if (string.IsNullOrEmpty(model["Password"]))
                    {
                        IsValid = false;
                        ModelState.AddModelError("Password", "Password required");
                    }

                    if (!string.IsNullOrEmpty(model["Password"]) && (model["Password"].Length < 6 || model["Password"].Length > 25))
                    {
                        IsValid = false;
                        ModelState.AddModelError("Password", "Password length must be greater than 6 and less than 25");
                    }
                    if (string.IsNullOrEmpty(model["EmailAddress"].ToString()))
                    {
                        IsValid = false;
                        ModelState.AddModelError("EmailAddress", "Email is required");
                    }
                    if (!Helper.Utility.isValidEmail(model["EmailAddress"].ToString()))
                    {
                        IsValid = false;
                        ModelState.AddModelError("EmailAddress", "Invalid Email");
                    }
                    if (!(string.IsNullOrEmpty(model["EmailAddress"].ToString())) && userEmail)
                    {
                        IsValid = false;
                        ModelState.AddModelError("EmailAddress", "Email already Exist");
                    }
                    if (string.IsNullOrEmpty(model["CNIC"].ToString()))
                    {
                        IsValid = false;
                        ModelState.AddModelError("CNIC", "CNIC is required");
                    }
                    if (!string.IsNullOrEmpty(model["CNIC"].ToString()) && userCNIC.Count == 1)
                    {
                        IsValid = false;
                        ModelState.AddModelError("CNIC", "CNIC already Exist");
                    }
                    if (string.IsNullOrEmpty(model["CellPhoneNumber"].ToString()))
                    {
                        IsValid = false;
                        ModelState.AddModelError("CellPhoneNumber", "Phone Number is required");
                    }
                    if (!string.IsNullOrEmpty(model["CellPhoneNumber"]) && !Helper.Utility.IsValidPhoneNumber(model["CellPhoneNumber"]))
                    {
                        IsValid = false;
                        ModelState.AddModelError("CellPhoneNumber", "Phone Number is Invalid");
                    }
                    if (!string.IsNullOrEmpty(model["CellPhoneNumber"].ToString()) && userPhone.Count == 1)
                    {
                        IsValid = false;
                        ModelState.AddModelError("CellPhoneNumber", "Phone Number already Exist");
                    }

                    if (IsValid)
                    {
                        Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided data has been successfully validated.Details: Firstname: {model["FirstName"]}, LastName: {model["LastName"]}, Email: {model["EmailAddress"]}, CNIC: {model["CNIC"]}, PhoneNumber: {model["CellPhoneNumber"]} ");
                        var result = DB.WEB_CreateUser(model["FirstName"], model["MiddleName"], model["LastName"], model["CNIC"], model["CellPhoneNumber"], model["EmailAddress"], model["Password"].ToString()).ToList().FirstOrDefault();
                        if (result.Response != 0)
                        {
                            var currentUser = DB.PersonalInformations.ToList().Where(x => x.Id == result.Response).FirstOrDefault();
                            Helper.Utility.AddLog(Constants.LogType.ActivityLog, "User account has been successfully created." + "UserID: "+ result.Response + "UserName" + currentUser.FirstName + " " + currentUser.LastName);
                            Helper.Utility.SetSession(Constants.Session.UserId, result.Response.ToString());
                            Helper.Utility.SetUserSession(currentUser);
                            Helper.Utility.SetSession(Constants.Session.UserName, currentUser.FirstName + " " + currentUser.LastName);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Helper.Utility.AddLog(Constants.LogType.ActivityLog, "User Registration Failed" + result.Reason.ToString());
                            ViewBag.Result = "Registration Failed," + result.Reason.ToString();
                        }
                    }
                    else
                    {
                        Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided data is Invalid. Details: Firstname: {model["FirstName"]}, LastName: {model["LastName"]}, Email: {model["EmailAddress"]}, CNIC: {model["CNIC"]}, PhoneNumber: {model["CellPhoneNumber"]} ");
                        return View(GivenModel);
                    }
                    return View(GivenModel);
                }
                else
                {
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided data is Invalid. Details: Firstname: {model["FirstName"]}, LastName: {model["LastName"]}, Email: {model["EmailAddress"]}, CNIC: {model["CNIC"]}, PhoneNumber: {model["CellPhoneNumber"]} ");
                    ViewBag.Result = "Invalid Model";
                    return View(GivenModel);
                }
            }
            catch (System.Exception ex)
            {
                Helper.Utility.AddLog(Constants.LogType.ActivityLog, "Exception occurred during user registration." + ((ex.InnerException != null)? ex.InnerException.Message: ""));
                ViewBag.Result = "Registration Failed, Please try again later";
                return View(GivenModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Remove("UserId");
            return RedirectToAction("Login","Account");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}