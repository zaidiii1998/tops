﻿using HUTOPS.EAppDBModel;
using HUTOPS.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace HUTOPS.Codebase
{
    public static class RecordsProcessing
    {
        public static Dictionary<string, string> province = new Dictionary<string, string>()
        {
            {"Sindh","SN" },
            {"Punjab","PJ" },
            {"Khyber Pakhtunkhwa","KP" },
            {"Islamabad Capital Territory","ICT" },
            {"Gilgit-Baltistan","GB" },
            {"FATA","FATA" },
            {"Balochistan","Bl" },
            {"Azad Kashmir","AJK" },

        };
        public static Dictionary<string, string> country = new Dictionary<string, string>()
        {
            {"Pakistan","PAK" }
        };
        public static bool MoveRecordToEApp(PersonalInformation personalInformation, Educational educationalInformation)
        {
            HUTOPSEntities DB = new HUTOPSEntities();
            
            try
            {
                var EApp_PersonalInformation = new EAppDBModel.PersonalInformation()
                {
                    Appid = "",
                    FirstName = personalInformation.FirstName,
                    MiddleName = personalInformation.MiddleName,
                    LastName = personalInformation.LastName,
                    Gender = personalInformation.Gender,
                    HusbandName = personalInformation.HusbandName,
                    FatherFirstName = personalInformation.FatherFirstName,
                    FatherMiddleName = personalInformation.FatherMiddleName,
                    FatherLastName = personalInformation.FatherLastName,
                    FatherName = personalInformation.FatherFirstName + personalInformation.FatherMiddleName + personalInformation.FatherLastName,
                    DateofBirth = personalInformation.DateOfBirth,
                    CountryAdd = "Pakistan",
                    PhoneNo = personalInformation.CellPhoneNumber,
                    Tellus = null,
                    CurrentAddress = personalInformation.ResidentialAddress,
                    PostalAddress = personalInformation.ResidentialAddress,
                    Country = personalInformation.ResidentialCountry,
                    City = personalInformation.ResidentialCity,
                    Provience = personalInformation.ResidentialProvince,
                    Postal = null,
                    Email = personalInformation.EmailAddress,
                    AlterEmail = personalInformation.AlterEmailAddress,
                    Password = personalInformation.Password,
                    CPassword = personalInformation.Password,
                    Savedate = personalInformation.CreatedDatetime, // Save Date
                    Updatedate = personalInformation.CreatedDatetime, // Updated Date
                    AppStatus = 1,
                    StudentStatus = "0",
                    TempID = null,
                    HereYou = personalInformation.HearAboutHU,
                    HereOther = personalInformation.HearAboutHUOther,
                    NewEmail = "",
                    Discount = null,
                    DiscountFee = null,
                    Yourinterests = null,
                    YourinterestsOther = null,

                    Permanent_addres = personalInformation.PermanentAddress,
                    Permanent_country = country[personalInformation.PermanentCountry.Trim()],
                    Permanent_provience = province[personalInformation.PermanentProvince.Trim()],
                    Permanent_city = personalInformation.PermanentCity,
                    Permanent_cityother = personalInformation.PermanentCityOther,
                    Permanent_postal = null,


                    Postal_addres = personalInformation.ResidentialAddress,
                    Postal_country = personalInformation.ResidentialCountry,
                    Postal_provience = province[personalInformation.ResidentialProvince.Trim()],
                    Postal_city = personalInformation.ResidentialCity,
                    Postal_cityother = personalInformation.ResidentialCityOther,
                    Postal_postal = null,

                    AppliedBefore = personalInformation.IsAppliedBefore == 0 ? "No" : "Yes",
                    AppliedBeforeMonth = null,
                    AppliedBeforeYear = personalInformation.AppliedBeforeYear,

                    WhatsAppNumber = personalInformation.WhatsAppNumber,

                    TestDate = null,
                    SubmissionDate = personalInformation.SubmissionDate,
                    HUTopsCandidate = "Yes",
                    HuTopsId = personalInformation.HUTopId,

                    // Education 
                    School = educationalInformation.HUSchoolName,
                    SchoolName = "Other",
                    Currentqualification = educationalInformation.CurrentLevelOfEdu,
                    AlternateMobile = personalInformation.AlternateCellPhoneNumber,
                    Alternatelandline = personalInformation.AlternateLandline,
                    Cityother = personalInformation.ResidentialCityOther,
                    OtherCurrentqualification = educationalInformation.HSSCSchoolName,
                    Userid = null,
                    chk = null,
                    Modeofstudy = "Regular",
                    BoardofEducation = educationalInformation.HSSCBoardName,
                    Intendedprogram = educationalInformation.IntendedProgram,
                    Retake = false,
                    CurrentHighSchoolOther = educationalInformation.HSSCSchoolName,
                    CurrentHighSchoolCode_old = 0,
                    BoardofEducationOther = "",
                    CurrentHighSchoolCode = "-1"
                };

                using (EApplicationEntities EApp_DB = new EApplicationEntities())
                {
                    EApp_DB.PersonalInformations.Add(EApp_PersonalInformation);
                    EApp_DB.SaveChanges();
                }
                Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record Move to E-Application against HUTOPS Id: {personalInformation.HUTopId}");
                return true;
                
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Utility.AddLog(Constants.LogType.Exception, $"Error: {ve.PropertyName}, {ve.ErrorMessage} Error Occured while moving record to E-App against HUTOPS Id : {personalInformation.HUTopId}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Utility.AddLog(Constants.LogType.Exception, $"Error Occured while moving record to E-App against HUTOPS Id : {personalInformation.HUTopId} Error Details: {ex.Message}");
                return false;
            }
        }
    }
}