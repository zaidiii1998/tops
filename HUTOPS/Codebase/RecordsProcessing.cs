using HUTOPS.EAppDBModel;
using HUTOPS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HUTOPS.Codebase
{
    public static class RecordsProcessing
    {
        public static bool MoveRecordToEApp(int Id)
        {
            HUTOPSEntities DB = new HUTOPSEntities();
            
            try
            {
                Utility.AddLog(Constants.LogType.ActivityLog, $"Service request to fetch record against Id : {Id}");

                var personalInformation = DB.PersonalInformations.ToList().Where(x => x.Id == Id).FirstOrDefault();
                var educationalInformation = new Educational();
                if (personalInformation != null)
                {
                    educationalInformation = DB.Educationals.Where(x => x.UserId == personalInformation.Id).FirstOrDefault();
                    if (personalInformation.IsRecordMoveToEApp != 1)
                    {
                        if (educationalInformation.Id != 0)
                        {
                            if (personalInformation.Result == 2)
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
                                    CountryAdd = null,
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
                                    Savedate = personalInformation.CreatedDatetime,
                                    Updatedate = personalInformation.UpdateDate,
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
                                    Permanent_country = personalInformation.PermanentCountry,
                                    Permanent_provience = personalInformation.PermanentProvince,
                                    Permanent_city = personalInformation.PermanentCity,
                                    Permanent_cityother = personalInformation.PermanentCityOther,
                                    Permanent_postal = null,


                                    Postal_addres = personalInformation.ResidentialAddress,
                                    Postal_country = personalInformation.ResidentialCountry,
                                    Postal_provience = personalInformation.ResidentialProvince,
                                    Postal_city = personalInformation.ResidentialCity,
                                    Postal_cityother = personalInformation.ResidentialCityOther,
                                    Postal_postal = null,

                                    AppliedBefore = personalInformation.IsAppliedBefore == 0 ? "No" : "Yes",
                                    AppliedBeforeMonth = null,
                                    AppliedBeforeYear = personalInformation.AppliedBeforeYear,

                                    WhatsAppNumber = personalInformation.WhatsAppNumber,

                                    TestDate = personalInformation.TestDate,
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
                                    Modeofstudy = null,
                                    BoardofEducation = educationalInformation.HSSCBoardName,
                                    Intendedprogram = educationalInformation.IntendedProgram,
                                    Retake = false,
                                    CurrentHighSchoolOther = educationalInformation.HSSCSchoolName,
                                    CurrentHighSchoolCode_old = 0,
                                    BoardofEducationOther = "",
                                    CurrentHighSchoolCode = "-1"
                                };

                                using (E_ApplicationEntities EApp_DB = new E_ApplicationEntities())
                                {
                                    EApp_DB.PersonalInformations.Add(EApp_PersonalInformation);
                                    EApp_DB.SaveChanges();
                                }
                                personalInformation.IsRecordMoveToEApp = 1;
                                DB.SaveChanges();

                                Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record Move to E-Application against HUTOPS Id: {personalInformation.HUTopId}");

                            }
                            else
                            {
                                personalInformation.IsRecordMoveToEApp = 0;
                                DB.SaveChanges();
                                Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already move to E-Application against HUTOPS Id: {personalInformation.HUTopId}");
                            }
                        }
                        else
                        {
                            personalInformation.IsRecordMoveToEApp = 1;
                            DB.SaveChanges();
                            Utility.AddLog(Constants.LogType.ActivityLog, $"Educational Information not found against HUTOPS Id: {personalInformation.HUTopId}");
                        }

                    }
                    else
                    {
                        Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record already move to E-Application against HUTOPS Id: {personalInformation.HUTopId}");
                    }

                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Personal Information Record not found against HUTOPS Id: {personalInformation.HUTopId}");
                    
                }
                return false;
            }
            catch (Exception ex)
            {
                using (HUTOPSEntities tempDb = new HUTOPSEntities())
                {
                    var personalInformation = tempDb.PersonalInformations.Where(x => x.Id == Id).FirstOrDefault();
                    personalInformation.IsRecordMoveToEApp = 1;
                    DB.SaveChanges();
                }
                Utility.AddLog(Constants.LogType.Exception, $"Error Occured while processing Batch Record against Id : {Id} Error Details: {ex.Message}");
                return false;
            }
        }
    }
}