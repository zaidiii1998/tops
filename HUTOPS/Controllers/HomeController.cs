using HUTOPS.Helper;
using System.Linq;
using System.Web.Mvc;
using HUTOPS.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.EnterpriseServices.Internal;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class HomeController : Controller
    {
        //HU_TOPSEntities DB = new HU_TOPSEntities(); // Local System DB
        HUTOPSEntities DB = new HUTOPSEntities(); // Server DB
        public ActionResult Index()
        {
            try
            {
                var LoginUser = Utility.GetUserFromSession();
                var country = DB.Countries.ToList();
                var province = DB.States.ToList();
                var city = DB.Cities.ToList();
                var pageModel = new PersonalInfoPageModel()
                {
                    Main = LoginUser,
                    Country = country,
                    Province = province,
                    City = city
                };
                if (TempData["Result"] != null)
                {
                    ViewBag.Result = TempData["Result"].ToString();
                }
                //ViewBag.Country = Helper.Extensions.ConvertToSelect(country, item => item.Id, item => item.Name);
                //ViewBag.Province = Helper.Extensions.ConvertToSelect(province, item => item.Id, item => item.Name);
                //ViewBag.City = Helper.Extensions.ConvertToSelect(city, item => item.Id, item => item.Name);

                return View(pageModel);
            }
            catch (System.Exception)
            {
                return View();
            }
            
        }
        public ActionResult Activity()
        {
            try
            {
                var personalInfo = Utility.GetUserFromSession();
                List<Activity> activities = DB.Activities.ToList().Where(x => x.UserId == personalInfo.Id).ToList();
                ViewBag.Declaration = personalInfo.Declaration;
                return View(activities);
            }
            catch (System.Exception)
            {
                return View();
            }
        }
        public ActionResult SubmitActivity(string[] ActivityName, string[] ActivityDuration)
        {
            try
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"User request to Update Activities ");
                var personalInfo = Utility.GetUserFromSession();

                if (personalInfo.Declaration == 1)
                {
                    return Json(new { status = false, message = "You have already submited your application" });
                }
                List<Activity> activities = DB.Activities.ToList().Where(x => x.UserId == personalInfo.Id).ToList();
                
                ActivityName = ActivityName[0].Split(',');
                ActivityDuration = ActivityDuration[0].Split(',');
                if (ActivityName != null || ActivityName.Length != 0)
                {
                    foreach (Activity activity in activities)
                    {
                        DB.Activities.Remove(activity);
                        DB.SaveChanges();
                    }
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User Previous Activities has been removed");

                    for (var i = 0; i < ActivityName.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(ActivityName[i])) { 
                            DB.Activities.Add(new Activity
                            {
                                UserId = personalInfo.Id,
                                Name = ActivityName[i],
                                Duration = ActivityDuration[i],
                                CreatedDatetime = DateTime.Now
                            });
                            DB.SaveChanges();
                        }
                    }
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User Activities has been updated successfully");

                    return Json(new { status = true, message = "Activities Updated Successfully" });
                }
                else {
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User Activities updation Failed");
                    return Json(new { status = false, message = "Activities are not provided" });
                }

            }
            catch (System.Exception ex)
            {
                Helper.Utility.AddLog(Constants.LogType.Exception, "Exception occurred during Submiting User Activities." + ((ex.InnerException != null) ? ex.InnerException.Message : ""));                
                    return Json(new { status = false, message = "Activities Updation Failed" });
            }

        }
        public ActionResult TestDate()
        {
            try
            {
                return View(Utility.GetUserFromSession());
            }
            catch (System.Exception)
            {
                return View();
            }
        }
        public ActionResult SubmitTestDate(string Date)
        {
            try
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"User request to update test date");

                var user = Utility.GetUserFromSession();
                if (user.Declaration == 1)
                {
                    return Json(new { status = false, message = "You have already submited your application" });
                }

                if (!string.IsNullOrEmpty(Date))
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"User provided Date is Verified");
                    user.TestDate = Date;
                    DB.SaveChanges();
                    Utility.SetSession(user);
                    return Json(new { status = true, message = "Test Date Updated Successfully" });
                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"User provided Date is NULL or empty string");
                    return Json(new { status = false, message = "Please select Test Date " });
                }
                
            }
            catch (System.Exception ex)
            {
                Helper.Utility.AddLog(Constants.LogType.Exception, "Exception occurred while submiting Test Date." + ((ex.InnerException != null) ? ex.InnerException.Message : ""));            

                return Json(new { status = false, message = "Test Date Updation Failed" });
            }
        }
        public ActionResult Save(PersonalInformation model)
        {
            try
            {
                var personalInfo = Utility.GetUserFromSession();
                model.Id = personalInfo.Id;
                model.IsCompleted = 0;
                Utility.AddLog(Constants.LogType.ActivityLog, $"User-requested to Update Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                DB.WEB_UpdatePersonal(
                        model.Id,
                        model.FirstName,
                        model.MiddleName,
                        model.LastName,
                        model.FatherFirstName,
                        model.FatherMiddleName,
                        model.FatherLastName,
                        model.Gender == null? null : model.Gender.ToString(),
                        model.HusbandName,
                        model.DateOfBirth == null ? null : model.DateOfBirth.ToString(),
                        model.CNIC,
                        model.EmailAddress,
                        model.AlterEmailAddress,
                        // Contact
                        model.CellPhoneNumber,
                        model.WhatsAppNumber,
                        model.AlternateCellPhoneNumber,
                        model.HomePhoneNumber,
                        model.AlternateLandline,
                        model.GuardianCellPhoneNumber,
                        model.GuardianEmailAddress,
                        // Address
                        model.ResidentialAddress,
                        model.ResidentialCountry,
                        model.ResidentialProvince,
                        model.ResidentialCity,
                        model.ResidentialCityOther,
                        model.ResidentialPostalCode,
                        model.PermanentAddress,
                        model.PermanentCountry,
                        model.PermanentProvince,
                        model.PermanentCity,
                        model.PermanentCityOther,
                        model.PermanentPostalCode,
                        model.IsCompleted
                        );
                Utility.SetSession(model);
                Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User has Successfully Updated Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                return Json(new { status = true, message = "Personal Information Successfully Updated" });
            }
            catch (System.Exception ex)
            {
                Helper.Utility.AddLog(Constants.LogType.Exception, "Exception occurred during Save personal Information." + ((ex.InnerException != null) ? ex.InnerException.Message : "") + "Model Details: " + JsonConvert.SerializeObject(model));
                return Json(new { status = false, message = "Failed to save Record, Please try again" });
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(PersonalInformation personalInfo)
        {
            try
            {
                var user = Utility.GetUserFromSession();
                if (user.Declaration == 1)
                {
                    return Json(new { status = false, message = "You have already submited your application" });
                }
                personalInfo.Id = user.Id;
                personalInfo.IsCompleted = 1;
                Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User-requested to Submit Personal Information. Details: {JsonConvert.SerializeObject(personalInfo)}");
                var IsValid = true;
                if (string.IsNullOrEmpty(personalInfo.FirstName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FirstName", "First Name is required");
                }
                if (!string.IsNullOrEmpty(personalInfo.FirstName) && (personalInfo.FirstName.Length < 3 || personalInfo.FirstName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FirstName", "First Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(personalInfo.LastName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.LastName", "Last Name is required");
                }
                if (!string.IsNullOrEmpty(personalInfo.LastName) && (personalInfo.LastName.Length < 3 || personalInfo.LastName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.LastName", "Last Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(personalInfo.FatherFirstName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherFirstName", "Father First Name is required");
                }
                if (!string.IsNullOrEmpty(personalInfo.FatherFirstName) && (personalInfo.FatherFirstName.Length < 3 || personalInfo.FatherFirstName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherFirstName", "Father First Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(personalInfo.FatherLastName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherLastName", "Father Last Name is required");
                }
                if (!string.IsNullOrEmpty(personalInfo.FatherLastName) && (personalInfo.FatherLastName.Length < 3 || personalInfo.FatherLastName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherLastName", "Father Last Name length must be greater than 3 and less than 25 characters");
                }
                if (!string.IsNullOrEmpty(personalInfo.AlterEmailAddress) && !Helper.Utility.isValidEmail(personalInfo.AlterEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.AlterEmailAddress", "Provided Email Address is Invalid");
                }
                if (string.IsNullOrEmpty(personalInfo.GuardianEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianEmailAddress", "Guardian Email Address is Required");
                }
                if (!string.IsNullOrEmpty(personalInfo.GuardianEmailAddress) && !Helper.Utility.isValidEmail(personalInfo.GuardianEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianEmailAddress", "Provided Email Address is Invalid");
                }
                if (!string.IsNullOrEmpty(personalInfo.CellPhoneNumber) && !Helper.Utility.IsValidPhoneNumber(personalInfo.CellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.CellPhoneNumber", "Cell Phone Number is Invalid");
                }
                if (!string.IsNullOrEmpty(personalInfo.WhatsAppNumber) && !Helper.Utility.IsValidPhoneNumber(personalInfo.WhatsAppNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.WhatsAppNumber", "WhatsApp Number Number is Invalid");
                }
                if (!string.IsNullOrEmpty(personalInfo.AlternateCellPhoneNumber) && !Helper.Utility.IsValidPhoneNumber(personalInfo.AlternateCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.AlternateCellPhoneNumber", "Alternate Cell Phone Number is Invalid");
                }
                if (!string.IsNullOrEmpty(personalInfo.GuardianCellPhoneNumber) && !Helper.Utility.IsValidPhoneNumber(personalInfo.GuardianCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianCellPhoneNumber", "Guardian Cell Phone Number is Invalid");
                }
                if (string.IsNullOrEmpty(personalInfo.GuardianCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianCellPhoneNumber", "Phone Number is Required");
                }
                if (string.IsNullOrEmpty(personalInfo.HomePhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.HomePhoneNumber", "Phone Number is Required");
                }
                if (IsValid)
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"User provided data is Valid for Submit Personal Information. Details: {JsonConvert.SerializeObject(personalInfo)}");
                    DB.WEB_UpdatePersonal(
                        personalInfo.Id,
                        personalInfo.FirstName = Helper.Utility.ToCamelCase(personalInfo.FirstName),
                        personalInfo.MiddleName,
                        personalInfo.LastName,
                        personalInfo.FatherFirstName,
                        personalInfo.FatherMiddleName,
                        personalInfo.FatherLastName,
                        personalInfo.Gender.ToString(),
                        personalInfo.HusbandName,
                        personalInfo.DateOfBirth.ToString(),
                        personalInfo.CNIC,
                        personalInfo.EmailAddress,
                        personalInfo.AlterEmailAddress,
                        // Contact
                        personalInfo.CellPhoneNumber,
                        personalInfo.WhatsAppNumber,
                        personalInfo.AlternateCellPhoneNumber,
                        personalInfo.HomePhoneNumber,
                        personalInfo.AlternateLandline,
                        personalInfo.GuardianCellPhoneNumber,
                        personalInfo.GuardianEmailAddress,
                        // Address
                        personalInfo.ResidentialAddress,
                        personalInfo.ResidentialCountry,
                        personalInfo.ResidentialProvince,
                        personalInfo.ResidentialCity,
                        personalInfo.ResidentialCityOther,
                        personalInfo.ResidentialPostalCode,
                        personalInfo.PermanentAddress,
                        personalInfo.PermanentCountry,
                        personalInfo.PermanentProvince,
                        personalInfo.PermanentCity,
                        personalInfo.PermanentCityOther,
                        personalInfo.PermanentPostalCode,
                        personalInfo.IsCompleted
                        );
                    Utility.SetSession(personalInfo);
                    TempData["Result"] = "Personal Inofmation Submited Successfully";
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User Submited Personal Information Successfully. Details: {JsonConvert.SerializeObject(personalInfo)}");
                    return RedirectToAction("Index", "Education");
                }
                else
                {

                    var country = DB.Countries.ToList();
                    var province = DB.States.ToList();
                    var city = DB.Cities.ToList();

                    var pageModel = new PersonalInfoPageModel()
                    {
                        Main = personalInfo,
                        Country = country,
                        Province = province,
                        City = city
                    };
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User provided data is InValid for Submit Personal Information. Details: {JsonConvert.SerializeObject(personalInfo)}");
                    return View(pageModel);
                }
            }
            catch (System.Exception ex)
            {
                Helper.Utility.AddLog(Constants.LogType.Exception, "Exception occurred during Submiting personal Information." + ((ex.InnerException != null) ? ex.InnerException.Message : "")+"Model Details: " + JsonConvert.SerializeObject(personalInfo));
                TempData["Result"] = "Error occured while submiting your form";
                return RedirectToAction("Index", "Home");

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