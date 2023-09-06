using System.Collections.Generic;

namespace HUTOPS.Models
{
    public class PersonalInfoPageModel
    {
        public PersonalInformation Main { get; set; }
        public List<City> City { get; set; }
        public List<Country> Country { get; set; }
        public List<State> Province { get; set; }
    }
}