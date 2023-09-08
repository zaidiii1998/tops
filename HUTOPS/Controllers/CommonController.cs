using HUTOPS.Helper;
using Microsoft.Ajax.Utilities;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace HUTOPS.Controllers
{
    public class CommonController : Controller
    {
        HU_TOPSEntities DB = new HU_TOPSEntities();
        // GET: Common
        public ActionResult CheckPhoneNumber(string number)
        {
            try
            {
                var result = DB.PersonalInformations.ToList();
                var res = result.Where(x => x.CellPhoneNumber == number).ToList();
                if (res.Count == 0)
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
                var result = DB.PersonalInformations.ToList();
                var res = result.Where(x => x.EmailAddress == email).ToList();
                if (res.Count == 0)
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
                var result = DB.PersonalInformations.ToList();
                var res = result.Where(x => x.CNIC == cnic).ToList();
                if (res.Count == 0)
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
                var result = DB.WEB_CheckPersonalInfo(int.Parse(Helper.Helper.GetSession(Constants.Session.UserId))).ToList().FirstOrDefault();
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
    }
}