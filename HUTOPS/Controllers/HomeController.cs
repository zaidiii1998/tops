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
                int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                var user = DB.PersonalInformations.ToList();
                var LoginUser = user.Where(x => x.Id == userId).FirstOrDefault();
                if (TempData["Result"] != null )
                {
                    ViewBag.Result = TempData["Result"].ToString();
                }
                return View(LoginUser);
            }
            catch (System.Exception)
            {
                return View();
            }
            
        }

        public ActionResult UpdatePersonalInfo(PersonalInformation model)
        {
            try
            {
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
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

                return Json(new { status = true, message = "Personal Information Successfully Updated" });
            }
            catch (System.Exception)
            {

                return Json(new { status = false, message = "Failed to save Record, Please try again" });
            }
        }
        public ActionResult UpdateContactDetails(PersonalInformation model)
        {
            try
            {
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
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

                return Json(new { status = true, message = "Contact Details Successfully Updated" });
            }
            catch (System.Exception)
            {

                return Json(new { status = false, message = "Failed to save Record, Please try again" });
            }
        }
        public ActionResult UpdateAddressDetails(PersonalInformation model)
        {
            try
            {
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                DB.WEB_UpdateAddressDetails(
                    model.Id,
                    model.ResidentialAddress,
                    model.ResidentialCountry,
                    model.ResidentialProvince,
                    model.ResidentialCity,
                    model.ResidentialCityOther,
                    model.ResidentialPostalCode.ToString(),
                    model.PermanentAddress,
                    model.PermanentCountry,
                    model.PermanentProvince,
                    model.PermanentCity,
                    model.PermanentCityOther,
                    model.PermanentPostalCode.ToString()

                    );

                return Json(new { status = true, message = "Address details Successfully Updated" });
            }
            catch (System.Exception)
            {

                return Json(new { status = false, message = "Failed to save Record, Please try again" });
            }
        }

        public ActionResult Save(PersonalInformation model)
        {
            try
            {
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
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
                    model.ResidentialCountry,
                    model.ResidentialProvince,
                    model.ResidentialCity,
                    model.ResidentialCityOther,
                    model.ResidentialPostalCode.ToString(),
                    model.PermanentAddress,
                    model.PermanentCountry,
                    model.PermanentProvince,
                    model.PermanentCity,
                    model.PermanentCityOther,
                    model.PermanentPostalCode.ToString()

                    );

                return Json(new { status = true, message = "Personal Information Successfully Updated" });
            }
            catch (System.Exception)
            {

                return Json(new { status = false, message = "Failed to save Record, Please try again" });
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(PersonalInformation model)
        {
            try
            {
                var IsValid = true;
                var errorMsg = "";
                if (string.IsNullOrEmpty(model.FirstName))
                {
                    IsValid = false;
                    ModelState.AddModelError("FirstName", "First Name is required");
                }
                if (!string.IsNullOrEmpty(model.FirstName) && (model.FirstName.Length < 3 || model.FirstName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("FirstName", "First Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.LastName))
                {
                    IsValid = false;
                    ModelState.AddModelError("LastName", "Last Name is required");
                }
                if (!string.IsNullOrEmpty(model.LastName) && (model.LastName.Length < 3 || model.LastName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("LastName", "Last Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.FatherFirstName))
                {
                    IsValid = false;
                    ModelState.AddModelError("FatherFirstName", "Father First Name is required");
                }
                if (!string.IsNullOrEmpty(model.FatherFirstName) && (model.FatherFirstName.Length < 3 || model.FatherFirstName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("FatherFirstName", "Father First Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.FatherLastName))
                {
                    IsValid = false;
                    ModelState.AddModelError("FatherLastName", "Father Last Name is required");
                }
                if (!string.IsNullOrEmpty(model.FatherLastName) && (model.FatherLastName.Length < 3 || model.FatherLastName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("FatherLastName", "Father Last Name length must be greater than 3 and less than 25 characters");

                }
                if (IsValid) { 
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
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
                    model.ResidentialCountry,
                    model.ResidentialProvince,
                    model.ResidentialCity,
                    model.ResidentialCityOther,
                    model.ResidentialPostalCode.ToString(),
                    model.PermanentAddress,
                    model.PermanentCountry,
                    model.PermanentProvince,
                    model.PermanentCity,
                    model.PermanentCityOther,
                    model.PermanentPostalCode.ToString()
                    );
                
                    TempData["Result"] = "Personal Inofmation Submited Successfully";
                    return RedirectToAction("Index", "Education");
                }
                else
                {
                    int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                    var user = DB.PersonalInformations.ToList();
                    var LoginUser = user.Where(x => x.Id == userId).FirstOrDefault();
                    return View(LoginUser);
                }
            }
            catch (System.Exception)
            {
                TempData["Result"] = "Error occured while submiting your form";
                return RedirectToAction("Index", "Home");

            }
        }
    }
}