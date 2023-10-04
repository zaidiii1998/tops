using ExcelDataReader;
using HUTOPS.Helper;
using HUTOPS.Models;
using System;
using System.Collections.Generic;
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
                        HUTOPSIdsFile = filePath,
                        CreatedBy = Utility.GetAdminFromSession().Name
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
            catch (System.Exception ex)
            {
                if (System.IO.File.Exists(filePath)) { System.IO.File.Delete(filePath); }
                
                return Json(new { status = false, message = "Error Occur while processing you request" + ex.Message});
            }
        }
    }
}