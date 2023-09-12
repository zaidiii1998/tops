using System.Web;

namespace HUTOPS.Helper
{
    public class Constants
    {
        public class Session
        {
            public const string UserId = "UserId";
            public const string UserName = "Name";
            public const string UserSession = "LogedInUserSession";
        }
        public class LogType
        {
            public const string ActivityLog = "ActivityLog";
            public const string Exception = "Exception";
        }

    }
}