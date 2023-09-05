$(document).ready(function () {
    $('.number').inputmask("92399-9999999");
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

function toggleFormSection(sectionId, checkboxId) {
    var section = document.getElementById(sectionId);
    var checkbox = document.getElementById(checkboxId);

    section.style.display = checkbox.checked ? "block" : "none";
}
function updateCities(provienceId, cityId) {
    var provinceSelect = document.getElementById(provienceId.toString());
    var citySelect = document.getElementById(cityId.toString());

    var selectedProvince = provinceSelect.value;


    var citiesByProvince = {
        "Punjab": ["Lahore", "Faisalabad", "Rawalpindi", "Multan", "Gujranwala"],
        "Sindh": ["Karachi", "Hyderabad", "Sukkur", "Larkana"],
        "Khyber Pakhtunkhwa": ["Peshawar", "Abbottabad", "Swat", "Mardan"],
        "Balochistan": ["Quetta", "Gwadar", "Khuzdar", "Turbat"],
        "Gilgit-Baltistan": ["Gilgit", "Skardu", "Hunza", "Ghizer"],
        "Azad Kashmir": ["Muzaffarabad", "Mirpur", "Kotli", "Rawalakot"]
        // ... and so on
    };

    // Update city select box options based on selected province
    citySelect.innerHTML = "";
    var cities = citiesByProvince[selectedProvince];
    var option = document.createElement("option");
    option.value = "";
    option.text = "Select City";
    citySelect.appendChild(option);
    for (var j = 0; j < cities.length; j++) {
        var option = document.createElement("option");
        option.value = cities[j];
        option.text = cities[j];
        citySelect.appendChild(option);
    }
    var option = document.createElement("option");
    option.value = "other";
    option.text = "Other";
    citySelect.appendChild(option);
}

function updateProvince(countryId, provienceId) {
    var countrySelect = document.getElementById(countryId.toString());
    var provinceSelect = document.getElementById(provienceId.toString());

    var selectedCountry = countrySelect.value;

    // Simulated data (you can replace this with actual data)
    var provincesByCountry = {
        "pakistan": ["Punjab", "Sindh", "Khyber Pakhtunkhwa", "Balochistan", "Gilgit-Baltistan", "Azad Kashmir"]
    };


    // Update province select box options based on selected country
    provinceSelect.innerHTML = "";
    var provinces = provincesByCountry[selectedCountry];
    var option = document.createElement("option");
    option.value = "";
    option.text = "Select Provience/State";
    provinceSelect.appendChild(option);
    for (var i = 0; i < provinces.length; i++) {
        var option = document.createElement("option");
        option.value = provinces[i];
        option.text = provinces[i];
        provinceSelect.appendChild(option);
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
