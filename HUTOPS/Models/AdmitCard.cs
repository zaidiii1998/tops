using System;
using System.Web;

namespace HUTOPS.Models
{
    public class AdmitCardBatchModel
    {
        public DateTime TestDate { get; set; }
        public string Shift { get; set; }
        public string Venue { get; set; }
        public byte Type { get; set; }
        public byte Result { get; set; }
        public HttpPostedFileBase HUTOPSIdsFile { get; set; }
    }
    public class AdmitCardModel
    {
        public int Id { get; set; }
        public DateTime TestDate { get; set; }
        public string Shift { get; set; }
        public string Venue { get; set; }
    }
    public class ExcelData
    {
        public string HUTOPSIds { get; set; }
    }
}