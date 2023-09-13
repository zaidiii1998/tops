$(document).ready(function () {
    $('.number').inputmask("0399-9999999");
    $('.cnic').inputmask("99999-9999999-9");
});
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
    debugger
    $.notify(msg, "success");
}
function ShowDivError(msg) {
    debugger
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

    CallAsyncService('/Common/CheckEmail?email=' + $('#' + Id).val(), null, checkEmailCB);

    function checkEmailCB(response) {
        const email = $('#email').val();
        const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

        if (!emailRegex.test(email)) {
            debugger
            $('#emailError').html("Please Enter a valid Email Address");
            return false;
        }
        if (!response.status) {
            msg = response.message;
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
        $('#passwordError').html('Confrim Password not match or length less then 6')
        $('#cpasswordError').html('Confrim Password not match or length less then 6')
        return false;

    } else {
        pass.style.borderBlockColor = "green";
        cpass.style.borderBlockColor = "green";
        $('#passwordError').html('')
        $('#cpasswordError').html('')

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



function validateText(input) {
    // Remove non-numeric characters using a regular expression
    input.value = input.value.replace(/[^A-Za-z]/g, '');
        //input.value = input.value.replace(/[^!@#$%^&*()_+{}\[\]:;<>,.?~\\\-]/g, '');
} 
function validateNumber(input) {
        input.value = input.value.replace(/[^0-9.]/g, '');
}
function validateRegisterForm() {


    var pass = document.getElementById("password");
    var cpass = document.getElementById("cpassword");


    const email = $('#email').val();
    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

    

    if (pass.value != cpass.value || pass.value.length < 6) {
        pass.style.borderBlockColor = "red";
        cpass.style.borderBlockColor = "red";

        return false;

    } else if (!emailRegex.test(email)) {
        debugger
        $('#emailError').html("Please Enter a valid Email Address");
        return false;
    } else if ($('#comboHearHU').val() == '') {
        $('#hearAboutHU').html('Please Select Value');
        return false;
    } else if ($('#comboHearHU').val() == 'Other' && $('#OtherHearHU').val() == '') {
        $('#hearAboutHUOther').html('Please Enter Other Value');
        return false;
    } else {
        pass.style.borderBlockColor = "green";
        cpass.style.borderBlockColor = "green";
        return true;
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

