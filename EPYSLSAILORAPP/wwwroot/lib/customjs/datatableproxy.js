/*!
 * datatable proxy functions JavaScript Library v1.0 
 *
 * Copyright Mahmudur rahman Foundation and other contributors
 * Released under the MIT license
 * https://jquery.org/license
 * Date: 2021-03-02T17:08Z
 */

'use strict';


var dir = $("html").attr("dir");

$.extend(true, $.fn.dataTable.defaults, {
    "oLanguage": {
      
        "oPaginate": {
            "sPrevious": "Previous",
            "sNext": "Next",
            "sLast": "Last",
            "sFirst": "First",
           
        }
    },
    "lengthMenu": [[5,10, 25, 50, 100, 200], [10, 25, 50, 100, 200]],
    "bDestroy": true,
    "processing": true, // for show progress bar
    "serverSide": true, // for process server side
    "filter": true, // this is for disable filter (search box)
    "orderMulti": false, // for disable multiple column at once
    "responsive": true,
    "bAutoWidth": false,
    "autoWidth": false,
    "order": [0, 'desc'],
    "attr": {
        "autocomplete": 'off'
    },
    "bPaginate": true,
    "bInfo": true,
    "paging": true,
    "pagingType": "full_numbers",
    //"pagingType": "simple_numbers",
    "data":null,
    "stripeClasses": [],

    "language": {
        "infoFiltered": "",
        "processing": "<img style='width:80px;' src='../Images/loading.gif' />"
    },

   // dom: '<"row w-100  mb-3"<"col-lg-12"B>><"row   mb-3"<"col-lg-12">><"row w-100 "<"col-lg-2"l><"col-lg-7"><"col-lg-2"f>>rt<"row w-100"<"col-lg-6"i><"col-lg-6 text-center"p>>',
    //"buttons": ['print', 'copy', 'excel', 'pdf'],

    //"drawCallback": function (settings) {

    //    if (Math.ceil((this.fnSettings().fnRecordsDisplay()) / this.fnSettings()._iDisplayLength) > 1) {
    //        $('#Dt_paginate').css("display", "block");
    //    } else {
    //        $('#Dt_paginate').css("display", "none");
    //    }

    //    if (typeof jsdatatableloaded != 'undefined') {
    //        jsdatatableloaded();
    //    }


    //    ConvertNumberToCurrency();
    //    BindCurrentEventForTextBox();

    //}

    "drawCallback": function (settings) {
        var api = this.api(); // Use DataTables API for accessing settings

        if (Math.ceil(api.page.info().recordsDisplay / api.page.len()) > 1) {
            $('#Dt_paginate').css("display", "block");
        } else {
            $('#Dt_paginate').css("display", "none");
        }

        if (typeof jsdatatableloaded != 'undefined') {
            jsdatatableloaded();
        }

        ConvertNumberToCurrency();
        BindCurrentEventForTextBox();
    }


});
$.fn.DataTable.ext.pager.numbers_length = 10;


$.fn.dataTable.json = function (opts) {

    // Configuration options
    var conf = $.extend({
        pages: 10,     // number of pages to cache
        url: '',      // script url
        //data: null,   // function or object with parameters to send to the server
        // matching how `ajax.data` works in DataTables
        method: 'POST' // Ajax HTTP method
    }, opts);

    return function (request, drawCallback, settings) {
        var ajax = true;
        var requestStart = request.start;
        var requestLength = request.length;


        if (ajax) {

            request.start = requestStart;
            request.length = requestLength;
            if ($.isFunction(conf.data)) {
                var c = conf.data(request);
                if (d) {
                    $.extend(request, d);
                }
            }
            else if ($.isPlainObject(conf.data)) {
                $.extend(request, conf.data);
            }

            settings.jqXHR = $.ajax({
                "type": conf.method,
                "url": conf.url,
                "data": request,
                "dataType": "json",
                "cache": false,
                //"dataSrc": "json.data",
                //"dataSrc": "",
                'beforeSend': function (request) {
                    request.setRequestHeader('X-CSRF-TOKEN', $('#RequestVerificationToken').val());
                },
                "success": function (json) {
                    drawCallback(json);
                }
            });
        }
    }
};