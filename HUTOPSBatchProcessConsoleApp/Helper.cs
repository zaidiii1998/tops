using System;


namespace HUTOPSBatchProcessConsoleApp
{
    public static class Helper
    {
        public static void AddLog(string LogType, string Description)
        {
            HUTOPSEntities DB = new HUTOPSEntities();
            DB.Logs.Add(new Log
            {
                Type = LogType,
                Description = Description,
                CreatedDatetime = DateTime.UtcNow + TimeSpan.FromHours(5)
            });
            DB.SaveChanges();
        }
    }
}
