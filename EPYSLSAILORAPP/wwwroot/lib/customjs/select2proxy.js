/*!
 * select2 proxy functions JavaScript Library v1.0
 *
 * Copyright Mahmudur rahman Foundation and other contributors
 * Released under the MIT license
 * https://jquery.org/license
 * Date: 2021-03-02T17:08Z
 */

'use strict';


var dir = $("html").attr("dir");
var LocaltokenValue = $('#X-CSRF-TOKEN-WEBADMINHEADER').val();

function returnSelect2Options() {

    var select2Options = {
        placeholder: 'PLACEHOLDERHERE',
        minimumInputLength: 0,
        allowClear: true,
        dropdownParent: null,
        pagination: true,
        multiple: false,
       // templateResult: formatresult,
       
        ajax: {
            type: 'GET',
            url: 'URLHERE',
            async: false,
            cache: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            //headers: {
            //    'Authorization': BaseTokenValues() == "" ? LocaltokenValue : 'bearer ' + BaseTokenValues() ,
            //    'X-CSRF-TOKEN-WEBADMINHEADER': BaseTokenValues() == "" ? LocaltokenValue : BaseTokenValues()

            //},
            headers: {
                'X-CSRF-TOKEN-WEBADMINHEADER': LocaltokenValue
            },
            delay: 250,
            //data: function (params) {
            //    return BaseAjaxDataValues(params);

            //},
            processResults: function (data, params) {
                params.page = params.page || 1;

                if (data.data != null) {
                    return {
                        results: $.map(data.data, function (obj) {
                            return { id: obj.id, text: obj.text };
                        }),
                        pagination: {
                            more: (params.page * 10) < data.recordstotal
                        }
                    }
                };
                //params.page = params.page || 1;
                //var more = (params.page * 10) < data.recordsTotal;
                //if (data.data != null) {
                //    return {
                //        results: $.map(data.data, function (obj) {
                //            return { id: obj.id, text: obj.text };
                //        }),
                //        pagination: {
                //            more: (params.page * 10) < data.recordstotal
                //        }
                //    };
                //}

               //// return processData(params, data);

            }
        },
        //initSelection: function (element, callback) {
        //    callback(GetPreLoadedValues())
        //  },
        transport: function (params, success, failure) {
            var $request = $.ajax(params);
            $request.then(success);
            $request.fail(failure);
            return $request;
        }
    };
    return select2Options;
}




