using HUTOPS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class TestDateController : Controller
    {
        // GET: TestDate

        HUTOPSEntities DB = new HUTOPSEntities();
        public ActionResult Index()
        {
            var Records = DB.TestDates.ToList();
            return View(Records);
        }
        [HttpPost]
        public ActionResult Submit(TestDate testDate)
        {
            try
            {
                if (testDate.Id != 0)
                {
                    // Update Record
                    var date = DB.TestDates.ToList().Where(x => x.Id == testDate.Id).FirstOrDefault();
                    if (date != null)
                    {
                        date.Value = testDate.Value;
                        date.Text = testDate.Text;
                        date.AdmissionSession = testDate.AdmissionSession;
                        date.DeadlineDate = testDate.DeadlineDate;
                        date.Visibility = testDate.Visibility;
                        DB.SaveChanges();
                        return Json(new { status = true, message = "Test Date Updated Successfully" });
                    }
                    return Json(new { status = false, message = "Record not Found" });
                }
                else
                {
                    // Add new record
                    testDate.CreatedBy = Utility.GetAdminFromSession().Name;
                    testDate.CreatedOn = DateTime.Now;
                    DB.TestDates.Add(testDate);
                    DB.SaveChanges();
                    return Json(new { status = true, message = "Test Date Inserted Successfully" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Error Occur while saving record " + ex.Message });
            }
            
        }
    }
}