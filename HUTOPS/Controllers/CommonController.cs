using HUTOPS.Helper;
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
                var personalInfo = Utility.GetUserFromSession();
                var PersonalInformation = DB.PersonalInformations.ToList();

                var res = PersonalInformation.Exists(x => x.CellPhoneNumber == number && x.Id != personalInfo.Id);
                if (!res)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false, message = "Phone Number Is already exist" });
                }
            }
            catch (System.Exception)
            {
                return Json(new { status = false, message = "Phone Number Is already exist" });
            }
        }
        public ActionResult CheckEmail(string email)
        {
            try
            {
                var personalInfo = Utility.GetUserFromSession();
                var PersonalInformation = DB.PersonalInformations.ToList();
                var res = PersonalInformation.Exists(x => x.EmailAddress == email && x.Id != personalInfo.Id);
                if (!res)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false, message = "Email Is already exist" });
                }
            }
            catch (System.Exception)
            {
                return Json(new { status = false, message = "Email Is already exist" });
            }
        }
        public ActionResult CheckCNIC(string cnic)
        {
            try
            {
                var personalInfo = Utility.GetUserFromSession();
                var PersonalInformation = DB.PersonalInformations.ToList();
                var res = PersonalInformation.Exists(x => x.CNIC == cnic && x.Id != personalInfo.Id);
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

        public ActionResult GetBoardList()
        {
            return Json(DB.Boards.ToList());
        }
        public ActionResult GetGroupList(string BoardId)
        {
            return Json(DB.BoardGroups.ToList().Where(x => x.BoardId == (BoardId == ""? 0 : int.Parse(BoardId))).ToList());
        }
        public ActionResult GetSubjectList(string GroupId)
        {
            return Json(DB.GroupSubjects.ToList().Where(x => x.GroupId == (GroupId == ""? 0 : int.Parse(GroupId))).ToList());
        }
        
        public ActionResult CheckPersonalInfo()
        {
            try
            {
                var personalInfo = Utility.GetUserFromSession();
                var result = DB.WEB_CheckPersonalInfo(personalInfo.Id).ToList().FirstOrDefault();
                if (result.Response == 1)
                {
                    return Json(new { status = true, message = result.Reason });
                }
                else
                {
                    return Json(new { status = false, message = result.Reason });
                }
            }
            catch (System.Exception)
            {

                throw;
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