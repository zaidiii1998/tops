$(document).ready(function () {
    $('.number').inputmask("0399-9999999");
    $('.cnic').inputmask("99999-9999999-9");
    $('.Percentage').inputmask("99.99%");
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


$('.number').inputmask("9999-9999999");
$('.cnic').inputmask("99999-9999999-9");

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

    } else {
        $('#' + errSpanId).html('');
    }
}

function validateEmail(inputId, errSpanId) {

    const email = $('#' + inputId).val();
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (!emailRegex.test(email)) {
        debugger
        $('#' + errSpanId).html("Please Enter a valid Email Address");
        return false;
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
    
    CallAsyncService("Common/GetCities?ProvinceId=" + provinceId, param, updateCitiesCB)
    function updateCitiesCB(response) {
        $('#' + ComboCityId).html('');
        $.each(response, function (key, value) {
            $('#' + ComboCityId).append(new Option(value.name, value.id, false, false));
        })
        $('#' + ComboCityId).append(new Option("Other", "other", false, false));

    }
}

function updateProvince(comboCountryId, comboProvienceId) {
    debugger
    var countryId = $('#' + comboCountryId).val();
    var param = {
        CountryId: countryId
    }
    CallAsyncService("Common/GetProvince?CountryId=" + countryId, null, updateCitiesCB)
    function updateCitiesCB(response) {
        $('#' + ComboCityId).val('');
        $.each(response, function (key, value) {
            $('#' + comboProvienceId).append(new Option(value.name, value.id, false, false));
        })
    }

}



function LoadYear(comboId) {
    var comboYear = document.getElementById(comboId);
    comboYear.innerHTML = "";
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
