
/*!
 * Ajax Get/Post Proxy functions JavaScript Library v1.0
 *
 * Copyright Mahmudur rahman Foundation and other contributors
 * Released under the MIT license
 * https://jquery.org/license
 * Date: 2021-03-02T17:08Z
 */

'use strict';


//const { Alert } = require("bootstrap");

var methodTypePost = "POST";
var methodTypeGet = "GET";

function PostObjectProxy(url, params, successCallback, isStringify = false, errorCallback = null, isAsync) {
    // showLoader();

    if (url == "") {
        return;
    }

    var tokenValue = $('#RequestVerificationToken').val();

    try {
        //showLoader();

        if (errorCallback == null) {
            $.ajax({
                type: methodTypePost,
                url: url,
                data: isStringify == false ? params : JSON.stringify(params),
                async: isAsync,
                cache: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                //headers: {
                //   'X-CSRF-TOKEN': tokenValue
                //},
                headers: {
                    'X-CSRF-TOKEN': tokenValue,
                    'Content-Type': 'application/json; charset=utf-8'
                },
                beforeSend: function () {
                    // showLoader();
                    console.log("before_send");
                },
                success: successCallback,
                error: function (xhr, textStatus, errorThrown) {
                    hideLoader();

                    showErrorAlert('Error');
                },
                failure: function (response) {

                    hideLoader();
                    // showErrorAlert("Error", response, "موافق");
                },
                complete: function () {
                    hideLoader();

                },
            });
        }
        else {
            $.ajax({
                type: methodTypePost,
                url: url,
                data: isStringify == false ? params : JSON.stringify(params),
                async: isAsync,
                cache: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                //headers: {
                //   'X-CSRF-TOKEN': tokenValue
                //},
                headers: {
                    'X-CSRF-TOKEN': tokenValue,
                    'Content-Type': 'application/json; charset=utf-8'
                },
                beforeSend: function () {
                    //showLoader();
                    console.log("before_send");
                },
                success: successCallback,
                error: errorCallback,
                failure: function (response) {
                    hideLoader();
                    // showErrorAlert("Error", response, "موافق");
                },
                complete: function () {
                    hideLoader();
                    console.log("coplete req");
                },
            });
        }

    } catch (e) {
        // hideLoader();
        // showErrorAlert("Error", e.message, "موافق");
    }
};

var ajaxPostObjectHandler = function (url, parameters, func, isStringify, errFunc, isAsync = false) {


    function onSuccess(response) {
        //hideLoader();
        if (response === 'SESSION_EXPIRED') {
            //showInformationAlert("Error", "Your session is expired. Please login to continue.", "موافق");
            window.location.href = '/Account/login';
        }
        else if (response === 'SESSION_EXPIRED') {
            // showInformationAlert("Error", "Your session is expired. Please login to continue.", "موافق");
            window.location.href = '/Account/login';
        }
        else
            func(response);
    };

    function onError(response) {
        //hideLoader();
        if (errFunc != null)
            errFunc(response);
        else {

            showErrorAlert('Error', "Internal Error. Please try again.", "OK");
        }
    };

    try {
        PostObjectProxy(url, parameters, onSuccess, isStringify, onError, isAsync);
    } catch (e) {
        showErrorAlert("Error", e.message, "موافق");
    }
};

function GetProxy(url, params, successCallback, isStringify = false) {

    // showLoader();
    if (url == "") {
        return;
    }

    if (params != null)
        params = isStringify == false ? params : JSON.stringify(params);
    else
        params = "";

    var tokenValue = $('#RequestVerificationToken').val();

    try {
        $.ajax({
            type: methodTypeGet,
            url: url,
            data: params,
            contentType: "application/json",
            async: true,
            cache: false,
            headers: {
                'X-CSRF-TOKEN': $('#RequestVerificationToken').val(),
            },
            beforeSend: function () {

            },
            success: successCallback,
            error: function (xhr, textStatus, errorThrown) {

                hideLoader();

                if (xhr.statusText != null) {
                    //showErrorAlert("Error", xhr.statusText, "موافق");
                }
                else {
                    //showErrorAlert("Error", xhr.responseJSON._ajaxresponse.responsetext, "موافق");
                }
            },
            failure: function (response) {
                hideLoader();
                //showErrorAlert("Error", response, "موافق");
            },
            complete: function () {
                hideLoader();
            },
        });
    } catch (e) {
        // hideLoader();
        hideLoader();
    }

};

var ajaxGetHandler = function (url, parameters, func, isStringify, isAsync = false) {

    function onSuccess(response) {

        if (response === 'SESSION_EXPIRED') {
            // showInformationAlert("Error", "Your session is expired. Please login to continue.", "موافق");
            window.location.href = '/Account/login';
        }
        else
            func(response);
    };
    try {
        GetProxy(url, parameters, onSuccess, isStringify);
    } catch (e) {
        showErrorAlert("Error", e.message, "موافق");
    }
};

var ajaxPostFormDataHandler = function (url, formdata, func) {

    function onSuccess(response) {

        if (response === 'SESSION_EXPIRED') {
            showInformationAlert("Error", "Your session is expired. Please login to continue.", "موافق");
            window.location.href = '/Account/login';
        }
        else if (response === 'SESSION_EXPIRED') {
            showInformationAlert("Error", "Your session is expired. Please login to continue.", "موافق");
            window.location.href = '/Account/login';
        }
        else
            func(response);
    };
    try {
        PostFormDataProxy(url, formdata, onSuccess);
    } catch (e) {
        showErrorAlert('Error', "Internal Error. Please try again.", "OK");
    }
};

function PostFormDataProxy(url, formData, successCallback) {
    //showLoader();
    if (url == "") {
        return;
    }

    var tokenValue = $('#RequestVerificationToken').val();

    try {

        $.ajax({
            type: methodTypePost,
            url: url,
            data: formData,

            //dataType: "json",
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            headers: {
                'X-CSRF-TOKEN': tokenValue
            },

            beforeSend: function () {

            },
            success: successCallback,
            error: function (xhr, textStatus, errorThrown) {
                hideLoader();

                showErrorAlert('Error');
            },
            failure: function (response) {

                hideLoader();
                // showErrorAlert("Error", response, "موافق");
            },
            complete: function () {
                hideLoader();
            },
        });

    } catch (e) {

        showErrorAlert('Error', "Internal Error. Please try again.", "OK");
    }

};

