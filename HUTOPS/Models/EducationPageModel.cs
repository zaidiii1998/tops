using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HUTOPS.Models
{
    public class EducationPageModel
    {
        public Educational Education { get; set; }
        public List<EducationalSubject> Subjects { get; set; }
    }
}