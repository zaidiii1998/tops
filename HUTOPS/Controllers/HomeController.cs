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
                int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
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
                int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                List<Activity> activities = DB.Activities.ToList().Where(x => x.UserId == userId).ToList();
                ViewBag.Declaration = DB.PersonalInformations.ToList().Where(x => x.Id == userId).ToList().FirstOrDefault().Declaration;
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
                Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User request to Update Activities ");
                int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
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
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User Previous Activities has been removed");

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
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User Activities has been updated successfully");

                    return Json(new { status = true, message = "Activities Updated Successfully" });
                }
                else {
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User Activities updation Failed");
                    return Json(new { status = false, message = "Activities are not provided" });
                }

            }
            catch (System.Exception ex)
            {
                Helper.Helper.AddLog(Constants.LogType.Exception, "Exception occurred during Submiting User Activities." + ((ex.InnerException != null) ? ex.InnerException.Message : ""));                
                    return Json(new { status = false, message = "Activities Updation Failed" });
            }

        }
        public ActionResult TestDate()
        {
            try
            {
                int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
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
                Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User request to update test date");

                int userId = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                var user = DB.PersonalInformations.ToList().Where(x => x.Id == userId).ToList().FirstOrDefault();
                if(!string.IsNullOrEmpty(Date))
                {
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User provided Date is Verified");
                    user.TestDate = Date;
                    DB.SaveChanges();
                    return Json(new { status = true, message = "Test Date Updated Successfully" });
                }
                else
                {
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User provided Date is NULL or empty string");
                    return Json(new { status = false, message = "Please select Test Date " });
                }
                
            }
            catch (System.Exception ex)
            {
                Helper.Helper.AddLog(Constants.LogType.Exception, "Exception occurred while submiting Test Date." + ((ex.InnerException != null) ? ex.InnerException.Message : ""));            

                return Json(new { status = false, message = "Test Date Updation Failed" });
            }
        }
        public ActionResult Save(PersonalInformation model)
        {
            try
            {
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                model.IsCompleted = 0;
                Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User-requested to Update Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                DB.WEB_UpdatePersonal(
                        model.Id,
                        Helper.Helper.ToCamelCase(model.FirstName),
                        Helper.Helper.ToCamelCase(model.MiddleName),
                        Helper.Helper.ToCamelCase(model.LastName),
                        Helper.Helper.ToCamelCase(model.FatherFirstName),
                        Helper.Helper.ToCamelCase(model.FatherMiddleName),
                        Helper.Helper.ToCamelCase(model.FatherLastName),
                        model.Gender == null? null : model.Gender.ToString(),
                        Helper.Helper.ToCamelCase(model.HusbandName),
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
                Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User has Successfully Updated Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                return Json(new { status = true, message = "Personal Information Successfully Updated" });
            }
            catch (System.Exception ex)
            {
                Helper.Helper.AddLog(Constants.LogType.Exception, "Exception occurred during Save personal Information." + ((ex.InnerException != null) ? ex.InnerException.Message : "") + "Model Details: " + JsonConvert.SerializeObject(model));
                return Json(new { status = false, message = "Failed to save Record, Please try again" });
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(PersonalInformation model)
        {
            try
            {
                model.Id = int.Parse(Helper.Helper.GetSession(Constants.Session.UserId));
                model.IsCompleted = 1;
                Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User-requested to Submit Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                var IsValid = true;
                if (string.IsNullOrEmpty(model.FirstName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FirstName", "First Name is required");
                }
                if (!string.IsNullOrEmpty(model.FirstName) && (model.FirstName.Length < 3 || model.FirstName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FirstName", "First Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.LastName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.LastName", "Last Name is required");
                }
                if (!string.IsNullOrEmpty(model.LastName) && (model.LastName.Length < 3 || model.LastName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.LastName", "Last Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.FatherFirstName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherFirstName", "Father First Name is required");
                }
                if (!string.IsNullOrEmpty(model.FatherFirstName) && (model.FatherFirstName.Length < 3 || model.FatherFirstName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherFirstName", "Father First Name length must be greater than 3 and less than 25 characters");

                }
                if (string.IsNullOrEmpty(model.FatherLastName))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherLastName", "Father Last Name is required");
                }
                if (!string.IsNullOrEmpty(model.FatherLastName) && (model.FatherLastName.Length < 3 || model.FatherLastName.Length > 25))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.FatherLastName", "Father Last Name length must be greater than 3 and less than 25 characters");
                }
                if (!string.IsNullOrEmpty(model.AlterEmailAddress) && !Helper.Helper.isValidEmail(model.AlterEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.AlterEmailAddress", "Provided Email Address is Invalid");
                }
                if (string.IsNullOrEmpty(model.GuardianEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianEmailAddress", "Guardian Email Address is Required");
                }
                if (!string.IsNullOrEmpty(model.GuardianEmailAddress) && !Helper.Helper.isValidEmail(model.GuardianEmailAddress))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianEmailAddress", "Provided Email Address is Invalid");
                }
                if (!string.IsNullOrEmpty(model.CellPhoneNumber) && !Helper.Helper.IsValidPhoneNumber(model.CellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.CellPhoneNumber", "Cell Phone Number is Invalid");
                }
                if (!string.IsNullOrEmpty(model.WhatsAppNumber) && !Helper.Helper.IsValidPhoneNumber(model.WhatsAppNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.WhatsAppNumber", "WhatsApp Number Number is Invalid");
                }
                if (!string.IsNullOrEmpty(model.AlternateCellPhoneNumber) && !Helper.Helper.IsValidPhoneNumber(model.AlternateCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.AlternateCellPhoneNumber", "Alternate Cell Phone Number is Invalid");
                }
                if (!string.IsNullOrEmpty(model.GuardianCellPhoneNumber) && !Helper.Helper.IsValidPhoneNumber(model.GuardianCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianCellPhoneNumber", "Guardian Cell Phone Number is Invalid");
                }
                if (string.IsNullOrEmpty(model.GuardianCellPhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.GuardianCellPhoneNumber", "Phone Number is Required");
                }
                if (string.IsNullOrEmpty(model.HomePhoneNumber))
                {
                    IsValid = false;
                    ModelState.AddModelError("Main.HomePhoneNumber", "Phone Number is Required");
                }
                if (IsValid)
                {
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User provided data is Valid for Submit Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                    DB.WEB_UpdatePersonal(
                        model.Id,
                        Helper.Helper.ToCamelCase(model.FirstName),
                        Helper.Helper.ToCamelCase(model.MiddleName),
                        Helper.Helper.ToCamelCase(model.LastName),
                        Helper.Helper.ToCamelCase(model.FatherFirstName),
                        Helper.Helper.ToCamelCase(model.FatherMiddleName),
                        Helper.Helper.ToCamelCase(model.FatherLastName),
                        model.Gender.ToString(),
                        Helper.Helper.ToCamelCase(model.HusbandName),
                        model.DateOfBirth.ToString(),
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

                    TempData["Result"] = "Personal Inofmation Submited Successfully";
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User Submited Personal Information Successfully. Details: {JsonConvert.SerializeObject(model)}");
                    return RedirectToAction("Index", "Education");
                }
                else
                {
                    var country = DB.Countries.ToList();
                    var province = DB.States.ToList();
                    var city = DB.Cities.ToList();
                    var pageModel = new PersonalInfoPageModel()
                    {
                        Main = model,
                        Country = country,
                        Province = province,
                        City = city
                    };
                    Helper.Helper.AddLog(Constants.LogType.ActivityLog, $"User provided data is InValid for Submit Personal Information. Details: {JsonConvert.SerializeObject(model)}");
                    return View(pageModel);
                }
            }
            catch (System.Exception ex)
            {
                Helper.Helper.AddLog(Constants.LogType.Exception, "Exception occurred during Submiting personal Information." + ((ex.InnerException != null) ? ex.InnerException.Message : "")+"Model Details: " + JsonConvert.SerializeObject(model));
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