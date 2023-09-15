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

    }
}