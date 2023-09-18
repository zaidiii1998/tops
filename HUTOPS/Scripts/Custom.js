$(document).ready(function () {
    $('.number').inputmask("0399-9999999");
    $('.cnic').inputmask("99999-9999999-9");
    $('.Percentage').inputmask("99.99%");

    $('.trashActivity').click(function () {
        $(this).closest(".row").remove();
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


//$("#expandSideBarBtn, #sidebarDashboard").on("click hover", function () {
//    $("#sidebarDashboard").toggleClass("expandedSidebar");
//    $("#contentWrapperDashboard").toggleClass("contentWrapAdon");
//    $(".adminDashboard").toggleClass("sidexpendBody");
//});






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
    } else { return true }
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
            $('#' + ComboCityId).append(new Option(value.Name, value.Id, false, false));
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
}

function validatePersonalInfoForm() {
    var isValid = true;
    debugger
    $('input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");

        } else {
            $(this).removeClass("error");
        }
    });
    $('select[required]').each(function () {

        if (($(this).val() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");

        } else {
            $(this).removeClass("error");
        }
    });
    $('textarea[required]').each(function () {

        if (($(this).val().trim() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");

        } else {
            $(this).removeClass("error");
        }
    });
    debugger
    if (isValid) {
        $('#mainLoader').show();
        $('#mainForm').submit();
        return true;
    } else {
        return false;
    }
    
}

// Educational Info Functions


function LoadEducationalData(Model) {
    $('#currentLevel').val(Model.Education.CurrentLevelOfEdu == null ? '' : Model.Education.CurrentLevelOfEdu.toString());
    $('#currentLevel').trigger('change');
    $('#startingYear').val(Model.Education.HSSCStartDate == null ? '' : Model.Education.HSSCStartDate.toString());
    $('#startingYear').trigger('change');
    $('#completionYear').val(Model.Education.HSSCCompletionDate == null ? '' : Model.Education.HSSCCompletionDate.toString());
    $('#completionYear').trigger('change');

    var program = Model.Education.IntendedProgram.toString();
    if (Model.Education.HUSchoolName.toString() === 'SE') {
        $('#dhananiSchool').click();
    } else {
        $('#artsSchool').click();
    }
    $('#degreeProgram').val(Model.Education.IntendedProgram.toString());
    $('#degreeProgram').trigger('change');

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
    var option = document.createElement("option");
    option.value = "";
    option.text = "Select Year";
    comboYear.appendChild(option);
    for (var j = 2023; j > 1950; j--) {
        var option = document.createElement("option");
        option.value = j;
        option.text = j;
        comboYear.appendChild(option);
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

        for (let i = 1; i <= 4; i++) {
            var newCol = document.createElement("div");
            newCol.className = "col-lg-3";
            var newcontrol = document.createElement("div");
            newcontrol.className = "form-group";

            var input = document.createElement("input");
            input.type = "text";
            input.className = "form-control";
            if (i === 1) {
                input.classList.add("SubjectName");
                input.value = value.Name;
            } else if (i === 2) {
                input.classList.add("SubjectObtain");
            } else if (i === 3) {
                input.classList.add("SubjectTotal");
            } else {
                input.classList.add("SubjectGrade");
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

    for (let i = 1; i <= 4; i++) {
        var newCol = document.createElement("div");
        newCol.className = "col-lg-3";
        var newcontrol = document.createElement("div");
        newcontrol.className = "form-group";

        var input = document.createElement("input");
        input.type = "text";
        input.className = "form-control";
        if (i === 1) {
            input.classList.add("SubjectName");
        } else if (i === 2) {
            input.classList.add("SubjectObtain");
        } else if (i === 3) {
            input.classList.add("SubjectTotal");
        } else {
            input.classList.add("SubjectGrade");
        }
        newcontrol.appendChild(input);
        newCol.appendChild(newcontrol);
        newRow.appendChild(newCol);
    }

    $('#divSubjects').append(newRow);
}
function loadPrograms(radioValue) {
    const selectBox = document.getElementById('degreeProgram');
    selectBox.innerHTML = ''; // Clear previous options

    if (radioValue === 'SE') {
        const programs = ['BS Electrical Engineering', 'BS Computer Science', 'BS Computer Engineering'];
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Program';
        defaultOption.value = "";
        selectBox.add(defaultOption);
        programs.forEach(programs => {
            const option = document.createElement('option');
            option.value = programs;
            option.text = programs;
            selectBox.add(option);
        });
    } else if (radioValue === 'SA') {
        const programs = ['BA (Honours) Communication and Design', 'BSc (Honours) Social Development and Policy', 'BA (Honors) Comparative Humanities'];
        const defaultOption = document.createElement('option');
        defaultOption.text = 'Select Program';
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
    $('input[required]').each(function () {

        if ($(this).val().trim() === '') {
            isValid = false;
            $(this).addClass("error");
            
        } else {
            $(this).removeClass("error");
        }
    });
    $('select[required]').each(function () {

        if (($(this).val().trim() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");

        } else {
            $(this).removeClass("error");
        }
    });
    $('textarea[required]').each(function () {

        if (($(this).val().trim() === '') || ($(this).val() === null)) {
            isValid = false;
            $(this).addClass("error");

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

    if (isValid)
        {
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
        CallAsyncService('/Education/Submit', JSON.stringify(param), editEducationCallback);
    
        function editEducationCallback(response) {
            $('#mainLoader').hide();
            debugger
            if (response.status) {
                ShowDivSuccess(response.message);
                setTimeout(function () {
                    document.getElementById("redirectLnk").click();
                }, 2000);
                
                
            }
            else {
                debugger
                ShowDivError(response.message)
                console.log(response.error);
                var errstring = response.error.toString();
                var stringList = errstring.split(",");
                $.each(stringList, function (key, value) {
                    $("#Error ul").append('<li class="text-danger">'+ value +'</li>');
                })
                

            }
        }
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

        } else {
            $(this).removeClass("error");
        }
    });
    if (isValid) {
        var form = $('#fileUploadForm');
        var formData = new FormData(form[0]);
        formData.append("UserId", sessionUserId)
        $('#mainLoader').show();
        $.ajax({
            url: '/Handler1.ashx', // Change the URL to your MVC controller's action
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                $('#mainLoader').hide();
                debugger
                if (response.status) {
                    ShowDivSuccess(response.message);
                    setTimeout(function () {
                        document.getElementById("redirectToActivity").click();
                    }, 2000);
                }
                else {
                    ShowDivError(response.message)
                    console.log(response.error);
                    var errstring = response.error.toString();
                    var stringList = errstring.split(",");
                    $.each(stringList, function (key, value) {
                        $("#Error ul").append('<li class="text-danger">' + value + '</li>');
                    })
                }

            },
            error: function (xhr, status, error) {
                debugger
                ShowDivError(response.message);

            }
        });
    } else {return false }
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
    CallAsyncService("/Home/SubmitActivity?ActivityName=" + ActivityName + "&ActivityDuration=" + ActivityDuration, null, SubmitActivityCB)
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
    var check1 = $('#validateLaw').is(':checked');
    var check2 = $('#invalidInfo').is(':checked');
    var check3 = $('#validInfo').is(':checked');
    if (check1 == true && check2 == true && check3 == true) {
        $('#mainLoader').show();
        CallAsyncService("/Declaration/Submit?check1=true&check2=true&check3=true", null, submitDeclarationCB)
        function submitDeclarationCB(response) {
            $('#mainLoader').hide();
            if (response.status) {
                ShowDivSuccess(response.message)
            }
            else {
                ShowDivError(response.message)
            }
        }

    } else {
        ShowDivError("All checkboxes are required");
    }

}

// Test Date Section
function LoadTestDate(date) {
    $('#TestDate').val(date == null ? '' : date);
    $('#TestDate').trigger('change');
}
function SubmitTestDate() {
    date = $('#TestDate').val();

    if (date != null || date != "") {

        $('#mainLoader').show();
        CallAsyncService("/Home/SubmitTestDate?Date=" + date, null, SubmitActivityCB)
        function SubmitActivityCB(response) {
            $('#mainLoader').hide();
            if (response.status) {
                ShowDivSuccess(response.message)
                document.getElementById("redirectToDeclaration").click();
            }
            else {
                $('#testdateError').html(response.message);

                //ShowDivError(response.message)
            }
        }
    } else {
        //ShowDivError("Please Select Test Date")
        $('#testdateError').html(response.message);


    }
}