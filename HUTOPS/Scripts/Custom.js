﻿$(document).ready(function () {
    //document.addEventListener('scroll', handleScroll, true);
    $('.number').inputmask("\\92399-9999999");
    $('.cnic').inputmask("99999-9999999-9");
    $('.Percentage').inputmask("99.99%");

    $('.trashActivity').click(function () {
        $(this).closest(".row").remove();
    });
    tinymce.init({
        selector: '.tinyMceTxt',
        plugins: "preview powerpaste casechange searchreplace autolink autosave save directionality advcode visualblocks visualchars fullscreen image link media mediaembed template codesample advtable table charmap pagebreak nonbreaking anchor advlist lists checklist wordcount tinymcespellchecker a11ychecker help formatpainter permanentpen pageembed linkchecker emoticons export",
        height: '700px',
        toolbar_sticky: true,
        icons: 'thin',
        autosave_restore_when_empty: false,
        content_style: `
                body {
                    background: #fff;
                }

                @media (min-width: 840px) {
                    html {
                        background: #eceef4;
                        min-height: 100%;
                        padding: 0 .5rem
                    }

                    body {
                        background-color: #fff;
                        box-shadow: 0 0 4px rgba(0, 0, 0, .15);
                        box-sizing: border-box;
                        margin: 1rem auto 0;
                        max-width: 820px;
                        min-height: calc(100vh - 1rem);
                        padding:4rem 6rem 6rem 6rem
                    }
                }
            `,
            

    });

});


function showLoader() {
    $('.overlay').show();
    $('.adminDashboard').removeClass('loaded');
}
function hideLoader() {
    $('.overlay').hide();
    $('.adminDashboard').addClass('loaded');
}

//expandSideBarBtn
$("#expandSideBarBtn").click(function () {
    $("#sidebarDashboard").toggleClass("expandedSidebar");
    $("#contentWrapperDashboard").toggleClass("contentWrapAdon");
    $(".adminDashboard").toggleClass("sidexpendBody");
    
});



function increaseProgressBarWidth() {
    var percent = 0;
    var isValidPersonalInfo = true;
    $('#PersonalInfoForm input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValidPersonalInfo = false;
        }
    });
    $('#PersonalInfoForm select[required]').each(function () {

        if (($(this).val() === '') || ($(this).val() === null)) {
            isValidPersonalInfo = false;
        }
    });
    $('#PersonalInfoForm textarea[required]').each(function () {

        if (($(this).val().trim() === '') || ($(this).val() === null)) {
            isValidPersonalInfo = false;
        }
    });

    $("input[name=IsAppliedBefore]").each(function () {
        var checked = $("input[name=IsAppliedBefore]:checked");
        if (checked.length == 0) {
            isValidPersonalInfo = false;
            $('#errAppliedBefore').html('field is required');
        } else {
            $('#errAppliedBefore').html('');
        }
    });
    if ($("input[name='IsAppliedBefore']:checked").val() === '1') {
        if (($('#AppliedBeforeId').val().trim() == '')) {
            $('#AppliedBeforeId').addClass('error');
            $('#AppliedBeforeId').focus();
            isValidPersonalInfo = false;
        } else { $('#AppliedBeforeId').removeClass('error'); }
        if ($('#AppliedBeforeYear :selected').val() === "") {
            $('#AppliedBeforeYear').addClass('error');
            $('#AppliedBeforeYear').focus();
            isValidPersonalInfo = false;
        } else { $('#AppliedBeforeYear').removeClass('error'); }
    }


    if (isValidPersonalInfo) { percent = percent + 35 }

    var isValidEducation = true;
    $('#EducationForm input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValidEducation = false;
        }
    });
    $('#EducationForm select[required]').each(function () {
        
        if (($(this).val() === '') || ($(this).val() === null)) {
            isValidEducation = false;
        }
    });
    $('#EducationForm textarea[required]').each(function () {

        if (($(this).val().trim() === '') || ($(this).val() === null)) {
            isValidEducation = false;
        }
    });

    if ($('input[name="huSchool"]:checked').val() == "" || $('input[name="huSchool"]:checked').val() == null) {
        isValidEducation = false;
        $('#errHUSchool').html("Please select School Name");
    } else {
        $('#errHUSchool').html("");
    }
    if ($('#currentLevel').val() == 'Already enrolled in a University' && $('#universityName').val() == '') {
        isValidEducation = false;
        $('#errUniversityName').html('University Name is required');
    } else {
        $('#errUniversityName').html('');
    }

    if (isValidEducation) { percent = percent + 35 }

    isValidDocument = true;
    $('input[type="file"][required]').each(function () {

        if ($(this).val().trim() === '') {
            isValidDocument = false;
        }
    });
    if (isValidDocument) { percent = percent + 10}

    if ($('#TestDate').val() != null && $('#TestDate').val() != "") { percent = percent + 10 }

    var check1 = $('#validateLaw').is(':checked');
    var check2 = $('#invalidInfo').is(':checked');
    var check3 = $('#validInfo').is(':checked');
    if (check1 == true && check2 == true && check3 == true) { percent = percent + 10 }

    let containerWidth = document.getElementById('barContainer').clientWidth;  // get the container width
    let amount = Math.round(containerWidth * percent / 100);      // get amount of pixels the bars width needs to increase
    let barWidth = document.getElementById('progressBar').offsetWidth;  // get the current bar width

    // if the bar width + amount of pixels to increase exceeds the container's width, reduce it accordingly
    //if (barWidth + amount > containerWidth) {
    //    return false   // we reached 100% so clear the interval
    //}

    let totalPercent = Math.round((amount) * 100 / containerWidth); // calculate the total percent finished

    document.getElementById('progressBar').style.width = amount + "px";    // increase bar width
    document.getElementById('progressBar').innerHTML = totalPercent + "%";           // update the percentage text
}




function CallAsyncService(url, Param, CallbackFunction) {
    try {
        $.ajax({
            type: "POST",
            url: url,
            data: Param,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            timeout: (1000 * 60 * 4),
            success: function (data) {
                CallbackFunction(data);
            },
            error: function (xhr) {
                if (xhr.responseText) {
                    var err = xhr.responseText;
                    if (err) {
                        ShowDivError(err);

                    }
                    else {
                        ShowDivError('Error');
                    }
                }
            }
        });
    }
    catch (Err) {
        ShowDivError(Err);
    }
    return false;
}

function CallFileAsyncService(url, fileData, CallbackFunction) {
    try {
        $.ajax({
            type: "POST",
            url: url,
            data: fileData,
            contentType: false,
            processData: false,
            async: true,
            timeout: (1000 * 60 * 4),
            success: function (data) {
                CallbackFunction(data);
            },
            error: function (xhr) {
                if (xhr.responseText) {
                    var err = xhr.responseText;
                    if (err) {
                        ShowDivError(err);
                    }
                    else {
                        ShowDivError('Error');
                    }
                }
            }
        });
    }
    catch (Err) {
        ShowDivError(Err);
    }
    return false;
}

function ShowDivSuccess(msg) {
    $.notify(msg, "success");
}
function ShowDivError(msg) {
    $.notify(msg, "error");
}


function checkPhoneNumber(Id) {
    // Check Phone Number
    CallAsyncService('/Common/CheckPhoneNumber?number=' + $('#' + Id).val(), null, checkPhoneNumberCB);

    function checkPhoneNumberCB(response) {
        if (!response.status) {
            msg = response.message;
            $('#numberError').html(msg);
            return false;
        } else {
            $('#numberError').html('');
            return true;
        }
    }
}

function checkEmail(Id) {
    // Check Email

    CallAsyncService('/Common/CheckEmail?email=' + $('#' + Id).val(), null, checkEmail);

    function checkEmail(response) {
        if (!response.status) {
            msg = response.message;
            $('#emailError').html(msg);
            return false;
        } else {
            $('#emailError').html('');
            return true;
        }
    }
}

function checkCNIC(Id) {
    // Check CNIC

    CallAsyncService('/Common/CheckCNIC?cnic=' + $('#' + Id).val(), null, checkCNICCB);

    function checkCNICCB(response) {
        if (!response.status) {
            msg = response.message;
            $('#cnicError').html(msg);
            return false;
        } else {
            $('#cnicError').html('');
            return true;
        }
    }
}

function validatePassword(passId, cpassId) {
    var pass = document.getElementById(passId);
    var cpass = document.getElementById(cpassId);
    if (pass.value != cpass.value || pass.value.length < 6) {
        pass.style.borderBlockColor = "red";
        cpass.style.borderBlockColor = "red";
        return false;

    } else {
        pass.style.borderBlockColor = "green";
        cpass.style.borderBlockColor = "green";
        return true;
    }
}

function validateName(inputId, errSpanId) {
    var name = document.getElementById(inputId);
    if (name.value.length < 3) {
        $('#' + errSpanId).html('Name should contain minimum 3 characters');
        return false;

    } else {
        $('#' + errSpanId).html('');
        return true;
    }
}

function validateEmail(inputId, errSpanId) {

    const email = $('#' + inputId).val();
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (!emailRegex.test(email)) {
        debugger
        $('#' + errSpanId).html("Please Enter a valid Email Address");
        return false;
    } else {
        $('#' + errSpanId).html("");
        return true
    }
}


function validateText(input) {
    // Remove non-numeric characters using a regular expression
    input.value = input.value.replace(/[^A-Za-z]/g, '');
    //input.value = input.value.replace(/[^!@#$%^&*()_+{}\[\]:;<>,.?~\\\-]/g, '');
}
function validateNumber(input) {
    input.value = input.value.replace(/[^0-9.]/g, '');
}


// Personal Information Page Functions
function toggleOtherCityInput(cityId, otherCityId) {
    var select = document.getElementById(cityId.toString(), otherCityId.toString());
    var otherInput = document.getElementById(otherCityId.toString());

    if (cityId == 'residentialCity') {
        $('#txtResidentialCity').val($('#residentialCity :selected').text())
    } else {
        $('#txtPermanentCity').val($('#permanentCity :selected').text())
    }

    if (select.value === "other") {
        otherInput.style.display = "block";
    } else {
        otherInput.style.display = "none";
    }
}
function toggleFieldInput(comboId,OnValue, fieldToShow) {
    var select = document.getElementById(comboId.toString());
    var otherInput = document.getElementById(fieldToShow.toString());

    if (select.value === OnValue) {
        otherInput.style.display = "block";
    } else {
        otherInput.style.display = "none";
    }
}


function toggleFormSection(sectionId, checkboxId) {
    var section = document.getElementById(sectionId);
    var checkbox = document.getElementById(checkboxId);

    section.style.display = checkbox.checked ? "block" : "none";
}
function updateCities(comboProvinceId, ComboCityId) {
    var provinceId = $('#' + comboProvinceId).val();

    if (comboProvinceId == 'residentialProvince') {
        $('#txtResidentialProvince').val($('#residentialProvince :selected').text())
    } else {
        $('#txtPermanentProvince').val($('#permanentProvince :selected').text())
    }
    CallAsyncService("/Common/GetCities?ProvinceId=" + provinceId, null, updateCitiesCB)
    function updateCitiesCB(response) {
        $('#' + ComboCityId).html('');
        $('#' + ComboCityId).append(new Option("Select City", "", false, false));
        $.each(response, function (key, value) {

            if (value.Name == "Karachi") {
                $('#' + ComboCityId).append(new Option(value.Name, value.Id, true, true));
            } else {
                $('#' + ComboCityId).append(new Option(value.Name, value.Id, false, false));
            }
        })
        $('#' + ComboCityId).append(new Option("Other", "other", false, false));

    }
}

function updateProvince(comboCountryId, comboProvinceId) {
    debugger
    if (comboCountryId == 'residentialCountry') {
        $('#txtResidentialCountry').val($('#residentialCountry :selected').text())
    } else {
        $('#txtPermanentCountry').val($('#permanentCountry :selected').text())
    }
    var countryId = $('#' + comboCountryId).val();
    var param = {
        CountryId: countryId
    }
    CallAsyncService("/Common/GetProvince?CountryId=" + countryId, null, updateCitiesCB)
    function updateCitiesCB(response) {
        $('#' + comboProvinceId).html('');
        $('#' + comboProvinceId).append(new Option("Select Province", "", false, false));
        $.each(response, function (key, value) {
            $('#' + comboProvinceId).append(new Option(value.Name, value.Id, false, false));
        })
    }

}

function Save() {
    // Create a new Date object using the selected values
    var param = {
        Id: $('#id').val(),
        firstName: $('#firstName').val(),
        middleName: $('#middleName').val(),
        lastName: $('#lastName').val(),
        fatherFirstName: $('#fatherFirstName').val(),
        fatherMiddleName: $('#fatherMiddleName').val(),
        fatherLastName: $('#fatherLastName').val(),
        cnic: $('#cnic').val(),
        emailAddress: $('#email').val(),
        alterEmailAddress: $('#altEmail').val(),
        gender: document.getElementById("gender").value,
        husbandName: $('#husbandName').val(),
        dateofBirth: $('#dob').val(),
        // Contact Info
        cellPhoneNumber: $('#cellPhone').val(),
        whatsAppNumber: $('#whatsappNumber').val(),
        alternateCellPhoneNumber: $('#altCellPhone').val(),
        homePhoneNumber: $('#homePhone').val(),
        alternateLandline: $('#altLandline').val(),
        guardianCellPhoneNumber: $('#guardianCellPhone').val(),
        guardianEmailAddress: $('#guardianEmail').val(),
        // Address info
        residentialAddress: $('#residentialAddress').val(),
        residentialCountry: $('#residentialCountry :selected').text(),
        residentialProvince: $('#residentialProvince :selected').text(),
        residentialCity: $('#residentialCity :selected').text(),
        residentialCityOther: $('#residentialCityOther').val(),
        residentialPostalCode: $('#residentialPostalCode').val(),

        permanentAddress: $('#permanentAddress').val(),
        permanentCountry: $('#permanentCountry :selected').text(),
        permanentProvince: $('#permanentProvince :selected').text(),
        permanentCity: $('#permanentCity :selected').text(),
        permanentCityOther: $('#permanentCityOther').val(),
        permanentPostalCode: $('#permanentPostalCode').val(),


    };
    $('#mainLoader').show();
    CallAsyncService('/Home/Save', JSON.stringify(param), editSaveCallback);

    function editSaveCallback(response) {
        $('#mainLoader').hide();
        if (response.status) {
            ShowDivSuccess(response.message)
        }
        else {
            ShowDivError(response.message)
        }
    }

}

function LoadDate(stringDate, DateId) {
    var date = new Date(stringDate);
    var newDate = date.getFullYear() + '-' + (date.getMonth() + 1).toString().padStart(2, '0') + '-' + date.getDate().toString().padStart(2, '0');
    $('#' + DateId).val(newDate);
    debugger
    // For Load Combos For Day Month Year
    if (stringDate != "") {
        var day = date.getDate().toString().padStart(2, '0');
        var month = (date.getMonth() + 1).toString().padStart(2, '0');
        var year = date.getFullYear();
        $('#dobDay').val(day.toString());
        $('#dobDay').trigger('change');
        $('#dobMonth').val(month.toString());
        $('#dobMonth').trigger('change');
        $('#dobYear').val(year.toString());
        $('#dobYear').trigger('change');
    }
}

function validatePersonalInfoForm() {
    var isValid = true;
    

    $('#PersonalInfoForm input[required]').each(function () {

            if ($(this).val().trim() === '') {
                isValid = false;
                $(this).addClass("error");
                $(this).focus();

            } else {
                $(this).removeClass("error");
            }
        });
    $('#PersonalInfoForm select[required]').each(function () {

        if (($(this).val() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    $('#PersonalInfoForm textarea[required]').each(function () {

        if (($(this).val().trim() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });

    checkCNIC('cnic');
    checkEmail('email');
    checkPhoneNumber('cellPhone');

    if ($('#emailError').html() != "") {
        isValid = false;
        $('#email').addClass('error');
        $('#email').focus();
    }
    if ($('#cnicError').html() != "") {
        isValid = false;
        $('#cnic').addClass('error');
        $('#cnic').focus();
    }
    if ($('#numberError').html() != "") {
        isValid = false;
        $('#cellPhone').addClass('error');
        $('#cellPhone').focus();
    }

    if ($('#comboHearHU').val() == 'Other' && $('#OtherHearHU').val() == '') {
        $('#hearAboutHUOther').html('Please Enter Other Value');
        isValid = false;
    }

    $("input[name=IsAppliedBefore]").each(function () {
        var checked = $("input[name=IsAppliedBefore]:checked");
        if (checked.length == 0) {
            isValid = false;
            $('#errAppliedBefore').html('field is required');
        } else {
            $('#errAppliedBefore').html('');
        }
    });
    if ($("input[name='IsAppliedBefore']:checked").val() === '1') {
        if (($('#AppliedBeforeId').val().trim() == '')) {
            $('#AppliedBeforeId').addClass('error');
            $('#AppliedBeforeId').focus();
            isValid = false;
        } else { $('#AppliedBeforeId').removeClass('error'); }
        if ($('#AppliedBeforeYear :selected').val() === "") {
            $('#AppliedBeforeYear').addClass('error');
            $('#AppliedBeforeYear').focus();
            isValid = false;
        } else { $('#AppliedBeforeYear').removeClass('error'); }
    }
    
    if (isValid) {
        debugger
        increaseProgressBarWidth();
        $('#btnEducation').prop('disabled', false);
        $('#btnEducation').trigger('click');
        window.scrollTo(0, 100);
    }
    
}

// Educational Info Functions


function LoadEducationalData(Model) {
    debugger
    if (Model.Education != null) {
        $('#currentLevel').val(Model.Education.CurrentLevelOfEdu == null ? '' : Model.Education.CurrentLevelOfEdu.toString());
        $('#currentLevel').trigger('change');
        $('#startingYear').val(Model.Education.HSSCStartDate == null ? '' : Model.Education.HSSCStartDate.toString());
        $('#startingYear').trigger('change');
        $('#completionYear').val(Model.Education.HSSCCompletionDate == null ? '' : Model.Education.HSSCCompletionDate.toString());
        $('#completionYear').trigger('change');

    
        if (Model.Education.HUSchoolName != null) {
    
            if (Model.Education.HUSchoolName.toString() === 'SE') {
                $('#dhananiSchool').click();
            } else {
                $('#artsSchool').click();
            }
            $('#degreeProgram').val(Model.Education.IntendedProgram.toString());
            $('#degreeProgram').trigger('change');

        }
    }
}


function ShowHideField(divId, selectId, value) {
    var select = document.getElementById(selectId);
    if (select.value === value) {
        $('#' + divId).show();
    } else {
        $('#' + divId).hide();
    }
}

function LoadYear(selectId) {
    var comboYear = document.getElementById(selectId);
    comboYear.innerHTML = '';
    var option = document.createElement("option");
    option.value = "";
    option.text = "Year";
    option.selected = true;
    option.disabled = true;
    comboYear.appendChild(option);
    if (selectId == "completionYear") {
        var option = document.createElement("option");
        option.value = "2024";
        option.text = "2024";
        comboYear.appendChild(option);
        for (var j = 2023; j > 1950; j--) {
            var option = document.createElement("option");
            option.value = j;
            option.text = j;
            comboYear.appendChild(option);
        }
    } else if (selectId == 'startingYear' && $('#currentLevel').val() == 'HSSC II Completed') {

        for (var j = 2021; j > 1950; j--) {
            var option = document.createElement("option");
            option.value = j;
            option.text = j;
            comboYear.appendChild(option);
        }
    }
    else
    {
        for (var j = 2023; j > 1950; j--) {
            var option = document.createElement("option");
            option.value = j;
            option.text = j;
            comboYear.appendChild(option);
        }
    }
}

function GetBoardList() {
    CallAsyncService('/Common/GetBoardList', null, GetBoardListCallback);
}
function GetBoardListCallback(response) {
    $('#boardOfEducation').html('');
    $('#boardOfEducation').append(new Option("Select Board", "", false, false));
    $.each(response, function (key, value) {
        $('#boardOfEducation').append(new Option(value.Name, value.Id, false, false));

    })
}

function GetGroupListFromObj(BoardId) {
    const selectBox = document.getElementById('groupOfStudy');
    selectBox.innerHTML = ''; // Clear previous options
    debugger

    if (BoardId === 'IB') {
        const groups = ['Pre-Engineering', 'Pre-Medical', 'General Science/Computer Science', 'Commerce', 'Arts/Humanities', 'Home Economics'];
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Group';
        defaultOption.value = "";
        selectBox.add(defaultOption);
        groups.forEach(groups => {
            const option = document.createElement('option');
            option.value = groups;
            option.text = groups;
            selectBox.add(option);
        });
    } else if (BoardId === 'FB') {
        const groups = ['Pre-Engineering', 'Pre-Medical', 'General Science/Computer Science', 'Commerce', 'Arts/Humanities', 'Home Economics'];
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Group';
        defaultOption.value = "";
        selectBox.add(defaultOption);
        groups.forEach(groups => {
            const option = document.createElement('option');
            option.value = groups;
            option.text = groups;
            selectBox.add(option);
        });
    } else if (BoardId === 'AKUB') {
        const groups = ['Pre-Medical', 'Pre-Engineering', 'Commerce', 'Arts/Humanities','Home Economics'];
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Group';
        defaultOption.value = "";
        selectBox.add(defaultOption);
        groups.forEach(groups => {
            const option = document.createElement('option');
            option.value = groups;
            option.text = groups;
            selectBox.add(option);
        });

    } else {
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Group';
        selectBox.add(defaultOption);
    }
}
function GetSubjects(GroupName) {
    $('#divSubjects').html('');
    

    if (GroupName === 'Pre-Medical') {
        const subjects = ['Physics', 'Chemistry', 'Biology'];

        subjects.forEach(subject => {
            var newRow = document.createElement("div");
            newRow.className = "row";

            var newCol = document.createElement("div");
            newCol.className = "col-lg-3";
            var newcontrol = document.createElement("div");
            newcontrol.className = "form-group";

            var input = document.createElement("input");
            input.className = "form-control";
            input.type = "text";
            input.classList.add("SubjectName");
            input.value = subject;

            newcontrol.appendChild(input);
            newCol.appendChild(newcontrol);
            newRow.appendChild(newCol);

            $('#divSubjects').append(newRow);
        });
    }
    else if (GroupName === 'Pre-Engineering') {
        const subjects = ['Physics', 'Chemistry', 'Mathematics'];

        subjects.forEach(subject => {
            var newRow = document.createElement("div");
            newRow.className = "row";

            var newCol = document.createElement("div");
            newCol.className = "col-lg-3";
            var newcontrol = document.createElement("div");
            newcontrol.className = "form-group";

            var input = document.createElement("input");
            input.className = "form-control";
            input.type = "text";
            input.classList.add("SubjectName");
            input.value = subject;

            newcontrol.appendChild(input);
            newCol.appendChild(newcontrol);
            newRow.appendChild(newCol);

            $('#divSubjects').append(newRow);
        });
    }
    else if (GroupName === 'General Science/Computer Science') {
        const subjects = ['Physics', 'Chemistry', 'Computer Science/Computer Studies'];

        subjects.forEach(subject => {
            var newRow = document.createElement("div");
            newRow.className = "row";

            var newCol = document.createElement("div");
            newCol.className = "col-lg-3";
            var newcontrol = document.createElement("div");
            newcontrol.className = "form-group";

            var input = document.createElement("input");
            input.className = "form-control";
            input.type = "text";
            input.classList.add("SubjectName");
            input.value = subject;

            newcontrol.appendChild(input);
            newCol.appendChild(newcontrol);
            newRow.appendChild(newCol);

            $('#divSubjects').append(newRow);
        });
    }
    else if (GroupName === 'Commerce') {
        const subjects = ['', '', '', ''];

        subjects.forEach(subject => {
            var newRow = document.createElement("div");
            newRow.className = "row";

            var newCol = document.createElement("div");
            newCol.className = "col-lg-3";
            var newcontrol = document.createElement("div");
            newcontrol.className = "form-group";

            var input = document.createElement("input");
            input.className = "form-control";
            input.type = "text";
            input.classList.add("SubjectName");
            input.value = subject;

            newcontrol.appendChild(input);
            newCol.appendChild(newcontrol);
            newRow.appendChild(newCol);

            $('#divSubjects').append(newRow);
        });
    }
    else if (GroupName === 'Arts/Humanities') {
        const subjects = ['', '', '', ''];

        subjects.forEach(subject => {
            var newRow = document.createElement("div");
            newRow.className = "row";

            var newCol = document.createElement("div");
            newCol.className = "col-lg-3";
            var newcontrol = document.createElement("div");
            newcontrol.className = "form-group";

            var input = document.createElement("input");
            input.className = "form-control";
            input.type = "text";
            input.classList.add("SubjectName");
            input.value = subject;

            newcontrol.appendChild(input);
            newCol.appendChild(newcontrol);
            newRow.appendChild(newCol);

            $('#divSubjects').append(newRow);
        });
    }
    else if (GroupName === 'Home Economics') {
        const subjects = ['', '', '', ''];

        subjects.forEach(subject => {
            var newRow = document.createElement("div");
            newRow.className = "row";

            var newCol = document.createElement("div");
            newCol.className = "col-lg-3";
            var newcontrol = document.createElement("div");
            newcontrol.className = "form-group";

            var input = document.createElement("input");
            input.className = "form-control";
            input.type = "text";
            input.classList.add("SubjectName");
            input.value = subject;

            newcontrol.appendChild(input);
            newCol.appendChild(newcontrol);
            newRow.appendChild(newCol);

            $('#divSubjects').append(newRow);
        });
    }
}

function GetGroupList(BoardId) {
    CallAsyncService('/Common/GetGroupList?BoardId=' + BoardId, null, GetGroupListCallback);
}
function GetGroupListCallback(response) {
    $('#groupOfStudy').html('');
    $('#groupOfStudy').append(new Option("Select Group", "", false, false));
    $.each(response, function (key, value) {
        $('#groupOfStudy').append(new Option(value.Name, value.Id, false, false));

    })
}

function GetSubjectList(GroupId) {

    CallAsyncService('/Common/GetSubjectList?GroupId=' + $('#' + GroupId).val(), null, GetSubjectListCallback);
}
function GetSubjectListCallback(response) {
    $('#divSubjects').html('');
    var rowIndex = 0;
    $.each(response, function (key, value) {
        rowIndex++;
        var newRow = document.createElement("div");
        newRow.className = "row";

        for (let i = 1; i <= 2; i++) {
            var newCol = document.createElement("div");
            newCol.className = "col-lg-3";
            var newcontrol = document.createElement("div");
            newcontrol.className = "form-group";

            var input = document.createElement("input");
            
            input.className = "form-control";
            if (i === 1) {
                input.type = "text";
                input.classList.add("SubjectName");
                input.value = value.Name;
            } else {
                input.type = "number";
                input.classList.add("SubjectObtain");
            } 
            newcontrol.appendChild(input);
            newCol.appendChild(newcontrol);
            newRow.appendChild(newCol);
        }

        $('#divSubjects').append(newRow);
    })
}

function AddRow() {
    var newRow = document.createElement("div");
    newRow.className = "row";

    var newCol = document.createElement("div");
    newCol.className = "col-lg-3";
    var newcontrol = document.createElement("div");
    newcontrol.className = "form-group";

    var input = document.createElement("input");
        
    input.className = "form-control";
        input.type = "text";
        input.classList.add("SubjectName");
        
    newcontrol.appendChild(input);
    newCol.appendChild(newcontrol);
    newRow.appendChild(newCol);
    
    $('#divSubjects').append(newRow);
}
function loadPrograms(radioValue) {
    const selectBox = document.getElementById('degreeProgram');
    selectBox.innerHTML = ''; // Clear previous options

    if (radioValue === 'SE') {
        const programs = ['Electrical Engineering', 'Computer Science', 'Computer Engineering'];
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Bachelor\'s Program';
        defaultOption.value = "";
        defaultOption.selected = true;
        defaultOption.disabled = true;
        selectBox.add(defaultOption);
        programs.forEach(programs => {
            const option = document.createElement('option');
            option.value = programs;
            option.text = programs;
            selectBox.add(option);
        });
    } else if (radioValue === 'SA') {
        const programs = ['Communication and Design', 'Social Development Policy', 'Comparative Humanities'];
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Bachelor\'s Program';
        defaultOption.value = "";
        selectBox.add(defaultOption);
        programs.forEach(programs => {
            const option = document.createElement('option');
            option.value = programs;
            option.text = programs;
            selectBox.add(option);
        });
    } else {
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Program';
        selectBox.add(defaultOption);
    }
}

function ValidateProgram(ProgramValue) {
    if (ProgramValue == "Electrical Engineering" && $('#groupOfStudy').val() != "Pre-Engineering" && $('#groupOfStudy').val() != 'General Science/Computer Science') {
        $('#errDegreeProgram').html("Students who have studied Physics,Mathematics, Chemistry or Computer Science / Computer Studies in HSSC can only apply for Habib University’s BS Electrical Engineering program.");
        $('#degreeProgram').focus();
        return false;
    } else if (ProgramValue == "Computer Science" && $('#groupOfStudy').val() != "Pre-Engineering" && $('#groupOfStudy').val() != 'General Science/Computer Science') {
        $('#errDegreeProgram').html("Students who have studied Mathematics in HSSC can only apply for Habib University’s Computer Science program.");
        $('#degreeProgram').focus();
        return false;
    } else if (ProgramValue == "Computer Engineering" && $('#groupOfStudy').val() != "Pre-Engineering" && $('#groupOfStudy').val() != 'General Science/Computer Science') {
        $('#errDegreeProgram').html("Students who have studied Physics,Mathematics, Chemistry or Computer Science/Computer Studies in HSSC can only apply for Habib University’s BS Electrical Engineering program.");
        $('#degreeProgram').focus();
        return false;
    } else {
        $('#errDegreeProgram').html('');
        return true;
    }
}
function SaveEducation() {
    var SubjectName = [];
    var SubjectObtain = [];
    var SubjectTotal = [];
    var SubjectGrade = [];

    // Iterate over input elements with the class "my-input"
    $(".SubjectName").each(function () {
        SubjectName.push($(this).val());
    });
    $(".SubjectObtain").each(function () {
        SubjectObtain.push($(this).val());
    });
    $(".SubjectTotal").each(function () {
        SubjectTotal.push($(this).val());
    });
    $(".SubjectGrade").each(function () {
        SubjectGrade.push($(this).val());
    });



    var param = {
        UserId: $('#id'),
        CurrentLevelOfEdu: $('#currentLevel').val(),
        HSSCSchoolName: $('#collegeName').val(),
        HSSCSchoolAddress: $('#collegeAddress').val(),
        HSSCPercentage: $('#hsscPercentage').val(),
        HSSCStartDate: $('#startingYear').val(),
        HSSCCompletionDate: $('#completionYear').val(),
        HSSCBoardId: $('#boardOfEducation').val(),
        HSSCBoardName: $('#boardOfEducation').text(),

        HSSCGroupId: $('#groupOfStudy').val(),
        HSSCGroupName: $('#groupOfStudy').text(),
        SSCSchoolName: $('#secondarySchoolName').val(),
        SSCSchoolAddress: $('#secondarySchoolAddress').val(),
        SSCPercentage: $('#sscPercentage').val(),
        UniversityName: $('#universityName').val(),
        IntendedProgram: $('#degreeProgram').val(),
        HUSchoolName: $('input[name="huSchool"]:checked').val(),

        SubjectName: SubjectName,
        SubjectObtain: SubjectObtain,
        SubjectTotal: SubjectTotal,
        SubjectGrade: SubjectGrade
    };
    $('#mainLoader').show();
    CallAsyncService('/Education/Save', JSON.stringify(param), editEducationCallback);

    function editEducationCallback(response) {
        $('#mainLoader').hide();
        debugger
        if (response.status) {
            ShowDivSuccess(response.message)
        }
        else {
            ShowDivError(response.message)
        }
    }

};
function SubmitEducation() {

    var isValid = true;
    debugger
    $('#EducationForm input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();
            
        } else {
            $(this).removeClass("error");
        }
    });
    $('#EducationForm select[required]').each(function () {

        if (($(this).val() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    $('#EducationForm textarea[required]').each(function () {

        if (($(this).val().trim() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });

    if ($('input[name="huSchool"]:checked').val() == "" || $('input[name="huSchool"]:checked').val() == null) {
        isValid = false;
        $('#errHUSchool').html("Please select School Name");
    } else {
        $('#errHUSchool').html("");
    }

    //if ($('#currentLevel').val() == 'Already enrolled in a University' && $('#universityName').val() == '') {
    //    isValid = false;
    //    $('#errUniversityName').html('University Name is required');
    //    $('#universityName').focus();
    //} else {
    //    $('#errUniversityName').html('');
    //}

    if (isValid && ValidateProgram($('#degreeProgram').val()))
        {
        increaseProgressBarWidth();
        $('#btnDocument').prop('disabled', false);

        $('#btnDocument').trigger('click');
        window.scrollTo(0, 200);

    }
};

// Document Section

function checkFileSize(fileId, ErrSpanId){
    debugger
    
    var file = $('#' + fileId);
    var fileSize = file[0].files[0].size; // Get the file size in bytes
    var maxFileSize = 2097152; // 2 MB
    // Check if the file size exceeds the maximum limit
    if (fileSize > maxFileSize) {
        
        $("#" + ErrSpanId).html(" Select the file Maximum size 2MB"); // Clear any previous error message
    } else {
        $("#" + ErrSpanId).html(""); // Clear any previous error message
    }
}
function submitDocuments(sessionUserId) {
    debugger
    isValid = true;
    $('input[type="file"][required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    if (isValid) {
        increaseProgressBarWidth();
        $('#btnTest').prop('disabled', false);

        $('#btnTest').trigger('click');
        window.scrollTo(0, 300);

    }
}

// Activity Section

function AddActivityRow() {
    var newRow = document.createElement("div");
    newRow.className = "row";

    for (let i = 1; i <= 3; i++) {
        var newCol = document.createElement("div");
        newCol.className = "col-lg-3";
        var newcontrol = document.createElement("div");
        newcontrol.className = "form-group";

        if (i === 1) {
            var input = document.createElement("input");
            input.type = "text";
            input.className = "form-control";
            input.classList.add("ActivityName");
            newcontrol.appendChild(input);
        } else if (i === 2) {
            var input = document.createElement("input");
            input.type = "text";
            input.className = "form-control";
            input.classList.add("ActivityDuration");
            newcontrol.appendChild(input);
        } else {
            var button = document.createElement("button");
            button.onclick = removeActivityRow(this);
            button.className = "form-control";
            button.classList.add("trashActivity");
            button.classList.add("border-0");
            var icon = document.createElement("i");
            icon.classList.add("fa");
            icon.classList.add("fa-trash");
            button.appendChild(icon);
            newcontrol.appendChild(button);
        }


        newCol.appendChild(newcontrol);
        newRow.appendChild(newCol);
        $('.trashActivity').click(function () {
            $(this).closest(".row").remove();
        });
    }

    $('#divActivity').append(newRow);
}

function removeActivityRow(button) {
    $(button).closest(".row").remove();
}

function SubmitActivity() {
    var ActivityName = [];
    var ActivityDuration = [];

    // Iterate over input elements with the class "my-input"
    $(".ActivityName").each(function () {
        ActivityName.push($(this).val());
    });
    $(".ActivityDuration").each(function () {
        ActivityDuration.push($(this).val());
    });
    $('#mainLoader').show();
    debugger
    CallAsyncService("/Home/SubmitActivity?ActivityName=" + ActivityName + "&ActivityDuration=" + ActivityDuration + "&UserId=" + $('#id').val(), null, SubmitActivityCB)
    function SubmitActivityCB(response) {
        $('#mainLoader').hide();
        if (response.status) {
            ShowDivSuccess(response.message);
            setTimeout(function () {
                document.getElementById("redirectToTestDate").click();
            }, 2000);
        }
        else {
            ShowDivError(response.message)
        }
    }

}
// Declaration Section
function submitDeclaration() {
    var isValid = true;
    $('input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    $('select[required]').each(function () {

        if (($(this).val() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    $('textarea[required]').each(function () {

        if (($(this).val().trim() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });


    if ($('#comboHearHU').val() == 'Other' && $('#OtherHearHU').val() == '') {
        $('#hearAboutHUOther').html('Please Enter Other Value');
        isValid = false;
    }


    $("input[name=IsAppliedBefore]").each(function () {
        var checked = $("input[name=IsAppliedBefore]:checked");
        if (checked.length == 0) {
            isValid = false;
            $('#errAppliedBefore').html('field is required');
        } else {
            $('#errAppliedBefore').html('');
        }
    });
    if ($("input[name='IsAppliedBefore']:checked").val() === '1') {
        if (($('#AppliedBeforeId').val().trim() == '')) {
            $('#AppliedBeforeId').addClass('error');
            $('#AppliedBeforeId').focus();
            isValid = false;
        } else { $('#AppliedBeforeId').removeClass('error'); }
        if ($('#AppliedBeforeYear :selected').val() === "") {
            $('#AppliedBeforeYear').addClass('error');
            $('#AppliedBeforeYear').focus();
            isValid = false;
        } else { $('#AppliedBeforeYear').removeClass('error'); }
    }

    var check1 = $('#validateLaw').is(':checked');
    var check2 = $('#invalidInfo').is(':checked');
    var check3 = $('#validInfo').is(':checked');
    var isProgramValid = ValidateProgram($('#degreeProgram').val());
    if (check1 == true && check2 == true && check3 == true && isValid == true && isProgramValid == true) {
        increaseProgressBarWidth();
        const day = $('#dobDay').val();
        const month = $('#dobMonth').val();
        const year = $('#dobYear').val();
        // Create a new Date object using the selected values
        var dob = `${year}-${month}-${day}`;

        
        var personalInfo = {
            'PersonalInfo.Id': $('#id').val(),
            'PersonalInfo.firstName': $('#firstName').val(),
            'PersonalInfo.middleName': $('#middleName').val(),
            'PersonalInfo.lastName': $('#lastName').val(),
            'PersonalInfo.fatherFirstName': $('#fatherFirstName').val(),
            'PersonalInfo.fatherMiddleName': $('#fatherMiddleName').val(),
            'PersonalInfo.fatherLastName': $('#fatherLastName').val(),
            'PersonalInfo.cnic': $('#cnic').val(),
            'PersonalInfo.emailAddress': $('#email').val(),
            'PersonalInfo.alterEmailAddress': $('#altEmail').val(),
            'PersonalInfo.gender': $("#gender").val(),
            'PersonalInfo.husbandName': $('#husbandName').val(),
            'PersonalInfo.dateOfBirth': dob,
            // Contact Info
            'PersonalInfo.cellPhoneNumber': $('#cellPhone').val(),
            'PersonalInfo.whatsAppNumber': $('#whatsappNumber').val(),
            'PersonalInfo.alternateCellPhoneNumber': $('#altCellPhone').val(),
            'PersonalInfo.homePhoneNumber': $('#homePhone').val(),
            'PersonalInfo.alternateLandline': $('#altLandline').val(),
            'PersonalInfo.guardianCellPhoneNumber': $('#guardianCellPhone').val(),
            'PersonalInfo.guardianEmailAddress': $('#guardianEmail').val(),
            // Address info
            'PersonalInfo.residentialAddress': $('#residentialAddress').val(),
            'PersonalInfo.residentialCountry': $('#residentialCountry :selected').text(),
            'PersonalInfo.residentialProvince': $('#residentialProvince :selected').text(),
            'PersonalInfo.residentialCity': $('#residentialCity :selected').text(),
            'PersonalInfo.residentialCityOther': $('#residentialCityOther').val(),
            'PersonalInfo.residentialPostalCode': $('#residentialPostalCode').val(),

            //'PersonalInfo.permanentAddress': $('#permanentAddress').val(),
            //'PersonalInfo.permanentCountry': $('#permanentCountry :selected').text(),
            //'PersonalInfo.permanentProvince': $('#permanentProvince :selected').text(),
            //'PersonalInfo.permanentCity': $('#permanentCity :selected').text(),
            //'PersonalInfo.permanentCityOther': $('#permanentCityOther').val(),
            //'PersonalInfo.permanentPostalCode': $('#permanentPostalCode').val(),

            'PersonalInfo.IsAppliedBefore': $("input[name='IsAppliedBefore']:checked").val(),
            'PersonalInfo.AppliedBeforeYear': $('#AppliedBeforeYear').val(),
            'PersonalInfo.AppliedBeforeId': $('#AppliedBeforeId').val(),

            // Hear About
            'PersonalInfo.HearAboutHU': $('#comboHearHU').val(),
            'PersonalInfo.HearAboutHUOther': $('#OtherHearHU').val(),

            // Test Date

            'PersonalInfo.TestDate': $('#TestDate').val()

        }
        var education = {

            'Education.CurrentLevelOfEdu': $('#currentLevel').val(),
            'Education.HSSCSchoolName': $('#collegeName').val(),
            'Education.HSSCSchoolAddress': $('#collegeAddress').val(),
            'Education.HSSCPercentage': $('#hsscPercentage').val(),
            'Education.HSSCStartDate': $('#startingYear').val(),
            'Education.HSSCCompletionDate': $('#completionYear').val(),
            'Education.HSSCBoardId': $('#boardOfEducation').val(),
            'Education.HSSCBoardName': $('#boardOfEducation :selected').val(),

            'Education.HSSCGroupId': $('#groupOfStudy').val(),
            'Education.HSSCGroupName': $('#groupOfStudy :selected').text(),
            'Education.SSCSchoolName': $('#secondarySchoolName').val(),
            'Education.SSCSchoolAddress': $('#secondarySchoolAddress').val(),
            'Education.SSCPercentage': $('#sscPercentage').val(),
            'Education.UniversityName': $('#universityName').val(),
            'Education.IntendedProgram': $('#degreeProgram').val(),
            'Education.HUSchoolName': $('input[name="huSchool"]:checked').val(),

        }
        var document = {
        }

        var SubjectName = [];
        var SubjectObtain = [];

        // Iterate over input elements with the class "my-input"
        $(".SubjectName").each(function () {
            SubjectName.push($(this).val());
        });
        $(".SubjectObtain").each(function () {
            SubjectObtain.push($(this).val());
        });
        var data = new FormData();
        data.append("Photograph", jQuery("#Photograph").get(0).files[0]);
        data.append("CNIC", jQuery("#CNIC").get(0).files[0]);
        data.append("SSCMarkSheet", jQuery("#SSCMarkSheet").get(0).files[0]);
        data.append("HSSCMarkSheet", jQuery("#HSSCMarkSheet").get(0).files[0]);

        Object.entries(personalInfo).forEach(([key, value]) => {
            data.append(key, value);
        });
        debugger
        if (!$('#permanentDifferent').prop('checked')) {
            data.append('PersonalInfo.permanentAddress', $('#residentialAddress').val());
            data.append('PersonalInfo.permanentCountry', $('#residentialCountry :selected').text());
            data.append('PersonalInfo.permanentProvince', $('#residentialProvince :selected').text());
            data.append('PersonalInfo.permanentCity', $('#residentialCity :selected').text());
            data.append('PersonalInfo.permanentCityOther', $('#residentialCityOther').val());
            data.append('PersonalInfo.permanentPostalCode', $('#residentialPostalCode').val());
        } else {
            data.append('PersonalInfo.permanentAddress', $('#permanentAddress').val());
            data.append('PersonalInfo.permanentCountry', $('#permanentCountry :selected').text());
            data.append('PersonalInfo.permanentProvince', $('#permanentProvince :selected').text());
            data.append('PersonalInfo.permanentCity', $('#permanentCity :selected').text());
            data.append('PersonalInfo.permanentCityOther', $('#permanentCityOther').val());
            data.append('PersonalInfo.permanentPostalCode', $('#permanentPostalCode').val());
        }

        Object.entries(education).forEach(([key, value]) => {
            data.append(key, value);
        });
        Object.entries(document).forEach(([key, value]) => {
            data.append(key, value);
        });

        data.append("SubjectName", SubjectName);
        data.append("SubjectObtain", SubjectObtain);






        $('#mainLoader').show();
        CallFileAsyncService("/Application/Submit", data , submitDeclarationCB)
        function submitDeclarationCB(response) {
            $('#mainLoader').hide();
            if (response.status) {
                $('#errorBlock').hide();
                $('#redirectToSuccess')[0].click();
                ShowDivSuccess(response.message);
                
            }
            else {
                ShowDivError(response.message);
                $("#PersonalError ul").html('');
                $("#EducationError ul").html('');
                $("#DocumentError ul").html('');

                var PersonalErrors = response.PersonalErrors.toString();
                var PersonalErrorsList = PersonalErrors.split(",");
                $.each(PersonalErrorsList, function (key, value) {
                    $("#PersonalError ul").append('<li class="text-danger">'+ value +'</li>');
                })
                $('#errorBlock').show();
                var EducationErrors = response.EducationErrors.toString();

                var EducationErrorsList = EducationErrors.split(",");
                $.each(EducationErrorsList, function (key, value) {
                    $("#EducationError ul").append('<li class="text-danger">' + value + '</li>');
                })
                var DocumentErrors = response.DocumentErrors.toString();

                var DocumentErrorsList = DocumentErrors.split(",");
                $.each(DocumentErrorsList, function (key, value) {
                    $("#DocumentError ul").append('<li class="text-danger">' + value + '</li>');
                })
            
            
            }
        }

    } else {
        $('input[required]').filter(':first').focus();
        ShowDivError("Please check all sections and enter in all required fields");
    }

}

// Test Date Section
function LoadTestDate(date) {
    $('#TestDate').val(date == null ? '' : date);
    $('#TestDate').trigger('change');
}
function SubmitTestDate() {
    date = $('#TestDate').val();
    debugger

    if (date != null && date != "") {
        increaseProgressBarWidth();
        $('#btnDeclaration').prop('disabled', false);
        $('#btnDeclaration').trigger('click');
        window.scrollTo(0, 500);
        //document.body.scrollTop = 0;
        //$('#mainLoader').show();
        //CallAsyncService("/Home/SubmitTestDate?Date=" + date + "&UserId=" + $('#id').val(), null, SubmitActivityCB)
        //function SubmitActivityCB(response) {
        //    $('#mainLoader').hide();
        //    if (response.status) {
        //        ShowDivSuccess(response.message)
        //        document.getElementById("redirectToDeclaration").click();
        //    }
        //    else {
        //        $('#testdateError').html(response.message);

        //        ShowDivError(response.message)
        //    }
        //}
    } else {
        //ShowDivError("Please Select Test Date")
        $('#testdateError').html("Please select Test Date");

    }
}

// Students DataTable

function LoadStudentDatatable() {
    var mainTable = $('#main-datatables').DataTable({
        dom: 'Blrtip',
        "processing": true,
        "serverSide": true,
        "pagingType": "full_numbers",
        "paging": true,
        "lengthMenu": [10, 25, 50, 75],
        "ordering": false,
        "language": {
            "processing": "<div class='loader' id='mainLoader'><img src = '/Content/Images/preloader.gif' /></div >"
        },
        buttons: [
            {
                text: 'My button',
                action: function (e, dt, node, config) {
                    alert('Button activated');
                }
            }
        ],
        "ajax": {
            "type": "POST",
            "url": '/Student/Get',
            'data': function (data) {
                return (data);

            }
        },
        "columns": [
            { "data": "Id" },
            { "data": "HUTopId" },
            { "data": "FirstName" },
            { "data": "LastName" },
            { "data": "CellPhoneNumber" },
            { "data": "EmailAddress" },
            { "data": "CNIC" },
            {
                "data": "Result",
                render: function (data) {
                    if (data == 1) {
                        return "<span class='badge badge-pill badge-danger'>FAILED</span>";
                    } else if (data == 2) {
                        return "<span class='badge badge-pill badge-success'>Passed</span>";
                    } else {
                        return "<span class='badge badge-pill badge-info'>Pending</span>";
                    }
                }
            },
            {
                "data": "IsRecordMoveToEApp",
                render: function (data) {
                    if (data == 1) {
                        return "<span class='badge badge-pill badge-success'>MOVED</span>";
                    } else {
                        return "<span class='badge badge-pill badge-info'>Pending</span>";
                    }
                }
            },
            {
                "data": "IsAdmitCardGenerated",
                render: function (data) {
                    if (data == 1) {

                        return ('<i class="fa fa-check text-success" style="font-size: 35px;">');
                    } else {
                        return ('<i class="fa fa-xmark text-danger" style="font-size: 35px;">');
                    }
                }    
            },
            {
                "data": "AdmitCardGeneratedOn",
                render: function (data) {
                    if (data != null) {
                        var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
                        return new Date(parseInt(/-?\d+/.exec(data)[0])).toLocaleDateString("en-US", options);
                    }else
                    {
                        return(data);
                    }
                }
            },
            {
                "data": "IsAdmitCardSent",
                render: function (data) {
                    if (data == 1) {

                        return ('<i class="fa fa-check text-success" style="font-size: 35px;">');
                    } else {
                        return ('<i class="fa fa-xmark text-danger" style="font-size: 35px;">');
                    }
                }   
            },
            {
                "data": "AdmitCardSentOn",
                render: function (data) {
                    if (data != null) {
                        var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
                        return new Date(parseInt(/-?\d+/.exec(data)[0])).toLocaleDateString("en-US", options);
                    } else {
                        return (data);
                    }
                }
            },
            {
                "data": "Id",
                //"defaultContent": "<button data-v-aa799a9e='' id='btnEdit' type='button' class='btn btn-icon savebtn global-btn-purple'><i class='fa fa-edit'></i></button> "
                //"defaultContent": "<div style='min-width: 150px;'><button id='btnEdit' type='button'><i class='fa fa-edit' style='font-size: 30px; color:#5d2468;'></i></button> <button id='btnAdmitCard' type='button'><i class='fa-solid fa-file-pdf' style='font-size: 30px;color:#5d2468;'></i></button> <button id='btnSend' type='button'><i class='fa-regular fa-paper-plane' style='font-size: 30px;color:#5d2468;'></i></button></div>"
                //render: function (data) {
                //    return "<li class='actionDropWrap' onclick='ToggleShow($(this))'><div class= 'nameWrapper'><i class='fa-solid fa-list-ul'></i></div ><ul class='logoutDrop'><li class='global-btn-purple w-100' id='btnEdit'>Edit</li><li class='global-btn-purple w-100' onclick='ShowAdmitCardModal(" + data + ")'>Generate Admit Card</li><li onclick='sendAdmitCard(" + data + ")' class='global-btn-purple w-100'>Send Admit Card</li></ul></li > "
                //}
                render: function (data) {
                    return "<li class='actionDropWrap' id='" + data + "' onclick='ToggleShow(this.id)'><div class= 'nameWrapper'><i class='fa-solid fa-list-ul'></i></div ><ul class='actionDrop'><li class='global-btn-purple w-100' id='btnEdit'><a>Edit</a></li><li class='global-btn-purple w-100' onclick='ShowAdmitCardModal(" + data + ")'><a>Generate Admit Card</a></li><li onclick='sendAdmitCard(" + data + ")' class='global-btn-purple w-100'><a>Send Admit Card</a></li><li onclick='moveRecordToEApp(" + data + ")' class='global-btn-purple w-100'><a>Move Record To E-App</a></li></ul></li > "
                }
            }
        ],
        "columnDefs": [
            {
                "targets": "_all",
                "className": "dt-center",
                "orderable": true
            },
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ]

    });
    $('#btnSearch').click(function () {
        mainTable.column(1).search($("#txtFilterHUTOPSId").val());
        mainTable.column(2).search($("#txtFilterName").val());
        mainTable.column(3).search($("#txtFilterCNIC").val());
        mainTable.column(4).search($("#txtFilterPhoneNumber").val());
        mainTable.column(5).search($("#txtFilterEmail").val());

        mainTable.draw();
    });

    $('#btnClear').click(function (e) {
        e.preventDefault();

        $("#txtFilterHUTOPSId").val('');
        $("#txtFilterName").val('');
        $("#txtFilterCNIC").val('');
        $("#txtFilterPhoneNumber").val('');
        $("#txtFilterEmail").val('');
        $('#btnSearch').trigger('click');
    })

    // Edit Transaction
    $('#main-datatables tbody').on('click', '#btnEdit', function () {
        debugger
        var data = mainTable.row($(this).parents('tr')).data();
        $('#mainLoader').show();
        CallAsyncService("/Student/UpdateSession?Id=" + data.Id, null, CBFunction)
        function CBFunction(response) {
            $('#mainLoader').hide();
            if (response.status) {
                $('#redirectToStudent')[0].click();
            } else {
                ShowDivError(response.message);
            }
        }

    });

    $('#main-datatables tbody').on('click', '.checkbox', function () {
        debugger
        var data = mainTable.row($(this).parents('tr')).data();
        if ($(this).prop('checked') == true) {
            if (data.IsAdmitCardGenerated == 0) {
                $('#txtSelectedIds').val($('#txtSelectedIds').val() + ',' + data.Id)
            }
        } else {
            if (data.IsAdmitCardGenerated == 0) {
                var ids = $('#txtSelectedIds').val().toString();
                ids = ids.replace(','+data.Id, "");
                $('#txtSelectedIds').val(ids)
            }
        }
        var Ids = $('#txtSelectedIds').val().toString();
        Ids = Ids.replace(Ids.charAt(0),"")
        if (Ids == "") {
            $('#btnGenerateAdmitCard').hide();
        } else {
            $('#btnGenerateAdmitCard').show();
        }
        
        
    })

}

function closeStudentProfile(){
    CallAsyncService("/Student/CloseSession", null, closeStudentProfileCB);
    function closeStudentProfileCB(response) {
        if (response.status) {
            ShowDivSuccess(response.message);
        } else {
            ShowDivError(response.message);
        }

    }
}

function ShowAdmitCardModal(applicantId) {
    debugger
    var modal = $('#modalAdmitCard');
    $('#applicantId').val(applicantId);
    modal.modal({ show: true })
}

function SubmitAdmitCard() {
    var isValid = true;
    $('input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    $('select[required]').each(function () {

        if (($(this).val() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    $("input[name=venue]").each(function () {

        var checked = $("input[name=venue]:checked");
        debugger
        if (checked.length == 0) {
            isValid = false;
            $('#errVenue').html('Please select Venue');
        } else {
            $('#errVenue').html('');
        }
    });
    if (isValid) {
        var param = {
            'Id': $('#applicantId').val(),
            'TestDate': $('#testDate').val(),
            'Shift': $('#comboShift').val(),
            'Venue': $("input[name='venue']:checked").val()
        };
        $('#modalAdmitCard').modal('hide');
        $('#mainLoader').show();
        CallAsyncService("/Student/GenerateAdmitCard", JSON.stringify(param), SubmitAdmitCardBatchCB);
        function SubmitAdmitCardBatchCB(response) {
            $('#mainLoader').hide();
            setInterval(function () { location.reload() }, 3000);
            if (response.status) {
                ShowDivSuccess(response.message);
            } else {
                ShowDivError(response.message);
            }
        }
    } else {
        return false;
    }
    

}

function ToggleShow(Id) {
    $('#' + Id).children('ul').toggleClass("dropdown-show");
}

function sendAdmitCard(applicantId) {
    
    $('#mainLoader').show();
    CallFileAsyncService("/Student/SendAdmitCard?Id=" + applicantId, null, SubmitAdmitCardBatchCB);
    function SubmitAdmitCardBatchCB(response) {
        $('#mainLoader').hide();
        if (response.status) {
            ShowDivSuccess(response.message);
        } else {
            ShowDivError(response.message);
        }
    }
}

function moveRecordToEApp(applicantId) {

    $('#mainLoader').show();
    CallFileAsyncService("/Student/RecordMoveToEApp?Id=" + applicantId, null, SubmitAdmitCardBatchCB);
    function SubmitAdmitCardBatchCB(response) {
        $('#mainLoader').hide();
        if (response.status) {
            ShowDivSuccess(response.message);
        } else {
            ShowDivError(response.message);
        }
    }
}


// Email Tab

function LoadEmailTemplate(Id, model) {
    TempVal = $('#' + Id).val();
    $.each(model, function (key, value) {
        if (value.Id == TempVal) {
            tinymce.activeEditor.setContent(value.Body);
            $('#txtSubject').val(value.Subject);
        }
    })
}

function SaveEmailTemplate() {
    var param = {
        Id: $('#comboEmailTemp').val(),
        Subject: $('#txtSubject').val(),
        Body: tinymce.get("textAreaEmailTemp").getContent()
    }
    $('#mainLoader').show();
    CallAsyncService("/Email/Save", JSON.stringify(param), SaveEmailTemplateCB);
    function SaveEmailTemplateCB(response) {
        $('#mainLoader').hide();
        setInterval(function () { location.reload() }, 3000);
        if (response.status) {
            ShowDivSuccess(response.message);
        }
        else {
            ShowDivError(response.message);
        }
    }
}

function AddNewEmailTemplate() {
    tinymce.activeEditor.setContent('');
    $('#txtSubject').val('');
    $('#txtDescription').val('');
    $('#btnAddNewEmail').prop('disabled', true);
    $('#btnAddNewEmail').css("background-color", "#6c757d");
    $('#btnAddNewEmail').unbind("mouseenter mouseleave");
}


// Test Date Section

function LoadTestDateDatatable() {
    var TestDateTable = $('#main-datatables').DataTable({
        dom: 'lrtip',
        "processing": true,
        "serverSide": false,
        "pagingType": "full_numbers",
        "paging": true,
        "lengthMenu": [10, 25, 50, 75],
        "ordering": false,
        "language": {
            "processing": "<div class='loader' id='mainLoader'><img src = '/Content/Images/preloader.gif' /></div > "
        },
        "columnDefs": [
            {
                "targets": "_all",
                "className": "dt-center",
                "orderable": true
            },
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ]
        
        

    });

    // Edit Transaction
    $('#main-datatables tbody').on('click', '#btnEdit', function () {
        debugger
        var data = TestDateTable.row($(this).parents('tr')).data();
        var modal = $('#modal');
        console.log(data[3]);
        modal.on('show.bs.modal', function () {
            $('#txtId').val(data[0]);
            LoadDate(data[1], 'dateValue');
            $('#txtText').val(data[2]);
            $('#comboAdmissionSession').val(data[5]);
            LoadDate(data[4], 'dateDeadline')
            if (data[3] == '1') {
                $('#checkVisibility').prop('checked', true);
            } else { $('#checkVisibility').prop('checked', false); }


        });

        modal.modal({ show: true })

    });
}

function ShowModal() {
    var modal = $('#modal');
    modal.on('show.bs.modal', function () {
        $('#txtId').val('');
        $('#dateValue').val('');
        $('#txtText').val('');
        $('#comboAdmissionSession').val(null);
        $('#dateDeadline').val('');
        $('#checkVisibility').prop('checked', false);


    });
    modal.modal({ show: true })
}

function AddUpdateTestDate() {

    var isValid = true;
    $('input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    $('select[required]').each(function () {

        if (($(this).val() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });
    if (isValid) {
        var visible = '0';
        if ($('#checkVisibility').prop('checked') == true) {
            visible = '1';
        }
        var param = {
            Id: $('#txtId').val(),
            Value: $('#dateValue').val(),
            Text: $('#txtText').val(),
            AdmissionSession: $('#comboAdmissionSession').val(),
            DeadlineDate: $('#dateDeadline').val(),
            Visibility: visible
        }
        $('#modal').modal('hide');
        $('#mainLoader').show();
        CallAsyncService("/TestDate/Submit", JSON.stringify(param), AddUpdateTestDateCB);
        function AddUpdateTestDateCB(response) {
            $('#mainLoader').hide();
            setTimeout(function () { location.reload() }, 3000)
            if (response.status) {
                ShowDivSuccess(response.message);
            } else {
                ShowDivError(response.message);
            }
        }
    } else { return false; }
    
}

// Admit card Managment 
function SubmitAdmitCardBatch() {
    debugger
    var isValid = true; 
    if ($("input[name='type']:checked").val() != null) {
        if ($("input[name='type']:checked").val() == 2) {
            $('#errVenue').html('');
            $('input[required]').each(function () {
                $(this).removeClass("error");
            });
            $('select[required]').each(function () {
                $(this).removeClass("error");
            });
            if ($("#HUTOPSIdsFile").get(0).files[0] == null) {
                isValid = false;
                $("#HUTOPSIdsFile").addClass("error");
                $("#HUTOPSIdsFile").focus();

            }
        } else {
            $('input[required]').each(function () {

                if ($(this).val().trim() === '') {
                    isValid = false;
                    $(this).addClass("error");
                    $(this).focus();

                } else {
                    $(this).removeClass("error");
                }
            });
            $('select[required]').each(function () {

                if (($(this).val() === '') || ($(this).val() === null)) {
                    isValid = false;
                    $(this).addClass("error");
                    $(this).focus();

                } else {
                    $(this).removeClass("error");
                }
            });

            $("input[name=vanue]").each(function () {
                
                var checked = $("input[name=vanue]:checked");
                debugger
                if (checked.length == 0) {
                    $('#errVenue').html('Please select Venue');
                } else {
                    $('#errVenue').html('');
                }
            });
        }
    } else {
        isValid = false;
        ShowDivError("Please Select Action Radio");
        return false;
    }


    if (isValid) {
        var data = new FormData();
        data.append("HUTOPSIdsFile", jQuery("#HUTOPSIdsFile").get(0).files[0]);
        data.append("TestDate", $('#testDate').val());
        data.append("Shift", $('#comboShift').val());
        data.append("Venue", $("input[name='vanue']:checked").val());
        data.append("Type", $("input[name='type']:checked").val());

        $('#mainLoader').show();
        CallFileAsyncService("/AdmitCard/Submit", data, SubmitAdmitCardBatchCB);
        function SubmitAdmitCardBatchCB(response) {
            $('#mainLoader').hide();
            if (response.status) {
                ShowDivSuccess(response.message);
            } else {
                ShowDivError(response.message);
            }
        }
    } else {
        ShowDivError("Please enter in all required fields");
    }

   

}

// Upload Result

function SubmitResultBatch() {

    var isValid = true;
    $('#modalForm input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });

    $("#modalForm input[name=result]").each(function () {

        var checked = $("input[name=result]:checked");
        debugger
        if (checked.length == 0) {
            $('#errResult').html('Please select Result Value');
        } else {
            $('#errResult').html('');
        }
    });
    if ($("input[name='result']:checked").val() == '2' && $("input[name=shift]:checked").length == 0) {
        isValid = false;
        $('#errShift').html(' Please select Value');
    } else {
        $('#errShift').html('');
    }


    if (isValid) {
        var data = new FormData();
        data.append("HUTOPSIdsFile", jQuery("#HUTOPSIdsFile").get(0).files[0]);
        data.append("Result", $("input[name='result']:checked").val());
        data.append("Type", 4);
        data.append("IsRecordSendToEApp", $("input[name='shift']:checked").val())

        $('#mainLoader').show();
        
        CallFileAsyncService("/AdmitCard/Submit", data, SubmitAdmitCardBatchCB);
        function SubmitAdmitCardBatchCB(response) {
            $('#mainLoader').hide();
            
            if (response.status) {
                $('#modalForm')[0].reset();
                ShowDivSuccess(response.message);
            } else {
                ShowDivError(response.message);
            }
        }
    } else {
        ShowDivError("Please enter in all required fields");
    }
   

}

// Upload Batch to shift records to E-Application

function SubmitRecordsToEApp() {
    var isValid = true;
    $('#EAppForm input[type="file"][required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");
            $(this).focus();

        } else {
            $(this).removeClass("error");
        }
    });



    if (isValid) {
        var data = new FormData();
        data.append("HUTOPSIdsFile", jQuery("#eAppHUTOPSIdsFile").get(0).files[0]);
        data.append("Type", 5);

        $('#mainLoader').show();
        debugger
        CallFileAsyncService("/AdmitCard/Submit", data, SubmitAdmitCardBatchCB);
        function SubmitAdmitCardBatchCB(response) {
            $('#mainLoader').hide();
            if (response.status) {
                ShowDivSuccess(response.message);
            } else {
                ShowDivError(response.message);
            }
        }
    } else {
        ShowDivError("Please enter in all required fields");
        $('#eAppHUTOPSIdsFile').addClass('error');
        $('#eAppHUTOPSIdsFile').focus();
    }


}