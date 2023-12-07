using HUTOPSBatchProcessConsoleApp.Model;
using System;

namespace HUTOPSBatchProcessConsoleApp
{
    public static class Helper
    {
        public static void AddLog(string LogType, string Description)
        {
            AzureEntityFrameworkHandler dbContextHandler = new AzureEntityFrameworkHandler();

            dbContextHandler.ExecuteWithRetry(() =>
            {
                using (HUTOPSEntities DB = new HUTOPSEntities())
                {
                    DB.Logs.Add(new Log
                    {
                        Type = LogType,
                        Description = Description,
                        CreatedDatetime = DateTime.UtcNow + TimeSpan.FromHours(5)
                    });
                    DB.SaveChanges();
                }
            });

            // Generate Transient Exception For Testing

            //dbContextHandler.ExecuteWithRetry(() =>
            //{
            //    using (HUTOPSEntities context = new HUTOPSEntities())
            //    {
            //        try
            //        {
            //            context.Database.Connection.Open();

            //            // Simulate a transient error by executing a T-SQL statement that throws an error
            //            using (var command = context.Database.Connection.CreateCommand())
            //            {
            //                // The following T-SQL statement intentionally throws a transient error
            //                command.CommandText = "THROW 50001, 'Simulated transient error', 1;";
            //                command.ExecuteNonQuery();
            //            }

            //            // Your normal database operations here...
            //            // For example: var data = context.YourTableName.ToList();
            //        }
            //        catch (SqlException ex)
            //        {
            //            // Handle the transient error
            //            Console.WriteLine($"Transient error: {ex.Message}");
            //            // Implement your retry or error-handling logic here
            //        }
            //    }
            //});
        }
    }
}
