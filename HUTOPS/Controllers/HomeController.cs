using HUTOPS.Helper;
using System.Linq;
using System.Web.Mvc;
using HUTOPS.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

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
                int userId = int.Parse(Helper.Utility.GetSession(Constants.Session.UserId));
                var user = DB.PersonalInformations.ToList();
                var LoginUser = user.Where(x => x.Id == userId).FirstOrDefault();
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
                ViewBag.Declaration = DB.PersonalInformations.ToList().Where(x => x.Id == personalInfo.Id).ToList().FirstOrDefault().Declaration;
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
                Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User request to Update Activities ");
                int userId = int.Parse(Helper.Utility.GetSession(Constants.Session.UserId));
                List<Activity> activities = DB.Activities.ToList().Where(x => x.UserId == userId).ToList();
                
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
                                UserId = userId,
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
                int userId = int.Parse(Helper.Utility.GetSession(Constants.Session.UserId));
                var user = DB.PersonalInformations.ToList().Where(x => x.Id == userId).ToList().FirstOrDefault();

                return View(user);
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

                int userId = int.Parse(Helper.Utility.GetSession(Constants.Session.UserId));
                var user = DB.PersonalInformations.ToList().Where(x => x.Id == userId).ToList().FirstOrDefault();
                if(!string.IsNullOrEmpty(Date))
                {
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User provided Date is Verified");
                    user.TestDate = Date;
                    DB.SaveChanges();
                    return Json(new { status = true, message = "Test Date Updated Successfully" });
                }
                else
                {
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User provided Date is NULL or empty string");
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
                model.Id = int.Parse(Helper.Utility.GetSession(Constants.Session.UserId));
                model.IsCompleted = 0;
                Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User-requested to Update Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                DB.WEB_UpdatePersonal(
                        model.Id,
                        Helper.Utility.ToCamelCase(model.FirstName),
                        Helper.Utility.ToCamelCase(model.MiddleName),
                        Helper.Utility.ToCamelCase(model.LastName),
                        Helper.Utility.ToCamelCase(model.FatherFirstName),
                        Helper.Utility.ToCamelCase(model.FatherMiddleName),
                        Helper.Utility.ToCamelCase(model.FatherLastName),
                        model.Gender == null? null : model.Gender.ToString(),
                        Helper.Utility.ToCamelCase(model.HusbandName),
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
                        model.ResidentialCountry == null ? null : DB.Countries.ToList().Where(x => x.Id == int.Parse(model.ResidentialCountry)).ToList().FirstOrDefault().Name,
                        model.ResidentialProvince == null ? null : DB.States.ToList().Where(x => x.Id == int.Parse(model.ResidentialProvince)).ToList().FirstOrDefault().Name,
                        model.ResidentialCity == null ? null : model.ResidentialCity == "other" ? "Other" : DB.Cities.ToList().Where(x => x.Id == int.Parse(model.ResidentialCity)).ToList().FirstOrDefault().Name,
                        model.ResidentialCityOther,
                        model.ResidentialPostalCode,
                        model.PermanentAddress,
                        model.PermanentCountry == null ? null : DB.Countries.ToList().Where(x => x.Id == int.Parse(model.PermanentCountry)).ToList().FirstOrDefault().Name,
                        model.PermanentProvince == null ? null : DB.States.ToList().Where(x => x.Id == int.Parse(model.PermanentProvince)).ToList().FirstOrDefault().Name,
                        model.PermanentCity == null ? null : model.PermanentCity == "other" ? "Other" : DB.Cities.ToList().Where(x => x.Id == int.Parse(model.PermanentCity)).ToList().FirstOrDefault().Name,
                        model.PermanentCityOther,
                        model.PermanentPostalCode,
                        model.IsCompleted
                        );
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
                personalInfo.Id = int.Parse(Helper.Utility.GetSession(Constants.Session.UserId));
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
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User provided data is Valid for Submit Personal Information. Details: {JsonConvert.SerializeObject(personalInfo)}");
                    DB.WEB_UpdatePersonal(
                        personalInfo.Id,
                        personalInfo.FirstName = Helper.Utility.ToCamelCase(personalInfo.FirstName),
                        Helper.Utility.ToCamelCase(personalInfo.MiddleName),
                        Helper.Utility.ToCamelCase(personalInfo.LastName),
                        Helper.Utility.ToCamelCase(personalInfo.FatherFirstName),
                        Helper.Utility.ToCamelCase(personalInfo.FatherMiddleName),
                        Helper.Utility.ToCamelCase(personalInfo.FatherLastName),
                        personalInfo.Gender.ToString(),
                        Helper.Utility.ToCamelCase(personalInfo.HusbandName),
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
                        personalInfo.ResidentialCountry == null ? null : DB.Countries.ToList().Where(x => x.Id == int.Parse(personalInfo.ResidentialCountry)).ToList().FirstOrDefault().Name,
                        personalInfo.ResidentialProvince == null ? null : DB.States.ToList().Where(x => x.Id == int.Parse(personalInfo.ResidentialProvince)).ToList().FirstOrDefault().Name,
                        personalInfo.ResidentialCity == null ? null : personalInfo.ResidentialCity == "other" ? "Other" : DB.Cities.ToList().Where(x => x.Id == int.Parse(personalInfo.ResidentialCity)).ToList().FirstOrDefault().Name,
                        personalInfo.ResidentialCityOther,
                        personalInfo.ResidentialPostalCode,
                        personalInfo.PermanentAddress,
                        personalInfo.PermanentCountry == null ? null : DB.Countries.ToList().Where(x => x.Id == int.Parse(personalInfo.PermanentCountry)).ToList().FirstOrDefault().Name,
                        personalInfo.PermanentProvince == null ? null : DB.States.ToList().Where(x => x.Id == int.Parse(personalInfo.PermanentProvince)).ToList().FirstOrDefault().Name,
                        personalInfo.PermanentCity == null ? null : personalInfo.PermanentCity == "other" ? "Other" : DB.Cities.ToList().Where(x => x.Id == int.Parse(personalInfo.PermanentCity)).ToList().FirstOrDefault().Name,
                        personalInfo.PermanentCityOther,
                        personalInfo.PermanentPostalCode,
                        personalInfo.IsCompleted
                        );
                    Utility.SetUserSession(personalInfo);
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