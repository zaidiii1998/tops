using System.Web;

namespace HUTOPS.Helper
{
    public class Constants
    {
        public class Session
        {
            public const string UserSession = "LogedInUserSession";
            public const string EducationSession = "LogedInEducationSession";
            public const string DocumentSession = "LogedInDocumentSession";

            public const string AdminSession = "LogedInAdminSession";
        }
        public class LogType
        {
            public const string ActivityLog = "ActivityLog";
            public const string Exception = "Exception";
        }
        public class Shift
        {
            public const string FirstShift = "09:00 AM - 11:00 AM";
            public const string SecondShift = "12:00 AM - 02:00 PM";
            public const string ThirtShift = "03:00 PM - 05:00 PM";
        }
        public class ReportingTime
        {
            public const string FirstShift = "08:30 AM";
            public const string SecondShift = "11:30 AM";
            public const string ThirtShift = "02:30 PM";
        }
        public class Vanue
        {
            public const string Karachi = "Habib University, Block 18, Gulistan-e-Jauhar University Avenue,\r\nOff Shahrah-e-Faisal Karachi, Pakistan. \r\nCell Phone : +92 322 2850247 & +92 321 820 3568 \r\n";
            public const string Islamabad = "Margala, Hotel, Sahara Kashmir Rd, Shakar Parian, Islamabad, Islamabad Capital Territory, Pakistan. (Hotel Focal Person: 0304 5099997)\r\nHabib University Contact Details : +92 322 2850247,+92 321 820 3568";
        }
        public class SchoolName
        {
            public const string SE = "Dhanani School of Science and Engineering";
            public const string SA = "School of Arts, Humanities and Social Sciences";
        }
    }
}