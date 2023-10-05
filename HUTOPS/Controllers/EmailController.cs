using HUTOPS.Helper;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class EmailController : Controller
    {
        // GET: Email
        HUTOPSEntities DB = new HUTOPSEntities();
        public ActionResult Index()
        {
            return View(DB.EmailTemplates.ToList());
        }
        [HttpPost]
        public ActionResult Save(EmailTemplate emailTemplate)
        {
            try
            {
                if(emailTemplate.Id != 0)
                {
                    if (string.IsNullOrEmpty(emailTemplate.Subject) || string.IsNullOrEmpty(emailTemplate.Body))
                    {
                        return Json(new { status = false, message = "Please Enter in all required fields" });
                    }

                    Utility.AddLog(Constants.LogType.ActivityLog, $"Admin Request to Update Email Template. Template Details: {JsonConvert.SerializeObject(emailTemplate)}");
                    var email = DB.EmailTemplates.Where(x => x.Id == emailTemplate.Id).FirstOrDefault();
                    email.Subject = emailTemplate.Subject;
                    email.Body = emailTemplate.Body;
                    email.UpdatedOn = DateTime.Now;
                    DB.SaveChanges();
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Email Template Updated Successfully. Template Details: {JsonConvert.SerializeObject(emailTemplate)}");

                    return Json(new { status = true, message = "Email Template Updated Successfully" });
                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Email Template not Found.");
                    return Json(new { status = false, message = "Email Template not Found" });
                }

            }
            catch (Exception ex)
            {
                Utility.AddLog(Constants.LogType.Exception, "Exception occurred during Saving Email Template." + ((ex.InnerException != null) ? ex.InnerException.Message : "") + "Model Details: " + JsonConvert.SerializeObject(emailTemplate));

                return Json(new { status = false, message = "Error occured while saving Email Template" });
            }
        }
    }
}