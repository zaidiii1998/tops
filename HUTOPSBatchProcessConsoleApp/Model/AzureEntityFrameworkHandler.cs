using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace HUTOPSBatchProcessConsoleApp.Model
{
    public class AzureEntityFrameworkHandler
    {
        private const int MaxRetryCount = 3;
        private const int DelayBaseMilliseconds = 3000; // Initial delay in milliseconds

        public void ExecuteWithRetry(Action dbContextAction)
        {
            int retryCount = 0;

            while (true)
            {
                try
                {
                    // Attempt to execute the DbContext action
                    dbContextAction();

                    // If successful, break out of the retry loop
                    break;
                }
                catch (DbUpdateException ex) when (IsTransientError(ex) && retryCount < MaxRetryCount)
                {
                    // If the exception is a transient error, log it and retry
                    LogException(ex, retryCount);
                    Helper.AddLog(Constants.LogType.Exception, $"Get Transient Error Details: {ex}, RetryCount: {retryCount}");
                    retryCount++;

                    // Apply exponential backoff before the next retry
                    System.Threading.Thread.Sleep(GetDelayMilliseconds(retryCount));
                }
                catch (Exception ex)
                {
                    // Handle non-transient errors or rethrow if needed
                    LogException(ex, retryCount);
                    throw;
                }
            }
        }

        private bool IsTransientError(DbUpdateException ex)
        {
            // Check if the exception or its inner exception is a transient SQL error
            return ex.InnerException is SqlException sqlException && IsTransientError(sqlException);
        }

        private bool IsTransientError(SqlException ex)
        {
            // Check if the SQL error is a transient error
            // You may need to customize this based on the specific error codes you want to handle
            // For example, you might check for error codes 40613, 40501, 49918, etc.
            return ex.Class == 20 || (ex.Class == 14 && (ex.Number == 40613 || ex.Number == 40501 || ex.Number == 49918));
        }

        private int GetDelayMilliseconds(int retryCount)
        {
            // Exponential backoff with jitter
            Random random = new Random();
            double jitter = random.NextDouble() * 0.2; // Add up to 20% jitter
            return (int)(Math.Pow(2, retryCount) * DelayBaseMilliseconds * (1 + jitter));
        }

        private void LogException(Exception ex, int retryCount)
        {
            // Log the exception details along with retry count
            Console.WriteLine($"Retry {retryCount + 1}: {ex.GetType().Name} - {ex.Message}");
        }
    }
}
