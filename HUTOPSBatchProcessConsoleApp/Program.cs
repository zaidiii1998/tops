using System.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;
using HUTOPSBatchProcessConsoleApp.Model;
using OfficeOpenXml;
using Newtonsoft.Json;
using HUTOPSBatchProcessConsoleApp.Codebase;

namespace HUTOPSBatchProcessConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HUTOPSEntities DB = new HUTOPSEntities();
            try
            {
                Helper.AddLog(Constants.LogType.ActivityLog, "Batch service started");

                List<BatchUpload> Batch = DB.BatchUploads.ToList().Where(x => x.Status == 0).ToList();

                Helper.AddLog(Constants.LogType.ActivityLog, $"Service get {Batch.Count} Batches for processing" );

                foreach (BatchUpload batch in Batch)
                {
                    var records = new List<ExcelData>();
                    Helper.AddLog(Constants.LogType.ActivityLog, $"Service get Batch for processing {JsonConvert.SerializeObject(Batch)}");

                    using (var stream = File.Open(batch.HUTOPSIdsFile, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Assuming the Excel file has a single column in the first sheet
                            Helper.AddLog(Constants.LogType.ActivityLog, $"Stream Reader read Batch file and generate list {JsonConvert.SerializeObject(Batch)}");

                            while (reader.Read())
                            {
                                if (!string.IsNullOrEmpty(reader.GetString(0))){ 
                                records.Add(new ExcelData { HUTOPSIds = reader.GetString(0) });
                                }
                            }
                        }
                    }
                    var Result = new List<ExcelData>(); ;
                    if (records.Count > 0)
                    {
                        switch (batch.Type)
                        {
                            case 1:
                                Result = BatchProcessing.GenerateAdmitCard(records, batch.Shift, batch.Venue, batch.TestDate.ToString());
                                break;

                            case 2:
                                Result = BatchProcessing.SendAdmitCard(records);
                                break;

                            case 3:
                               Result  = BatchProcessing.GenerateSendAdmitCard(records, batch.Shift, batch.Venue, batch.TestDate.ToString());
                            break;

                            case 4:
                                Result = BatchProcessing.UpdateResult(records, batch.Result, batch.IsRecordSendToEApp);
                                break;
                            case 5:
                                Result = BatchProcessing.ShiftRecordToEApp(records);
                                break;

                        }
                    }
                    Helper.AddLog(Constants.LogType.ActivityLog, $"Batch Executed successfully Details: {JsonConvert.SerializeObject(Batch)}");
                    // Update Batch Status
                    using (HUTOPSEntities tempDB = new HUTOPSEntities())
                    {
                        var batchUpload = tempDB.BatchUploads.ToList().Where(x => x.Id == batch.Id).FirstOrDefault();
                        batchUpload.Status = 1;
                        tempDB.SaveChanges();
                    }
                    Helper.AddLog(Constants.LogType.ActivityLog, $"Update Batch Status to Completed Batch Details : {JsonConvert.SerializeObject(Batch)}");

                    // Add Result sheet in the existing file 
                    var fileInfo = new FileInfo(batch.HUTOPSIdsFile);

                    // Create a new package and load the existing file
                    using (var package = new ExcelPackage(fileInfo))
                    {
                        Helper.AddLog(Constants.LogType.ActivityLog, $"Add Result sheet in the Batch file Details: {JsonConvert.SerializeObject(Batch)}");
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        // Create a new worksheet
                        var worksheet = package.Workbook.Worksheets.Add("Result");


                        // Loop through the list and insert data into the worksheet
                        int row = 1;
                        foreach (var item in Result)
                        {
                            worksheet.Cells[row, 1].Value = item.HUTOPSIds; // Change property names as needed
                            worksheet.Cells[row, 2].Value = item.Status;
                            // Add more columns as needed
                            row++;
                        }

                        // Save changes to the Excel file
                        package.Save();
                        package.Dispose();
                    }
                    Helper.AddLog(Constants.LogType.ActivityLog, $"Result sheet Updated Successfully Batch file Details: {JsonConvert.SerializeObject(Batch)}");
                    var EmailCC = System.Configuration.ConfigurationSettings.AppSettings["EmailCC"].Split(',').ToList();
                    CPD.Framework.Core.EmailService.SendEmail(System.Configuration.ConfigurationSettings.AppSettings["AdminEmails"], EmailCC, null, "Generate Admit Card Report", "Generate Admit Card Report Body", batch.HUTOPSIdsFile, null, null);
                    // Update Batch Status
                    Helper.AddLog(Constants.LogType.ActivityLog, $"Email has been sent to Admin with Batch result file : {JsonConvert.SerializeObject(Batch)}");

                    using (HUTOPSEntities tempDB = new HUTOPSEntities())
                    {
                        var batchUpload = tempDB.BatchUploads.ToList().Where(x => x.Id == batch.Id).FirstOrDefault();
                        batchUpload.IsStatusEmailSent = 1;
                        batchUpload.EmailSentOn = DateTime.Now;
                        tempDB.SaveChanges();
                    }
                    Helper.AddLog(Constants.LogType.ActivityLog, $"Update Email sent Status in BatchUploads Tabel : {JsonConvert.SerializeObject(Batch)}");

                }
            }
            catch (System.Exception ex)
            {
                Helper.AddLog(Constants.LogType.Exception, $"Error Occured while processing Batch File Error Details: {ex.Message}");
                throw;
            }
        }
    }
}
