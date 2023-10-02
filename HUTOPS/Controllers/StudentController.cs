using HUTOPS.Helper;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class StudentController : Controller
    {
        // GET: Student

        HUTOPSEntities DB = new HUTOPSEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Get(DTParameters param)
        {
            try
            {
                var HUTOPSId = Utility.GetParam(param, 1);
                var Name = Utility.GetParam(param, 2);
                var CNIC = Utility.GetParam(param, 3);
                var Phone = Utility.GetParam(param, 4);
                var Email = Utility.GetParam(param, 5);

                DTResult<PersonalInformation> Result = new DTResult<PersonalInformation>();
                var Response = DB.SP_GetStudents(param.Start, param.Length,HUTOPSId, Name, CNIC, Phone, Email);

                var Records = Response.ToList();
                var SingleRecord = Records.FirstOrDefault();
                int Total = 0;

                if (Records != null)
                {
                    foreach (var rec in Records.ToList())
                    {
                        Result.data.Add(new PersonalInformation()
                        {
                            Id = rec.Id,
                            HUTopId = rec.HUTopId,
                            FirstName = rec.FirstName,
                            LastName = rec.LastName,
                            CellPhoneNumber = rec.CellPhoneNumber,
                            EmailAddress = rec.EmailAddress,
                            CNIC = rec.CNIC,
                            IsAdmitCardGenerated = rec.IsAdmitCardGenerated,
                            AdmitCardGeneratedOn = rec.AdmitCardGeneratedOn,
                            IsAdmitCardSent = rec.IsAdmitCardSent,
                            AdmitCardSentOn = rec.AdmitCardSentOn
                        });
                    }
                    Total = Records.Count;
                }
                Result.draw = param.Draw;
                Result.recordsTotal = Total == 0 ? 0 : int.Parse(SingleRecord.Count.ToString());
                Result.recordsFiltered = Total == 0 ? 0 : int.Parse(SingleRecord.Count.ToString());
                return Json(Result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult UpdateSession(int? Id)
        {

            var currentSession = Utility.GetUserFromSession();
            if(currentSession.Id != 0)
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} has already open an applicant profile");

                return Json(new { status = false, message = "you have already open Student Profile. Kindly close the profile first" });
            }

            var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
            var education = DB.Educationals.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
            var document = DB.Documents.ToList().Where(x => x.UserId == personalInformation.Id).FirstOrDefault();

            Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} Request to get Student Access For Updation. Student Details: {JsonConvert.SerializeObject(personalInformation)}");


            Utility.SetSession(personalInformation == null ? new PersonalInformation() : personalInformation);
            Utility.SetSession(education == null ? new Educational() : education);
            Utility.SetSession(document == null ? new Document() : document);
            return Json(new {status = true, message = "Session Updated Successfully"});
        }
        public ActionResult CloseSession()
        {

            Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} Request to close Current Student profile.");

            Utility.SetSession( new PersonalInformation());
            Utility.SetSession( new Educational());
            Utility.SetSession( new Document());

            return Json(new { status = true, message = "Student Profile Closed Successfully" });
        }
    }
}