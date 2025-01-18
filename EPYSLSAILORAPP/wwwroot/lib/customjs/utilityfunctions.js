//$.fn.modal.Constructor.prototype.enforceFocus = function () { };
//$('body').on('shown.bs.modal', '.modal', function () {
//    $(this).find('select').each(function () {
//        var dropdownParent = $(document.body);
//        if ($(this).parents('.modal.in:first').length !== 0)
//            dropdownParent = $(this).parents('.modal.in:first');
//        $(this).select2({
//            dropdownParent: dropdownParent
//            // ...
//        });
//    });
//});

var currencyInput;
var currency = '৳';

function toCamelCase(inputString) {
    return inputString.replace(/(?:^\w|[A-Z]|\b\w)/g, function (match, index) {
        return match.toUpperCase();
    });
}

function formatTimestamp(isoTimestamp) {
    // Create a Date object from the ISO 8601 timestamp
    const date = new Date(isoTimestamp);

    // Options for formatting the date
    const options = {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
        second: 'numeric',
        hour12: true
    };

    // Format the date using toLocaleString
    return date.toLocaleString('en-US', options);
}


function downloadFile(filename, base64string) {
    // Create a blob from the base64 string
    var byteCharacters = atob(base64string);
    var byteNumbers = new Array(byteCharacters.length);
    for (var i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    var byteArray = new Uint8Array(byteNumbers);
    var blob = new Blob([byteArray], { type: 'application/pdf' }); // Change the type accordingly

    // Create a temporary anchor element and trigger the download
    var a = document.createElement('a');
    a.href = URL.createObjectURL(blob);
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}
function base64ToBlob(base64, contentType) {
    var byteCharacters = atob(base64);
    var byteArrays = [];

    for (var offset = 0; offset < byteCharacters.length; offset += 512) {
        var slice = byteCharacters.slice(offset, offset + 512);

        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    var blob = new Blob(byteArrays, { type: contentType });
    return blob;
}

function ViewPDF(base64PDF) {
    var pdfBlob = base64ToBlob(base64PDF, 'application/pdf');

    // Create a URL for the Blob
    var pdfUrl = URL.createObjectURL(pdfBlob);

    // Open the PDF in a new tab when the button is clicked

    window.open(pdfUrl, '_blank');

}
function removeField(array, fieldToRemove) {
    return array.map(function (obj) {
        // Create a copy of the object
        var newObj = Object.assign({}, obj);
        // Delete the specified field
        delete newObj[fieldToRemove];
        return newObj;
    });
}
function getAbreviation(inputString) {
    const words = inputString.split(' ');

    let initials = '';

    words.forEach(word => {
        if (word.length > 0) {
            initials += word[0].replace('(', '').replace(')', '');
        }
    });

    return initials.toUpperCase();
}
function convertNumberToWords(n) {
    var strret = "";
    var crore = Math.floor(n / 10000000);
    var remainder = n % 10000000;

    if (remainder == 0)
        strret = ($.spellingNumber(crore) + "Crore Taka").toLowerCase();
    else {
        var lakh = Math.floor(remainder / 100000);
        var lakh_remainder = remainder % 100000;

        if (lakh == 0)
            strret = ((crore != 0 ? ($.spellingNumber(crore) + "Crore ").toLowerCase() : "") + ($.spellingNumber(remainder) + " Taka").toLowerCase());
        else {
            if (lakh_remainder == 0)
                strret = ((crore != 0 ? ($.spellingNumber(crore) + "Crore ").toLowerCase() : "") + (lakh != 0 ? ($.spellingNumber(lakh) + "Lakh ").toLowerCase() : ""));
            else {
                strret = ((crore != 0 ? ($.spellingNumber(crore) + "Crore ").toLowerCase() : "") + (lakh != 0 ? ($.spellingNumber(lakh) + "Lakh ").toLowerCase() : "") +
                    ($.spellingNumber(lakh_remainder) + " Taka").toLowerCase());

            }
        }
    }

    return toCamelCase(strret);
}

function convertNumberToCrore(n) {
    var strret = "";
    var crore = n / 10000000;

    return crore.toFixed(2).toString() + " Cr.";
}

function ClearMenuSearch() {

    $("#txtmenusearch").val('');

    $("#dv_menu .main_menu").removeClass("menu-open");

    $.each($("#dv_menu .main_menu").slice(0, 2), function (key, val) {
        $(val).addClass("menu-open");
    });

}

function generateGUID() {
    return 'xxxxxxxxxxxx4xxxyxxxxxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
            v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
function ShowBarCode() {
    $.each($(".barcode_to_show"), function (key, val) {

        $(val).css("display", "none");

        var svgid = "svg_" + generateGUID();

        var barcode_text = $(val).val();

        $(val).next().append("<svg id=" + svgid + "></svg>");

        JsBarcode("#" + svgid, barcode_text, {
            format: "CODE128", // Specify the barcode format
            lineColor: "#0aa", // Barcode color
            width: 2, // Barcode line width
            height: 30, // Barcode height
            displayValue: true // Display the string value below the barcode
        });
    });
}
$(document).ready(function () {


    BindCurrentEventForTextBox();

    $.each($("#dv_menu .nav-link"), function (key, lnk) {
        if ($(lnk).attr("href").trim().toUpperCase() == document.location.pathname.trim().toUpperCase()) {
            $(lnk).css("background-color", "#336481");

            $("#dv_menu li").removeClass("menu-open");

            $(lnk).closest(".main_menu").addClass("menu-open");
        }
    });

    $("#txtmenusearch").keydown(function () {

        $("#dv_menu li").removeClass("menu-open");

        if ($("#txtmenusearch").val().trim() != "") {
            $.each($("#dv_menu .nav-link"), function (key, lnk) {

                var menu_title = $(lnk).find("p").text().toUpperCase();

                if (menu_title.indexOf($("#txtmenusearch").val().trim().toUpperCase()) > -1) {

                    //$(lnk).addClass("menu-open");

                    $(lnk).closest(".main_menu").addClass("menu-open");
                }
            });
        }
        else {
            ClearMenuSearch();
        }

    });

});

function RegisterTabEvent() {

    $('.landingtab li').click(function (e) {

        e.preventDefault();

        var current_index = $(this).attr("tab_index");

        var next_tab = $(this).parent().find("li[tab_index=" + current_index + "]");

        if (next_tab.length > 0) {

            $('#' + tabid + ' li').removeClass("active");

            $(this).addClass("active");

            $(this).parent().parent().find(".tab-pane").removeClass("active");

            $(this).parent().parent().find(".tab-pane[tabpane_index=" + current_index + "]").addClass("active");

        }

    });
}

function ConvertNumbertoCurrency(val) {
    var value = parseFloat(val);
    var newval = value.toLocaleString('en-US', { style: 'decimal', minimumFractionDigits: 0, maximumFractionDigits: 20 });

    if (newval.indexOf('৳') == -1) {
        return '৳ ' + newval;
    }
    else {
        return newval;
    }
}

function ConvertNumbertoCommaSeparated(val) {
    var value = parseFloat(val);
    var newval = value.toLocaleString('en-US', { style: 'decimal', minimumFractionDigits: 0, maximumFractionDigits: 20 });

    return newval;

}

function RemoveTableRow(elem) {

    var rowcount = $(elem).closest("table").find("tbody tr").length;

    $(elem).closest("tr").remove();

    return rowcount;
}

function getCookieValue(cookieName) {
    const cookies = document.cookie.split(';');
    for (const cookie of cookies) {
        const [name, value] = cookie.trim().split('=');
        if (name === cookieName) {
            return decodeURIComponent(value);
        }
    }
    return null;
}

function BindCurrentEventForTextBox() {
    $('input[type="currency"]').change(function () {

    });

    // 
    $('input[type="currency"]').on("blur", function () {
        if ($(this).val()) {
            $(this).val($(this).val().replace(/[^\d.]/g, ''));
        }

        if (check_value($(this).val()) != null) {

            $(this).val(ConvertNumbertoCurrency($(this).val()))
        }
    });

    $('input[type="currency"]').focus(function () {

        var value = $(this).val();

        if ($(this).val().length > 0) {
            $(this).val(localStringToNumber(value));
        }
    });

    $('input[type="currency_ext"]').on("blur", function () {

        if ($(this).val()) {
            $(this).val($(this).val().replace(/[^\d.]/g, ''));
        }

        if (check_value($(this).val()) != null) {
            $(this).val(ConvertNumbertoCommaSeparated($(this).val()))
        }
    });

    $('input[type="currency_ext"]').focus(function () {

        var value = $(this).val();

        if ($(this).val().length > 0) {
            $(this).val(localStringToNumber(value));
        }
    });

    $('input[type="number"]').on('input', function () {
        var max = parseInt($(this).attr('max'));

        if (max) {
            var value = parseInt($(this).val());
            if (value > max) {
                $(this).val("0");
            }
        }
    });

}

function ConvertNumberToCurrency() {

    $.each($("input[type=currency]"), function (key, elem) {
        //if (check_value($(elem).val()) != null) {
        if ($(elem).val() == "") {
            $(elem).val('0');
        }
        if ($(elem).val().indexOf('৳') == -1) {
            $(elem).val(ConvertNumbertoCurrency($(elem).val()))
        }
        //  }
    });

    $.each($("input[type=currency_ext]"), function (key, elem) {
        //if (check_value($(elem).val()) != null) {
        if ($(elem).val() == "") {
            $(elem).val('0');
        }
        if ($(elem).val().indexOf(',') == -1) {
            $(elem).val(ConvertNumbertoCommaSeparated($(elem).val()))
        }
        //  }
    });

}

function localStringToNumber(myString) {
    try {
        var cleanNumberString = myString.replace(/[^\d.-]/g, '');

        // Convert the cleaned string to a float
        var myNumber = parseFloat(cleanNumberString);
        return Number(myNumber);
    }
    catch (ex) {
        return Number(0);
    }

}

function modal_tab_events_click_next_tab(current_link) {

    $(".modal-content .nav-tabs li").each(function () {
        $(this).removeClass("active");
    });

    var tab_index = $(current_link).attr("tab_index");

    var next_tab = $(".modal-content .nav-tabs li[tab_index=" + (parseInt(tab_index) + 1) + "]");

    if (next_tab != null) {
        $(next_tab).addClass("active");
        $(next_tab).removeClass("disablelink");
    }
    $(".modal-content .tab-content div").each(function () {
        $(this).removeClass("active");
    });

    $(".modal-content .tab-content " + $(next_tab).find("a").attr("href")).addClass("active");

}

function modal_tab_events_click_previous_tab(current_link) {

    $(".modal-content .nav-tabs li").each(function () {
        $(this).removeClass("active");
    });

    var tab_index = $(current_link).attr("tab_index");

    var next_tab = $(".modal-content .nav-tabs li[tab_index=" + (parseInt(tab_index) - 1) + "]");

    if (next_tab != null) {
        $(next_tab).addClass("active");
        $(next_tab).removeClass("disablelink");
    }
    $(".modal-content .tab-content div").each(function () {
        $(this).removeClass("active");
    });

    $(".modal-content .tab-content " + $(next_tab).find("a").attr("href")).addClass("active");

}

function modal_carousel_click_next(btn) {
    // e.preventDefault();
    var current_index = $(btn).closest(".carousel").find(".carousel-inner").find(".active").attr("data-slide-to");

    var next_tab = $(btn).closest(".carousel").find(".carousel-inner").find(".carousel-item[data-slide-to=" + (parseInt(current_index) + 1) + "]");

    if (next_tab.length > 0) {
        $(btn).closest(".carousel").find(".carousel-item").removeClass("active");
        $(next_tab).addClass("active");
    }
    else {

    }

}


function BindTabEvents(tabid) {

    $('#' + tabid + ' li').click(function (e) {

        e.preventDefault();

        var current_index = $(this).attr("tab_index");

        var next_tab = $(this).parent().find("li[tab_index=" + current_index + "]");

        if (next_tab.length > 0) {

            $('#' + tabid + ' li').removeClass("active");

            $(this).addClass("active");

            $(this).parent().parent().find(".tab-pane").removeClass("active");

            $(this).parent().parent().find(".tab-pane[tabpane_index=" + current_index + "]").addClass("active");

        }

    });

}

function checkAllElementOfParent(row) {

    var ret = true;
    $.each($(row).find("input[type=text],input[type=number],select"), function (key, val) {

        if ($(val).is(":visible") || $(val).is(":enabled")) {

            if (check_value($(val).val()) == null) {
                $(val).css("border", "1px solid red");
                ret = false;
            }
            else {
                $(val).css("border", "");
            }

        }
    })

    return ret;

}
function checknullValue(value) {
    if (value === null || value === undefined || value === 0 || value === '' || value === "0" || isNaN(value)) {
        //console.log('The value is null, undefined, 0, or empty.');
        return false;
    } else {
        //console.log('The value is not null, undefined, 0, or empty.');
        return true;
    }
}
function check_textbox_value(el) {
    if (el.length == 0)
        return null;
    else if ($(el).val().length == 0)
        return null;
    else if ($(el).val() == "")
        return null;
    else if ($(el).val() == null)
        return null;
    else
        return $(el).val();
}

function _cusFormValidate(formID) {

    var flg = false;
    var form = $('#' + formID);
    jQuery.validator.unobtrusive.parse();
    jQuery.validator.unobtrusive.parse(form);

    if (form.valid()) {
        flg = true;
    }
    else {
        flg = false;
    }

    return flg;
}


function check_value(value) {
    if (value === null || value === undefined || value === 0 || value === '' || value === "0" || isNaN(value))
        return null;
    else if (value == "")
        return null;
    else if (value == "0")
        return null;
    else if (value == null)
        return null;
    else
        return value;
}
function check_zerovalue(value) {
    if (value === null || value === undefined || value === 0 || value === '' || value === "0" || isNaN(value))
        return 0;
    else if (value == "")
        return 0;
    else if (value == "0")
        return 0;
    else if (value == null)
        return 0;
    else
        return value;
}
function modal_tab_events() {
    $(document).on("click", ".modal-content li a", function (e) {

        e.preventDefault();

        $(".modal-content .nav-tabs li").each(function () {
            $(this).removeClass("active");
        });

        $(this).parent().addClass("active");

        var tab = $(this).attr("href");
        $(".modal-content .tab-content div").each(function () {
            $(this).removeClass("active");
        });
        $(".modal-content .tab-content " + tab).addClass("active");
    });
}
function getTotalByClassInsideTable(tableid, custom_class) {

    var total = 0;

    $(tableid + " ." + custom_class).each(function (index, element) {

        var str = $(element).val().length == 0 ? 0 : parseFloat(localStringToNumber($(element).val()));

        if (str > 0)
            total += parseFloat(str);

    });

    return total;
}

function getTotalByClassInsideTableObject(table, custom_class) {

    var total = 0;

    $(table).find(" ." + custom_class).each(function (index, element) {

        var str = $(element).val().length == 0 ? 0 : parseFloat(localStringToNumber($(element).val()));

        if (str > 0)
            total += parseFloat(str);

    });

    return total;
}


function getQueryStringParameter(name) {
    var url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
function showLoader(loadertext) {

    var sendingicon = "~/Images/";

    if (loadertext == null || loadertext == undefined) {
        loadertext = "Loading .........";
    }


    $.blockUI({
        fadeIn: 1,
        fadeOut: 1,
        // timeout: 1000,
        baseZ: 100000,
        css: {
            border: 'none',
            padding: '5px',
            backgroundColor: 'transparent!important',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .8,
            color: '#fff'
        },

        message: ("<div class='body-block-loader d - none' style='cursor: wait; '>"
            + '<div class="loader bg-transparent no-shadow p-0" style = "display: flex; justify-content: center;" >'
            + '<div class="ball-grid-pulse">'
            + '<div class="bg-primary"></div>'
            + '<div class="bg-primary"></div>'
            + '<div class="bg-primary"></div>'
            + '<div class=""><img style="width: 90px;background: none; " src="../Images/loading.gif" alt="Italian Trulli"></div>'
            + '<div class="bg-primary"></div>'
            + '<div class="bg-primary"></div>'
            + '<div class="bg-primary"></div>'
            + '<div class="bg-primary"></div>'
            + '<div class="bg-primary"></div>'
            + '</div>'
            + '</div >'
            + '<h4 style="display: flex; justify-content: center;" > ' + loadertext + '</h4>'
            + '</div>')
    });
}
function hideLoader() {
    $.unblockUI();
}

function getUrlVars(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function showAdminMessage(message) {

    Swal.fire({
        title: "Anouncement From Admin!",
        text: message,
        imageUrl: "/Images/sadmin.png",
        imageWidth: 400,
        imageHeight: 200,
        imageAlt: "Custom image"
    });
}
function showErrorAlert(title, text, btntext, callbackfunction) {
    Swal.fire({
        icon: 'error',
        title: "",
        text: text,

        showCancelButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false,
        showCloseButton: true,
        confirmButtonText: btntext,

        showClass: {
            popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
        }
    }).then((result) => {

        if (typeof callbackfunction != 'undefined')
            callbackfunction();
    });
};
//
function ShowImage(elem) {

    var base64string = $(elem).attr("src");

    swal.fire({
        //title: "Image",
        width: 750,
        html: "<img src='" + base64string + "' style='width:750px;height:auto;'>",
        showCloseButton: true, showCancelButton: false, showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
        }
    }).then((result) => {
        setTimeout(function () {
            $(".swal2-popup").css("width", "750px!important");
        }, 200);

    });
}
function showConfirmationAlert(title, text, btntext, callbackfunction, failcallbackfunction) {
    Swal.fire({
        title: "", //title,
        text: text,
        icon: 'error',

        showCancelButton: true,

        allowOutsideClick: false,
        allowEscapeKey: false,
        confirmButtonClass: 'btn-danger',
        confirmButtonText: btntext,

        showClass: {
            popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
        }
    }).then((result) => {
        if (result.isConfirmed) {
            if (typeof callbackfunction != 'undefined')
                callbackfunction();
        }
        else {
            if (typeof failcallbackfunction != 'undefined')
                failcallbackfunction();
        }

    });
};
function showSuccessAlert(title, text, btntext, callbackfunction, params) {
    Swal.fire({
        title: "", //title,
        text: text,
        icon: "success",
        //width: "50%",
        showCancelButton: false,
        showCloseButton: true,
        allowOutsideClick: false,
        allowEscapeKey: false,
        confirmButtonClass: 'btn-success',
        confirmButtonText: btntext,

        showClass: {
            popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp'
        }
    }).then((result) => {

        if (typeof callbackfunction != 'undefined')
            callbackfunction();

    });

};

function showSuccessAlertAutoClose(title, text, btntext, callbackfunction, exec_time) {

    var exectime = 2000;

    if (exec_time != null)
        exectime = exec_time;

    const Toast = Swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: exectime,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;

            if (typeof callbackfunction != 'undefined')
                callbackfunction();
        }
    });
    Toast.fire({
        icon: "success",
        title: text
    });

};


function getFileExtension(fname) {
    return fname.slice((Math.max(0, fname.lastIndexOf(".")) || Infinity) + 1);
}
function getFileIcon(extension, indx) {
    var fileicon = null;
    var filetype = null;
    switch (extension) {
        case 'ppt':
        case 'pptx':
        case 'pptm':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file-powerpoint" style="display: contents;font-size: 7rem;color:orange"></i>';
            break;
        case 'txt':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file-text" style="display: contents;font-size: 7rem;color:white"></i>';
            break;
        case 'pdf':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file-pdf" style="display: contents;font-size: 7rem;color:red"></i>';
            break;
        case 'docx':
        case 'doc':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file-word" style="display: contents;font-size: 7rem;color:blue"></i>';
            break;
        case 'xls':
        case 'xlsx':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file-excel" style="display: contents;font-size: 7rem;color:green"></i>';
            break;
        case 'mp3':
        case 'ogg':
        case 'wav':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file-audio" style="display: contents;font-size: 7rem;color:#4B0082"></i>';
            break;
        case 'mp4':
        case 'mov':
        case 'mkv':
        case 'avi':
        case 'wmv':
        case '3gp':
        case 'webm':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file-video" style="display: contents;font-size: 7rem;color:#D2691E"></i>';
            break;
        case 'zip':
        case '7z':
        case 'rar':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file-archive" style="display: contents;font-size: 7rem;color:purple"></i>';
            break;
        case 'jpg':
        case 'jpeg':
        case 'jfif':
        case 'svg':
        case 'webp':
        case 'png':
        case 'gif':
        case 'bmp':
        case 'ico':
        case 'tif':
        case 'tiff':
            fileicon = '<i id="attachment' + indx + '" class="fa fa-image" style="display: contents;font-size: 7rem;color:seagreen"></i>';
            break;
        default:
            fileicon = '<i id="attachment' + indx + '" class="fa fa-file" style="display: contents;font-size: 7rem;color:grey"></i>';
    }
    return fileicon;
}

//function downloadFile(filearray, index, file_ext) {
//    // Convert Base64 to a Blob
//    var byteCharacters = atob(filearray[index].base64string);
//    var byteNumbers = new Array(byteCharacters.length);
//    for (var i = 0; i < byteCharacters.length; i++) {
//        byteNumbers[i] = byteCharacters.charCodeAt(i);
//    }
//    var byteArray = new Uint8Array(byteNumbers);
//    var blob = new Blob([byteArray], { type: 'application/octet-stream' });

//    // Create a download link
//    var link = document.createElement('a');
//    link.href = window.URL.createObjectURL(blob);

//    // Specify the file name (you can change it)
//    link.download = 'downloaded_file.' + file_ext;

//    // Append the link to the document
//    document.body.appendChild(link);

//    // Trigger a click on the link to start the download
//    link.click();

//    // Remove the link from the document
//    document.body.removeChild(link);
//}


function removeItemFromFormDataByIndex(formDataObj, indexToRemove) {
    const formDataArray = Array.from(formDataObj.entries());
    const updatedFormData = new FormData();

    formDataArray.forEach(([key, value], index) => {
        if (index !== indexToRemove) {
            updatedFormData.append(key, value);
        }
    });

    return updatedFormData;
}

function MakePopupReadOnly(popupid, closebuttonid) {

    $("#" + popupid + " input").prop("disabled", true);
    $("#" + popupid + " select").prop("disabled", true);
    $("#" + popupid + " button").prop("disabled", true);

    $("#" + popupid + " #" + closebuttonid).removeAttr("disabled");

}

function buildColumn_def(arrlength,hideFirstColumn) {
    columnDefs = []; var arr = new Array(arrlength);
    $.each(arr, function (index, value) {
        columnObj = {};
        columnObj["visible"] = (hideFirstColumn && (index == 0)) ? false : true;
        columnObj["targets"] = index;
        columnObj["sortable"] = false;
        columnObj["className"] = "text-center";
        columnDefs.push(columnObj);
    });
    return columnDefs;
}
function initialize_datatable(table) {

    dt_source = $(table.tableId).DataTable({
        layout: {
            top2End: {
                search: {
                    placeholder: table.placeholder,

                },

                buttons: [
                    {
                        text: table.btn_text,
                        className: table.btn_class_name,
                        action: function (e, dt, node, config) {

                            $(table.filterId).val('');
                            $(table.tableId).DataTable().search('').draw();
                        }
                    }
                ]
            },


            topEnd: null,

        },

        search: {
            return: true
        },
        "ajax": $.fn.dataTable.json({ url: table.ajax_url, data: table.input }),
        scrollCollapse: true,
        responsive: true,
        "jQueryUI": true,
        columnDefs: buildColumn_def(table.columns.length, table.hideFirstColumn),
        createdRow: table.createdRow_func,

        "columns": table.columns,


    });
}