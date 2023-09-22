using HUTOPS.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using static HUTOPS.Helper.Constants;

namespace HUTOPS.Helper
{
    public class Utility
    {
        public static string ToCamelCase(string input)
        {
            if(input.IsNullOrWhiteSpace()) { return ""; }
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = input.ToLower(); // Convert the string to lowercase
            return textInfo.ToTitleCase(input).Replace(" ", ""); // Convert to title case and remove spaces
        }
        public static string ConvertArrayToCSV(string[] array)
        {
            if (array == null || array.Length == 0)
            {
                return ""; // Return an empty string if the array is null or empty
            }

            // Use string.Join to concatenate the array elements with commas
            return string.Join(",", array);
        }
        public static string GetParam(DTParameters param, int Index)
        {
            return param.Columns[Index].Search.Value == null ? null : param.Columns[Index].Search.Value;
        }
        public static void SetSession(PersonalInformation personalInformation)
        {
            if (personalInformation != null)
            {
                HttpContext.Current.Session[Constants.Session.UserSession] = personalInformation;
            }
        }
        public static void SetSession(Admin admin)
        {
            if (admin != null)
            {
                HttpContext.Current.Session[Constants.Session.AdminSession] = admin;
            }
        }
        public static void SetSession(Educational educational)
        {
            if (educational != null)
            {
                HttpContext.Current.Session[Constants.Session.EducationSession] = educational;
            }
        }
        public static void SetSession(Document document)
        {
            if (document != null)
            {
                HttpContext.Current.Session[Constants.Session.DocumentSession] = document;
            }
        }
        public static PersonalInformation GetUserFromSession()
        {

            PersonalInformation personalInformation = new PersonalInformation();
            var userObjSession = HttpContext.Current.Session[Constants.Session.UserSession];
            if (userObjSession != null)
            {
                personalInformation = userObjSession as PersonalInformation;
            }

            return personalInformation;

        }
        public static Educational GetEducationFromSession()
        {

            Educational educational = new Educational();
            var userObjSession = HttpContext.Current.Session[Constants.Session.EducationSession];
            if (userObjSession != null)
            {
                educational = userObjSession as Educational;
            }

            return educational;

        }
        public static Document GetDocumentFromSession()
        {

            Document document = new Document();
            var userObjSession = HttpContext.Current.Session[Constants.Session.DocumentSession];
            if (userObjSession != null)
            {
                document = userObjSession as Document;
            }

            return document;

        }
        public static Admin GetAdminFromSession()
        {

            Admin admin = new Admin();
            var userObjSession = HttpContext.Current.Session[Constants.Session.AdminSession];
            if (userObjSession != null)
            {
                admin = userObjSession as Admin;
            }

            return admin;

        }
        public static void AddLog(string LogType, string Description) {
            HUTOPSEntities DB = new HUTOPSEntities();
             DB.Logs.Add(new Log
            {
                Type = LogType,
                Description = Description,
                CreatedDatetime = DateTime.Now
            });
            DB.SaveChanges();
        }
        
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Define a regular expression pattern for a US phone number (10 digits)
            string pattern = @"^\d{4}-\d{7}$";

            // Use Regex.IsMatch to check if the input matches the pattern
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public static List<string> ValidatePersonalInfo(PersonalInformation personalInfo)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(personalInfo.FirstName))
            {
                errors.Add("First Name is required");
            }
            if (!string.IsNullOrEmpty(personalInfo.FirstName) && (personalInfo.FirstName.Length < 3 || personalInfo.FirstName.Length > 25))
            {
                errors.Add("First Name length must be greater than 3 and less than 25 characters");
            }
            if (string.IsNullOrEmpty(personalInfo.LastName))
            {
                errors.Add("Last Name is required");
            }
            if (!string.IsNullOrEmpty(personalInfo.LastName) && (personalInfo.LastName.Length < 3 || personalInfo.LastName.Length > 25))
            {
                errors.Add("Last Name length must be greater than 3 and less than 25 characters");
            }
            if (string.IsNullOrEmpty(personalInfo.FatherFirstName))
            {
                errors.Add("Father First Name is required");
            }
            if (!string.IsNullOrEmpty(personalInfo.FatherFirstName) && (personalInfo.FatherFirstName.Length < 3 || personalInfo.FatherFirstName.Length > 25))
            {
                errors.Add("Father First Name length must be greater than 3 and less than 25 characters");
            }
            if (string.IsNullOrEmpty(personalInfo.FatherLastName))
            {
                errors.Add("Father Last Name is required");
            }
            if (!string.IsNullOrEmpty(personalInfo.FatherLastName) && (personalInfo.FatherLastName.Length < 3 || personalInfo.FatherLastName.Length > 25))
            {
                errors.Add("Father Last Name length must be greater than 3 and less than 25 characters");
            }
            if (!string.IsNullOrEmpty(personalInfo.AlterEmailAddress) && !Helper.Utility.isValidEmail(personalInfo.AlterEmailAddress))
            {
                errors.Add("Provided Email Address is Invalid");
            }
            if (string.IsNullOrEmpty(personalInfo.GuardianEmailAddress))
            {
                errors.Add("Guardian Email Address is Required");
            }
            if (!string.IsNullOrEmpty(personalInfo.GuardianEmailAddress) && !Helper.Utility.isValidEmail(personalInfo.GuardianEmailAddress))
            {
                errors.Add("Provided Email Address is Invalid");
            }
            if (!string.IsNullOrEmpty(personalInfo.CellPhoneNumber) && !Helper.Utility.IsValidPhoneNumber(personalInfo.CellPhoneNumber))
            {
                errors.Add("Cell Phone Number is Invalid");
            }
            if (!string.IsNullOrEmpty(personalInfo.WhatsAppNumber) && !Helper.Utility.IsValidPhoneNumber(personalInfo.WhatsAppNumber))
            {
                errors.Add("WhatsApp Number Number is Invalid");
            }
            if (!string.IsNullOrEmpty(personalInfo.AlternateCellPhoneNumber) && !Helper.Utility.IsValidPhoneNumber(personalInfo.AlternateCellPhoneNumber))
            {
                errors.Add("Alternate Cell Phone Number is Invalid");
            }
            if (!string.IsNullOrEmpty(personalInfo.GuardianCellPhoneNumber) && !Helper.Utility.IsValidPhoneNumber(personalInfo.GuardianCellPhoneNumber))
            {
                errors.Add("Guardian Cell Phone Number is Invalid");
            }
            if (string.IsNullOrEmpty(personalInfo.GuardianCellPhoneNumber))
            {
                errors.Add("Phone Number is Required");
            }
            if (string.IsNullOrEmpty(personalInfo.HomePhoneNumber))
            {
                errors.Add("Phone Number is Required");
            }
            if (string.IsNullOrEmpty(personalInfo.HearAboutHU))
            {
                errors.Add("Hear About HU Field is required");
            }
            if (personalInfo.HearAboutHU == "Other" && string.IsNullOrEmpty(personalInfo.HearAboutHUOther))
            {
                errors.Add("Hear About HU Other Value is required");
            }

            return errors;
        }

        public static List<string> ValidateEducation(Educational educational)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(educational.CurrentLevelOfEdu))
            {
                errors.Add("Your Current Level of Study: is required");
            }
            if (string.IsNullOrEmpty(educational.HSSCStartDate.ToString()))
            {
                errors.Add("Starting Year: is required");
            }
            if (string.IsNullOrEmpty(educational.HSSCCompletionDate.ToString()))
            {
                errors.Add("Completion Year: is required");
            }
            if (string.IsNullOrEmpty(educational.HSSCSchoolName))
            {
                errors.Add("Current College/Last College Name: is required");
            }
            if (string.IsNullOrEmpty(educational.HSSCSchoolAddress))
            {
                errors.Add("College Address with City Name: is required");
            }
            if (string.IsNullOrEmpty(educational.HSSCBoardId))
            {
                errors.Add("Board Of Education: is required");
            }
            if (string.IsNullOrEmpty(educational.HSSCGroupId))
            {
                errors.Add("Group Of Studies: is required");
            }
            if (string.IsNullOrEmpty(educational.HSSCPercentage))
            {
                errors.Add("Overall HSSC Percentage: is required");
            }
            if (string.IsNullOrEmpty(educational.SSCSchoolName))
            {
                errors.Add("Secondary Education School Name: is required");
            }
            if (string.IsNullOrEmpty(educational.SSCSchoolAddress))
            {
                errors.Add("Secondary Education School Address with City Name: is required");
            }
            if (string.IsNullOrEmpty(educational.SSCPercentage))
            {
                errors.Add("Overall SSC Percentage: is required");
            }
            if (string.IsNullOrEmpty(educational.IntendedProgram))
            {
                errors.Add("Which Degree Program you are planning to pursue at Habib University?: is required");
            }

            return errors;
        }
    }
}