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
        public ActionResult GetGroupList(int BoardId)
        {
            return Json(DB.BoardGroups.ToList().Where(x => x.BoardId == BoardId).ToList());
        }
        public ActionResult GetSubjectList(int GroupId)
        {
            return Json(DB.GroupSubjects.ToList().Where(x => x.GroupId == GroupId).ToList());
        }
    }
}