﻿using ExcelDataReader;
using HUTOPS.Helper;
using HUTOPS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace HUTOPS.Controllers
{
    [SessionValidatorActionFilter]
    public class AdmitCardController : Controller
    {
        // GET: AdmitCard
        HUTOPSEntities DB = new HUTOPSEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Submit(AdmitCardBatchModel admitCardBatchModel)
        {
            string filePath = "";
            try
            {
                if (admitCardBatchModel.Type == 0)
                {
                    return Json(new { status = false, message = "Please Select Action First" });
                }
                if((admitCardBatchModel.Type != 0 && admitCardBatchModel.Type == BatchType.SendAdmitCard.GetHashCode()) && admitCardBatchModel.HUTOPSIdsFile == null) 
                {
                    return Json(new { status = false, message = "Please Select HUTOPS Ids File to send Emails" });
                }
                else if(admitCardBatchModel.Type != 0 &&(admitCardBatchModel.Type == BatchType.GenerateAdmitCard.GetHashCode()) || admitCardBatchModel.Type == BatchType.GenerateAndSendAdmitCard.GetHashCode())
                {
                    if (!ModelState.IsValid)
                    {
                        return Json(new { status = false, message = "Provided Information is not Valid" });
                    }
                }
                else if ((admitCardBatchModel.Type != 0 && admitCardBatchModel.Type == BatchType.MoveRecordToEApp.GetHashCode()) && admitCardBatchModel.HUTOPSIdsFile == null)
                {
                    return Json(new { status = false, message = "Please Select HUTOPS Ids File to Shift Records to E-Applicaiton" });
                }
                else if (admitCardBatchModel.Type != 0 && admitCardBatchModel.Type == BatchType.Result.GetHashCode() && (admitCardBatchModel.HUTOPSIdsFile == null || admitCardBatchModel.Result == 0))
                {
                    return Json(new { status = false, message = "Please enter in all reqired fields to Update Result" });
                }




                Utility.AddLog(Constants.LogType.ActivityLog, $"Admin {Utility.GetAdminFromSession().Name} Requested to Add Batch File for generate Admit Card :");

                string uploadDirectory = HttpContext.Server.MapPath("~/UploadedFiles");
                string BatchDirectory = Path.Combine(HttpContext.Server.MapPath("~/UploadedFiles/"), "AdmitCardBatch");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                if (!Directory.Exists(BatchDirectory))
                {
                    Directory.CreateDirectory(BatchDirectory);
                }
                if (admitCardBatchModel.HUTOPSIdsFile != null)
                {
                    if(Path.GetExtension(admitCardBatchModel.HUTOPSIdsFile.FileName) != ".xlsx" && Path.GetExtension(admitCardBatchModel.HUTOPSIdsFile.FileName) != ".xls")
                    {
                        return Json(new { status = false, message = "HUTOPS Ids file is not Valid (only excel file accepted)" });
                    }
                    filePath = Path.Combine(BatchDirectory, DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(admitCardBatchModel.HUTOPSIdsFile.FileName));
                    admitCardBatchModel.HUTOPSIdsFile.SaveAs(filePath);

                    Utility.AddLog(Constants.LogType.ActivityLog, $"Upload Admit card batch file to server by {Utility.GetAdminFromSession().Name} :");


                    List<string> IdsFound = new List<string>();
                    List<string> IdsNotFound = new List<string>();


                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Assuming the Excel file has a single column in the first sheet
                            var records = new List<ExcelData>();
                            while (reader.Read())
                            {
                                records.Add(new ExcelData { HUTOPSIds = reader.GetString(0) });
                            }
                            if(records.Count > 0){
                                foreach (var record in records)
                                {
                                    var isFound = DB.PersonalInformations.ToList().Exists(x => x.HUTopId.Trim() == record.HUTOPSIds.Trim()); 
                                    if (isFound){ IdsFound.Add(record.HUTOPSIds); }
                                    else { IdsNotFound.Add(record.HUTOPSIds); }
                                }
                            }
                        }
                    }
                    DB.BatchUploads.Add(new BatchUpload
                    {
                        TestDate = admitCardBatchModel.TestDate,
                        Shift = admitCardBatchModel.Shift,
                        Venue = admitCardBatchModel.Venue,
                        Type = admitCardBatchModel.Type,
                        Result = admitCardBatchModel.Result,
                        IsRecordSendToEApp = admitCardBatchModel.IsRecordSendToEApp,
                        HUTOPSIdsFile = filePath,
                        CreatedBy = Utility.GetAdminFromSession().Email
                    }); ;
                    DB.SaveChanges();
                    Utility.AddLog(Constants.LogType.ActivityLog, $"Insert Admit card batch Record by {Utility.GetAdminFromSession().Name} :");


                    return Json(new { status = true, message = "Your File Uploaded Successfully and the response will be sent on your email address:" });
                    

                }
                else
                {
                    Utility.AddLog(Constants.LogType.ActivityLog, $"HUTOPS Ids File not provided by: {Utility.GetAdminFromSession().Name}");

                    return Json(new { status = false, message = "HUTOPS Ids File not provided" });
                }


            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Utility.AddLog(Constants.LogType.Exception, $"Error: {ve.PropertyName}, {ve.ErrorMessage}");
                    }
                }
                return Json(new { status = false, message = "Error Occur while processing your request" + ex.Message });
            }
            catch (System.Exception ex)
            {
                if (System.IO.File.Exists(filePath)) { System.IO.File.Delete(filePath); }
                
                return Json(new { status = false, message = "Error Occur while processing you request " + ex.Message});
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}