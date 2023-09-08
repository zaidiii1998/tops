using HUTOPS.Helper;
using System.Linq;
using System.Web.Mvc;
using HUTOPS.Models;
using Newtonsoft.Json;

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
                int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                var user = DB.PersonalInformations.ToList();
                var LoginUser = user.Where(x => x.Id == userId).FirstOrDefault();
                var country = DB.Countries.ToList();
                var province = DB.States.ToList();
                var city = DB.Cities.ToList();
                var pageModel = new PersonalInfoPageModel()
                {
                    Main = LoginUser,
                    Country = country,
                    Province = province,
                    City = city
                };
                if (TempData["Result"] != null)
                {
                    ViewBag.Result = TempData["Result"].ToString();
                }
                //ViewBag.Country = Helper.Extensions.ConvertToSelect(country, item => item.Id, item => item.Name);
                //ViewBag.Province = Helper.Extensions.ConvertToSelect(province, item => item.Id, item => item.Name);
                //ViewBag.City = Helper.Extensions.ConvertToSelect(city, item => item.Id, item => item.Name);

                return View(pageModel);
            }
            catch (System.Exception)
            {
                return View();
            }
            
        }
        public ActionResult Save(PersonalInformation model)
        {
            try
            {
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User-requested to Update Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                DB.WEB_UpdatePersonalInfo(
                    model.Id,
                    Helper.Helper.ToCamelCase(model.FirstName),
                    Helper.Helper.ToCamelCase(model.MiddleName),
                    Helper.Helper.ToCamelCase(model.LastName),
                    Helper.Helper.ToCamelCase(model.FatherFirstName),
                    Helper.Helper.ToCamelCase(model.FatherMiddleName),
                    Helper.Helper.ToCamelCase(model.FatherLastName),
                    model.Gender.ToString(),
                    Helper.Helper.ToCamelCase(model.HusbandName),
                    model.DateOfBirth.ToString(),
                    model.CNIC,
                    model.EmailAddress,
                    model.AlterEmailAddress
                    );
                DB.WEB_UpdateContactDetails(
                    model.Id,
                    model.CellPhoneNumber,
                    model.WhatsAppNumber,
                    model.AlternateCellPhoneNumber,
                    model.HomePhoneNumber,
                    model.AlternateLandline,
                    model.GuardianCellPhoneNumber,
                    model.GuardianEmailAddress
                    );
                DB.WEB_UpdateAddressDetails(
                    model.Id,
                    model.ResidentialAddress,
                    model.ResidentialCountry == null? null : DB.Countries.ToList().Where(x => x.Id == int.Parse(model.ResidentialCountry)).ToList().FirstOrDefault().Name,
                    model.ResidentialProvince == null? null : DB.States.ToList().Where(x => x.Id == int.Parse(model.ResidentialProvince)).ToList().FirstOrDefault().Name,
                    model.ResidentialCity == null? null: model.ResidentialCity == "other"? "Other" : DB.Cities.ToList().Where(x => x.Id == int.Parse(model.ResidentialCity)).ToList().FirstOrDefault().Name,
                    model.ResidentialCityOther,
                    model.ResidentialPostalCode,
                    model.PermanentAddress,
                    model.PermanentCountry == null? null :DB.Countries.ToList().Where(x => x.Id == int.Parse(model.PermanentCountry)).ToList().FirstOrDefault().Name,
                    model.PermanentProvince == null? null : DB.States.ToList().Where(x => x.Id == int.Parse(model.PermanentProvince)).ToList().FirstOrDefault().Name,
                    model.PermanentCity == null ? null : model.PermanentCity == "other" ? "Other" : DB.Cities.ToList().Where(x => x.Id == int.Parse(model.PermanentCity)).ToList().FirstOrDefault().Name,
                    model.PermanentCityOther,
                    model.PermanentPostalCode

                    );
                Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User has Successfully Updated Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                return Json(new { status = true, message = "Personal Information Successfully Updated" });
            }
            catch (System.Exception ex)
            {
                Helper.Helper.AddLog(Constants.LogType.Exception, "Exception occurred during Save personal Information." + ((ex.InnerException != null) ? ex.InnerException.Message : "") + "Model Details: " + JsonConvert.SerializeObject(model));
                return Json(new { status = false, message = "Failed to save Record, Please try again" });
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(PersonalInformation model)
        {
            try
            {
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User-requested to Submit Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                var IsValid = true;
                if (string.IsNullOrEmpty(model.FirstName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FirstName", "First Name is required");
                }
                if (!string.IsNullOrEmpty(model.FirstName) && (model.FirstName.Length < 3 || model.FirstName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FirstName", "First Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.LastName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.LastName", "Last Name is required");
                }
                if (!string.IsNullOrEmpty(model.LastName) && (model.LastName.Length < 3 || model.LastName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.LastName", "Last Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.FatherFirstName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherFirstName", "Father First Name is required");
                }
                if (!string.IsNullOrEmpty(model.FatherFirstName) && (model.FatherFirstName.Length < 3 || model.FatherFirstName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherFirstName", "Father First Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.FatherLastName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherLastName", "Father Last Name is required");
                }
                if (!string.IsNullOrEmpty(model.FatherLastName) && (model.FatherLastName.Length < 3 || model.FatherLastName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherLastName", "Father Last Name length must be greater than 3 and less than 25 characters");
                }
                if (!string.IsNullOrEmpty(model.AlterEmailAddress) && !Helper.Helper.isValidEmail(model.AlterEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.AlterEmailAddress", "Provided Email Address is Invalid");
                }
                if (string.IsNullOrEmpty(model.GuardianEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianEmailAddress", "Guardian Email Address is Required");
                }
                if (!string.IsNullOrEmpty(model.GuardianEmailAddress) && !Helper.Helper.isValidEmail(model.GuardianEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianEmailAddress", "Provided Email Address is Invalid");
                }
                if (!string.IsNullOrEmpty(model.CellPhoneNumber) && !Helper.Helper.IsValidPhoneNumber(model.CellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.CellPhoneNumber", "Cell Phone Number is Invalid");
                }
                if (!string.IsNullOrEmpty(model.WhatsAppNumber) && !Helper.Helper.IsValidPhoneNumber(model.WhatsAppNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.WhatsAppNumber", "WhatsApp Number Number is Invalid");
                }
                if (!string.IsNullOrEmpty(model.AlternateCellPhoneNumber) && !Helper.Helper.IsValidPhoneNumber(model.AlternateCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.AlternateCellPhoneNumber", "Alternate Cell Phone Number is Invalid");
                }
                if (!string.IsNullOrEmpty(model.GuardianCellPhoneNumber) && !Helper.Helper.IsValidPhoneNumber(model.GuardianCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianCellPhoneNumber", "Guardian Cell Phone Number is Invalid");
                }
                if (string.IsNullOrEmpty(model.GuardianCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianCellPhoneNumber", "Phone Number is Required");
                }
                if (string.IsNullOrEmpty(model.HomePhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.HomePhoneNumber", "Phone Number is Required");
                }
                if (IsValid)
                {
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User provided data is Valid for Submit Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                    DB.WEB_UpdatePersonal(
                        model.Id,
                        Helper.Helper.ToCamelCase(model.FirstName),
                        Helper.Helper.ToCamelCase(model.MiddleName),
                        Helper.Helper.ToCamelCase(model.LastName),
                        Helper.Helper.ToCamelCase(model.FatherFirstName),
                        Helper.Helper.ToCamelCase(model.FatherMiddleName),
                        Helper.Helper.ToCamelCase(model.FatherLastName),
                        model.Gender.ToString(),
                        Helper.Helper.ToCamelCase(model.HusbandName),
                        model.DateOfBirth.ToString(),
                        model.CNIC,
                        model.EmailAddress,
                        model.AlterEmailAddress,
                        // Contact
                        model.CellPhoneNumber,
                        model.WhatsAppNumber,
                        model.AlternateCellPhoneNumber,
                        model.HomePhoneNumber,
                        model.AlternateLandline,
                        model.GuardianCellPhoneNumber,
                        model.GuardianEmailAddress,
                        // Address
                        model.ResidentialAddress,
                        model.ResidentialCountry == null ? null : DB.Countries.ToList().Where(x => x.Id == int.Parse(model.ResidentialCountry)).ToList().FirstOrDefault().Name,
                        model.ResidentialProvince == null ? null : DB.States.ToList().Where(x => x.Id == int.Parse(model.ResidentialProvince)).ToList().FirstOrDefault().Name,
                        model.ResidentialCity == null ? null : model.ResidentialCity == "other" ? "Other" : DB.Cities.ToList().Where(x => x.Id == int.Parse(model.ResidentialCity)).ToList().FirstOrDefault().Name,
                        model.ResidentialCityOther,
                        model.ResidentialPostalCode,
                        model.PermanentAddress,
                        model.PermanentCountry == null ? null : DB.Countries.ToList().Where(x => x.Id == int.Parse(model.PermanentCountry)).ToList().FirstOrDefault().Name,
                        model.PermanentProvince == null ? null : DB.States.ToList().Where(x => x.Id == int.Parse(model.PermanentProvince)).ToList().FirstOrDefault().Name,
                        model.PermanentCity == null ? null : model.PermanentCity == "other" ? "Other" : DB.Cities.ToList().Where(x => x.Id == int.Parse(model.PermanentCity)).ToList().FirstOrDefault().Name,
                        model.PermanentCityOther,
                        model.PermanentPostalCode
                        );

                    TempData["Result"] = "Personal Inofmation Submited Successfully";
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User Submited Personal Information Successfully. Details: {JsonConvert.SerializeObject(model)}");
                    return RedirectToAction("Index", "Education");
                }
                else
                {
                    var country = DB.Countries.ToList();
                    var province = DB.States.ToList();
                    var city = DB.Cities.ToList();
                    var pageModel = new PersonalInfoPageModel()
                    {
                        Main = model,
                        Country = country,
                        Province = province,
                        City = city
                    };
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User provided data is InValid for Submit Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                    return View(pageModel);
                }
            }
            catch (System.Exception ex)
            {
                Helper.Helper.AddLog(Constants.LogType.Exception, "Exception occurred during Submiting personal Information." + ((ex.InnerException != null) ? ex.InnerException.Message : "")+"Model Details: " + JsonConvert.SerializeObject(model));
                TempData["Result"] = "Error occured while submiting your form";
                return RedirectToAction("Index", "Home");

            }
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