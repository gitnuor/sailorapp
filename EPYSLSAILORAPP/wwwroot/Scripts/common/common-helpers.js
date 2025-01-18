﻿'use strict'

var itemSegmentValues;
var dateFormats = Object.freeze({
    DEFAULT: "MM/DD/YYYY"
});

function initCommonControls($formEl) {
    $(function () { // document ready
        $formEl.find('.ej2-datepicker').each(function (i, el) {
            $(el).datepicker({
                todayHighlight: true,
                format: "mm/dd/yyyy", //"mm/dd/yyyy"
                autoclose: true,
                todayBtn: "linked"
            }).on("show", function (date) {
                if (this.value && !date.date) {
                    $(this).datepicker('update', this.value);
                }
            });
        });
    })
}

function sumOfArrayItem(sourceArray, field) {
    return sourceArray.map(function (el) { return el[field]; }).reduce(function (prev, next) { return prev + next; })
}

function makeArray(stringData) {
    return $.parseJSON(stringData);
}

String.prototype.replaceAll = function (stringToFind, stringToReplace) {
    if (stringToFind === stringToReplace) return this;
    var temp = this;
    var index = temp.indexOf(stringToFind);
    while (index != -1) {
        temp = temp.replace(stringToFind, stringToReplace);
        index = temp.indexOf(stringToFind);
    }
    return temp;
};

function showBootboxConfirm(title, message, callback) {
    bootbox.confirm({
        title: title,
        message: message,
        size: "small",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            return callback(result);
        }
    });
}

function showBootboxAlert(message, title = "") {
    bootbox.alert({
        message: message,
        size: 'small',
        title: title
    });
}

function showBootboxPrompt(title, message, callback) {
    bootbox.prompt({
        title: title,
        message: "<p>" + message + "</p>",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            return callback(result);
        }
    });
}

/**
 * Show bootbox.js Select Prompt
 * @param {any} title - Prompt Title
 * @param {any} optionsArray - Input Options Array
 * @param {any} size - Prompt Size
 * @param {any} callback - Callback function
 */
function showBootboxSelectPrompt(title, optionsArray, size, callback) {
    if (!size) size = "small";
    bootbox.prompt({
        title: title,
        size: size,
        inputType: 'select',
        inputOptions: optionsArray,
        callback: function (result) {
            return callback(result);
        }
    });
}

/**
 * Show Bootbox Select2 Dialog
 * @param {any} label - Label for Select element
 * @param {any} elId - HTML element id
 * @param {any} title - Bootbox title
 * @param {any} data - Select2 options
 * @param {any} callback - callback function
 */
function showBootboxSelect2Dialog(label, elId, title, data, callback, value) {
    var dialog = bootbox.dialog({
        message: '<div class="form-group">'
            + '<label class= "control-label col-md-3">' + label + '</label>'
            + '<div class="col-md-9">'
            + '<select id="' + elId + '" class="form-control" style="width:100%"></select>'
            + '</div>'
            + '</div>'
            + '<div class="clearfix"></div><br>',
        title: title,
        animate: true,
        buttons: {
            cancel: {
                label: "Cancel",
                className: 'btn-danger m-w-100',
                callback: function () {
                    return callback(null);
                }
            },
            ok: {
                label: "Ok",
                className: 'btn-primary m-w-100',
                callback: function () {
                    var selectedData = $('#' + elId).select2('data')[0];
                    return callback(selectedData);
                }
            }
        }
    });

    dialog.on('shown.bs.modal', function () {
        dialog.removeAttr("tabindex");
        initSelect2($('#' + elId), data);
        if (value) setSelect2Data($('#' + elId), value);
    });
}

/**
 * Show Bootbox Select2 Dialog
 * @param {any} label - Label for Select element
 * @param {any} elId - HTML element id
 * @param {any} title - Bootbox title
 * @param {any} data - Select2 options
 * @param {any} callback - callback function
 */
function showBootboxSelect2DialogWithoutCancel(label, elId, title, data, callback, value) {
    var dialog = bootbox.dialog({
        message: '<div class="form-group">'
            + '<label class= "control-label col-md-3">' + label + '</label>'
            + '<div class="col-md-9">'
            + '<select id="' + elId + '" class="form-control" style="width:100%"></select>'
            + '</div>'
            + '</div>'
            + '<div class="clearfix"></div><br>',
        title: title,
        animate: true,
        buttons: {
            ok: {
                label: "Ok",
                className: 'btn-primary m-w-100',
                callback: function () {
                    var selectedData = $('#' + elId).select2('data')[0];
                    if (!selectedData || !selectedData.id) {
                        toastr.warning(`You must select ${elId}`);
                        return false;
                    }
                    else return callback(selectedData);
                }
            }
        }
    });

    dialog.on('shown.bs.modal', function () {
        dialog.removeAttr("tabindex");
        initSelect2($('#' + elId), data);
        if (value) setSelect2Data($('#' + elId), value);
    });
}

/**
 * Show Bootbox Select2 Dialog
 * @param {any} label - Label for Select element
 * @param {any} elId - HTML element id
 * @param {any} title - Bootbox title
 * @param {any} data - Select2 options
 * @param {any} callback - callback function
 */
function showBootboxSelect2MultipleDialog(label, elId, title, data, callback, value) {
    var dialog = bootbox.dialog({
        message: '<div class="form-group">'
            + '<label class= "control-label col-md-3">' + label + '</label>'
            + '<div class="col-md-9">'
            + '<select id="' + elId + '" class="form-control" style="width:100%" multiple></select>'
            + '</div>'
            + '</div>'
            + '<div class="clearfix"></div><br>',
        title: title,
        animate: true,
        buttons: {
            cancel: {
                label: "Cancel",
                className: 'btn-danger m-w-100',
                callback: function () {
                    return callback(null);
                }
            },
            ok: {
                label: "Ok",
                className: 'btn-primary m-w-100',
                callback: function () {
                    var selectedData = $('#' + elId).select2('data');
                    return callback(selectedData);
                }
            }
        }
    });

    dialog.on('shown.bs.modal', function () {
        dialog.removeAttr("tabindex");
        initSelect2($('#' + elId), data);
        if (value && Array.isArray(value)) setSelect2Data($('#' + elId), value);
    });
}

/**
 * Shows Sweet Alert Confirm Box
 * @param {string} title - Your confirm title
 * @param {string} message - Your confirm message
 * @param {Function} callback - Your callback function
 */
function sweetConfirmBox(title, message, callback) {
    swal({
        title: title,
        text: message,
        icon: "info",
        buttons: true
    }).then(function (yes) {
        return callback(yes);
    });
}

/**
 * Shows Sweet Alert Confirm Box
 * @param {string} title - Your confirm title
 * @param {Function} callback - Your callback function
 */
function sweetConfirmBoxWithPrompt(title, callback) {
    swal(title, {
        content: "input"
    })
        .then(function (value) {
            return callback(value);
        });
}

/**
 * Show Sweet Alert Success Box
 * @param {any} title - Your confirm title
 * @param {any} message - Your confirm message
 */
function sweetSuccessBox(title, message) {
    swal({
        title: title,
        text: message,
        icon: "success"
    });
}

/**
 * Preview on image load
 * @param {Event} event - onchange evnet
 * @param {HTMLImageElement} previewImgId - Preview image id
 */
function previewImage(event, previewImgId) {
    var preview = document.getElementById(previewImgId);
    var file = event.target.files[0];
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        preview.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}
function SetImageData(data) {
    if (data == null)
        return '/images/no-image.png';
    return 'data:image/jpeg;base64,' + data;
}
/**
 * Converts a string to boolean
 * @param {string} str - input string
 */
function convertToBoolean(str) {
    if (!str)
        return false;

    switch (str.toLowerCase()) {
        case "on":
        case "true":
        case "yes":
        case "1":
            return true;
        default: return false;
    }
}

String.prototype.ForGridQueryString = function () {
    return this.replaceAll("&amp;", "_^amp;").replaceAll("+", "_`amp;");
};
String.prototype.FromGridQueryString = function () {
    return this.replaceAll("&amp;", "&").replaceAll("_`amp;", "+");
};
String.prototype.ForQueryString = function () {
    return this.replaceAll("&", "_^amp;");
};
String.prototype.FromQueryString = function () {
    return this.replaceAll("_^amp;", "&");
};

function formDataToJson(data) {
    var jsonObj = {};
    $.each(data, function (i, v) {
        jsonObj[v.name] = v.value;
    });

    return jsonObj;
}

function formElToJson($formEl) {
    setAllFormCheckBoxValue($formEl);
    var disabledInputs = $formEl.find(':disabled');
    disabledInputs.each(function () {
        $(this).removeAttr('disabled');
    });

    var data = $formEl.serializeArray();
    var jsonObj = {};
    for (var i = 0; i < data.length; i++) {
        var prop = data[i];
        jsonObj[prop.name] = prop.value;
    }

    disabledInputs.each(function () {
        $(this).attr('disabled', 'disabled');
    });

    return jsonObj;
}

/**
 * Set
 * @param {any} $formEl
 * @returns FormData
 */
function getFormData($formEl) {
    setAllFormCheckBoxValue($formEl);
    var data = $formEl.serializeArray();

    var formData = new FormData();
    $.each(data, function (i, v) {
        formData.append(v.name, v.value);
    });

    return formData;
}

function setAllFormCheckBoxValue($formEl) {
    $formEl.find(':checkbox').each(function () {
        this.value = this.checked;
    });
}

/**
 * Set Form Data
 * @param {any} $formEl - Form Element
 * @param {any} data - data object
 */
function setFormData($formEl, data) {
    if (!data && ! typeof data === 'object') {
        console.error("Your data is not valid.");
        return;
    }

    try {
        $formEl.find("input, select, textarea").each(function () {
            try {
                var $input = $(this);
                var value = data[this.name];
                if (this.tagName.toLowerCase() === "textarea") {
                    $input.val(value);
                }
                else if (this.tagName.toLowerCase() === "input") {
                    switch (this.type) {
                        case "checkbox":
                            $input.prop("checked", value);
                            break;
                        case "radio":
                            $input.each(function (i) {
                                if ($(this).val() == value) $(this).attr({ checked: true })
                                else $(this).attr({ checked: false })
                            });
                            break;
                        case "date":
                            $input.val(formatDateToDefault(value));
                            break;
                        case "file":
                            break;
                        default:
                            $input.val(value);
                            break;
                    }
                }
                else if (this.tagName.toLowerCase() === "select") {
                    var optionListName = "";
                    if (this.multiple) {
                        optionListName = this.name.replace("Ids", '');
                        optionListName = optionListName.replace("IDs", '');
                        optionListName = this.name.replace("_ids", '');
                        optionListName = optionListName.replace("_IDs", '');
                    } else {
                        optionListName = this.name.replace("Id", '');
                        optionListName = optionListName.replace("ID", '');
                        optionListName = this.name.replace("_id", '');
                        optionListName = optionListName.replace("_ID", '');
                    }
                    optionListName += "_list";
                    initSelect2($input, data[optionListName], true, "Select Item", false);
                    $input.val(value).trigger("change");
                }
            } catch (e) {
                console.error(e);
            }
        });
    } catch (e) {
        console.error(e);
    }
}

/**
 * Disable elemnt
 * @param {HTMLElement} el - HTMLElement
 */
function disableElement(el) {
    el.attr('disabled', 'disabled');
}

/**
 * Enable element
 * @param {HTMLElement} el - HTMLElement
 */
function enableElement(el) {
    el.removeAttr('disabled');
}

/**
 * Makes an input readonly
 * @param {HTMLInputElement} el - Input element
 */
function makeReadonly(el) {
    el.prop("readonly", true);
}

/**
 * Removes readonly attribute
 * @param {HTMLInputElement} el - Input element
 */
function removeReadonly(el) {
    el.prop("readonly", false);
}

/**
 * Check a checkbox element
 * @param {HTMLInputElement} el - Checkbox Element
 */
function checkInput(el) {
    el.prop("checked", true);
}

function setCheckBox(el, value) {
    el.prop("checked", value);
}

/**
 * Un-check a checkbox element
 * @param {HTMLInputElement} el - Checkbox Element
 */
function unCheckInput(el) {
    el.prop("checked", false);
}

/**
 * Returned if checkbox is checked or not
 * @param {HTMLInputElement} el - Checkbox Element
 */
function isChecked(el) {
    return el.is(':checked')
}

/**
 * Sets Select2 Combo
 * @param {HTMLInputElement} el - Select Element
 */
function setSelect2Combo(el, val) {
    el.val(val).trigger("change");
}

/**
 * Resets Select2 Combo
 * @param {HTMLInputElement} el - Select Element
 */
function resetSelect2Combo(el) {
    el.val(null).trigger("change");
}

/**
 * Convert Select2 Array to Select Array
 * @param {Array} srcArray - Select2 Array
 */
function convertToSelectOptions(srcArray) {
    return srcArray.map(function (obj) {
        return { value: obj.id, text: obj.text };
    });
}

/**
 * Converts a select2 option array into key value pair object.
 * @param {any} list - Select2 Option Array
 * @returns object - {key : "value"} pair format
 */
function convertSelect2ArrayToSelectOptionForFilter(list) {
    var result = list.reduce(function (result, item) {
        result[item.id] = item.text;
        return result;
    }, {});

    return result;
}

function filterPlaceholderControl() {
    $(".filter-control input").attr('placeholder', 'Type & Enter for Search');
    $(".filter-control input").css('border', '1px solid gray');
    $(".filter-control input").css('font-size', '11px');
}

function setMultiSelectValueInBootstrapTableEditable(list, value) {
    var selectedNameIds = _.filter(_.uniq(value), function (value) {
        return value > 0;
    });

    var selectedListItem = _.filter(list, function (i) { return this.values.indexOf(i.id) > -1; }, { "values": selectedNameIds });
    var selectedText = _.map(selectedListItem, function (el) { return el.text }).join(",");
    return { id: selectedNameIds.toString(), text: selectedText };
}

function setMultiSelectValueByValueInBootstrapTableEditable(list, value, $el) {
    var selectedNameIds = _.filter(_.uniq(value.split(',')), function (value) {
        return value > 0;
    });

    var selectedListItem = _.filter(list, function (i) { return this.values.indexOf(i.value) > -1; }, { "values": selectedNameIds });
    var selectedText = _.map(selectedListItem, function (el) { return el.text }).join(",");
    $($el).text(selectedText);
    $($el).editable('setValue', selectedText);
    return { id: selectedNameIds.toString(), text: selectedText };
}

/**
 * Initialize Select2
 * @param {HTMLSelectElement} Select Element
 * @param {Array} Array of Select2 Data
 * @param {Boolean} Allow Clear
 * @param {String} Placeholder
 * @param {Boolean} Show Default Option
 */
function initSelect2($el, data, allowClear = true, placeholder = "Select a Value", showDefaultOption = true) {
    if (showDefaultOption)
        data.unshift({ id: '', text: '' });

    $el.html('').select2({ 'data': data, 'allowClear': allowClear, 'placeholder': placeholder });
}

/**
 *
 * @param {Array} Array Data
 * @param {string} Filter By Array element
 */
function getMaxIdForArray(data, filterBy) {
    if (data.length == 0)
        return 1;
    if (!filterBy)
        throw "Filter By is required.";

    var maxId = Math.max.apply(Math, data.map(function (el) { return el[filterBy]; }));
    return ++maxId;
}
function filterFormArray(data, column, search) {
    var result = data.filter(m => m[column] == search);
    return result;
}

function setSelect2Data($el, value) {
    if (value)
        $el.val(value).trigger('change');
}

/**
 *
 * @param {HTMLButtonElement} el - HTML Button element
 * @param {HTMLDivElement} $parentEl - Jquery Div element
 */
function toggleActiveToolbarBtn(el, $parentEl) {
    $parentEl.find('button').not("#" + el.id).removeClass("btn-success").addClass("btn-default text-grey");

    if (el instanceof jQuery)
        el.removeClass("btn-default text-grey").addClass("btn-success text-white");
    else
        $(el).removeClass("btn-default text-grey").addClass("btn-success text-white");
}

/**
 * Get Day, Month, Year
 * @param {any} date - Date object
 */
function getDayMonthYear(date) {
    var formattedDate = formatDateToDDMMMMYYYY(date);
    var dArray = formattedDate.split(' ');
    return { day: dArray[0], month: dArray[1], year: dArray[2] };
}

/**
 * Format Date like (Jan 17, 2021)
 * @param {any} date - Date Object
 */
function formatDateToMMMDDYYYY(date) {
    if (!date) return "";
    return moment(date).format("MMM DD, YYYY");
}

/**
 * Format Date like (26 August 2021)
 * @param {any} date - Date Object
 */
function formatDateToDDMMMMYYYY(date) {
    if (!date) return "";
    return moment(date).format("DD MMMM YYYY");
}

/**
 * Format Date like (1st January 2020)
 * @param {any} date - Date Object
 */
function formatDateToDoMMMMYYYY(date) {
    if (!date) return "";
    return moment(date).format("Do MMMM YYYY");
}

/**
 * Format date like (1 Jan 2020)
 * @param {any} date
 */
function formatDateToDMMMYYYY(date) {
    if (!date) return "";
    return moment(date).format("D MMM YYYY");
}

function formatDateToMMMMDoYYYY(date) {
    if (!date) return "";
    return moment(date).format("MMMM, Do, YYYY");
}

/**
 * Format date like (9:00 AM 1 Jan 2020)
 * @param {any} date
 */
function formatDateToHHMMADMMMYYYY(date) {
    if (!date) return "";
    return moment(date).format("hh:mm A D MMM YYYY");
}

/**
 * Format date like (7:00 AM)
 * @param {any} date
 */
function formatDateTohhmmA(date) {
    if (!date) return "";
    return moment(date).format("hh:mm A");
}

/**
 * Format date like (01/01/2020)
 * @param {any} date
 */
function formatDateToDDMMYYYY(date) {
    if (!date) return "";
    return moment(date).format("DD/MM/YYYY");
}

/**
 * Format date like (12/30/2020)
 * @param {any} date
 */
function formatDateToDefault(date) {
    if (!date) return "";
    return moment(date).format(dateFormats.DEFAULT);
}

/**
 * Format date like (12/30/2020)
 * @param {any} date
 */
function formatDateTimeToDefault(date) {
    if (!date) return "";
    return moment(date).format("MM/DD/YYYY HH:mm");
}

/**
 * Format date like (19:20)
 * @param {any} date - Date Object
 */
function formatDateToHHmm(date) {
    if (!date) return "";
    return moment(date).format("HH:mm")
}

/**
 * Get day of week like Monday, Sunday from date
 * @param {any} data
 */
function getDayofWeek(date) {
    if (!date) return "";
    return moment(date).format("dddd");
}

function getChatDate(date) {
    if (!date) return "";
    return moment(date).format("MMM DD");
}

function showResponseError(error) {
    console.error(error);
    try {
        if (error.response && error.response.data && error.response.data.Message) toastr.error(error.response.data.Message);
        else toastr.error(error);
    } catch (e) {
        toastr.error(error);
    }
}

function showValidationErrorToast(errors) {
    console.error(errors);
    toastr.error("Please correct all valiation errors.");
}

function distinctArrayByProperty(arr, key) {
    var unique = {};
    var distinct = [];
    for (var i = 0; i < arr.length; i++) {
        var x = arr[i];
        if (!unique[x[key]]) {
            distinct.push(x);
            unique[x[key]] = true;
        }
    }

    return distinct;
}

function getNewArrayItems(oldArray, newArray, property) {
    var newItems = [];
    for (var i = 0; i < newArray.length; i++) {
        var item = newArray[i];
        var exists = oldArray.find(function (el) { return el[property] === item[property] });
        if (!exists) newItems.push(item);
    }

    return newItems;
}

// #region EJ2 Grid
/**
 * Get data using ej2 grid
 * @param {any} apiEndPoint
 */
function ej2GetData(apiEndPoint) {
    var baseUrl = window.location.protocol + '//' + window.location.host;
    var url = new URL(`${baseUrl}${apiEndPoint}`);
    var dataManager = new ej.data.DataManager({
        url: url.href,
        adaptor: new ej.data.WebApiAdaptor(),
        crossDomain: true,
        headers: [{ 'Authorization': `Bearer ${localStorage.getItem("token")}` }]
    });
    return dataManager;
}
var selData = function () { };
var selRows = [];
/**
 * @param title - Finder Title.
 * @param pageId - PageID where this is being used.
 * @param apiEndPoint - Api endpoint to fetch data.
 * @param generateColumnsDynamically - if true columns will be generated dynamically, other wise fields and headerTexts fields are mandatory.
 * @param fields - Column fields to show seperated by comma(,).
 * @param headerTexts - Column display value seperated by comma(,).
 * @param hiddenFields - Define columns you want to hide seperated by comma(,). Required only when fields & headerTexts are empty.
 * @param widths - column widths seperated by comma(,)
 * @param isMultiselect - is multiselect?
 * @param allowResizing - allow column resizing.
 * @param allowFiltering - allow column filtering.
 * @param modalSize - modal size, options are "modal-sm", "modal-lg", "". Default value "".
 * @param onSelect - on select call back function.
 * @param onMultiselect - on multi select call back function.
 * @param onClose - on close function.
 * */
function commonFinder() {
    var self = this;
    this.args;
    if (arguments) {
        this.args = arguments[0];
    }
    this.title = this.args.title;
    this.pageId = this.args.pageId;
    this.apiEndPoint = this.args.apiEndPoint;
    this.fields = this.args.fields || null;
    this.headerTexts = this.args.headerTexts || null;
    this.widths = this.args.widths || null;
    this.formats = this.args.formats || null;
    this.customFormats = this.args.customFormats || null;
    this.editTypes = this.args.editTypes || null;
    this.hiddenFields = this.args.hiddenFields || null;
    this.generateColumnsDynamically = (!this.args.fields && !this.args.headerTexts) ? true : false;
    this.isMultiselect = false;
    this.selectedIds = "";
    this.allowResizing = true;
    this.allowFiltering = this.args.allowFiltering || true;
    this.allowPaging = true;
    this.enableContextMenu = false;
    this.modalSize = this.args.modalSize || "";
    this.top = this.args.top || "30px";
    this.onSelect = this.args.onSelect || null;
    this.height = this.args.height || 300;
    this.onMultiselect = this.args.onMultiselect || null;
    this.onFinish = this.args.onFinish || null;
    this.data = this.args.data || [];
    this.primaryKeyColumn = this.args.primaryKeyColumn;
    this.selectedUniqueIds = this.args.selectedUniqueIds || [];
    this.autofitColumns = true;
    this.filterSettings = this.args.filterSettings || "Excel";
    this.allowEditing = this.args.allowEditing || false;
    this.seperateSelection = this.args.seperateSelection || false;
    if (this.args.isMultiselect != null && this.args.isMultiselect != undefined) this.isMultiselect = this.args.isMultiselect;
    if (this.args.allowResizing != null && this.args.allowResizing != undefined) this.allowResizing = this.args.allowResizing;
    if (this.args.allowFiltering != null && this.args.allowFiltering != undefined) this.allowFiltering = this.args.allowFiltering;
    if (this.args.allowPaging != null && this.args.allowPaging != undefined) this.allowPaging = this.args.allowPaging;
    if (this.isMultiselect) this.selectedIds = this.args.selectedIds;
    if (this.args.autofitColumns != null && this.args.autofitColumns != undefined) {
        this.autofitColumns = this.args.widths != null && this.args.widths != undefined ? false : this.args.autofitColumns;
    }
    else {
        this.autofitColumns = this.args.widths != null && this.args.widths != undefined ? false : true;
    }
    if (this.args.enableContextMenu != null && this.args.enableContextMenu != undefined) this.enableContextMenu = this.args.enableContextMenu;
    if (this.args.seperateSelection != null && this.args.seperateSelection != undefined) {
        if (this.args.seperateSelection) {
            this.seperateSelection = this.args.seperateSelection;
            this.height = 200;
        }
    }

    this.selectedRowIndexs = [];
    this.columns = [];
    this.filterSettings = [];
    this.modalId = `modal-common-finder-${this.pageId}`;
    this.tableId = `tbl-common-finder-${this.pageId}`;
    this.$modalEl = null;
    this.$tblEl = null;
    this.$tblElSelected = null;
    this.copiedRecord = null;

    if (!this.apiEndPoint && !(this.data || this.data.length === 0)) {
        throw "Api endoint is required for finder.";
    }
    if (!this.primaryKeyColumn) {
        throw "Primary key column is required.";
    }
    if (!this.pageId) {
        throw "PageId is required finder for finder.";
    }

    this.template =
        `<div class="modal fade" id="${this.modalId}" tabindex="-1" role="dialog">
            <div class="modal-dialog ${this.modalSize}" style="margin-top: ${this.top};">
                <div class="modal-content">
                    <div class="modal-header" onmousedown="if (drag) drag(this.parentNode, event)">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true"><i class="fa fa-times" style="margin-top: 5px; margin-right: 5px;"></i></span>
                        </button>
                        <h4 class="modal-title">${this.title}</h4>
                    </div>
                    <div class="modal-body">
                        <table id="${this.tableId}"></table>
                        ${this.seperateSelection ? `<button type="button" id="btn-selection-${this.tableId}` + "-selected" + `" class="btn btn-primary" onclick="AddSelectedRow(event)">Add Selected</button>
                                                    <table id="${this.tableId}` + "-selected" + `"></table>` : ""}
                    </div>
                    <div class="modal-footer" " ${this.isMultiselect ? "" : 'style= "display: none;"'} >
                        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                        ${this.isMultiselect ? '<button type="button" id="btn-selection-' + this.pageId + '" class="btn btn-primary">Ok</button>' : ""}
                    </div>
                </div>
            </div>
        </div>`;
    selRows = [];
    this.showModal = function () {
        $(`#${self.pageId}`).append(self.template);
        self.$modalEl = $(`#${self.modalId}`);

        self.$modalEl.on('hidden.bs.modal', function (e) {
            $(`#${self.pageId}`).find(`#${self.modalId}`).remove();
        });
        self.$modalEl.find(`#btn-selection-${self.pageId}`).click(function () {
            var selectedRows = self.$tblEl.getSelectedRecords();
            if (self.$tblElSelected)
                selectedRows = self.$tblElSelected.dataSource;
            if (selectedRows.length > 0) self.onMultiselect(selectedRows);
            else toastr.warning("You didn't select any record.");

            if (self.onFinish) self.onFinish();
            else self.$modalEl.modal("hide");
        });

        if (!self.generateColumnsDynamically) {
            if (!self.fields) throw "Fields is required. Field names must be seperated by comma(,)";
            if (!self.headerTexts) throw "Header texts is required for finder. Header Texts must be seperated by comma(,)";

            var fieldArray = self.fields.split(",");
            var headerTextArray = self.headerTexts.split(",");
            if (fieldArray.length !== headerTextArray.length) throw "Number of fields and header texts must be same.";

            var widthArray;
            if (self.widths) {
                widthArray = self.widths ? self.widths.split(",") : [];
                if (widthArray && (fieldArray.length !== widthArray.length)) throw "Number of fields, header texts and widths must be same.";
            }

            var formatArray;
            if (self.formats) {
                formatArray = self.formats ? self.formats.split(",") : [];
                if (formatArray && (fieldArray.length !== formatArray.length)) throw "Number of fields, header texts, widths and formats must be same.";
            }

            var customFormatterArray;
            if (self.customFormats) {
                customFormatterArray = self.customFormats ? self.customFormats.split(",") : [];
                if (customFormatterArray && (fieldArray.length !== customFormatterArray.length)) throw "Number of fields, header texts, widths and formats must be same.";
            }

            var editTypeArray;
            if (self.editTypes) {
                editTypeArray = self.editTypes ? self.editTypes.split(",") : [];
                if (editTypeArray && (fieldArray.length !== editTypeArray.length)) throw "Number of fields, header texts, widths, formats and edit types must be same.";
            }

            for (var ind = 0; ind < fieldArray.length; ind++) {
                var headerText = headerTextArray[ind];
                var column;
                if (this.apiEndPoint != null && this.apiEndPoint != undefined) {
                    column = {
                        field: fieldArray[ind], headerText: headerText
                    };
                } else {
                    column = {
                        field: fieldArray[ind], headerText: headerText, filter: { operator: "contains" }
                    };
                }

                if (widthArray && widthArray.length > 0) if (widthArray[ind]) column["width"] = widthArray[ind];
                if (formatArray && formatArray.length > 0) if (formatArray[ind]) column["format"] = formatArray[ind];
                if (customFormatterArray && customFormatterArray.length > 0) if (customFormatterArray[ind]) column["valueAccessor"] = customFormatterArray[ind];

                if (editTypeArray && editTypeArray.length > 0) {
                    if (editTypeArray[ind]) {
                        column["allowEditing"] = true;
                        column["editType"] = editTypeArray[ind];
                        if (editTypeArray[ind] == "booleanedit") column["displayAsCheckBox"] = true;
                        if (editTypeArray[ind] == "numericedit") column["edit"] = { params: { showSpinButton: false, validateDecimalOnType: true, decimals: 0, format: "N", min: 1 } };
                    }
                    else column["allowEditing"] = false;
                }
                self.columns.push(column);
            }
        }

        self.showModalInfo();
    }

    this.hideModal = function () {
        self.$modalEl.modal("hide");
    }

    /**
     * Init table
     * @param {any} data - Array of records to show in table.
     */
    this.initTable = function (data) {
        var primaryKeyColumn = self.columns.find(function (el) { return el.field == self.primaryKeyColumn });
        if (primaryKeyColumn) primaryKeyColumn["isPrimaryKey"] = true;
        else self.columns.push({ field: self.primaryKeyColumn, isPrimaryKey: true, visible: false });

        var selectionType = "Single";
        if (self.isMultiselect) {
            self.columns.unshift({ type: 'checkbox', width: 20 });
            selectionType = "Multiple";
        }

        if (self.$tblEl) self.$tblEl.destroy();
        var gridOptions = {
            height: self.height,
            dataSource: data,
            allowSelection: self.isMultiselect,
            selectionSettings: { type: selectionType, checkboxOnly: true, persistSelection: true },
            allowResizing: self.allowResizing,
            allowFiltering: self.allowFiltering,
            //filterSettings: { type: 'Normal' }, //Normal
            allowTextWrap: true,
            textWrapSettings: { wrapMode: 'Header' },
            allowPaging: self.allowPaging,
            pageSettings: { pageSizes: true },
            columns: self.columns,
            recordDoubleClick: self.isMultiselect ? null : self.onSelect,
        }
        if (self.enableContextMenu) {
            gridOptions["editSettings"] = { allowAdding: true };
            gridOptions["enableContextMenu"] = true;
            gridOptions["contextMenuItems"] = [
                { text: 'Copy', target: '.e-content', id: 'copy' },
                { text: 'Paste', target: '.e-content', id: 'paste' },
                { text: 'Copy And Paste', target: '.e-content', id: 'copyAndPaste' },
            ];
            gridOptions["contextMenuClick"] = function (args) {
                if (args.item.id === 'copy') {
                    self.copiedRecord = objectCopy(args.rowInfo.rowData);
                }
                else if (args.item.id === 'paste') {
                    if (self.copiedRecord == null) {
                        toastr.error("Please copy first!");
                        return;
                    }
                    self.copiedRecord[self.primaryKeyColumn] = getMaxIdForArray(self.$tblEl.getCurrentViewRecords(), self.primaryKeyColumn);
                    self.$tblEl.addRecord(self.copiedRecord);
                }
                else if (args.item.id === 'copyAndPaste') {
                    self.copiedRecord = objectCopy(args.rowInfo.rowData);
                    self.copiedRecord[self.primaryKeyColumn] = getMaxIdForArray(self.$tblEl.getCurrentViewRecords(), self.primaryKeyColumn);
                    self.$tblEl.addRecord(self.copiedRecord);
                }
            }
        }

        if (self.allowEditing) gridOptions["editSettings"] = { allowEditing: true, mode: "Normal" };

        self.$tblEl = new ej.grids.Grid(gridOptions);

        self.$tblEl.appendTo(`#${self.tableId}`);

        self.setSeletedRows();

        if (self.isMultiselect && self.selectedIds) {
            self.$tblEl["dataBound"] = function (args) {
                if (self.autofitColumns) {
                    self.$tblEl.autoFitColumns();
                }
                var ids = self.selectedIds.split(',');
                var selIds = [];
                for (var i = 0; i < ids.length; i++) {
                    var primaryId = self.$tblEl.getRowIndexByPrimaryKey(parseInt(ids[i]));
                    if (primaryId < 0)
                        primaryId = self.$tblEl.getRowIndexByPrimaryKey(ids[i]);
                    selIds.push(primaryId);
                }
                self.$tblEl.selectRows(selIds);
            }
        } else {
            if (self.autofitColumns) {
                self.$tblEl["dataBound"] = function (e) {
                    self.$tblEl.autoFitColumns();
                };
            }
        }
    }

    /**
     * Init table
     * @param {any} data - Array of records to show in table.
     */
    this.initTableSelected = function (data) {
        //var primaryKeyColumn = self.columns.find(function (el) { return el.field == self.primaryKeyColumn });
        //if (primaryKeyColumn) primaryKeyColumn["isPrimaryKey"] = true;
        //else self.columns.push({ field: self.primaryKeyColumn, isPrimaryKey: true, visible: false });

        //var selectionType = "Single";
        if (self.isMultiselect) {
            //self.columns.unshift({ type: 'checkbox', width: 50 });
            //selectionType = "Multiple";
            var colList = [];
            for (var i = 0; i < self.columns.length; i++) {
                if (i != 0)
                    colList.push(self.columns[i]);
            }
        }

        if (self.$tblElSelected) self.$tblElSelected.destroy();
        var gridOptions = {
            height: self.height,
            dataSource: data,
            //allowSelection: self.isMultiselect,
            //selectionSettings: { type: selectionType, checkboxOnly: true, persistSelection: true },
            //allowResizing: self.allowResizing,
            //allowFiltering: self.allowFiltering,
            //filterSettings: { type: 'Normal' }, //Normal
            allowTextWrap: true,
            textWrapSettings: { wrapMode: 'Header' },
            allowPaging: self.allowPaging,
            pageSettings: { pageSizes: true },
            columns: colList,
            //recordDoubleClick: self.isMultiselect ? null : self.onSelect,
        }

        //if (self.enableContextMenu) {
        //    gridOptions["editSettings"] = { allowAdding: true };
        //    gridOptions["enableContextMenu"] = true;
        //    gridOptions["contextMenuItems"] = [
        //        { text: 'Copy', target: '.e-content', id: 'copy' },
        //        { text: 'Paste', target: '.e-content', id: 'paste' },
        //        { text: 'Copy And Paste', target: '.e-content', id: 'copyAndPaste' },
        //    ];
        //}

        //if (self.allowEditing) gridOptions["editSettings"] = { allowEditing: true, mode: "Normal" };

        self.$tblElSelected = new ej.grids.Grid(gridOptions);

        self.$tblElSelected.appendTo(`#${self.tableId}` + `-selected`);

        //self.setSeletedRows();

        if (self.autofitColumns) {
            self.$tblElSelected["dataBound"] = function (e) {
                self.$tblElSelected.autoFitColumns();
            };
        }
    }
    this.showModalInfo = function () {
        if (self.data && self.data.length > 0) {
            if (self.generateColumnsDynamically && self.columns.length === 0) {
                var hiddenFieldsArray = self.hiddenFields ? self.hiddenFields.split(",") : [];
                var obj = data.rows ? data.rows[0] : data[0];

                var fields = Object.keys(obj);
                for (var i = 0; i < fields.length; i++) {
                    var field = fields[i];
                    var headerText = field.replace(/([A-Z])/g, " $1");
                    headerText = headerText.charAt(0).toUpperCase() + headerText.slice(1);
                    var column = { field: field, headerText: headerText };

                    // If hidden column
                    if (hiddenFieldsArray.length > 0) {
                        var isHiddenColum = hiddenFieldsArray.find(function (el) { return el == column.field; });
                        if (isHiddenColum) return;
                    }

                    self.columns.push(column);
                }
            }

            self.initTable(self.data);
        }
        else {
            var dataAdapter = ej2GetData(self.apiEndPoint);
            self.initTable(dataAdapter);
        }
        if (self.seperateSelection) {
            self.initTableSelected([]);
        }
        self.$modalEl.modal("show");
    }

    this.setSeletedRows = function () {
        for (var i = 0; i < self.selectedUniqueIds.length; i++) {
            var rowIndex = self.$tblEl.getRowIndexByPrimaryKey(self.selectedUniqueIds[i]);
            self.$tblEl.selectRow(rowIndex);
        }
    }
    selData = function () {
        var selectedRows = self.$tblEl.getSelectedRecords();
        for (var i = 0; i < selectedRows.length; i++) {
            var result = filterFormArray(selRows, self.primaryKeyColumn, selectedRows[i][self.primaryKeyColumn]);
            if (result.length == 0) {
                selRows.push(selectedRows[i]);
            }
        }
        self.initTableSelected(selRows);
    }
}
function AddSelectedRow(e) {
    e.preventDefault();
    selData();
}
function initEJ2Grid() {
    //
    var prop;
    if (arguments) {
        prop = arguments[0];
    }

    var data = prop.data || null;
    var apiEndPoint = prop.apiEndPoint;
    var columns = prop.columns;
    var commandColumn = prop.commandColumn || "";
    var tableId = prop.tableId;
    var $tblEl;// = prop.$tblEl;
    var frozenColumns = 0;
    var frozenRows = 0;
    var allowExcelExport = true;
    var allowPdfExport = true;
    var allowSorting = false;
    var showColumnChooser = true;
    var allowResizing = true;
    var allowGrouping = false;
    var allowFiltering = true;
    var allowPaging = true;
    var autofitColumns = true;
    var enableContextMenu = false;
    var fields = prop.fields || null;
    var headerTexts = prop.headerTexts || null;
    var widths = prop.widths || null;
    var formats = prop.formats || null;
    var customFormats = prop.customFormats || null;
    var editTypes = prop.editTypes || null;
    var visibilities = prop.visibilities || null;
    var hiddenFields = prop.hiddenFields || null;
    var hasChildGrid = prop.hasChildGrid || false;
    var commandClick = prop.commandClick;
    var handleDoubleClick = prop.handleDoubleClick;
    var handleToolbarClick = prop.handleToolbarClick;
    var actionComplete = prop.actionComplete;
    var actionBegin = prop.actionBegin;
    var contextMenuClick = prop.contextMenuClick;
    var handleKeyPressed = prop.handleKeyPressed;
    var rowDataBound = prop.rowDataBound;
    var queryCellInfo = prop.queryCellInfo;
    //var toolbarClick = prop.toolbarClick;
    var editSettings = prop.editSettings || null;
    var contextMenuItems = prop.contextMenuItems || null;
    var enableSingleClickEdit = prop.enableSingleClickEdit || false;
    var aggregates = prop.aggregates || [];
    if (prop.allowExcelExport != null && prop.allowExcelExport != undefined) allowExcelExport = prop.allowExcelExport;
    if (prop.allowPdfExport != null && prop.allowPdfExport != undefined) allowPdfExport = prop.allowPdfExport;
    if (prop.allowSorting != null && prop.allowSorting != undefined) allowSorting = prop.allowSorting;
    if (prop.showColumnChooser != null && prop.showColumnChooser != undefined) showColumnChooser = prop.showColumnChooser;
    if (prop.allowResizing != null && prop.allowResizing != undefined) allowResizing = prop.allowResizing;
    if (prop.allowFiltering != null && prop.allowFiltering != undefined) allowFiltering = prop.allowFiltering;
    if (prop.allowPaging != null && prop.allowPaging != undefined) allowPaging = prop.allowPaging;
    if (prop.allowGrouping != null && prop.allowGrouping != undefined) allowGrouping = prop.allowGrouping;
    if (prop.frozenColumns != null && prop.frozenColumns != undefined) frozenColumns = prop.frozenColumns;
    if (prop.frozenRows != null && prop.frozenRows != undefined) frozenRows = prop.frozenRows;
    //if (prop.autofitColumns != null && prop.autofitColumns != undefined) autofitColumns = prop.autofitColumns;
    if (prop.autofitColumns != null && prop.autofitColumns != undefined) {
        autofitColumns = prop.widths != null && prop.widths != undefined ? false : prop.autofitColumns;
    }
    else {
        autofitColumns = prop.widths != null && prop.widths != undefined ? false : true;
    }
    //
    var showDefaultToolbar = prop.showDefaultToolbar == null || prop.showDefaultToolbar == undefined ? true : prop.showDefaultToolbar;
    var toolbar = prop.toolbar || [];
    if (prop.toolbar && showDefaultToolbar) {
        toolbar.push.apply(toolbar, ['ColumnChooser', 'ExcelExport', 'PdfExport', { text: 'Refresh', tooltipText: 'Refresh Data', prefixIcon: 'e-icons e-refresh', id: 'tblRefresh' }]);
    }

    if (!apiEndPoint && !data) throw "Api endoint is required.";

    if (!columns && !fields) throw "Column list is required";

    if (!tableId) throw "tableId is required.";

    if (!columns) {
        if (!fields) throw "Fields is required. Field names must be seperated by comma(,)";
        if (!headerTexts) throw "Header texts is required for finder. Header Texts must be seperated by comma(,)";

        var fieldArray = fields.split(",");
        var headerTextArray = headerTexts.split(",");
        if (fieldArray.length !== headerTextArray.length) throw "Number of fields and header texts must be same.";

        var widthArray;
        if (widths) {
            widthArray = widths ? widths.split(",") : [];
            if (widthArray && (fieldArray.length !== widthArray.length)) throw "Number of fields and header texts and widths must be same.";
        }

        var formatArray;
        if (formats) {
            formatArray = formats ? formats.split(",") : [];
            if (formatArray && (fieldArray.length !== formatArray.length)) throw "Number of fields and header texts and widths and formats must be same.";
        }

        var customFormatterArray;
        if (customFormats) {
            customFormatterArray = customFormats ? customFormats.split(",") : [];
            if (customFormatterArray && (fieldArray.length !== customFormatterArray.length)) throw "Number of fields, header texts, widths and formats must be same.";
        }

        var editTypeArray;
        if (editTypes) {
            editTypeArray = editTypes ? editTypes.split(",") : [];
            if (editTypeArray && (fieldArray.length !== editTypeArray.length)) throw "Number of fields, header texts, widths, formats and edit types must be same.";
        }

        var visibilityArray;
        if (visibilities) {
            visibilityArray = visibilities ? visibilities.split(",") : [];
            if (visibilityArray && (fieldArray.length !== visibilityArray.length)) throw "Number of fields and header texts and widths, formats and visibilities must be same.";
        }

        var hiddenFieldsArray = hiddenFields ? hiddenFields.split(",") : [];

        if (!columns) columns = [];
        for (var ind = 0; ind < fieldArray.length; i++) {
            var field = fieldArray[ind];
            if (visibilityArray && visibilityArray.length > 0 && !convertToBoolean(visibilityArray[ind])) return;

            var headerText = headerTextArray[ind];
            //var column = { field: field, headerText: headerText };
            var column;
            if (this.apiEndPoint != null && this.apiEndPoint != undefined) {
                column = {
                    field: fieldArray[ind], headerText: headerText
                };
            } else {
                column = {
                    field: fieldArray[ind], headerText: headerText, filter: { operator: "contains" }
                };
            }

            if (widthArray && widthArray.length > 0) if (widthArray[ind]) column["width"] = widthArray[ind];
            if (formatArray && formatArray.length > 0) if (formatArray[ind]) column["format"] = formatArray[ind];
            if (customFormatterArray && customFormatterArray.length > 0) if (customFormatterArray[ind]) column["valueAccessor"] = customFormatterArray[ind];

            if (editTypeArray && editTypeArray.length > 0) {
                if (editTypeArray[ind]) {
                    column["allowEditing"] = true;
                    column["editType"] = editTypeArray[ind];
                    if (editTypeArray[ind] == "booleanedit") column["displayAsCheckBox"] = true;
                }
                else column["allowEditing"] = false;
            }

            if (hiddenFieldsArray.length > 0) {
                var isHiddenColumn = hiddenFieldsArray.find(function (el) { return el == field; });
                if (isHiddenColumn) column["visible"] = false;
            }

            columns.push(column);
        }
    }

    if (commandColumn) columns.unshift(commandColumn);

    if (!data) data = ej2GetData(apiEndPoint);

    if ($tblEl) $tblEl.destroy();

    //var setFocus;
    ej.base.enableRipple(true);

    var gridOptions = {
        allowExcelExport: allowExcelExport,
        allowPdfExport: allowPdfExport,
        dataSource: data,
        allowSorting: allowSorting,
        allowResizing: allowResizing,
        allowFiltering: allowFiltering,
        allowPaging: allowPaging,
        allowGrouping: allowGrouping,
        frozenColumns: frozenColumns,
        frozenRows: frozenRows,
        showColumnChooser: showColumnChooser,
        commandClick: commandClick,
        actionComplete: function (args) {
            //if (args.requestType === 'beginEdit') {
            //    args.form.elements.namedItem(setFocus.field).focus();
            //}

            if (actionComplete) return actionComplete(args);
        },
        actionFailure: function (args) {
            if (args && args.error[0] && args.error[0].error) toastr.error(args.error[0].error.responseText);
        },
        actionBegin: actionBegin,
        recordDoubleClick: function (args) {
            //setFocus = args.column;
            if (handleDoubleClick) return handleDoubleClick(args);
        },
        keyPressed: function (args) {
            if (handleKeyPressed && args.key === "Enter") return handleKeyPressed(args)
        },
        allowTextWrap: true,
        textWrapSettings: { wrapMode: 'Header' },//wrapMode: 'Content'/'Header'/'Both'
        pageSettings: { pageCount: 5, currentPage: 1, pageSize: 10, pageSizes: true },
        editSettings: editSettings,
        contextMenuItems: contextMenuItems,
        enableContextMenu: enableContextMenu,
        contextMenuClick: contextMenuClick,
        rowDataBound: rowDataBound,
        queryCellInfo: queryCellInfo,
        //toolbarClick: toolbarClick,
        columns: columns,
        aggregates: aggregates
    };

    if (toolbar.length > 0) {
        //
        gridOptions["toolbar"] = toolbar;
        gridOptions["toolbarClick"] = function (args) {
            //
            if (args.item.id === "tblRefresh") $tblEl.refresh();
            else if (handleToolbarClick) return handleToolbarClick(args);
        }
    }

    if (enableSingleClickEdit) {
        gridOptions["load"] = function () {
            var instance = $(tableId)[0].ej2_instances[0];
            instance.element.addEventListener('mouseup', function (e) {
                if (e.which === 1 && e.target.classList.contains("e-rowcell")) {
                    if (instance.isEdit) instance.endEdit();
                    var index = parseInt(e.target.getAttribute("Index"));
                    instance.selectRow(index);
                    instance.startEdit();
                }
            })
        }
    }

    if (autofitColumns) {
        gridOptions["dataBound"] = function (e) {
            $tblEl.autoFitColumns();
        };
    }

    $tblEl = new ej.grids.Grid(gridOptions);

    if (hasChildGrid) {
        $tblEl["childGrid"] = initChildTable(prop.childOptions);

        // Child inside Child
        if (prop.childOptions.hasChildGrid) {
            $tblEl["childGrid"]["childGrid"] = initChildTable(prop.childOptions.childOptions)
        }
    }

    $tblEl.appendTo(tableId);

    return $tblEl;
}

function initChildTable(propChildOptions) {
    if (!propChildOptions) throw "Child options is required.";

    if (!propChildOptions.apiEndPoint && !propChildOptions.data) throw "Api endpoint or data is required for child grid.";

    if (!propChildOptions.columns) throw "Columns required for child grid.";

    var childOptions = {
        apiEndPoint: "",
        data: "",
        queryString: "Id",
        additionalQueryParams: "",
        allowPaging: true,
        allowSorting: true,
        allowResizing: true,
        allowFiltering: true,
        allowEditing: false,
        hasChildGrid: false,
        autofitColumns: false,
        //enableContextMenu = false,
        //contextMenuClick = null,
        //contextMenuItems = null,
        toolbar: "",
        columns: [],
        editSettings: null
    };

    childOptions.apiEndPoint = propChildOptions.apiEndPoint;
    childOptions.data = propChildOptions.data;
    childOptions.columns = propChildOptions.columns;
    childOptions.hasChildGrid = propChildOptions.hasChildGrid || false;
    childOptions.autofitColumns = propChildOptions.autofitColumns || false;
    childOptions.childOptions = propChildOptions.childOptions || null;
    childOptions.commandClick = propChildOptions.commandClick || null;
    childOptions.actionComplete = propChildOptions.actionComplete || null;
    childOptions.additionalQueryParams = propChildOptions.additionalQueryParams || "";
    childOptions.editSettings = propChildOptions.editSettings || null;

    //childOptions.contextMenuItems = propChildOptions.contextMenuItems || null;
    //childOptions.contextMenuClick = propChildOptions.contextMenuClick || null;

    //if (propChildOptions.enableContextMenu != null && propChildOptions.enableContextMenu != undefined) childOptions.enableContextMenu = propChildOptions.enableContextMenu;
    if (propChildOptions.queryString != null && propChildOptions.queryString != undefined) childOptions.queryString = propChildOptions.queryString;
    if (propChildOptions.allowPaging != null && propChildOptions.allowPaging != undefined) childOptions.allowPaging = propChildOptions.allowPaging;
    if (propChildOptions.allowSorting != null && propChildOptions.allowSorting != undefined) childOptions.allowSorting = propChildOptions.allowSorting;
    if (propChildOptions.allowResizing != null && propChildOptions.allowResizing != undefined) childOptions.allowResizing = propChildOptions.allowResizing;
    if (propChildOptions.allowFiltering != null && propChildOptions.allowFiltering != undefined) childOptions.allowFiltering = propChildOptions.allowFiltering;
    if (propChildOptions.allowEditing != null && propChildOptions.allowEditing != undefined) childOptions.allowEditing = propChildOptions.allowEditing;
    if (propChildOptions.autofitColumns != null && propChildOptions.autofitColumns != undefined) childOptions.autofitColumns = propChildOptions.autofitColumns;
    if (propChildOptions.toolbar != null && propChildOptions.toolbar != undefined) childOptions.toolbar = propChildOptions.toolbar;

    var $childTableEl = {
        queryString: childOptions.queryString,
        allowPaging: childOptions.allowPaging,
        allowSorting: childOptions.allowSorting,
        allowResizing: childOptions.allowResizing,
        allowFiltering: childOptions.allowFiltering,
        toolbar: childOptions.toolbar,
        allowEditing: childOptions.allowEditing,
        columns: childOptions.columns,
        childOptions: childOptions.childOptions,
        commandClick: childOptions.commandClick,
        //contextMenuItems: childOptions.contextMenuItems,
        //enableContextMenu: childOptions.enableContextMenu,
        //contextMenuClick: childOptions.contextMenuClick,
        actionComplete: childOptions.actionComplete,
        load: function () {
            var that = this;
            if (childOptions.additionalQueryParams) {
                var rowData = this.parentDetails.parentRowData;
                var fields = childOptions.additionalQueryParams.split(",");
                for (var i = 0; i < fields.length; i++) {
                    var field = fields[i];
                    if (field && rowData[field]) that.query.addParams(field, rowData[field]);
                }
            }
        }
    }

    if (childOptions.allowEditing) {
        $childTableEl[childOptions.editSettings] = { allowEditing: true, allowAdding: true, allowDeleting: true }
    }

    if (childOptions.autofitColumns) {
        $childTableEl["dataBound"] = function (e) {
            $childTableEl.autoFitColumns();
        };
    }

    if (childOptions.data) {
        $childTableEl.dataSource = childOptions.data;
    }
    else {
        $childTableEl.dataSource = ej2GetData(childOptions.apiEndPoint);
    }

    return $childTableEl;
}

/**
 *
 * */
function ej2GridDropDownObj() {
    var args = null;
    if (arguments) {
        args = arguments[0];
    }

    var fieldName;
    var element;
    var elementObj;
    var apiEndPoint = args.apiEndPoint;
    var dataSource = (args.dataSource == undefined && apiEndPoint != undefined) ? ej2GetData(apiEndPoint) : args.dataSource;
    var displayField = args.displayField;
    var defaultValue = args.defaultValue;
    var currentRowData;
    var ob;
    var fields = args.fields || { value: 'id', text: 'text' };
    var placeholder = args.placeholder || "Select an Item";
    var onChange = args.onChange;
    var width = (args.width == undefined) ? 200 : args.width;
    return {
        create: function () {
            element = document.createElement('input');
            return element;
        },
        read: function () {
            return elementObj.value;
        },
        destroy: function () {
            elementObj.destroy();
        },
        write: function (e) {
            ob = e;
            fieldName = e.element.name;
            currentRowData = e.rowData;
            var value = e.rowData[fieldName] ? e.rowData[fieldName].toString() : defaultValue;
            dataSource = (dataSource == undefined && e.column.dataSource == undefined && apiEndPoint != undefined) ? ej2GetData(apiEndPoint) : e.column.dataSource != undefined ? e.column.dataSource : dataSource;
            elementObj = new ej.dropdowns.DropDownList({
                dataSource: dataSource,
                fields: fields,
                value: value,
                placeholder: placeholder,
                floatLabelType: 'Never',
                width: width,
                maxWidth: 500,
                allowFiltering: true,
                filtering: function (e) {
                    var query = new ej.data.Query();
                    query = (e.text != "") ? query.where(fields.text, "contains", e.text, true) : query;
                    e.updateData(dataSource, query);
                },
                change: function (f) {
                    if (!f.isInteracted || !f.itemData) return false;

                    if (onChange) {
                        ob.rowData[displayField] = f.itemData.text;
                        currentRowData[fieldName] = f.itemData.id;
                        currentRowData[displayField] = f.itemData.text;
                        return onChange(f.itemData, currentRowData);
                    }
                }
            });
            elementObj.appendTo(element);
        }
    }
}

function ej2GridDropDownObjByfilter() {
    var args = null;
    if (arguments) {
        args = arguments[0];
    }

    var fieldName;
    var element;
    var elementObj;
    var apiEndPoint = args.apiEndPoint;
    var filterBy = args.filterBy;
    var displayField = args.displayField;
    var defaultValue = args.defaultValue;
    var currentRowData;
    var fields = args.fields || { value: 'id', text: 'text' };
    var placeholder = args.placeholder || "Select an Item";
    var onChange = args.onChange;

    var width = (args.width == undefined) ? 200 : args.width;

    return {
        create: function () {
            element = document.createElement('input');
            return element;
        },
        read: function () {
            return elementObj.value;
        },
        destroy: function () {
            elementObj.destroy();
        },
        write: async function (e) {
            fieldName = e.element.name;
            currentRowData = e.rowData;
            var value = e.rowData[fieldName] ? e.rowData[fieldName].toString() : defaultValue;

            var dataSource = await getSelectOptionFilterdDataAsync(apiEndPoint, currentRowData, filterBy);

            elementObj = new ej.dropdowns.DropDownList({
                dataSource: dataSource,
                fields: fields,
                value: value,
                placeholder: placeholder,
                floatLabelType: 'Never',
                width: width,
                maxWidth: 500,
                allowFiltering: true,
                filtering: async function (e) {
                    var query = new ej.data.Query();
                    query = (e.text != "") ? query.where(fields.text, "contains", e.text, true) : query;
                    e.updateData(dataSource, query);
                },
                change: function (e) {
                    if (!e.isInteracted || !e.itemData) return false;

                    if (onChange) {
                        currentRowData[fieldName] = e.itemData.id;
                        currentRowData[displayField] = e.itemData.text;
                        return onChange(e.itemData, currentRowData);
                    }
                }
            });
            elementObj.appendTo(element);
        }
    }
}

async function getSelectOptionFilterdDataAsync(apiEndpoint, rowData, filterBy) {
    var baseUrl = window.location.protocol + '//' + window.location.host;
    var url = new URL(`${baseUrl}${apiEndpoint}`);

    var filterArray = filterBy.split(",");
    for (var i = 0; i < filterArray.length; i++) {
        if (rowData[filterArray[i]]) url.searchParams.append(filterArray[i], rowData[filterArray[i]]);
    }
    var response = await axios.get(url.href);
    return response.data;
}

function ej2GridDropDownObjV2() {
    var args = null;
    if (arguments) {
        args = arguments[0];
    }
    var fieldName;
    var element;
    var elementObj;
    var apiEndPoint = args.apiEndPoint;
    var dataSource = (args.dataSource == undefined) ? ej2GetData(apiEndPoint) : args.dataSource;
    var displayField = args.displayField;
    var defaultValue = args.defaultValue;
    var currentRowData;
    var fields = args.fields || { value: 'id', text: 'text' };
    var placeholder = args.placeholder || "Select an Item";
    var onChange = args.onChange;

    var width = (args.width == undefined) ? 200 : args.width;

    return {
        create: function () {
            element = document.createElement('input');
            return element;
        },
        read: function () {
            return elementObj.value;
        },
        destroy: function () {
            elementObj.destroy();
        },
        write: async function (evt) {
            fieldName = evt.element.name;
            currentRowData = evt.rowData;
            var value = evt.rowData[fieldName] ? evt.rowData[fieldName].toString() : defaultValue;
            elementObj = new ej.dropdowns.DropDownList({
                dataSource: dataSource,
                fields: fields,
                value: value,
                placeholder: placeholder,
                floatLabelType: 'Never',
                width: width,
                maxWidth: 500,
                allowFiltering: true,
                filtering: function (e) {
                    var query = new ej.data.Query();
                    query = (e.text != "") ? query.where(fields.text, "contains", e.text, true) : query;
                    e.updateData(dataSource, query);
                },
                change: function (e) {
                    if (!e.isInteracted || !e.itemData) return false;

                    if (onChange) {
                        currentRowData[fieldName] = e.itemData.id;
                        currentRowData[displayField] = e.itemData.text;
                        return onChange(e.itemData, currentRowData);
                    }
                }
            });
            elementObj.appendTo(element);
        }
    }
}

function ej2GridDisplayFormatterV2(column, rowData, args) {
    var text = "";
    var field = column.replace(new RegExp("id", "ig"), "");
    if (column.contains("ValueId")) {
        text = rowData[field + "Desc"];
        if (!text) {
            var segmentValue = itemSegmentValues[field + "List"].find(function (el) { return el.id == rowData[column] });
            text = segmentValue ? segmentValue.text : "";
        }
    }
    else text = rowData[field + "Name"] || rowData[field + "Names"] || rowData[field] || "Empty";

    return text;
}

function ej2GridDropDownObjRemoteData() {
    var args = null;
    if (arguments) {
        args = arguments[0];
    }

    var fieldName;
    var element;
    var elementObj;
    var apiEndPoint = args.apiEndPoint;
    var displayField = args.displayField;
    var defaultValue = args.defaultValue;
    var currentRowData;
    var fields = args.fields || { value: 'id', text: 'text' };
    var placeholder = args.placeholder || "Select an Item";
    var onChange = args.onChange;

    var width = (args.width == undefined) ? 200 : args.width;

    return {
        create: function () {
            element = document.createElement('input');
            return element;
        },
        read: function () {
            return elementObj.value;
        },
        destroy: function () {
            elementObj.destroy();
        },
        write: async function (e) {
            fieldName = e.element.name;
            currentRowData = e.rowData;
            var value = e.rowData[fieldName] ? e.rowData[fieldName].toString() : defaultValue;

            var dataSource = ej2GetData(apiEndPoint);

            elementObj = new ej.dropdowns.DropDownList({
                dataSource: dataSource,
                fields: fields,
                value: value,
                placeholder: placeholder,
                floatLabelType: 'Never',
                width: width,
                maxWidth: 500,
                allowFiltering: true,
                change: function (e) {
                    if (!e.isInteracted || !e.itemData) return false;

                    if (onChange) {
                        currentRowData[fieldName] = e.itemData.id;
                        currentRowData[displayField] = e.itemData.text;
                        return onChange(e.itemData, currentRowData);
                    }
                }
            });

            elementObj.appendTo(element);
        }
    }
}

/**
 *
 * */
function ej2GridMultipleDropDownObj() {
    var element;
    var elementObj;
    var currentRowData;
    var fieldName;

    var args = null;
    if (arguments) {
        args = arguments[0];
    }

    var dataSource = args.dataSource;
    var displayField = args.displayField;
    var fields = args.fields || { value: 'id', text: 'text' };
    var placeholder = args.placeholder || "Select an Item";
    var onChange = args.onChange;
    var width = (args.width == undefined) ? 200 : args.width;

    var getSelectedText = function (selectedData) {
        return dataSource
            .filter(function (el) { return selectedData.includes(el.id) })
            .map(function (e) { return e.text }).toString();
    }

    return {
        read: function () {
            return getSelectedText(elementObj.value);
        },
        create: function () {
            element = document.createElement('input');
            return element;
        },
        write: function (args) {
            fieldName = args.element.name;
            var value = [];
            if (args.rowData[displayField]) {
                value = Array.isArray(args.rowData[displayField]) ? args.rowData[displayField] : [args.rowData[displayField]];
            }
            currentRowData = args.rowData;

            elementObj = new ej.dropdowns.MultiSelect({
                dataSource: dataSource,
                fields: fields,
                showSelectAll: true,
                value: value,
                placeholder: placeholder,
                width: width,
                maxWidth: 500,
                allowFiltering: true,
                change: function (e) {
                    if ((e && e.value) || e.oldValue) {
                        e.value = e.value || e.oldValue;
                        currentRowData[fieldName] = getSelectedText(e.value);
                        currentRowData[displayField] = e.value;
                        return onChange(e.value, currentRowData);
                    }
                }
            });
            elementObj.appendTo(element);
        }
    }
}

//async function ej2GridDropDownObjForShade() {
//    var args = null;
//    if (arguments) {
//        args = arguments[0];
//    }

//    var fieldName;
//    var element;
//    var elementObj;
//    var displayField = args.displayField;
//    var defaultValue = args.defaultValue;
//    var $tableEl = args.tableEl;
//    var currentRowData;
//    var fields = args.fields || { value: 'id', text: 'text' };
//    var placeholder = args.placeholder || "Select an Item";
//    var onChange = args.onChange;

//
//    var dataSource = await axios.get("/api/selectoption/shade-list");

//
//    return {
//        create: function () {
//            element = document.createElement('input');
//            return element;
//        },
//        read: function () {
//            return elementObj.value;
//        },
//        destroy: function () {
//            elementObj.destroy();
//        },
//        write: function (e) {
//
//            fieldName = e.element.name;
//            currentRowData = e.rowData;
//            var value = e.rowData[fieldName] ? e.rowData[fieldName].toString() : defaultValue;
//            elementObj = new ej.dropdowns.DropDownList({
//                dataSource: dataSource,
//                fields: fields,
//                value: value,
//                placeholder: placeholder,
//                floatLabelType: 'Never',
//                width: 200,
//                allowFiltering: true,
//                filtering: function (e) {
//
//                    var query = new ej.data.Query();
//                    query = (e.text != "") ? query.where(fields.text, "contains", e.text, true) : query;
//                    e.updateData(dataSource, query);
//                },
//                change: function (e) {
//                    if (!e.isInteracted || !e.itemData) return false;

//                    try {
//                        $tableEl.updateCell(0, displayField, e.itemData.text);
//                    } catch (e) {
//                        console.error(e);
//                    }

//                    if (onChange) {
//                        currentRowData[fieldName] = e.itemData.id;
//                        currentRowData[displayField] = e.itemData.text;
//                        return onChange(e.itemData, currentRowData);
//                    }
//                }
//            });
//            elementObj.appendTo(element);
//        }
//    }
//}

/**
 * EJ2 grid dropdown edit params for value fields only.
 * This is used where we only need a single field (text fileld mostly).
 * Field name is required because datasource will be mapped to an array of objects containing field name.
 * */
function ej2DropdownParams() {
    var self = this;
    this.args;
    if (arguments) {
        this.args = arguments[0];
    }

    if (!this.args.field) throw "Field name is required.";
    if (!this.args.dataSource && this.args.dataSource.length === 0) throw "Datasource is required.";

    var dataSource = this.args.dataSource.map(function (el) { return { [self.args.field]: el.text ? el.text : el } });

    return {
        params: {
            query: new ej.data.Query(),
            allowFiltering: true,
            dataSource: dataSource
        }
    }
}

function ej2GridEmptyFormatter(args, rowData, column) {
    column.disableHtmlEncode = false;
    return rowData[args]
        ? rowData[args]
        : '<a href="javascript:void(0)" class="editable-link edit">Empty</a>';
}

function ej2GridDisplayFormatter(args, rowData, column) {
    var text = "";
    var field = args.replace(new RegExp("id", "ig"), "");
    if (args.contains("ValueId")) {
        text = rowData[field + "Desc"];
        if (!text) {
            var segmentValue = itemSegmentValues[field + "List"].find(function (el) { return el.id == rowData[args] });
            text = segmentValue ? segmentValue.text : "";
        }
    }
    else {
        if (column.displayField != undefined) {
            var sv = column.dataSource.find(function (el) { return el.id == rowData[args] });
            rowData[column.displayField] = sv ? sv.text : "";
            field = column.displayField;
        }
        text = rowData[field + "Name"] || rowData[field + "Names"] || rowData[field] || "Empty"
    };

    return text;
}

function ej2GridImageFormatter(args, rowData, column) {
    //
    column.disableHtmlEncode = false;
    var img = rowData[column.field] ? rowData[column.field] : "";
    var imgs = img.split("/");
    var imgName = img.length > 0 ? imgs[imgs.length - 1] : "No Image";
    return `<div> <img style="width: 30px; height: 30px;" src="${img}" alt="${imgName}" /></div>`;
}

function ej2GridColorFormatter(args, rowData, column) {
    column.disableHtmlEncode = false;
    var color = rowData["RGBOrHex"] ? rowData["RGBOrHex"] : "";
    return `<span style="background:${color}; width: 100px; height: 20px; display:inline-block"></span>`;
}

function ej2GridDateFormatter(args, rowData, column) {
    column.disableHtmlEncode = false;
    var dt = rowData[column.field];
    var d = new Date(dt);
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    //if (day < 10) {
    //    day = "0" + day;
    //}
    //if (month < 10) {
    //    month = "0" + month;
    //}
    var date = month + "/" + day + "/" + year;
    return `<span style="width: 100px; height: 20px; display:inline-block">${date}</span>`;
}

// #endregion end of EJ2 Grid

// #region Yarn Item Grid
/**
 * Base Item master to be used across application
 * @param {any} id - Primary key
 * @param {any} unitId - Unit of measurement Id.
 * @param {any} displayUnit - Measurement unit name.
 */
function BaseItemMaster(id, unitId, displayUnit) {
    this.Id = id;
    this.ItemMasterID = 0;
    this.Segment1ValueId = 0;
    this.Segment1ValueDesc = "";
    this.Segment2ValueId = 0;
    this.Segment2ValueDesc = "";
    this.Segment3ValueId = 0;
    this.Segment3ValueDesc = "";
    this.Segment4ValueId = 0;
    this.Segment4ValueDesc = "";
    this.Segment5ValueId = 0;
    this.Segment5ValueDesc = "";
    this.Segment6ValueId = 0;
    this.Segment6ValueDesc = "";
    //this.Segment7ValueId = 0;
    //this.Segment7ValueDesc = "";
    this.Segment8ValueId = 0;
    this.Segment8ValueDesc = "";
    this.Segment9ValueId = 0;
    this.Segment9ValueDesc = "";
    this.Segment10ValueId = 0;
    this.Segment10ValueDesc = "";
    this.Segment11ValueId = 0;
    this.Segment11ValueDesc = "";
    this.Segment12ValueId = 0;
    this.Segment12ValueDesc = "";
    this.Segment13ValueId = 0;
    this.Segment13ValueDesc = "";
    this.Segment14ValueId = 0;
    this.Segment14ValueDesc = "";
    this.Segment15ValueId = 0;
    this.Segment15ValueDesc = "";
    this.UnitID = unitId;
    this.DisplayUnitDesc = displayUnit;
    this.EntityState = 4;
}

async function getYarnItemColumnsAsync(isEditable = true) {
    try {
        var response = await axios.get("/api/items/yarn/item-segments");
        itemSegmentValues = response.data;

        var columns = [
            {
                field: 'Segment1ValueId', headerText: 'Composition', allowEditing: isEditable, required: true, width: 350, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment1ValueList,
                displayField: "Segment1ValueDesc", edit: ej2GridDropDownObj({
                    width: 350
                })
            },
            { field: "Segment1ValueDesc", visible: false },
            {
                field: 'Segment2ValueId', headerText: 'Yarn Type', allowEditing: isEditable, width: 120, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment2ValueList,
                displayField: "Segment2ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment2ValueDesc", visible: false },
            {
                field: 'Segment3ValueId', headerText: 'Manufacturing Process', allowEditing: isEditable, width: 100, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment3ValueList,
                displayField: "Segment3ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment3ValueDesc", visible: false },
            {
                field: 'Segment4ValueId', headerText: 'Sub Process', allowEditing: isEditable, width: 100, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment4ValueList,
                displayField: "Segment4ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment4ValueDesc", visible: false },
            {
                field: 'Segment5ValueId', headerText: 'Quality Parameter', allowEditing: isEditable, width: 100, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment5ValueList,
                displayField: "Segment5ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment5ValueDesc", visible: false },
            {
                field: 'Segment6ValueId', headerText: 'Count', allowEditing: isEditable, width: 80, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment6ValueList,
                displayField: "Segment6ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment6ValueDesc", visible: false },
            //{
            //    field: 'Segment7ValueId', headerText: 'No of Ply', allowEditing: isEditable, width: 40, valueAccessor: ej2GridDisplayFormatter,dataSource: itemSegmentValues.Segment7ValueList,
            //        displayField: "Segment7ValueDesc", edit: ej2GridDropDownObj({
            //            defaultValue: "1",
            //            width: 50
            //        }), textAlign: 'Center', headerTextAlign: 'Center'
            //},
            //{ field: "Segment7ValueDesc", visible: false }
        ];

        if (isEditable) {
            columns.unshift({
                headerText: 'Commands', width: 120, commands: [
                    { type: 'Edit', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-edit' } },
                    { type: 'Delete', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-delete' } },
                    { type: 'Save', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-update' } },
                    { type: 'Cancel', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-cancel-icon' } }]
            });
        };

        return columns;
    } catch (e) {
        showResponseError(e);
    }
}

async function getDyesItemColumnsAsync(isEditable = true) {
    try {
        var response = await axios.get("/api/items/dyes/item-segments");
        itemSegmentValues = response.data;

        var columns = [
            {
                field: 'Segment1ValueId', headerText: 'Primary Group', allowEditing: isEditable, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment1ValueList,
                displayField: "Segment1ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment1ValueDesc", visible: false },
            {
                field: 'Segment2ValueId', headerText: 'Item Name', allowEditing: isEditable, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment2ValueList,
                displayField: "Segment2ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment2ValueDesc", visible: false }
        ];

        if (isEditable) {
            columns.splice(0, 0,
                {
                    headerText: 'Commands', width: 120, commands: [
                        { type: 'Edit', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-edit' } },
                        { type: 'Delete', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-delete' } },
                        { type: 'Save', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-update' } },
                        { type: 'Cancel', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-cancel-icon' } }]
                })
        };

        return columns;
    } catch (e) {
        showResponseError(e);
    }
}

async function getChemicalsItemColumnsAsync(isEditable = true) {
    try {
        var response = await axios.get("/api/items/chemicals/item-segments");
        itemSegmentValues = response.data;

        var columns = [
            {
                field: 'Segment1ValueId', headerText: 'Primary Group', allowEditing: isEditable, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment1ValueList,
                displayField: "Segment1ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment1ValueDesc", visible: false },
            {
                field: 'Segment2ValueId', headerText: 'Agent', allowEditing: isEditable, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment2ValueList,
                displayField: "Segment2ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment2ValueDesc", visible: false },
            {
                field: 'Segment3ValueId', headerText: 'Item Name', allowEditing: isEditable, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment3ValueList,
                displayField: "Segment3ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment3ValueDesc", visible: false },
            {
                field: 'Segment4ValueId', headerText: 'Form', allowEditing: isEditable, valueAccessor: ej2GridDisplayFormatter, dataSource: itemSegmentValues.Segment4ValueList,
                displayField: "Segment4ValueDesc", edit: ej2GridDropDownObj({
                })
            },
            { field: "Segment4ValueDesc", visible: false }
        ];

        if (isEditable) {
            columns.splice(0, 0,
                {
                    headerText: 'Commands', width: 120, commands: [
                        { type: 'Edit', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-edit' } },
                        { type: 'Delete', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-delete' } },
                        { type: 'Save', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-update' } },
                        { type: 'Cancel', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-cancel-icon' } }]
                })
        };

        return columns;
    } catch (e) {
        showResponseError(e);
    }
}

function getYarnItemColumnsForDisplayOnly() {
    var columns = [
        { field: 'Segment1ValueId', visible: false },
        { field: 'Segment1ValueDesc', headerText: 'Composition', allowEditing: false, width: 350 },
        { field: 'Segment1ValueId', visible: false },
        { field: 'Segment2ValueDesc', headerText: 'Yarn Type', allowEditing: false, width: 120 },
        { field: 'Segment2ValueId', visible: false },
        { field: 'Segment3ValueDesc', headerText: 'Process', allowEditing: false, width: 100 },
        { field: 'Segment3ValueId', visible: false },
        { field: 'Segment4ValueDesc', headerText: 'Sub Process', allowEditing: false, width: 100 },
        { field: 'Segment4ValueId', visible: false },
        { field: 'Segment5ValueDesc', headerText: 'Quality Parameter', allowEditing: false, width: 100 },
        { field: 'Segment5ValueId', visible: false },
        { field: 'Segment6ValueDesc', headerText: 'Count', allowEditing: false, width: 80 },
        { field: 'Segment6ValueId', visible: false },
        //{ field: 'Segment7ValueDesc', headerText: 'No of Ply', allowEditing: false, width: 40, textAlign: 'Center', headerTextAlign: 'Center' },
        //{ field: 'Segment7ValueId', visible: false }
    ];

    return columns;
}

async function getItemColumnsForDisplayBySubGroupAsync(subGroupName) {
    try {
        var response = await axios.get(`/api/items/item-structure-for-display/${subGroupName}`);
        var columns = [];
        for (var i = 0; i < response.data.length; i++) {
            var field = response.data[i];
            columns.push({ field: field.SegmentValueDescName, headerText: field.SegmentDisplayName, valueAccessor: ej2GridEmptyFormatter, allowEditing: false });
        }

        return columns;
    } catch (e) {
        showResponseError(e);
    }
}

async function getItemColumnsBySubGroupAsync(isEditable = true, itemSubGroupId) {
    try {
        var response = await axios.get(`/api/items/item-structure/${itemSubGroupId}`);
        var itemStructures = response.data.ItemStructures;
        itemSegmentValues = response.data.ItemSegmentValues;
        var columns = [];
        for (var i = 0; i < itemStructures.length; i++) {
            var field = itemStructures[i];
            if (!field.AllowAdd) {
                var itemWiseSegmentValues = itemSegmentValues.filter(function (el) { return el.desc == field.SegmentNameID });
                columns.push.apply(columns, [
                    {
                        field: field.SegmentValueIdName, headerText: field.SegmentDisplayName, allowEditing: isEditable, required: true, valueAccessor: ej2GridDisplayFormatter, dataSource: itemWiseSegmentValues, displayField: field.SegmentValueDescName, edit: ej2GridDropDownObj({
                            defaultValue: field.HasDefaultValue ? field.SegmentValueID : ""
                        })
                    },
                    { field: field.SegmentValueDescName, visible: false }
                ])
            } else {
                var column = field.IsNumericValue ?
                    { field: field.SegmentValueIdName, headerText: field.SegmentDisplayName, editType: "numericedit", edit: { params: { showSpinButton: false, decimals: 0, min: 1 } } }
                    : { field: field.SegmentValueIdName, headerText: field.SegmentDisplayName }
                columns.push(column);
            }
        }

        if (isEditable) {
            columns.splice(1, 0,
                {
                    headerText: 'Commands', width: 120, commands: [
                        { type: 'Edit', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-edit' } },
                        { type: 'Delete', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-delete' } },
                        { type: 'Save', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-update' } },
                        { type: 'Cancel', buttonOption: { cssClass: 'e-flat', iconCss: 'e-icons e-cancel-icon' } }]
                })
        };

        return columns;
    } catch (e) {
        showResponseError(e);
    }
}
// #endregion

// #region Validation
/**
 * Initialize validation on the container
 * @param {HTMLDivElement | HTMLFormElement} container - form container
 * @param {Array} constraints - Array of Constraints
 */
function initializeValidation(container, constraints) {
    extendValidation();
    var inputs = container.find("input[name], select[name]");
    for (var i = 0; i < inputs.length; ++i) {
        inputs[i].addEventListener("change", function (ev) {
            var errors = validate(container, constraints) || {};
            showErrorsForInput(this, errors[this.name])
        });
    }
}

/**
 * Validate the container
 * @param {HTMLDivElement | HTMLFormElement} container - form container
 * @param {Array} constraints - Array of Constraints
 */
function isValidForm(container, constraints) {
    var errors = validate(container, constraints);

    if (!errors)
        return true;
    else showErrors(container, errors || {});
}

function validateForm(container, constraints) {
    var errors = validate(container, constraints);

    if (!errors) return null;
    else {
        showErrors(container, errors || {});
        return errors;
    }
}

/**
 * Show errors to the form container
 * Loops through all the inputs and show the errors for that input
 * @param {HTMLDivElement | HTMLFormElement} container
 * @param {Array} errors - Array of errors
 */
function showErrors(container, errors) {
    var inputs = container.find("input[name], select[name]");
    $.each(inputs, function (i, value) {
        showErrorsForInput(value, errors && errors[value.name]);
    });
}

/**
 * shows/hide validation error for the input
 * @param {HTMLInputElement} input - HTMLInputElement
 * @param {string} error - Error message
 */
function showErrorsForInput(input, error) {
    var formGroup = $(input).closest('.form-group');

    if (!formGroup) return;

    // remove error messages
    formGroup.removeClass('has-error');
    $("#validation-message-" + input.id).remove();

    if (error === undefined || error.length === 0) return;

    formGroup.addClass('has-error');

    // add help-block
    var message = document.createElement('span');
    message.className = 'help-block validation-message';
    message.textContent = error;
    message.id = 'validation-message-' + input.id;

    // check if input block
    if ($(input).parent().hasClass("input-group"))
        $(input).closest('.input-group').parent().append(message);
    else
        $(input).after(message);
}

/**
 * Hides all validation error
 *  @param {HTMLDivElement | HTMLFormElement} container - form container
 */
function hideValidationErrors(container) {
    var inputs = container.find("input[name], select[name]");

    container.find('.help-block').remove();

    $.each(inputs, function (i, value) {
        var formGroup = $(value).closest('.form-group');
        if (!formGroup) return;
        formGroup.removeClass('has-error');
    });
}

function extendValidation() {
    // Before using it we must add the parse and format functions
    // Here is a sample implementation using moment.js
    validate.extend(validate.validators.datetime, {
        // The value is guaranteed not to be null or undefined but otherwise it
        // could be anything.
        parse: function (value, options) {
            return +moment.utc(value);
        },
        // Input is a unix timestamp
        format: function (value, options) {
            var format = options.dateOnly ? "YYYY-MM-DD" : "YYYY-MM-DD hh:mm:ss";
            return moment.utc(value).format(format);
        }
    });
}
// #endregion

// #region Yarn Category Calculation
function getYarnPOCategory(yComposition, yarnCount, yColor, yProgram, yType, ySubProgram) {
    //var yProgram = '';  //Comment by 15-01-2020
    if (yComposition.includes('(')) {
        yComposition = yComposition.split('(')[1].replaceAll(')', '');
    }
    if (yComposition.match(/BCI/g)) {
        yProgram = 'BCI';
        yComposition = yComposition.replaceAll('BCI ', '')
    }
    else if (yComposition.match(/Conventional/g)) {
        yProgram = 'Conventional';
        yComposition = yComposition.replaceAll('Conventional ', '')
    }
    else if (yComposition.match(/Melange/g)) {
        yProgram = 'Melange';
        yComposition = yComposition.replaceAll('Melange ', '')
    }
    else if (yComposition.match(/Organic/g)) {
        yProgram = 'Organic';
        yComposition = yComposition.replaceAll('Organic ', '')
    }
    else if (yComposition.match(/GOTS/g)) {
        yProgram = 'Gots';
        yComposition = yComposition.replaceAll('Gots ', '')
    }
    else if (yComposition.match(/Prima/g)) {
        yProgram = 'Prima';
        yComposition = yComposition.replaceAll('Prima ', '')
    }
    else if (yComposition.match(/Suprima/g)) {
        yProgram = 'Suprima';
        yComposition = yComposition.replaceAll('Suprima ', '')
    }
    else if (yComposition.match(/Inject/g)) {
        yProgram = 'Inject';
        yComposition = yComposition.replaceAll('Inject ', '')
    }
    else if (yComposition.match(/Siro/g)) {
        yProgram = 'Siro';
        yComposition = yComposition.replaceAll('Siro ', '')
    }

    var YCategory = '';

    var YCount = '';
    if (yarnCount)
        YCount = yarnCount.split(' ')[0];

    //if (ySubProgram.match(/Inject Melange/g))
    //    YCount = YCount + "Inject-Melange-";
    //else if (ySubProgram.match(/Inject Poly/g))
    //    YCount = YCount + "Inject-Poly-";
    //else if (ySubProgram.match(/Naps/g))
    //    YCount = YCount + "Naps-";

    var Com1 = getFabricCompSegment(yComposition, 1);
    if (Com1 != undefined && Com1.substr(0, 5) == 'Bio-C') {
        Com1 = 'C';
    }
    Com1 = Com1 == undefined ? '' : Com1 == 'Lyocell' ? 'Lyocell' : Com1 == 'Tencel' ? 'Tencel' : Com1.substr(0, 1);
    var Com1v = getFabricCompSegment(yComposition, 0);
    Com1v = Com1v == undefined ? parseInt(0) : parseInt(Com1v.replaceAll('%', ''));

    var Com2 = getFabricCompSegment(yComposition, 3);
    if (Com2 != undefined && Com2.substr(0, 5) == 'Bio-C') {
        Com2 = 'C';
    }
    Com2 = Com2 == undefined ? '' : Com2 == 'Lyocell' ? 'Lyocell' : Com2 == 'Tencel' ? 'Tencel' : Com2.substr(0, 1);
    var Com2v = getFabricCompSegment(yComposition, 2);
    Com2v = Com2v == undefined ? parseInt(0) : parseInt(Com2v.replaceAll('%', ''));

    var Com3 = getFabricCompSegment(yComposition, 5);
    if (Com3 != undefined && Com3.substr(0, 5) == 'Bio-C') {
        Com3 = 'C';
    }
    Com3 = Com3 == undefined ? '' : Com3 == 'Lyocell' ? 'Lyocell' : Com3 == 'Tencel' ? 'Tencel' : Com3.substr(0, 1);
    var Com3v = getFabricCompSegment(yComposition, 4);
    Com3v = Com3v == undefined ? parseInt(0) : parseInt(Com3v.replaceAll('%', ''));

    if (Com3 == '') {
        if (Com1 == 'C' && Com2 == 'P') {
            if (YCount.match(/Inject-Melange/g) != null || YCount.match(/Inject-Poly/g) != null || YCount.match(/Naps/g) != null) {
                YCount = YCount.length > 0 ? YCount.charAt(YCount.length - 1) == '-' ? YCount.slice(0, -1) + '' : YCount : YCount;
                //YCategory = YCount + '' + yProgram + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                YCategory = YCount + '' + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
            }
            else {
                if (yProgram == 'Organic') {
                    //YCategory = YCount + '' + yProgram + 'OCVC(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                    YCategory = YCount + '' + 'OCVC(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                }
                else if (yProgram == 'GOTS') {
                    //YCategory = YCount + '' + yProgram + 'OCVC(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                    YCategory = YCount + '' + 'OCVC(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                }
                else {
                    //YCategory = YCount + '' + yProgram + 'CVC(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                    YCategory = YCount + '' + 'CVC(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                }
            }
        }
        else if (Com1 == 'P' && Com2 == 'C') {
            if (YCount.match(/Inject-Melange/g) != null || YCount.match(/Inject-Poly/g) != null || YCount.match(/Naps/g) != null) {
                YCount = YCount.length > 0 ? YCount.charAt(YCount.length - 1) == '-' ? YCount.slice(0, -1) + '' : YCount : YCount;
                //YCategory = YCount + '' + yProgram + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')'; // Comment on 15-01-2020
                YCategory = YCount + '' + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
            }
            else
                //YCategory = YCount + '' + yProgram + 'PC(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                YCategory = YCount + '' + 'PC(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
        }
        else if (Com1 == 'C' && Com2 == 'M') {
            if (YCount.match(/Inject-Melange/g) != null || YCount.match(/Inject-Poly/g) != null || YCount.match(/Naps/g) != null) {
                YCount = YCount.length > 0 ? YCount.charAt(YCount.length - 1) == '-' ? YCount.slice(0, -1) + '' : YCount : YCount;
                //YCategory = YCount + '' + yProgram + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                YCategory = YCount + '' + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
            }
            else
                //YCategory = YCount + '' + yProgram + 'CM(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                YCategory = YCount + '' + 'CM(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
        }
        else if (Com1 == 'P' && Com2 == 'V') {
            if (YCount.match(/Inject-Melange/g) != null || YCount.match(/Inject-Poly/g) != null || YCount.match(/Naps/g) != null) {
                YCount = YCount.length > 0 ? YCount.charAt(YCount.length - 1) == '-' ? YCount.slice(0, -1) + '' : YCount : YCount;
                //YCategory = YCount + '' + yProgram + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                YCategory = YCount + '' + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
            }
            else
                //YCategory = YCount + '' + yProgram + 'PV(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                YCategory = YCount + '' + 'PV(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
        }

        else if (Com2 == 'V') {
            //if (yColor != 'Black') {
            if (YCount.match(/Inject-Melange/g) != null || YCount.match(/Inject-Poly/g) != null || YCount.match(/Naps/g) != null) {
                YCount = YCount.length > 0 ? YCount.charAt(YCount.length - 1) == '-' ? YCount.slice(0, -1) + '' : YCount : YCount;
                YCategory = YCount + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
            }
            else
                //YCategory = YCount + '' + yProgram + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                YCategory = YCount + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
            //}
            //else {
            if (ySubProgram) {
                if (ySubProgram.match(/Color Melange/g) == null) {
                    if (Com2v > 0 && Com2v < 5) {
                        if (yProgram == 'Organic') {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + 'OGM-' + Com2v + 'V';
                                YCategory = YCount + '' + 'OGM-' + Com2v + 'V';
                        } else if (yProgram == 'GOTS') {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + 'OGM-' + Com2v + 'V';
                                YCategory = YCount + '' + 'OGM-' + Com2v + 'V';
                        }
                        else {
                            if (yColor != 'Raw Color' && yColor != 'Raw White') {
                                if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                    //YCategory = YCount + '' + yProgram + 'GM-' + Com2v + 'V';
                                    YCategory = YCount + '' + 'GM-' + Com2v + 'V';
                            }
                        }
                    }
                    //else if (Com2v > 4 && Com2v < 31) {
                    else if (Com2v > 4 && Com2v < 41) {
                        if (yProgram == 'Organic') {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + 'OGM-' + Com2v + 'V';
                                YCategory = YCount + '' + 'OGM-' + Com2v + 'V';
                        } else if (yProgram == 'GOTS') {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + 'OGM-' + Com2v + 'V';
                                YCategory = YCount + '' + 'OGM-' + Com2v + 'V';
                        }
                        else {
                            if (yColor != 'Raw Color' && yColor != 'Raw White') {
                                if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                    //YCategory = YCount + '' + yProgram + 'GM-' + Com2v + 'V';
                                    YCategory = YCount + '' + 'GM-' + Com2v + 'V';
                            }
                        }
                    }
                    //else if (Com2v > 30) {
                    else if (Com2v > 40) {
                        if (yProgram == 'Organic') {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + 'O' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                                YCategory = YCount + '' + 'O' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                        } else if (yProgram == 'GOTS') {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + 'O' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                                YCategory = YCount + '' + 'O' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                        }
                        else {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                                YCategory = YCount + '' + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                        }
                    }
                }
            }
            else {
                if (Com2v > 0 && Com2v < 5) {
                    if (yProgram == 'Organic') {
                        if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                            //YCategory = YCount + '' + yProgram + 'OGM-' + Com2v + 'V';
                            YCategory = YCount + '' + 'OGM-' + Com2v + 'V';
                    } else if (yProgram == 'GOTS') {
                        if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                            //YCategory = YCount + '' + yProgram + 'OGM-' + Com2v + 'V';
                            YCategory = YCount + '' + 'OGM-' + Com2v + 'V';
                    }
                    else {
                        if (yColor != 'Raw Color' && yColor != 'Raw White') {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + 'GM-' + Com2v + 'V';
                                YCategory = YCount + '' + 'GM-' + Com2v + 'V';
                        }
                    }
                }
                //else if (Com2v > 4 && Com2v < 31) {
                else if (Com2v > 4 && Com2v < 41) {
                    if (yProgram == 'Organic') {
                        if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                            //YCategory = YCount + '' + yProgram + 'OGM-' + Com2v + 'V';
                            YCategory = YCount + '' + 'OGM-' + Com2v + 'V';
                    } else if (yProgram == 'GOTS') {
                        if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                            //YCategory = YCount + '' + yProgram + 'OGM-' + Com2v + 'V';
                            YCategory = YCount + '' + 'OGM-' + Com2v + 'V';
                    }
                    else {
                        if (yColor != 'Raw Color' && yColor != 'Raw White') {
                            if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                                //YCategory = YCount + '' + yProgram + 'GM-' + Com2v + 'V';
                                YCategory = YCount + '' + 'GM-' + Com2v + 'V';
                        }
                    }
                }
                //else if (Com2v > 30) {
                else if (Com2v > 40) {
                    if (yProgram == 'Organic') {
                        if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                            //YCategory = YCount + '' + yProgram + 'O' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                            YCategory = YCount + '' + 'O' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                    } else if (yProgram == 'GOTS') {
                        if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                            //YCategory = YCount + '' + yProgram + 'O' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                            YCategory = YCount + '' + 'O' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                    }
                    else {
                        if (YCount.match(/Inject-Melange/g) == null && YCount.match(/Inject-Poly/g) == null && YCount.match(/Naps/g) == null)
                            //YCategory = YCount + '' + yProgram + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                            YCategory = YCount + '' + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
                    }
                }
            }

            //}
        }
        else if (Com2 == '') {
            if (Com1 == 'P') {
                if (yType == 'Filament') {
                    if (YCount.match(/DSP/g) == 'DSP')//add on 03-Apr-2018 request by Mr. Alim
                    {
                        //YCategory = YCount + '' + yProgram;
                        YCategory = YCount;
                    }
                    else {
                        //YCategory = YCount + '' + yProgram + 'Poly';
                        YCategory = YCount + '' + 'Poly';
                    }
                }
                else {
                    if (YCount == '20/2' || YCount == '50/2') {
                        //YCategory = YCount + '' + yProgram + 'Poly';
                        YCategory = YCount + '' + 'Poly';
                    }
                    else if (YCount.match(/F/g) == 'F' || YCount.match(/D/g) == 'D')//add on 13-03-18 requested by Mr. Rafiq SCD
                    {
                        //YCategory = YCount + '' + yProgram;
                        YCategory = YCount;
                    }
                    else {
                        //YCategory = YCount + '' + yProgram + 'Spun-Poly';
                        YCategory = YCount + '' + 'Spun-Poly';
                    }
                }
            }
            else if (Com1 == 'C') {
                //if ((yType == 'Compact' || yType == 'Combed' || yType == 'Carded') && yProgram == 'Organic') {
                //    if (YCount.match(/Inject-Melange/g) != null || YCount.match(/Inject-Poly/g) != null || YCount.match(/Naps/g) != null)
                //    YCategory = YCount + '' + 'OC';
                //}
                //else if (yType == 'Compact' || yType == 'Combed') {
                //    if (YCount.match(/Inject-Melange/g) != null || YCount.match(/Inject-Poly/g) != null || YCount.match(/Naps/g) != null)
                //    YCategory = YCount + '' + 'CC';
                //}
                //else if (yType == 'Carded') {
                //    if (YCount.match(/Inject-Melange/g)!=null || YCount.match(/Inject-Poly/g)!=null || YCount.match(/Naps/g)!=null)
                //    YCategory = YCount + '' + 'KC';
                //}

                //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'Cotton';
                if (ySubProgram) {
                    if (ySubProgram.match(/Open End/g)) {
                        YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'OE';
                        ySubProgram = ySubProgram.replaceAll(', Open End', '').replaceAll('Open End', '');
                    }
                }
                else {
                    if ((yType == 'Compact' || yType == 'Combed' || yType == 'Carded') && yProgram == 'Organic') {
                        //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'OC'; // Comment on 15-01-2020
                        YCategory = YCount + '' + yarnCount.split(' ')[1] + 'OC';
                    } else if ((yType == 'Compact' || yType == 'Combed' || yType == 'Carded') && yProgram == 'GOTS') {
                        //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'OC';
                        YCategory = YCount + '' + yarnCount.split(' ')[1] + 'OC';
                    }
                    else if ((yType == 'Combed') && yProgram == 'BCI') {
                        YCategory = YCount + '' + yarnCount.split(' ')[1] + 'CC';
                    }
                    else if ((yType == 'Compact') && yProgram == 'BCI') {
                        //YCategory = YCount + '' + yarnCount.split(' ')[1] + 'CC-Compact' + '-' + yType;
                        YCategory = YCount + '' + yarnCount.split(' ')[1] + 'CC-Compact';
                    }
                    else if ((yType == 'Combed') && yProgram != 'BCI' && yProgram != 'GOTS') {
                        //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'CC';
                        YCategory = YCount + '' + yarnCount.split(' ')[1] + 'CC';
                    }
                    else if ((yType == 'Compact') && yProgram != 'BCI' && yProgram != 'GOTS') {
                        //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'CC-Compact';
                        YCategory = YCount + '' + yarnCount.split(' ')[1] + 'CC-Compact';
                    }
                    //else if (yType == 'Compact' || yType == 'Combed') {
                    //    YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'CC';
                    //}
                    else if ((yType == 'Carded' && yProgram == 'BCI') || (yType == 'Carded' && yProgram == 'Conventional')) {
                        //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'KC';
                        YCategory = YCount + '' + yarnCount.split(' ')[1] + 'KC';
                    }
                    else if (yType == 'Open End') {
                        //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'OE';
                        YCategory = YCount + '' + yarnCount.split(' ')[1] + 'OE';
                    }
                }
            }
            else if (Com1 == 'V') {
                //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'Viscose';
                YCategory = YCount + '' + '' + yarnCount.split(' ')[1] + 'Viscose';
            }
            else if (Com1 == 'M') {
                //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'Modal';
                YCategory = YCount + '' + '' + yarnCount.split(' ')[1] + 'Modal';
            }
            else if (Com1 == 'L') {
                //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'Linen';
                YCategory = YCount + '' + yarnCount.split(' ')[1] + 'Linen';
            }
            else if (Com1 == 'R') {
                //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1] + 'Rayon';
                YCategory = YCount + '' + yarnCount.split(' ')[1] + 'Rayon';
            }
            else if (Com1 == 'E') {         // Added New Line on 15-01-2020
                YCategory = YCount + '' + yarnCount.split(' ')[1];
            }
            else if (Com1 == 'Lyocell') {
                //YCategory = YCount + '' + yProgram + '' + 'Lyocell';
                YCategory = YCount + '' + 'Lyocell';
            }
            else if (Com1 == 'Tencel') {
                //YCategory = YCount + '' + yProgram + '' + 'Tencel';
                YCategory = YCount + '' + 'Tencel';
            }
            else {
                //YCategory = YCount + '' + yProgram + '' + yarnCount.split(' ')[1];
                YCategory = YCount + '' + yarnCount.split(' ')[1];
            }
        }
        else {
            //YCategory = YCount + '' + yProgram + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
            YCategory = YCount + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
        }
    }
    else {
        if (Com2 != '' && Com3 == '') {
            //YCategory = YCount + '' + yProgram + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
            YCategory = YCount + '' + Com1 + Com2 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + ')';
        }
        else if (Com2 != '' && Com3 != '') {
            //YCategory = YCount + '' + yProgram + '' + Com1 + Com2 + Com3 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + '/' + Com3 + Com3v + ')';
            YCategory = YCount + '' + Com1 + Com2 + Com3 + '(' + Com1 + Com1v + '/' + Com2 + Com2v + '/' + Com3 + Com3v + ')';
        }
    }

    //if (yComposition.match(/100% Cotton/g) && ySubProgram.match(/Melange/g)) {
    // //   YCategory = YCount + '' + yProgram + 'CM';
    //}

    YCategory = YCategory.replaceAll(' undefined', '').replaceAll('undefined', '').replaceAll('undefined', '');

    if (ySubProgram) {
        if (ySubProgram.match(/Inject Melange/g))
            YCount = YCategory + "Inject-Melange-";
        else if (ySubProgram.match(/Inject Poly/g))
            YCount = YCategory + "Inject-Poly-";
        else if (ySubProgram.match(/Naps/g))
            YCount = YCategory + "Naps-";

        ///----------add yarn brand into yarn category------------//
        if (yProgram.match(/Inject/g)) {
            YCategory = YCategory + 'Inject';
        }
        else if (yProgram.match(/Siro/g)) {
            YCategory = YCategory + 'Siro';
        }
    }

    ///----------add yarn type into yarn category------------//
    if (yType.match(/Ring/g)) {
        YCategory = YCategory + 'Ring';
    }
    else if (yType.match(/Vortex/g)) {
        YCategory = YCategory + 'Vortex';
    }
    YCategory = YCategory + getYarnSubProgramString(ySubProgram);
    if (yColor == 'White') {
        if (yComposition.match(/Viscose/g) && yColor == 'White') {
            YCategory = YCategory.replaceAll('-W', '');
        }
        else {
            YCategory = YCategory.replaceAll('-W', '') + '-W';
        }
    }

    if (yColor == 'Raw White') {
        YCategory = YCategory.replaceAll('-W', '') + '-W';
    }
    if (ySubProgram) {
        if (ySubProgram.match(/Color Melange/g)) {
            if (yColor == 'Raw Color') {
                YCategory = YCategory.replaceAll('Raw', '') + 'Raw';
            }
        }
    }
    return YCategory;
}

function getFabricCompSegment(fbcomp, seq) {
    var ycSegment = fbcomp.split(' ');
    if (ycSegment[seq] != undefined) {
        return ycSegment[seq].trim();
    }
}

function getYarnSubProgramString(ySubProgram) {
    var ysp = '';
    if (ySubProgram) {
        var ySubProgram = ySubProgram.split(',');
        for (var i = 0; i < ySubProgram.length; i++) {
            if (i == 0) {
                if (ysp == '')
                    ysp = ySubProgram[i].trim().replaceAll(' ', '-');
                else
                    ysp = ySubProgram[i].trim().replaceAll(' ', '-');
            }
            else {
                if (ysp == '')
                    ysp = ySubProgram[i].trim().replaceAll(' ', '-');
                else
                    ysp = ysp + "-" + ySubProgram[i].trim().replaceAll(' ', '-');
            }
        }
    }

    if (ysp != '') {
        ysp = '-' + ysp + '-';
    }
    return ysp;
}

function calculateYarnCategory(row) {
    if (row.Segment1ValueId && row.Segment2ValueId && row.Segment3ValueId) {
        var yProgram = '';
        if (row.YarnProgram)
            yProgram = row.YarnProgram;

        var yComposition = '';
        if (row.Segment3ValueDesc)
            yComposition = row.Segment3ValueDesc;

        var yarnCount = '';
        if (row.Segment2ValueDesc)
            yarnCount = row.Segment2ValueDesc;

        var yColor = '';
        if (row.Segment5ValueDesc)
            yColor = row.Segment5ValueDesc;

        var yType = '';
        if (row.Segment1ValueDesc)
            yType = row.Segment1ValueDesc;

        var yarnCategory = '';
        var YCategory = getYarnPOCategory(yComposition, yarnCount, yColor, yProgram, yType);
        YCategory = YCategory != '' ? YCategory.replaceAll('  ', ' ') : YCategory;
        YCategory = YCategory.length > 0 ? YCategory.charAt(YCategory.length - 1) == '-' ? YCategory.slice(0, -1) + '' : YCategory : YCategory;
        YCategory = YCategory.replaceAll('--', '-');
        if (row.YarnSubProgramNames)
            yarnCategory = YCategory + "-" + row.YarnSubProgramNames;
        else
            yarnCategory = YCategory.replaceAll('-', '');

        return yarnCategory;
    }
}
// #endregion

// #region Polyfills
// https://tc39.github.io/ecma262/#sec-array.prototype.find
if (!Array.prototype.find) {
    Object.defineProperty(Array.prototype, 'find', {
        value: function (predicate) {
            // 1. Let O be ? ToObject(this value).
            if (this == null) {
                throw new TypeError('"this" is null or not defined');
            }

            var o = Object(this);

            // 2. Let len be ? ToLength(? Get(O, "length")).
            var len = o.length >>> 0;

            // 3. If IsCallable(predicate) is false, throw a TypeError exception.
            if (typeof predicate !== 'function') {
                throw new TypeError('predicate must be a function');
            }

            // 4. If thisArg was supplied, let T be thisArg; else let T be undefined.
            var thisArg = arguments[1];

            // 5. Let k be 0.
            var k = 0;

            // 6. Repeat, while k < len
            while (k < len) {
                // a. Let Pk be ! ToString(k).
                // b. Let kValue be ? Get(O, Pk).
                // c. Let testResult be ToBoolean(? Call(predicate, T, « kValue, k, O »)).
                // d. If testResult is true, return kValue.
                var kValue = o[k];
                if (predicate.call(thisArg, kValue, k, o)) {
                    return kValue;
                }
                // e. Increase k by 1.
                k++;
            }

            // 7. Return undefined.
            return undefined;
        },
        configurable: true,
        writable: true
    });
}

// https://tc39.github.io/ecma262/#sec-array.prototype.findindex
if (!Array.prototype.findIndex) {
    Object.defineProperty(Array.prototype, 'findIndex', {
        value: function (predicate) {
            // 1. Let O be ? ToObject(this value).
            if (this == null) {
                throw new TypeError('"this" is null or not defined');
            }

            var o = Object(this);

            // 2. Let len be ? ToLength(? Get(O, "length")).
            var len = o.length >>> 0;

            // 3. If IsCallable(predicate) is false, throw a TypeError exception.
            if (typeof predicate !== 'function') {
                throw new TypeError('predicate must be a function');
            }

            // 4. If thisArg was supplied, let T be thisArg; else let T be undefined.
            var thisArg = arguments[1];

            // 5. Let k be 0.
            var k = 0;

            // 6. Repeat, while k < len
            while (k < len) {
                // a. Let Pk be ! ToString(k).
                // b. Let kValue be ? Get(O, Pk).
                // c. Let testResult be ToBoolean(? Call(predicate, T, « kValue, k, O »)).
                // d. If testResult is true, return k.
                var kValue = o[k];
                if (predicate.call(thisArg, kValue, k, o)) {
                    return k;
                }
                // e. Increase k by 1.
                k++;
            }

            // 7. Return -1.
            return -1;
        },
        configurable: true,
        writable: true
    });
}

// Search substring in a string
if (!String.prototype.contains) {
    String.prototype.contains = function (search, start) {
        'use strict';
        if (typeof start !== 'number') {
            start = 0;
        }

        if (start + search.length > this.length) {
            return false;
        } else {
            return this.indexOf(search, start) !== -1;
        }
    };
}
// #endregion
// Search substring in a string
function getIndexFromArray(arr, search, val) {
    for (var i = 0; i < arr.length; ++i) {
        if (arr[i][search] == val)
            return i;
    }
    return -1;
}
// #endregion

//Get textile Grade Value from formula
function getValueForGrade(totalPoint, rollLength, fabricWidth) {
    if (rollLength == 0 || fabricWidth == 0) {
        return 0;
    } else {
        return ((totalPoint * 36 * 100) / (rollLength * fabricWidth));
    }
}

//Get textile Grade by points
function getGradeFromValue(totalPoint) {
    if (1 <= totalPoint && totalPoint <= 20)
        return "A (Pass)";
    else if (20 < totalPoint && totalPoint <= 28)
        return "B (Pass)";
    else if (totalPoint > 28)
        return "C (Fail)";
    else return "No Grade";
}

//get a copy of object
function objectCopy(o) {
    var copy = o, k;
    if (o && typeof o === 'object') {
        copy = Object.prototype.toString.call(o) === '[object Array]' ? [] : {};
        for (k in o) {
            copy[k] = objectCopy(o[k]);
        }
    }
    return copy;
}

function buildFormData(formData, data, parentKey) {
    if (data && typeof data === 'object' && !(data instanceof Date) && !(data instanceof File) && !(data instanceof Blob)) {
        Object.keys(data).forEach(key => {
            buildFormData(formData, data[key], parentKey ? `${parentKey}[${key}]` : key);
        });
    } else {
        const value = data == null ? '' : data;

        if (data instanceof File) formData.append("files", data);
        else formData.append(parentKey, value);
    }
}

function getDateTimeDifference(earlierDate, laterDate) {
    var oDiff = new Object();

    //  Calculate Differences
    //  -------------------------------------------------------------------  //
    var nTotalDiff = laterDate.getTime() - earlierDate.getTime();

    oDiff.days = Math.floor(nTotalDiff / 1000 / 60 / 60 / 24);
    nTotalDiff -= oDiff.days * 1000 * 60 * 60 * 24;

    oDiff.hours = Math.floor(nTotalDiff / 1000 / 60 / 60);
    nTotalDiff -= oDiff.hours * 1000 * 60 * 60;

    oDiff.minutes = Math.floor(nTotalDiff / 1000 / 60);
    nTotalDiff -= oDiff.minutes * 1000 * 60;

    oDiff.seconds = Math.floor(nTotalDiff / 1000);
    //  -------------------------------------------------------------------  //

    //  Format Duration
    //  -------------------------------------------------------------------  //
    //  Format Hours
    var hourtext = '00';
    if (oDiff.days > 0) { hourtext = String(oDiff.days); }
    if (hourtext.length == 1) { hourtext = '0' + hourtext };

    //  Format Minutes
    var mintext = '00';
    if (oDiff.minutes > 0) { mintext = String(oDiff.minutes); }
    if (mintext.length == 1) { mintext = '0' + mintext };

    //  Format Seconds
    var sectext = '00';
    if (oDiff.seconds > 0) { sectext = String(oDiff.seconds); }
    if (sectext.length == 1) { sectext = '0' + sectext };

    //  Set Duration
    var sDuration = hourtext + ':' + mintext + ':' + sectext;
    oDiff.duration = sDuration;
    //  -------------------------------------------------------------------  //

    return oDiff;
}



