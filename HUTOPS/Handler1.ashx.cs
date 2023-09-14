using HUTOPS.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace HUTOPS
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        HU_TOPSEntities DB = new HU_TOPSEntities();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User-requested to Submit Documents.");
                var userId = int.Parse(context.Request["UserId"]);
                var isSuccess = true;
                HttpPostedFile CnicFile = context.Request.Files["CNIC"];
                HttpPostedFile Photograph = context.Request.Files["Photograph"];
                HttpPostedFile SSC = context.Request.Files["SSCMarkSheet"];
                HttpPostedFile HSSC = context.Request.Files["HSSCMarkSheet"];

                string CnicPath = "";
                string SSCPath = "";
                string HSSCPath = "";
                string PhotographPath = "";

                List<string> Err = new List<string>();

                string uploadDirectory = context.Server.MapPath("~/UploadedFiles");
                string UserDirectory = context.Server.MapPath("~/UploadedFiles/" + userId);
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                if (!Directory.Exists(UserDirectory))
                {
                    Directory.CreateDirectory(UserDirectory);
                }
                if ((SSC != null && SSC.ContentLength > 0) && (Photograph != null && Photograph.ContentLength > 0))
                {
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User-provided Documents are validated.");
                    // Check if files are present
                    if (CnicFile != null && CnicFile.ContentLength > 0)
                    {
                        CnicPath = Path.Combine(UserDirectory, "CNIC.jpeg" );
                        CnicFile.SaveAs(CnicPath);
                    }
                    if (SSC != null && SSC.ContentLength > 0)
                    {
                        SSCPath = Path.Combine(UserDirectory, "SSC Mark Sheet.jpeg");
                        SSC.SaveAs(SSCPath);
                    }
                    if (HSSC != null && HSSC.ContentLength > 0)
                    {
                        HSSCPath = Path.Combine(UserDirectory, "HSSC Mark Sheet.jpeg");
                        HSSC.SaveAs(HSSCPath);
                    }
                    if (Photograph != null && Photograph.ContentLength > 0)
                    {
                        PhotographPath = Path.Combine(UserDirectory, "Photo.jpeg");
                        Photograph.SaveAs(PhotographPath);
                    }
                    var document = DB.Documents.Where(x => x.UserId == userId).ToList().FirstOrDefault();
                    if (document != null)
                    {
                        document.CNIC = CnicPath;
                        document.Photograph = PhotographPath;
                        document.SSCMarkSheet = SSCPath;
                        document.HSSCMarkSheet = HSSCPath;
                        document.IsCompleted = 1;
                        DB.SaveChanges();
                    }
                    else
                    {
                        DB.Documents.Add(new Document
                        {
                            UserId = userId,
                            CNIC = CnicPath,
                            Photograph = PhotographPath,
                            SSCMarkSheet = SSCPath,
                            HSSCMarkSheet = HSSCPath,
                            IsCompleted = 1
                        });
                        DB.SaveChanges();
                    }
                    Helper.Utility.AddLog(Constants.LogType.ActivityLog, $"User successfully submited Documents.");

                    context.Response.ContentType = "application/json";
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    context.Response.Write(js.Serialize(new {status = true, message = "Document Section submited Successfully"}));
                }
                else
                {
                    Err.Add("SSC Mark sheet: is required");
                    Err.Add("Passport size Photograph: is required");
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    context.Response.ContentType = "application/json";
                    context.Response.Write(js.Serialize(new { status = false, message = "Document Section submition Failed", error = Err }));
                }
            }
            catch (Exception)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                context.Response.ContentType = "application/json";
                context.Response.Write(js.Serialize(new { status = false, message = "Document Section submition Failed" }));
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}