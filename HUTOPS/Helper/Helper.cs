using Microsoft.Ajax.Utilities;
using System.Globalization;

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
    }
}