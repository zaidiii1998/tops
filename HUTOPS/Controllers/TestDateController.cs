using HUTOPS.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} Request to Update Test Date.");

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
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Test Date Updated Successfully by Admin ({Utility.GetAdminFromSession().Name}).");

                        return Json(new { status = true, message = "Test Date Updated Successfully" });
                    }

                    Utility.AddLog(Constants.LogType.ActivityLog, $"Test Date Updation Failed requested by Admin ({Utility.GetAdminFromSession().Name}).");
                    return Json(new { status = false, message = "Record not Found" });

                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} Request to Add new Test Date.");

                    // Add new record
                    testDate.CreatedBy = Utility.GetAdminFromSession().Name;
                    testDate.CreatedOn = DateTime.UtcNow + TimeSpan.FromHours(5);
                    DB.TestDates.Add(testDate);
                    DB.SaveChanges();

                    Utility.AddLog(Constants.LogType.ActivityLog, $"Test Date Inserted Successfully by Admin ({Utility.GetAdminFromSession().Name}).");

                    return Json(new { status = true, message = "Test Date Inserted Successfully" });
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Utility.AddLog(Constants.LogType.Exception, $"Error: {ve.PropertyName}, {ve.ErrorMessage} Error Occured while saving Test Date record by Admin ({Utility.GetAdminFromSession().Name}).");
                    }
                }
                return Json(new { status = false, message = "Error Occur while saving record " + ex.Message });
            }
            catch (Exception ex)
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"Error Occur while saving Test Date record by Admin ({Utility.GetAdminFromSession().Name}).");

                return Json(new { status = false, message = "Error Occur while saving record " + ex.Message });
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