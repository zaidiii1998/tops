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
            try
            {
                model.Id = int.Parse(Session["UserId"].ToString());
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
                model.Id = int.Parse(Session["UserId"].ToString());
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
                model.Id = int.Parse(Session["UserId"].ToString());
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
    }
}