using HUTOPS.Helper;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    public class CommonController : Controller
    {

        //HU_TOPSEntities DB = new HU_TOPSEntities(); // Local System DB
        HUTOPSEntities DB = new HUTOPSEntities(); // Server DB

        // GET: Common
        public ActionResult CheckPhoneNumber(string number)
        {
            try
            {
                var currentAdmissionSession = ConfigurationManager.AppSettings["CurrentAdmissionSession"].ToString();

                var personalInfo = Utility.GetUserFromSession();
                var PersonalInformation = DB.PersonalInformations.Where(x => x.Id > 2653).ToList();

                var res = PersonalInformation.Exists(x => x.CellPhoneNumber == number && x.Id != personalInfo.Id && x.HUTopId.StartsWith("HUTOPS" + currentAdmissionSession, StringComparison.OrdinalIgnoreCase));
                if (!res)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false, message = "This number is already in our records" });
                }
            }
            catch (System.Exception)
            {
                return Json(new { status = false, message = "This number is already in our records" });
            }
        }
        public ActionResult CheckEmail(string email)
        {
            try
            {
                var currentAdmissionSession = ConfigurationManager.AppSettings["CurrentAdmissionSession"].ToString();

                var personalInfo = Utility.GetUserFromSession();
                var PersonalInformation = DB.PersonalInformations.Where(x => x.Id > 2653).ToList();
                var res = PersonalInformation.Exists(x => x.EmailAddress == email && x.Id != personalInfo.Id && x.HUTopId.StartsWith("HUTOPS" + currentAdmissionSession, StringComparison.OrdinalIgnoreCase));
                if (!res)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false, message = "This email address is already in our records" });
                }
            }
            catch (System.Exception)
            {
                return Json(new { status = false, message = "This email address is already in our records" });
            }
        }
        public ActionResult CheckCNIC(string cnic)
        {
            try
            {
                var currentAdmissionSession = ConfigurationManager.AppSettings["CurrentAdmissionSession"].ToString();

                var personalInfo = Utility.GetUserFromSession();
                var PersonalInformation = DB.PersonalInformations.Where(x => x.Id > 2653).ToList();
                var res = PersonalInformation.Exists(x => x.CNIC == cnic && x.Id != personalInfo.Id && x.HUTopId.StartsWith("HUTOPS" + currentAdmissionSession, StringComparison.OrdinalIgnoreCase));
                if (!res)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false, message = "CNIC Is already exist" });
                }
            }
            catch (System.Exception)
            {
                return Json(new { status = false, message = "CNIC Is already exist" });
            }
        }


        public ActionResult GetCountry()
        {
            var result = DB.Countries.ToList();
            return Json(result);
        }
        public ActionResult GetProvince(string CountryId)
        {
            var result = DB.States.ToList().FindAll(x => x.CountryId == (CountryId == ""? 0 : int.Parse(CountryId)));
            return Json(result);
        }
        public ActionResult GetCities(string ProvinceId)
        {
            var result = DB.Cities.ToList().FindAll(x => x.StateId == (ProvinceId == "" ? 0 : int.Parse(ProvinceId)));
            return Json(result);
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