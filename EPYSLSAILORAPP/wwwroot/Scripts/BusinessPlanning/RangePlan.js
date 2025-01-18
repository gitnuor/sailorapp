function formatCollapsibleRow(d, RangeQnty) {

    var list = JSON.parse(d.strStyleSizes);

    var str = "<label style='font-weight:bold;font-size:12px;'>Range Plan Quantity : " + RangeQnty + "</label><br/><br/>";

    str += '<table class="table-striped table-bordered no-footer tblProductSize" style="width:280px;text-align: center;background-color: aliceblue;">' +
        '<tr style="font-weight:bold;background-color: #cedfdf;">' +
        '<td>Size</td><td>Ratio %</td><td>Qty</td>' +
        (d.is_separate_price == true ? '<td>Per Pc MRP Price</td>' : '') +
        (d.is_separate_price == true ? '<td>Per Pc CPU Price</td>' : '') +
        '</tr>';

    for (var ind = 0; ind < list.length; ind++) {

        str += '<tr>';

        str += '<td  style="width:120px;text-align:center;font-weight:bold;" >' + list[ind].style_product_size + '</td>';
        str += '<td style="width:110px"><input min="0" max="100" value="' + (parseInt(list[ind].size_quantity) / parseInt(RangeQnty) * 100).toFixed(2) + '" type="number"  class="txtPercentage" placeholder="Percentage %" style="width:110px;padding-right: 3px;" /></td>';
        str += '<td style="width:80px"><input type="number" min="0" range_plan_detail_id="' + list[ind].range_plan_detail_id + '" range_plan_detail_size_id="' + list[ind].range_plan_detail_size_id + '"  class="txtRangeQnty" style_product_size_group_detid="' + list[ind].style_product_size_group_detid + '" value="' + list[ind].size_quantity + '"/></td>';

        if (d.is_separate_price == true) {
            str += '<td style="width:80px"><input type="number" min="0"  class="txtSeparateMRPPrice" value="' + list[ind].size_per_pc_mrp_value + '"/></td>';
            str += '<td style="width:80px"><input type="number" min="0"  class="txtSeparateCPUPrice" value="' + list[ind].size_per_pc_cpu_value + '"/></td>';

        }
        str += '</tr>';
    }

    str += '<tr style="background-color:#bfbfa4;">';

    str += '<td style="width:120px;text-align:center;font-weight:bold;">Total</td>';
    str += '<td style="width:110px;text-align:right;"><label style="margin-right: 18px;font-weight:bold;" class="lblTotalSizePercentage"><label/></td>';
    str += '<td style="width:80px;text-align:right;"><label style="margin-right: 18px;font-weight:bold;" class="lblTotalSize"><label/></td>';

    if (d.is_separate_price == true) {
        str += '<td style="width:80px;text-align:right;"><label style="margin-right: 18px;font-weight:bold;" class="lblAvgMRPPrice"><label/></td>';
        str += '<td style="width:80px;text-align:right;"><label style="margin-right: 18px;font-weight:bold;" class="lblAvgCPUPrice"><label/></td>';

    }
    str += '</tr>';

    str += '</table><br/>';

    str += "<button  class='btnCancelProductSize btn btn-danger' style='font-size:12px;'>Cancel</button>";

    str += "<button disabled class='btnSaveProductSize btn btn-secondary' style='font-size:12px;margin-left:40px;'>Update</button><br/>";

    return str;
}
function BtnSizeClick(elem,tableid) {

    var btn_elem = $(elem);

    let tr = $(elem).parent().parent();

    var txtRangeQnty = tr.find(".txtRangePlanQnty").val();

    if (parseInt(txtRangeQnty) == 0) {
        tr.find(".txtRangePlanQnty").css("border", "1px solid red");
        tr.find(".txtRangePlanQnty").focus();
        return;
    }

    tr.find(".txtRangePlanQnty").css("border", "");

    var current_product_id = $(tr).attr("style_item_product_id");

    let row = dt_source.row(tr);

    if (row.child.isShown()) {
       
        row.child.remove();

    }
    else {
        tr.parent().find(".tblProductSize").parent().parent().remove();
        tr.parent().find(".tblProductSize").parent().parent().remove();

        row.child(formatCollapsibleRow(row.data(), txtRangeQnty)).show();
        // tr.css("text-align", "center");

        $("#" + tableid + " .tblProductSize").find(".txtPercentage").change(function () {

            var current_elem = $(this);

            $(current_elem).parent().parent().find(".txtRangeQnty").val(parseInt(parseFloat($(current_elem).val()) * parseFloat(txtRangeQnty) / 100));

            var total_percentage = 0, total_qnty = 0;
            $("#" + tableid + " .tblProductSize").find(".txtPercentage").each(function (index, element) {
                total_percentage += parseFloat($(element).val());
                total_qnty += parseInt($(element).parent().parent().find(".txtRangeQnty").val());
            });

            total_percentage = parseFloat(total_percentage.toFixed(0));

            if (total_percentage > 100) {
                showErrorAlert("Alert", "Total Percentage can not exceed 100", "OK", function () {
                    total_percentage -= parseFloat($(current_elem).val());
                    $(current_elem).val('0');
                    $(current_elem).parent().parent().find(".txtRangeQnty").val('0');
                    $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text(total_percentage);
                    return;
                });
            }
            else if (total_percentage == 100 && tr.attr("is_separate_price") != "true") {
                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
                if (parseInt(txtRangeQnty) - total_qnty > 0) {

                    $(current_elem).parent().parent().find(".txtRangeQnty").val(parseInt($(current_elem).parent().parent().find(".txtRangeQnty").val()) + (parseInt(txtRangeQnty) - total_qnty));
                }
                // if ($(btn_elem).text() != "Added")
                //  $("#" + tableid + " .btnSaveProductSize").trigger("click");
            }
            else
                $("#" + tableid + " .btnSaveProductSize").attr("disabled", "disabled");

            $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text(total_percentage);

            var total_qty = 0;
            $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").each(function (index, element) {
                total_qty += parseFloat($(element).val());
            });

            if (total_qty > parseInt(txtRangeQnty)) {
                showErrorAlert("Alert", "Exceeds the Total Range Plan Quantity", "OK", function () {

                    total_qty -= parseInt($(current_elem).val());
                    $(current_elem).val('0');
                    $(current_elem).parent().parent().find(".txtRangeQnty").val('0');

                    $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text(total_qty);

                    return;
                });
            }
            else if (total_qty == parseInt(txtRangeQnty) && tr.attr("is_separate_price") != "true") {

                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");

                // if ($(btn_elem).text() != "Added")
                //  $("#" + tableid + " .btnSaveProductSize").trigger("click");
            }
            else
                $("#" + tableid + " .btnSaveProductSize").attr("disabled", "disabled");

            $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text(total_qty);


        });

        $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").change(function () {

            var total_percentage = 0;

            var current_elem = $(this);

            $(current_elem).parent().parent().find(".txtPercentage").val((parseFloat($(current_elem).val()) / parseFloat(txtRangeQnty) * 100).toFixed(2));

            $("#" + tableid + " .tblProductSize").find(".txtPercentage").each(function (index, element) {
                total_percentage += parseFloat($(element).val());
            });

            total_percentage = parseFloat(total_percentage.toFixed(0));

            if (total_percentage > 100) {
                showErrorAlert("Alert", "Total Percentage can not exceed 100", "OK", function () {
                    total_percentage -= parseFloat($(current_elem).val());
                    $(current_elem).val('0');
                    $(current_elem).parent().parent().find(".txtPercentage").val('0');
                    $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text(total_percentage.toFixed(2));
                    return;
                });
            }
            else if (total_percentage == 100 && tr.attr("is_separate_price") != "true") {
                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");

                // if ($(btn_elem).text() != "Added")
                //  $("#" + tableid + " .btnSaveProductSize").trigger("click");
            }
            else
                $("#" + tableid + " .btnSaveProductSize").attr("disabled", "disabled");

            $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text(total_percentage.toFixed(2));

            var total_qty = 0;
            $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").each(function (index, element) {
                total_qty += parseInt($(element).val());
            });

            if (total_qty > parseInt(txtRangeQnty)) {
                showErrorAlert("Alert", "Exceeds the Total Range Plan Quantity", "OK", function () {
                    total_qty -= parseInt($(current_elem).val());
                    $(current_elem).val('0');
                    $(current_elem).parent().parent().find(".txtPercentage").val('0');
                    $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text(total_qty);

                    return;
                });
            }
            else if (total_qty == parseInt(txtRangeQnty) && tr.attr("is_separate_price") != "true") {
                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");

                // if ($(btn_elem).text() != "Added")
                //  $("#" + tableid + " .btnSaveProductSize").trigger("click");
            }
            else
                $("#" + tableid + " .btnSaveProductSize").attr("disabled", "disabled");

            $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text(total_qty);



        });

         $("#" + tableid + " .tblProductSize").find(".txtSeparateMRPPrice").change(function () {

            var total_price = 0, quantity = 0;

            var current_elem = $(this);

             $("#" + tableid + " .tblProductSize").find(".txtSeparateMRPPrice").each(function (index, element) {
                if (parseFloat($(element).val()) > 0) {
                    total_price += parseFloat($(element).val());
                    quantity++;
                }
            });


            var avg_price = parseFloat(total_price) / (quantity == 0 ? 1 : quantity);

             $("#" + tableid + " .tblProductSize").find(".lblAvgMRPPrice").text("(Avg) " + avg_price.toFixed(2));

            tr.find(".txtPerPcMRPVal").val(avg_price.toFixed(2));

            tr.find(".txtPerPcMRPVal").trigger("change");

            // if (parseInt( $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text())==100)
            // {
            //      $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
            // }
            // else if (parseInt( $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text()) == 100) {
            //      $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
            // }

        });

         $("#" + tableid + " .tblProductSize").find(".txtSeparateCPUPrice").change(function () {

            var total_price = 0, quantity = 0;

            var current_elem = $(this);

             $("#" + tableid + " .tblProductSize").find(".txtSeparateCPUPrice").each(function (index, element) {
                if (parseFloat($(element).val()) > 0) {
                    total_price += parseFloat($(element).val());
                    quantity++;
                }
            });

            var avg_price = parseFloat(total_price) / (quantity == 0 ? 1 : quantity);

             $("#" + tableid + " .tblProductSize").find(".lblAvgCPUPrice").text("(Avg) " + avg_price.toFixed(2));

            tr.find(".txtPerPcCPUVal").val(avg_price.toFixed(2));

            tr.find(".txtPerPcCPUVal").trigger("change");

            if (parseInt( $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text()) == 100) {
                 $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
            }
            else if (parseInt( $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text()) == 100) {
                 $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
            }

        });

         $("#" + tableid + " .btnSaveProductSize").click(function () {

            var objsizelist = [];

             $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").each(function (index, element) {

                var objSize = {
                    style_item_product_id: $(tr).attr('style_item_product_id'),
                    style_product_size_group_detid: $(element).attr("style_product_size_group_detid"),
                    size_quantity: parseInt($(element).val()),
                    size_per_pc_mrp_value: $(element).parent().parent().find(".txtSeparateMRPPrice").length > 0 ?
                        parseFloat($(element).parent().parent().find(".txtSeparateMRPPrice").val()) : 0,

                    size_per_pc_cpu_value: $(element).parent().parent().find(".txtSeparateCPUPrice").length > 0 ?
                        parseFloat($(element).parent().parent().find(".txtSeparateCPUPrice").val()) : 0,

                    range_plan_detail_size_id: $(element).attr('range_plan_detail_size_id') == "0" ? null : $(element).attr('range_plan_detail_size_id'),
                    range_plan_detail_id: $(element).attr('range_plan_detail_id') == "0" ? null : $(element).attr('range_plan_detail_id'),

                };

                objsizelist.push(objSize);
            });

            if (obj_detailList.filter(p => p.style_item_product_id == current_product_id).length > 0) {

                obj_detailList.filter(p => p.style_item_product_id == current_product_id)[0].size_list = objsizelist;

                if (dt_source.rows().data().filter(p => p.style_item_product_id == $(tr).attr('style_item_product_id')).length > 0)
                    dt_source.rows().data().filter(p => p.style_item_product_id == $(tr).attr('style_item_product_id'))[0].strStyleSizes = JSON.stringify(objsizelist);

                $(btn_elem).text("Added");
                $(btn_elem).attr("iscomplete", "Added");
                $(btn_elem).css("background-color", "green");
                CheckEntryCompleteness(tr);
                row.child.remove();

                tr.find(".txtPerPcMRPVal").focus();
            }

        });

         $("#" + tableid + " .btnCancelProductSize").click(function () {

            row.child.remove();

        });

        //   $("#" + tableid + " .tblProductSize").find(".txtPercentage").trigger("change");
         $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").trigger("change");
         $("#" + tableid + " .tblProductSize").find(".txtSeparateMRPPrice").trigger("change");

        if (row.data().is_separate_price == true) {
             $("#" + tableid + " .tblProductSize").find(".txtSeparateMRPPrice").trigger("change");
             $("#" + tableid + " .tblProductSize").find(".txtSeparateCPUPrice").trigger("change");
        }
    }

}

function formatCollapsibleViewRow(d, RangeQnty) {

    var list = JSON.parse(d.strStyleSizes);

    var str = "<label style='font-weight:bold;font-size:12px;'>Range Plan Quantity : " + RangeQnty + "</label><br/><br/>";

    str += '<table class="table-striped table-bordered no-footer tblProductSize" style="width:280px;text-align: center;background-color: aliceblue;">' +
        '<tr style="font-weight:bold;background-color: #cedfdf;">' +
        '<td>Size</td><td>Ratio %</td><td>Qty</td>' +
        (d.is_separate_price == true ? '<td>Per Pc MRP Price</td>' : '') +
        (d.is_separate_price == true ? '<td>Per Pc CPU Price</td>' : '') +
        '</tr>';

    for (var ind = 0; ind < list.length; ind++) {

        str += '<tr>';

        str += '<td  style="width:120px;text-align:center;font-weight:bold;" >' + list[ind].style_product_size + '</td>';
        str += '<td style="width:110px"><input min="0" max="100" value="' + (parseInt(list[ind].size_quantity) / parseInt(RangeQnty) * 100).toFixed(2) + '" type="number"  class="txtPercentage" placeholder="Percentage %" style="width:110px;padding-right: 3px;" /></td>';
        str += '<td style="width:80px"><input type="number" min="0" range_plan_detail_id="' + list[ind].range_plan_detail_id + '" range_plan_detail_size_id="' + list[ind].range_plan_detail_size_id + '"  class="txtRangeQnty" style_product_size_group_detid="' + list[ind].style_product_size_group_detid + '" value="' + list[ind].size_quantity + '"/></td>';

        if (d.is_separate_price == true) {
            str += '<td style="width:80px"><input type="number" min="0"  class="txtSeparateMRPPrice" value="' + list[ind].size_per_pc_mrp_value + '"/></td>';
            str += '<td style="width:80px"><input type="number" min="0"  class="txtSeparateCPUPrice" value="' + list[ind].size_per_pc_cpu_value + '"/></td>';

        }
        str += '</tr>';
    }

    str += '<tr style="background-color:#bfbfa4;">';

    str += '<td style="width:120px;text-align:center;font-weight:bold;">Total</td>';
    str += '<td style="width:110px;text-align:right;"><label style="margin-right: 18px;font-weight:bold;" class="lblTotalSizePercentage"><label/></td>';
    str += '<td style="width:80px;text-align:right;"><label style="margin-right: 18px;font-weight:bold;" class="lblTotalSize"><label/></td>';

    if (d.is_separate_price == true) {
        str += '<td style="width:80px;text-align:right;"><label style="margin-right: 18px;font-weight:bold;" class="lblAvgMRPPrice"><label/></td>';
        str += '<td style="width:80px;text-align:right;"><label style="margin-right: 18px;font-weight:bold;" class="lblAvgCPUPrice"><label/></td>';

    }
    str += '</tr>';

    str += '</table><br/>';

    str += "<button  class='btnCancelProductSize btn btn-danger' style='font-size:12px;'>Close</button>";

    return str;
}
function BtnSizeViewClick(elem, tableid) {

    var btn_elem = $(elem);

    let tr = $(elem).parent().parent();

    var txtRangeQnty = tr.find(".txtRangePlanQnty").val();

    if (parseInt(txtRangeQnty) == 0) {
        tr.find(".txtRangePlanQnty").css("border", "1px solid red");
        tr.find(".txtRangePlanQnty").focus();
        return;
    }

    tr.find(".txtRangePlanQnty").css("border", "");

    var current_product_id = $(tr).attr("style_item_product_id");

    let row = dt_source.row(tr);

    if (row.child.isShown()) {

        row.child.remove();

    }
    else {
        tr.parent().find(".tblProductSize").parent().parent().remove();
        tr.parent().find(".tblProductSize").parent().parent().remove();

        row.child(formatCollapsibleViewRow(row.data(), txtRangeQnty)).show();
        // tr.css("text-align", "center");

        $("#" + tableid + " .tblProductSize").find(".txtPercentage").change(function () {

            var current_elem = $(this);

            $(current_elem).parent().parent().find(".txtRangeQnty").val(parseInt(parseFloat($(current_elem).val()) * parseFloat(txtRangeQnty) / 100));

            var total_percentage = 0, total_qnty = 0;
            $("#" + tableid + " .tblProductSize").find(".txtPercentage").each(function (index, element) {
                total_percentage += parseFloat($(element).val());
                total_qnty += parseInt($(element).parent().parent().find(".txtRangeQnty").val());
            });

            total_percentage = parseFloat(total_percentage.toFixed(0));

            if (total_percentage > 100) {
                showErrorAlert("Alert", "Total Percentage can not exceed 100", "OK", function () {
                    total_percentage -= parseFloat($(current_elem).val());
                    $(current_elem).val('0');
                    $(current_elem).parent().parent().find(".txtRangeQnty").val('0');
                    $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text(total_percentage);
                    return;
                });
            }
            else if (total_percentage == 100 && tr.attr("is_separate_price") != "true") {
                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
                if (parseInt(txtRangeQnty) - total_qnty > 0) {

                    $(current_elem).parent().parent().find(".txtRangeQnty").val(parseInt($(current_elem).parent().parent().find(".txtRangeQnty").val()) + (parseInt(txtRangeQnty) - total_qnty));
                }
                // if ($(btn_elem).text() != "Added")
                //  $("#" + tableid + " .btnSaveProductSize").trigger("click");
            }
            else
                $("#" + tableid + " .btnSaveProductSize").attr("disabled", "disabled");

            $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text(total_percentage);

            var total_qty = 0;
            $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").each(function (index, element) {
                total_qty += parseFloat($(element).val());
            });

            if (total_qty > parseInt(txtRangeQnty)) {
                showErrorAlert("Alert", "Exceeds the Total Range Plan Quantity", "OK", function () {

                    total_qty -= parseInt($(current_elem).val());
                    $(current_elem).val('0');
                    $(current_elem).parent().parent().find(".txtRangeQnty").val('0');

                    $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text(total_qty);

                    return;
                });
            }
            else if (total_qty == parseInt(txtRangeQnty) && tr.attr("is_separate_price") != "true") {

                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");

                // if ($(btn_elem).text() != "Added")
                //  $("#" + tableid + " .btnSaveProductSize").trigger("click");
            }
            else
                $("#" + tableid + " .btnSaveProductSize").attr("disabled", "disabled");

            $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text(total_qty);


        });

        $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").change(function () {

            var total_percentage = 0;

            var current_elem = $(this);

            $(current_elem).parent().parent().find(".txtPercentage").val((parseFloat($(current_elem).val()) / parseFloat(txtRangeQnty) * 100).toFixed(2));

            $("#" + tableid + " .tblProductSize").find(".txtPercentage").each(function (index, element) {
                total_percentage += parseFloat($(element).val());
            });

            total_percentage = parseFloat(total_percentage.toFixed(0));

            if (total_percentage > 100) {
                showErrorAlert("Alert", "Total Percentage can not exceed 100", "OK", function () {
                    total_percentage -= parseFloat($(current_elem).val());
                    $(current_elem).val('0');
                    $(current_elem).parent().parent().find(".txtPercentage").val('0');
                    $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text(total_percentage.toFixed(2));
                    return;
                });
            }
            else if (total_percentage == 100 && tr.attr("is_separate_price") != "true") {
                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");

                // if ($(btn_elem).text() != "Added")
                //  $("#" + tableid + " .btnSaveProductSize").trigger("click");
            }
            else
                $("#" + tableid + " .btnSaveProductSize").attr("disabled", "disabled");

            $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text(total_percentage.toFixed(2));

            var total_qty = 0;
            $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").each(function (index, element) {
                total_qty += parseInt($(element).val());
            });

            if (total_qty > parseInt(txtRangeQnty)) {
                showErrorAlert("Alert", "Exceeds the Total Range Plan Quantity", "OK", function () {
                    total_qty -= parseInt($(current_elem).val());
                    $(current_elem).val('0');
                    $(current_elem).parent().parent().find(".txtPercentage").val('0');
                    $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text(total_qty);

                    return;
                });
            }
            else if (total_qty == parseInt(txtRangeQnty) && tr.attr("is_separate_price") != "true") {
                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");

                // if ($(btn_elem).text() != "Added")
                //  $("#" + tableid + " .btnSaveProductSize").trigger("click");
            }
            else
                $("#" + tableid + " .btnSaveProductSize").attr("disabled", "disabled");

            $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text(total_qty);



        });

        $("#" + tableid + " .tblProductSize").find(".txtSeparateMRPPrice").change(function () {

            var total_price = 0, quantity = 0;

            var current_elem = $(this);

            $("#" + tableid + " .tblProductSize").find(".txtSeparateMRPPrice").each(function (index, element) {
                if (parseFloat($(element).val()) > 0) {
                    total_price += parseFloat($(element).val());
                    quantity++;
                }
            });


            var avg_price = parseFloat(total_price) / (quantity == 0 ? 1 : quantity);

            $("#" + tableid + " .tblProductSize").find(".lblAvgMRPPrice").text("(Avg) " + avg_price.toFixed(2));

            tr.find(".txtPerPcMRPVal").val(avg_price.toFixed(2));

            tr.find(".txtPerPcMRPVal").trigger("change");

            // if (parseInt( $("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text())==100)
            // {
            //      $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
            // }
            // else if (parseInt( $("#" + tableid + " .tblProductSize").find(".lblTotalSize").text()) == 100) {
            //      $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
            // }

        });

        $("#" + tableid + " .tblProductSize").find(".txtSeparateCPUPrice").change(function () {

            var total_price = 0, quantity = 0;

            var current_elem = $(this);

            $("#" + tableid + " .tblProductSize").find(".txtSeparateCPUPrice").each(function (index, element) {
                if (parseFloat($(element).val()) > 0) {
                    total_price += parseFloat($(element).val());
                    quantity++;
                }
            });

            var avg_price = parseFloat(total_price) / (quantity == 0 ? 1 : quantity);

            $("#" + tableid + " .tblProductSize").find(".lblAvgCPUPrice").text("(Avg) " + avg_price.toFixed(2));

            tr.find(".txtPerPcCPUVal").val(avg_price.toFixed(2));

            tr.find(".txtPerPcCPUVal").trigger("change");

            if (parseInt($("#" + tableid + " .tblProductSize").find(".lblTotalSizePercentage").text()) == 100) {
                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
            }
            else if (parseInt($("#" + tableid + " .tblProductSize").find(".lblTotalSize").text()) == 100) {
                $("#" + tableid + " .btnSaveProductSize").removeAttr("disabled");
            }

        });

        $("#" + tableid + " .btnSaveProductSize").click(function () {

            var objsizelist = [];

            $("#" + tableid + " .tblProductSize").find(".txtRangeQnty").each(function (index, element) {

                var objSize = {
                    style_item_product_id: $(tr).attr('style_item_product_id'),
                    style_product_size_group_detid: $(element).attr("style_product_size_group_detid"),
                    size_quantity: parseInt($(element).val()),
                    size_per_pc_mrp_value: $(element).parent().parent().find(".txtSeparateMRPPrice").length > 0 ?
                        parseFloat($(element).parent().parent().find(".txtSeparateMRPPrice").val()) : 0,

                    size_per_pc_cpu_value: $(element).parent().parent().find(".txtSeparateCPUPrice").length > 0 ?
                        parseFloat($(element).parent().parent().find(".txtSeparateCPUPrice").val()) : 0,

                    range_plan_detail_size_id: $(element).attr('range_plan_detail_size_id') == "0" ? null : $(element).attr('range_plan_detail_size_id'),
                    range_plan_detail_id: $(element).attr('range_plan_detail_id') == "0" ? null : $(element).attr('range_plan_detail_id'),

                };

                objsizelist.push(objSize);
            });

            if (obj_detailList.filter(p => p.style_item_product_id == current_product_id).length > 0) {

                obj_detailList.filter(p => p.style_item_product_id == current_product_id)[0].size_list = objsizelist;

                if (dt_source.rows().data().filter(p => p.style_item_product_id == $(tr).attr('style_item_product_id')).length > 0)
                    dt_source.rows().data().filter(p => p.style_item_product_id == $(tr).attr('style_item_product_id'))[0].strStyleSizes = JSON.stringify(objsizelist);

                $(btn_elem).text("Added");
                $(btn_elem).attr("iscomplete", "Added");
                $(btn_elem).css("background-color", "green");
                CheckEntryCompleteness(tr);
                row.child.remove();

                tr.find(".txtPerPcMRPVal").focus();
            }

        });

        $("#" + tableid + " .btnCancelProductSize").click(function () {

            row.child.remove();

        });

       
        //$("#" + tableid + " .tblProductSize").find(".txtRangeQnty").trigger("change");
        //$("#" + tableid + " .tblProductSize").find(".txtSeparateMRPPrice").trigger("change");

        //if (row.data().is_separate_price == true) {
        //    $("#" + tableid + " .tblProductSize").find(".txtSeparateMRPPrice").trigger("change");
        //    $("#" + tableid + " .tblProductSize").find(".txtSeparateCPUPrice").trigger("change");
        //}
    }

}