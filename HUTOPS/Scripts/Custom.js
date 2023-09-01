//$(document).ready(function () {
//    setTimeout(function () {
//        $('body').addClass('loaded');
//        // Once the container has finished, the scroll appears
//        if ($('body').hasClass('loaded')) {
//            // It is so that once the container is gone, the entire preloader section is deleted
//            $('.overlay').delay(000).queue(function () {
//                $(this).css("display", "none");
//            });
//        }
//    }, 2000);
//});

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
    debugger
    $.notify(msg, "success");
}
function ShowDivError(msg) {
    debugegr
    $.notify(msg, "error");
}
//function ShowDivError(msg) {
//    $.notify({
//        // options
//        icon: 'flaticon-error',
//        title: 'Failed',
//        message: msg

//    }, {
//        // settings
//        element: 'body',
//        position: null,
//        type: "danger",
//        allow_dismiss: true,
//        newest_on_top: false,
//        showProgressbar: false,
//        placement: {
//            from: "bottom",
//            align: "right"
//        },
//        offset: 20,
//        spacing: 10,
//        z_index: 1031,
//        delay: 5000,
//        timer: 1000,
//        url_target: '_blank',
//        mouse_over: null,
//        animate: {
//            enter: 'animated fadeInDown',
//            exit: 'animated fadeOutUp'
//        },
//        onShow: null,
//        onShown: null,
//        onClose: null,
//        onClosed: null,
//        icon_type: 'class'
//    });
//}

//function ShowDivSuccess(msg) {
//    $.notify({
//        // options
//        icon: 'flaticon-success',
//        title: 'Success',
//        message: msg

//    }, {
//        // settings
//        element: 'body',
//        position: null,
//        type: "success",
//        allow_dismiss: true,
//        newest_on_top: false,
//        showProgressbar: false,
//        placement: {
//            from: "bottom",
//            align: "right"
//        },
//        offset: 20,
//        spacing: 10,
//        z_index: 1031,
//        delay: 5000,
//        timer: 1000,
//        url_target: '_blank',
//        mouse_over: null,
//        animate: {
//            enter: 'animated fadeInDown',
//            exit: 'animated fadeOutUp'
//        },
//        onShow: null,
//        onShown: null,
//        onClose: null,
//        onClosed: null,
//        icon_type: 'class'
//    });
//}

