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
using CPD.Framework.Core;
using System.Data.Entity.Validation;

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

                    var EmailTemp = new EmailTemplate();
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
                    var Result = new List<ExcelData>();
                    if (records.Count > 0)
                    {
                        switch ((BatchType)batch.Type)
                        {
                            case BatchType.GenerateAdmitCard:// Convert the byte value to the State enum        State currentState = (State)stateValue;: // Generate Admit Card
                                Result = BatchProcessing.GenerateAdmitCard(records, batch.Shift, batch.Venue, batch.TestDate.Value.ToString("dddd, dd MMMM yyyy"));
                                EmailTemp = DB.EmailTemplates.Where(x => x.Id == 5).FirstOrDefault();
                                break;

                            case BatchType.SendAdmitCard: // SendAdmitCard
                                Result = BatchProcessing.SendAdmitCard(records);
                                EmailTemp = DB.EmailTemplates.Where(x => x.Id == 7).FirstOrDefault();
                                break;

                            case BatchType.GenerateAndSendAdmitCard: // GenerateAndSendAdmitCard
                                Result  = BatchProcessing.GenerateSendAdmitCard(records, batch.Shift, batch.Venue, batch.TestDate.Value.ToString("dddd, dd MMMM yyyy"));
                                EmailTemp = DB.EmailTemplates.Where(x => x.Id == 6).FirstOrDefault();
                                break;

                            case BatchType.Result: // Result
                                Result = BatchProcessing.UpdateResult(records, batch.Result, batch.IsRecordSendToEApp);
                                EmailTemp = DB.EmailTemplates.Where(x => x.Id == 8).FirstOrDefault();
                                break;
                            case BatchType.MoveRecordToEApp: //MoveRecordToEApp
                                Result = BatchProcessing.ShiftRecordToEApp(records);
                                EmailTemp = DB.EmailTemplates.Where(x => x.Id == 9).FirstOrDefault();
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
                    var EmailCC = System.Configuration.ConfigurationSettings.AppSettings["BatchReportEmailCC"].Split(';').ToList();
                    EmailService.SendEmail(batch.CreatedBy, EmailCC, null, EmailTemp.Subject, EmailTemp.Body, batch.HUTOPSIdsFile, "tops@habib.edu.pk", null);
                    // Update Batch Status
                    Helper.AddLog(Constants.LogType.ActivityLog, $"Email has been sent to Admin with Batch result file : {JsonConvert.SerializeObject(Batch)}");

                    using (HUTOPSEntities tempDB = new HUTOPSEntities())
                    {
                        var batchUpload = tempDB.BatchUploads.ToList().Where(x => x.Id == batch.Id).FirstOrDefault();
                        batchUpload.IsStatusEmailSent = 1;
                        batchUpload.EmailSentOn = DateTime.UtcNow + TimeSpan.FromHours(5);
                        tempDB.SaveChanges();
                    }
                    Helper.AddLog(Constants.LogType.ActivityLog, $"Update Email sent Status in BatchUploads Tabel : {JsonConvert.SerializeObject(Batch)}");

                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Helper.AddLog(Constants.LogType.Exception, $"Error: {ve.PropertyName}, {ve.ErrorMessage} Error Occured while processing Batch File Error Details: {ex.Message}).");
                    }
                }
                throw;
            }
            catch (System.Exception ex)
            {
                Helper.AddLog(Constants.LogType.Exception, $"Error Occured while processing Batch File Error Details: {ex.Message}");
                throw;
            }
        }
    }
}
