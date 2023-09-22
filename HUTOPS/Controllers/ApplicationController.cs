using HUTOPS.Models;
using System.Web.Mvc;
using HUTOPS.Helper;
using System.Linq;

namespace HUTOPS.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: Application

        HUTOPSEntities DB = new HUTOPSEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Submit(ApplicationModel applicationModel)
        {
            try
            {
                var PersonalInfoErrors = Utility.ValidatePersonalInfo(applicationModel.PersonalInfo);
                var EducationError = Utility.ValidateEducation(applicationModel.Education);
                if (PersonalInfoErrors.Count > 0 || EducationError.Count > 0)
                {
                    return Json(new { status = false, message = "Error Occured while validating your data", PersonalErrors = PersonalInfoErrors, EducationErrors = EducationError });
                }
                else
                {
                    // Insertion INTO DB
                    var SubNames = Utility.ConvertArrayToCSV(applicationModel.SubjectName);
                    var SubObtain = Utility.ConvertArrayToCSV(applicationModel.SubjectObtain);
                    var result =
                    DB.SP_InsertApplication(
                            applicationModel.PersonalInfo.FirstName = Utility.ToCamelCase(applicationModel.PersonalInfo.FirstName),
                            applicationModel.PersonalInfo.MiddleName = Utility.ToCamelCase(applicationModel.PersonalInfo.MiddleName),
                            applicationModel.PersonalInfo.LastName = Utility.ToCamelCase(applicationModel.PersonalInfo.LastName),
                            applicationModel.PersonalInfo.FatherFirstName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherFirstName),
                            applicationModel.PersonalInfo.FatherMiddleName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherMiddleName),
                            applicationModel.PersonalInfo.FatherLastName = Utility.ToCamelCase(applicationModel.PersonalInfo.FatherLastName),
                            applicationModel.PersonalInfo.Gender.ToString(),
                            applicationModel.PersonalInfo.HusbandName,
                            applicationModel.PersonalInfo.DateOfBirth.ToString(),
                            applicationModel.PersonalInfo.CNIC,
                            applicationModel.PersonalInfo.EmailAddress,
                            applicationModel.PersonalInfo.AlterEmailAddress,
                            // Contact
                            applicationModel.PersonalInfo.CellPhoneNumber,
                            applicationModel.PersonalInfo.WhatsAppNumber,
                            applicationModel.PersonalInfo.AlternateCellPhoneNumber,
                            applicationModel.PersonalInfo.HomePhoneNumber,
                            applicationModel.PersonalInfo.AlternateLandline,
                            applicationModel.PersonalInfo.GuardianCellPhoneNumber,
                            applicationModel.PersonalInfo.GuardianEmailAddress,
                            // Address
                            applicationModel.PersonalInfo.ResidentialAddress,
                            applicationModel.PersonalInfo.ResidentialCountry,
                            applicationModel.PersonalInfo.ResidentialProvince,
                            applicationModel.PersonalInfo.ResidentialCity,
                            applicationModel.PersonalInfo.ResidentialCityOther,
                            applicationModel.PersonalInfo.ResidentialPostalCode,
                            applicationModel.PersonalInfo.PermanentAddress,
                            applicationModel.PersonalInfo.PermanentCountry,
                            applicationModel.PersonalInfo.PermanentProvince,
                            applicationModel.PersonalInfo.PermanentCity,
                            applicationModel.PersonalInfo.PermanentCityOther,
                            applicationModel.PersonalInfo.PermanentPostalCode,
                            applicationModel.PersonalInfo.HearAboutHU,
                            applicationModel.PersonalInfo.HearAboutHUOther,

                            // Education Info
                            applicationModel.Education.CurrentLevelOfEdu,
                            applicationModel.Education.HSSCSchoolName,
                            applicationModel.Education.HSSCSchoolAddress,
                            applicationModel.Education.HSSCStartDate.ToString(),
                            applicationModel.Education.HSSCCompletionDate.ToString(),
                            applicationModel.Education.HSSCPercentage,
                            applicationModel.Education.HSSCBoardId,
                            applicationModel.Education.HSSCBoardName,
                            applicationModel.Education.HSSCGroupId,
                            applicationModel.Education.HSSCGroupName,
                            applicationModel.Education.SSCSchoolName,
                            applicationModel.Education.SSCSchoolAddress,
                            applicationModel.Education.SSCPercentage,
                            applicationModel.Education.UniversityName,
                            applicationModel.Education.IntendedProgram,
                            applicationModel.Education.HUSchoolName,
                            SubNames + ',',
                            SubObtain + ','
                        ).FirstOrDefault();
                    if (result.Response != 0)
                    {
                        return Json(new { status = true, message = "Successfully Submitted", userId = result.Response });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Form Submittion Failed" + result.Reason.ToString() });
                    }

                }
            }
            catch (System.Exception ex)
            {

                return Json(new { status = false, message = "Form Submittion Failed" + ex.Message });

            }


        }
    }
}