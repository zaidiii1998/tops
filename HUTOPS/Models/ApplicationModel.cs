﻿namespace HUTOPS.Models
{
    public class ApplicationModel
    {
        public PersonalInformation PersonalInfo { get; set; }
        public Educational Education { get; set; }
        public Document Document { get; set; }
        public ApplicationModel()
        {
            PersonalInfo = new PersonalInformation();
            Education = new Educational();
            Document = new Document();
        }
    }
}