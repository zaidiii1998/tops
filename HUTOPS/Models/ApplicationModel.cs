using System.Collections.Generic;
using System.Web;

namespace HUTOPS.Models
{
    public class ApplicationModel
    {
        public PersonalInformation PersonalInfo { get; set; }
        public Educational Education { get; set; }
        public Document Document { get; set; }
        public string[] SubjectName { get; set; }
        public string[] SubjectObtain { get; set; }

        public HttpPostedFileBase CNIC { get; set; }
        public HttpPostedFileBase Photograph { get; set; }
        public HttpPostedFileBase SSCMarkSheet { get; set; }
        public HttpPostedFileBase HSSCMarkSheet { get; set; }

        // For Personal Info Form
        public List<City> City { get; set; }
        public List<Country> Country { get; set; }
        public List<State> Province { get; set; }

        // For  Education Form
        public List<EducationalSubject> Subjects { get; set; }
        public List<Board> Boards { get; set; }
        public List<BoardGroup> Groups { get; set; }
        public ApplicationModel()
        {
            PersonalInfo = new PersonalInformation();
            Education = new Educational();
            Document = new Document();
        }
    }
}