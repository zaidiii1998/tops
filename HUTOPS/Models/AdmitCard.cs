using System;
using System.Web;

namespace HUTOPS.Models
{
    public class AdmitCardBatchModel
    {
        public DateTime TestDate { get; set; }
        public string Shift { get; set; }
        public string Vanue { get; set; }
        public HttpPostedFileBase HUTOPSIdsFile { get; set; }
    }
    public class ExcelData
    {
        public string HUTOPSIds { get; set; }
    }
}