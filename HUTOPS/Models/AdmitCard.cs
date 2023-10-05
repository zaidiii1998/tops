using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HUTOPS.Models
{
    public class AdmitCardBatchModel
    {
        [Required]
        public DateTime TestDate { get; set; }
        [Required]
        public string Shift { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        public byte Type { get; set; }
        public byte Result { get; set; }
        public HttpPostedFileBase HUTOPSIdsFile { get; set; }
    }
    public class AdmitCardModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime TestDate { get; set; }
        [Required]

        public string Shift { get; set; }
        [Required]
        public string Venue { get; set; }
    }
    public class ExcelData
    {
        public string HUTOPSIds { get; set; }
    }
}