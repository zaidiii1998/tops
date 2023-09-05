﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HUTOPS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class HU_TOPSEntities : DbContext
    {
        public HU_TOPSEntities()
            : base("name=HU_TOPSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<BoardGroup> BoardGroups { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Educational> Educationals { get; set; }
        public virtual DbSet<GroupSubject> GroupSubjects { get; set; }
        public virtual DbSet<PersonalInformation> PersonalInformations { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<EducationalSubject> EducationalSubjects { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
    
        public virtual int InsertBoardGroups(string groupNames, Nullable<int> boardId)
        {
            var groupNamesParameter = groupNames != null ?
                new ObjectParameter("GroupNames", groupNames) :
                new ObjectParameter("GroupNames", typeof(string));
    
            var boardIdParameter = boardId.HasValue ?
                new ObjectParameter("BoardId", boardId) :
                new ObjectParameter("BoardId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertBoardGroups", groupNamesParameter, boardIdParameter);
        }
    
        public virtual int InsertGroupSubjects(string subjectNames, Nullable<int> groupId)
        {
            var subjectNamesParameter = subjectNames != null ?
                new ObjectParameter("SubjectNames", subjectNames) :
                new ObjectParameter("SubjectNames", typeof(string));
    
            var groupIdParameter = groupId.HasValue ?
                new ObjectParameter("GroupId", groupId) :
                new ObjectParameter("GroupId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertGroupSubjects", subjectNamesParameter, groupIdParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<WEB_UserLogin_Result> WEB_CreateUser(string firstName, string middleName, string lastName, string cNIC, string cellPhoneNumber, string emailAddress, string password)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var middleNameParameter = middleName != null ?
                new ObjectParameter("MiddleName", middleName) :
                new ObjectParameter("MiddleName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var cNICParameter = cNIC != null ?
                new ObjectParameter("CNIC", cNIC) :
                new ObjectParameter("CNIC", typeof(string));
    
            var cellPhoneNumberParameter = cellPhoneNumber != null ?
                new ObjectParameter("CellPhoneNumber", cellPhoneNumber) :
                new ObjectParameter("CellPhoneNumber", typeof(string));
    
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("EmailAddress", emailAddress) :
                new ObjectParameter("EmailAddress", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_UserLogin_Result>("WEB_CreateUser", firstNameParameter, middleNameParameter, lastNameParameter, cNICParameter, cellPhoneNumberParameter, emailAddressParameter, passwordParameter);
        }
    
        public virtual ObjectResult<WEB_GetAll_Result> WEB_GetAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_GetAll_Result>("WEB_GetAll");
        }
    
        public virtual ObjectResult<WEB_GetBoards_Result> WEB_GetBoards()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_GetBoards_Result>("WEB_GetBoards");
        }
    
        public virtual ObjectResult<WEB_GetGroups_Result> WEB_GetGroups(Nullable<int> boardId)
        {
            var boardIdParameter = boardId.HasValue ?
                new ObjectParameter("BoardId", boardId) :
                new ObjectParameter("BoardId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_GetGroups_Result>("WEB_GetGroups", boardIdParameter);
        }
    
        public virtual ObjectResult<WEB_GetSubjects_Result> WEB_GetSubjects(Nullable<int> groupId)
        {
            var groupIdParameter = groupId.HasValue ?
                new ObjectParameter("GroupId", groupId) :
                new ObjectParameter("GroupId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_GetSubjects_Result>("WEB_GetSubjects", groupIdParameter);
        }
    
        public virtual ObjectResult<WEB_InsertEducation_Result> WEB_InsertEducation(Nullable<int> userId, string currentLevel, string currentCollege, string collegeAddress, string collegeST, string collegeCD, string hSSCPercentage, string boardOfEdu, string group, string schoolName, string schoolAddress, string sSCPercentage, string universityName, string intendedProgram, string subjectName, string subjectObtain, string subjectTotal, string subjectGrade)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var currentLevelParameter = currentLevel != null ?
                new ObjectParameter("CurrentLevel", currentLevel) :
                new ObjectParameter("CurrentLevel", typeof(string));
    
            var currentCollegeParameter = currentCollege != null ?
                new ObjectParameter("CurrentCollege", currentCollege) :
                new ObjectParameter("CurrentCollege", typeof(string));
    
            var collegeAddressParameter = collegeAddress != null ?
                new ObjectParameter("CollegeAddress", collegeAddress) :
                new ObjectParameter("CollegeAddress", typeof(string));
    
            var collegeSTParameter = collegeST != null ?
                new ObjectParameter("CollegeST", collegeST) :
                new ObjectParameter("CollegeST", typeof(string));
    
            var collegeCDParameter = collegeCD != null ?
                new ObjectParameter("CollegeCD", collegeCD) :
                new ObjectParameter("CollegeCD", typeof(string));
    
            var hSSCPercentageParameter = hSSCPercentage != null ?
                new ObjectParameter("HSSCPercentage", hSSCPercentage) :
                new ObjectParameter("HSSCPercentage", typeof(string));
    
            var boardOfEduParameter = boardOfEdu != null ?
                new ObjectParameter("BoardOfEdu", boardOfEdu) :
                new ObjectParameter("BoardOfEdu", typeof(string));
    
            var groupParameter = group != null ?
                new ObjectParameter("Group", group) :
                new ObjectParameter("Group", typeof(string));
    
            var schoolNameParameter = schoolName != null ?
                new ObjectParameter("SchoolName", schoolName) :
                new ObjectParameter("SchoolName", typeof(string));
    
            var schoolAddressParameter = schoolAddress != null ?
                new ObjectParameter("SchoolAddress", schoolAddress) :
                new ObjectParameter("SchoolAddress", typeof(string));
    
            var sSCPercentageParameter = sSCPercentage != null ?
                new ObjectParameter("SSCPercentage", sSCPercentage) :
                new ObjectParameter("SSCPercentage", typeof(string));
    
            var universityNameParameter = universityName != null ?
                new ObjectParameter("UniversityName", universityName) :
                new ObjectParameter("UniversityName", typeof(string));
    
            var intendedProgramParameter = intendedProgram != null ?
                new ObjectParameter("IntendedProgram", intendedProgram) :
                new ObjectParameter("IntendedProgram", typeof(string));
    
            var subjectNameParameter = subjectName != null ?
                new ObjectParameter("SubjectName", subjectName) :
                new ObjectParameter("SubjectName", typeof(string));
    
            var subjectObtainParameter = subjectObtain != null ?
                new ObjectParameter("SubjectObtain", subjectObtain) :
                new ObjectParameter("SubjectObtain", typeof(string));
    
            var subjectTotalParameter = subjectTotal != null ?
                new ObjectParameter("SubjectTotal", subjectTotal) :
                new ObjectParameter("SubjectTotal", typeof(string));
    
            var subjectGradeParameter = subjectGrade != null ?
                new ObjectParameter("SubjectGrade", subjectGrade) :
                new ObjectParameter("SubjectGrade", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_InsertEducation_Result>("WEB_InsertEducation", userIdParameter, currentLevelParameter, currentCollegeParameter, collegeAddressParameter, collegeSTParameter, collegeCDParameter, hSSCPercentageParameter, boardOfEduParameter, groupParameter, schoolNameParameter, schoolAddressParameter, sSCPercentageParameter, universityNameParameter, intendedProgramParameter, subjectNameParameter, subjectObtainParameter, subjectTotalParameter, subjectGradeParameter);
        }
    
        public virtual int WEB_InsertEducationalSubjects(Nullable<int> educationalId, string subjectName, string subjectObtain, string subjectTotal, string subjectGrade)
        {
            var educationalIdParameter = educationalId.HasValue ?
                new ObjectParameter("EducationalId", educationalId) :
                new ObjectParameter("EducationalId", typeof(int));
    
            var subjectNameParameter = subjectName != null ?
                new ObjectParameter("SubjectName", subjectName) :
                new ObjectParameter("SubjectName", typeof(string));
    
            var subjectObtainParameter = subjectObtain != null ?
                new ObjectParameter("SubjectObtain", subjectObtain) :
                new ObjectParameter("SubjectObtain", typeof(string));
    
            var subjectTotalParameter = subjectTotal != null ?
                new ObjectParameter("SubjectTotal", subjectTotal) :
                new ObjectParameter("SubjectTotal", typeof(string));
    
            var subjectGradeParameter = subjectGrade != null ?
                new ObjectParameter("SubjectGrade", subjectGrade) :
                new ObjectParameter("SubjectGrade", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WEB_InsertEducationalSubjects", educationalIdParameter, subjectNameParameter, subjectObtainParameter, subjectTotalParameter, subjectGradeParameter);
        }
    
        public virtual int WEB_UpdateAddressDetails(Nullable<int> id, string residentialAddress, string residentialCountry, string residentialProvince, string residentialCity, string residentialCityOther, string residentialPostalCode, string permanentAddress, string permanentCountry, string permanentProvince, string permanentCity, string permanentCityOther, string permanentPostalCode)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var residentialAddressParameter = residentialAddress != null ?
                new ObjectParameter("ResidentialAddress", residentialAddress) :
                new ObjectParameter("ResidentialAddress", typeof(string));
    
            var residentialCountryParameter = residentialCountry != null ?
                new ObjectParameter("ResidentialCountry", residentialCountry) :
                new ObjectParameter("ResidentialCountry", typeof(string));
    
            var residentialProvinceParameter = residentialProvince != null ?
                new ObjectParameter("ResidentialProvince", residentialProvince) :
                new ObjectParameter("ResidentialProvince", typeof(string));
    
            var residentialCityParameter = residentialCity != null ?
                new ObjectParameter("ResidentialCity", residentialCity) :
                new ObjectParameter("ResidentialCity", typeof(string));
    
            var residentialCityOtherParameter = residentialCityOther != null ?
                new ObjectParameter("ResidentialCityOther", residentialCityOther) :
                new ObjectParameter("ResidentialCityOther", typeof(string));
    
            var residentialPostalCodeParameter = residentialPostalCode != null ?
                new ObjectParameter("ResidentialPostalCode", residentialPostalCode) :
                new ObjectParameter("ResidentialPostalCode", typeof(string));
    
            var permanentAddressParameter = permanentAddress != null ?
                new ObjectParameter("PermanentAddress", permanentAddress) :
                new ObjectParameter("PermanentAddress", typeof(string));
    
            var permanentCountryParameter = permanentCountry != null ?
                new ObjectParameter("PermanentCountry", permanentCountry) :
                new ObjectParameter("PermanentCountry", typeof(string));
    
            var permanentProvinceParameter = permanentProvince != null ?
                new ObjectParameter("PermanentProvince", permanentProvince) :
                new ObjectParameter("PermanentProvince", typeof(string));
    
            var permanentCityParameter = permanentCity != null ?
                new ObjectParameter("PermanentCity", permanentCity) :
                new ObjectParameter("PermanentCity", typeof(string));
    
            var permanentCityOtherParameter = permanentCityOther != null ?
                new ObjectParameter("PermanentCityOther", permanentCityOther) :
                new ObjectParameter("PermanentCityOther", typeof(string));
    
            var permanentPostalCodeParameter = permanentPostalCode != null ?
                new ObjectParameter("PermanentPostalCode", permanentPostalCode) :
                new ObjectParameter("PermanentPostalCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WEB_UpdateAddressDetails", idParameter, residentialAddressParameter, residentialCountryParameter, residentialProvinceParameter, residentialCityParameter, residentialCityOtherParameter, residentialPostalCodeParameter, permanentAddressParameter, permanentCountryParameter, permanentProvinceParameter, permanentCityParameter, permanentCityOtherParameter, permanentPostalCodeParameter);
        }
    
        public virtual int WEB_UpdateContactDetails(Nullable<int> id, string cellPhone, string whatsApp, string altCellPhone, string homeCellPhone, string altLandline, string guardianCellPhone, string guardianEmail)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var cellPhoneParameter = cellPhone != null ?
                new ObjectParameter("CellPhone", cellPhone) :
                new ObjectParameter("CellPhone", typeof(string));
    
            var whatsAppParameter = whatsApp != null ?
                new ObjectParameter("WhatsApp", whatsApp) :
                new ObjectParameter("WhatsApp", typeof(string));
    
            var altCellPhoneParameter = altCellPhone != null ?
                new ObjectParameter("AltCellPhone", altCellPhone) :
                new ObjectParameter("AltCellPhone", typeof(string));
    
            var homeCellPhoneParameter = homeCellPhone != null ?
                new ObjectParameter("HomeCellPhone", homeCellPhone) :
                new ObjectParameter("HomeCellPhone", typeof(string));
    
            var altLandlineParameter = altLandline != null ?
                new ObjectParameter("AltLandline", altLandline) :
                new ObjectParameter("AltLandline", typeof(string));
    
            var guardianCellPhoneParameter = guardianCellPhone != null ?
                new ObjectParameter("GuardianCellPhone", guardianCellPhone) :
                new ObjectParameter("GuardianCellPhone", typeof(string));
    
            var guardianEmailParameter = guardianEmail != null ?
                new ObjectParameter("GuardianEmail", guardianEmail) :
                new ObjectParameter("GuardianEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WEB_UpdateContactDetails", idParameter, cellPhoneParameter, whatsAppParameter, altCellPhoneParameter, homeCellPhoneParameter, altLandlineParameter, guardianCellPhoneParameter, guardianEmailParameter);
        }
    
        public virtual int WEB_UpdatePersonalInfo(Nullable<int> id, string fName, string mName, string lName, string fatherFName, string fatherMName, string fatherLName, string gender, string husbandName, string dOB, string cNIC, string email, string alterEmail)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var fNameParameter = fName != null ?
                new ObjectParameter("FName", fName) :
                new ObjectParameter("FName", typeof(string));
    
            var mNameParameter = mName != null ?
                new ObjectParameter("MName", mName) :
                new ObjectParameter("MName", typeof(string));
    
            var lNameParameter = lName != null ?
                new ObjectParameter("LName", lName) :
                new ObjectParameter("LName", typeof(string));
    
            var fatherFNameParameter = fatherFName != null ?
                new ObjectParameter("FatherFName", fatherFName) :
                new ObjectParameter("FatherFName", typeof(string));
    
            var fatherMNameParameter = fatherMName != null ?
                new ObjectParameter("FatherMName", fatherMName) :
                new ObjectParameter("FatherMName", typeof(string));
    
            var fatherLNameParameter = fatherLName != null ?
                new ObjectParameter("FatherLName", fatherLName) :
                new ObjectParameter("FatherLName", typeof(string));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var husbandNameParameter = husbandName != null ?
                new ObjectParameter("HusbandName", husbandName) :
                new ObjectParameter("HusbandName", typeof(string));
    
            var dOBParameter = dOB != null ?
                new ObjectParameter("DOB", dOB) :
                new ObjectParameter("DOB", typeof(string));
    
            var cNICParameter = cNIC != null ?
                new ObjectParameter("CNIC", cNIC) :
                new ObjectParameter("CNIC", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var alterEmailParameter = alterEmail != null ?
                new ObjectParameter("AlterEmail", alterEmail) :
                new ObjectParameter("AlterEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WEB_UpdatePersonalInfo", idParameter, fNameParameter, mNameParameter, lNameParameter, fatherFNameParameter, fatherMNameParameter, fatherLNameParameter, genderParameter, husbandNameParameter, dOBParameter, cNICParameter, emailParameter, alterEmailParameter);
        }
    
        public virtual ObjectResult<WEB_UserLogin_Result> WEB_UserLogin(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_UserLogin_Result>("WEB_UserLogin", emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<WEB_UserLogin_Result> WEB_CheckPersonalInfo(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WEB_UserLogin_Result>("WEB_CheckPersonalInfo", idParameter);
        }
    
        public virtual int WEB_UpdatePersonal(Nullable<int> id, string fName, string mName, string lName, string fatherFName, string fatherMName, string fatherLName, string gender, string husbandName, string dOB, string cNIC, string email, string alterEmail, string cellPhone, string whatsApp, string altCellPhone, string homeCellPhone, string altLandline, string guardianCellPhone, string guardianEmail, string residentialAddress, string residentialCountry, string residentialProvince, string residentialCity, string residentialCityOther, string residentialPostalCode, string permanentAddress, string permanentCountry, string permanentProvince, string permanentCity, string permanentCityOther, string permanentPostalCode)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));
    
            var fNameParameter = fName != null ?
                new ObjectParameter("FName", fName) :
                new ObjectParameter("FName", typeof(string));
    
            var mNameParameter = mName != null ?
                new ObjectParameter("MName", mName) :
                new ObjectParameter("MName", typeof(string));
    
            var lNameParameter = lName != null ?
                new ObjectParameter("LName", lName) :
                new ObjectParameter("LName", typeof(string));
    
            var fatherFNameParameter = fatherFName != null ?
                new ObjectParameter("FatherFName", fatherFName) :
                new ObjectParameter("FatherFName", typeof(string));
    
            var fatherMNameParameter = fatherMName != null ?
                new ObjectParameter("FatherMName", fatherMName) :
                new ObjectParameter("FatherMName", typeof(string));
    
            var fatherLNameParameter = fatherLName != null ?
                new ObjectParameter("FatherLName", fatherLName) :
                new ObjectParameter("FatherLName", typeof(string));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var husbandNameParameter = husbandName != null ?
                new ObjectParameter("HusbandName", husbandName) :
                new ObjectParameter("HusbandName", typeof(string));
    
            var dOBParameter = dOB != null ?
                new ObjectParameter("DOB", dOB) :
                new ObjectParameter("DOB", typeof(string));
    
            var cNICParameter = cNIC != null ?
                new ObjectParameter("CNIC", cNIC) :
                new ObjectParameter("CNIC", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var alterEmailParameter = alterEmail != null ?
                new ObjectParameter("AlterEmail", alterEmail) :
                new ObjectParameter("AlterEmail", typeof(string));
    
            var cellPhoneParameter = cellPhone != null ?
                new ObjectParameter("CellPhone", cellPhone) :
                new ObjectParameter("CellPhone", typeof(string));
    
            var whatsAppParameter = whatsApp != null ?
                new ObjectParameter("WhatsApp", whatsApp) :
                new ObjectParameter("WhatsApp", typeof(string));
    
            var altCellPhoneParameter = altCellPhone != null ?
                new ObjectParameter("AltCellPhone", altCellPhone) :
                new ObjectParameter("AltCellPhone", typeof(string));
    
            var homeCellPhoneParameter = homeCellPhone != null ?
                new ObjectParameter("HomeCellPhone", homeCellPhone) :
                new ObjectParameter("HomeCellPhone", typeof(string));
    
            var altLandlineParameter = altLandline != null ?
                new ObjectParameter("AltLandline", altLandline) :
                new ObjectParameter("AltLandline", typeof(string));
    
            var guardianCellPhoneParameter = guardianCellPhone != null ?
                new ObjectParameter("GuardianCellPhone", guardianCellPhone) :
                new ObjectParameter("GuardianCellPhone", typeof(string));
    
            var guardianEmailParameter = guardianEmail != null ?
                new ObjectParameter("GuardianEmail", guardianEmail) :
                new ObjectParameter("GuardianEmail", typeof(string));
    
            var residentialAddressParameter = residentialAddress != null ?
                new ObjectParameter("ResidentialAddress", residentialAddress) :
                new ObjectParameter("ResidentialAddress", typeof(string));
    
            var residentialCountryParameter = residentialCountry != null ?
                new ObjectParameter("ResidentialCountry", residentialCountry) :
                new ObjectParameter("ResidentialCountry", typeof(string));
    
            var residentialProvinceParameter = residentialProvince != null ?
                new ObjectParameter("ResidentialProvince", residentialProvince) :
                new ObjectParameter("ResidentialProvince", typeof(string));
    
            var residentialCityParameter = residentialCity != null ?
                new ObjectParameter("ResidentialCity", residentialCity) :
                new ObjectParameter("ResidentialCity", typeof(string));
    
            var residentialCityOtherParameter = residentialCityOther != null ?
                new ObjectParameter("ResidentialCityOther", residentialCityOther) :
                new ObjectParameter("ResidentialCityOther", typeof(string));
    
            var residentialPostalCodeParameter = residentialPostalCode != null ?
                new ObjectParameter("ResidentialPostalCode", residentialPostalCode) :
                new ObjectParameter("ResidentialPostalCode", typeof(string));
    
            var permanentAddressParameter = permanentAddress != null ?
                new ObjectParameter("PermanentAddress", permanentAddress) :
                new ObjectParameter("PermanentAddress", typeof(string));
    
            var permanentCountryParameter = permanentCountry != null ?
                new ObjectParameter("PermanentCountry", permanentCountry) :
                new ObjectParameter("PermanentCountry", typeof(string));
    
            var permanentProvinceParameter = permanentProvince != null ?
                new ObjectParameter("PermanentProvince", permanentProvince) :
                new ObjectParameter("PermanentProvince", typeof(string));
    
            var permanentCityParameter = permanentCity != null ?
                new ObjectParameter("PermanentCity", permanentCity) :
                new ObjectParameter("PermanentCity", typeof(string));
    
            var permanentCityOtherParameter = permanentCityOther != null ?
                new ObjectParameter("PermanentCityOther", permanentCityOther) :
                new ObjectParameter("PermanentCityOther", typeof(string));
    
            var permanentPostalCodeParameter = permanentPostalCode != null ?
                new ObjectParameter("PermanentPostalCode", permanentPostalCode) :
                new ObjectParameter("PermanentPostalCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("WEB_UpdatePersonal", idParameter, fNameParameter, mNameParameter, lNameParameter, fatherFNameParameter, fatherMNameParameter, fatherLNameParameter, genderParameter, husbandNameParameter, dOBParameter, cNICParameter, emailParameter, alterEmailParameter, cellPhoneParameter, whatsAppParameter, altCellPhoneParameter, homeCellPhoneParameter, altLandlineParameter, guardianCellPhoneParameter, guardianEmailParameter, residentialAddressParameter, residentialCountryParameter, residentialProvinceParameter, residentialCityParameter, residentialCityOtherParameter, residentialPostalCodeParameter, permanentAddressParameter, permanentCountryParameter, permanentProvinceParameter, permanentCityParameter, permanentCityOtherParameter, permanentPostalCodeParameter);
        }
    }
}
