using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using HUTOPS.Helper;
using HUTOPS.Models;
using Newtonsoft.Json;

namespace HUTOPS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        //HU_TOPSEntities DB = new HU_TOPSEntities(); // Local System DB
        HUTOPSEntities DB = new HUTOPSEntities(); // Server DB
        public ActionResult Login()
        {
            Utility.AddLog(Constants.LogType.ActivityLog, "User has navigated to the registration page.");
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                var result = DB.WEB_UserLogin(form["email"], HUCryptography.Crypto.Encrypt(form["psw"].ToString())).ToList().FirstOrDefault();
                if (result.Response != -1)
                {
                    var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == result.Response).FirstOrDefault();
                    var education = DB.Educationals.ToList().Where(x => x.UserId == result.Response).FirstOrDefault();
                    var document = DB.Documents.ToList().Where(x => x.UserId == result.Response).FirstOrDefault();
                    Utility.AddLog(Constants.LogType.ActivityLog, "User Successfully LogIn" + "UserID: " + result.Response + "UserName" + personalInformation.FirstName + " " + personalInformation.LastName);
                    
                    
                    Utility.SetSession(education);
                    Utility.SetSession(document);
                    if (personalInformation.UserType == 1)
                    {
                        Admin admin = new Admin();
                        admin.Id = result.Response;
                        admin.Name = personalInformation.FirstName + " " + personalInformation.LastName;
                        admin.Email = personalInformation.EmailAddress;
                        Utility.SetSession(admin);
                        Utility.SetSession(new PersonalInformation());
                        return RedirectToAction("Index", "Student");
                    }
                    else { Utility.SetSession(personalInformation); }
                    
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, "User LogIn Failed" + "Reason: " + result.Reason);
                    ViewBag.Result = "User Password/Email is not valid";
                    return View();
                }
            }
            catch (System.Exception ex)
            {
                Utility.AddLog(Constants.LogType.ActivityLog, "Exception Occur While Login" + "Ex: " + ex.InnerException.Message);
                ViewBag.Result = "User Password/Email is not valid";
                return View();
            }
            
        }
        [SessionValidatorActionFilter]
        public ActionResult Register()
        {
                Utility.AddLog(Constants.LogType.ActivityLog, "User has navigated to the registration page.");
                return View(new PersonalInformation());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection model)
        {
            PersonalInformation personalInfo = new PersonalInformation()
            {
                FirstName = model["FirstName"],
                MiddleName = model["MiddleName"],
                LastName = model["LastName"],
                EmailAddress = model["EmailAddress"],
                CNIC = model["CNIC"],
                CellPhoneNumber = model["CellPhoneNumber"],
                HearAboutHU = model["HearAboutHU"],
                HearAboutHUOther = model["HearAboutHUOther"]

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
                    Utility.AddLog(Constants.LogType.ActivityLog, "Model Validation Is true");
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
                    if (!Utility.isValidEmail(model["EmailAddress"].ToString()))
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
                    if (!string.IsNullOrEmpty(model["CellPhoneNumber"]) && !Utility.IsValidPhoneNumber(model["CellPhoneNumber"]))
                    {
                        IsValid = false;
                        ModelState.AddModelError("CellPhoneNumber", "Phone Number is Invalid");
                    }
                    if (!string.IsNullOrEmpty(model["CellPhoneNumber"].ToString()) && userPhone.Count == 1)
                    {
                        IsValid = false;
                        ModelState.AddModelError("CellPhoneNumber", "Phone Number already Exist");
                    }
                    if (string.IsNullOrEmpty(model["HearAboutHU"].ToString()))
                    {
                        IsValid = false;
                        ModelState.AddModelError("HearAboutHU", "Hear About HU Field is required");
                    }
                    if (model["HearAboutHU"].ToString() == "Other" && string.IsNullOrEmpty(model["HearAboutHUOther"].ToString()))
                    {
                        IsValid = false;
                        ModelState.AddModelError("HearAboutHUOther", "Hear About HU Other Value is required");
                    }

                    if (IsValid)
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided data has been successfully validated.Details: Firstname: {model["FirstName"]}, LastName: {model["LastName"]}, Email: {model["EmailAddress"]}, CNIC: {model["CNIC"]}, PhoneNumber: {model["CellPhoneNumber"]} ");
                        var result = DB.WEB_CreateUser(
                            personalInfo.FirstName,
                            personalInfo.MiddleName,
                            personalInfo.LastName,
                            personalInfo.CNIC,
                            personalInfo.CellPhoneNumber,
                            personalInfo.EmailAddress,
                            HUCryptography.Crypto.Encrypt(model["Password"].ToString()), personalInfo.HearAboutHU, personalInfo.HearAboutHUOther).ToList().FirstOrDefault();
                        if (result.Response != 0)
                        {
                            var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == result.Response).FirstOrDefault();
                            Admin admin = new Admin();
                            admin.Id = result.Response;
                            admin.Name = personalInformation.FirstName + " " + personalInformation.LastName;
                            admin.Email = personalInformation.EmailAddress;
                            
                            Utility.AddLog(Constants.LogType.ActivityLog, "User account has been successfully created." + "UserID: "+ result.Response + "UserName" + personalInformation.FirstName + " " + personalInformation.LastName);
                            Utility.SetSession(admin);
                            return RedirectToAction("Index", "Student");
                        }
                        else
                        {
                           Utility.AddLog(Constants.LogType.ActivityLog, "User Registration Failed" + result.Reason.ToString());
                            ViewBag.Result = "Registration Failed," + result.Reason.ToString();
                        }
                    }
                    else
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided data is Invalid. Details: Firstname: {model["FirstName"]}, LastName: {model["LastName"]}, Email: {model["EmailAddress"]}, CNIC: {model["CNIC"]}, PhoneNumber: {model["CellPhoneNumber"]} ");
                        return View(personalInfo);
                    }
                    return View(personalInfo);
                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided data is Invalid. Details: Firstname: {model["FirstName"]}, LastName: {model["LastName"]}, Email: {model["EmailAddress"]}, CNIC: {model["CNIC"]}, PhoneNumber: {model["CellPhoneNumber"]} ");
                    ViewBag.Result = "Invalid Model";
                    return View(personalInfo);
                }
            }
            catch (System.Exception ex)
            {
                Utility.AddLog(Constants.LogType.ActivityLog, "Exception occurred during user registration." + ((ex.InnerException != null)? ex.InnerException.Message: ""));
                ViewBag.Result = "Registration Failed, Please try again later";
                return View(personalInfo);
            }
        }
        public ActionResult LogOut()
        {
            Session.Remove(Constants.Session.UserSession);
            Session.Remove(Constants.Session.EducationSession);
            Session.Remove(Constants.Session.DocumentSession);
            Session.Remove(Constants.Session.AdminSession);
            return RedirectToAction("Login","Account");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string email)
        {
            if (Utility.isValidEmail(email))
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"user requested to forgot password against Email Address: {email}");

                var personalInfo = DB.PersonalInformations.ToList();
                var IsRecordFound = personalInfo.Where(x => x.EmailAddress == email).FirstOrDefault();
                if (IsRecordFound != null)
                {
                    var password = HUCryptography.Crypto.Decrypt(IsRecordFound.Password);
                    var Name = IsRecordFound.FirstName + " " + IsRecordFound.LastName;
                    var EmailTemplate = DB.EmailTemplates.ToList().Where(x => x.Description == "Forgot Password").FirstOrDefault();
                    string EmailBody = EmailTemplate.Body;
                    EmailBody = EmailBody.Replace("{{Name}}", Name);
                    EmailBody = EmailBody.Replace("{{Password}}", password);

                    CPD.Framework.Core.EmailService.SendEmail(email, null, null, EmailTemplate.Subject, EmailBody,null,"tops@habib.edu.pk",null);
                    ViewBag.Result = "We have sent an email to the registered email address";
                    return View();

                }
                else {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"user record not found against Email Address: {email}");

                    ViewBag.Result = "Record not found against this email address";
                    return View();
                }
            }
            else
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"user provide invalid Email Address: {email}");

                ViewBag.Result = "Invalid Email Address";
                return View();
            }

            return View();
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