using HUTOPS.Models;
using Microsoft.Ajax.Utilities;
using System;
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
    }
}