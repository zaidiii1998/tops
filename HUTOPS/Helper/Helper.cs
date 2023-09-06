using Microsoft.Ajax.Utilities;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace HUTOPS.Helper
{
    public class Helper
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
        public static string GetSession(string SessionName)
        {
            return HttpContext.Current.Session[SessionName].ToString();
        }
        public static void SetSession(string SessionName, string SessionValue)
        {
            HttpContext.Current.Session[SessionName] = SessionValue;
        }
        public static void AddLog(string LogType, string Description)
        {
            HU_TOPSEntities DB = new HU_TOPSEntities();
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