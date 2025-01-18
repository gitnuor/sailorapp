function SaveMcd(btn, isSubmit) {
    var success_status_Code = "200";

    if (!_cusFormValidate("frmAddReceive")) {
        return;
    }

    tranFabricDetails = [];
    var genItemMasterIds = [];
    var flag = 0;


    var poid = $('#poid').val();




    populateFabricDetail();


    var { unmatchingDetails, obj_FabricPo } = populateFabricMasterl();






    if (unmatchingDetails.length > 0) {
        setTimeout(function() {
            showLoader("Saving..........");
        }, 0);

        UpdateFabricPoItem();

    }
    else {
        SaveFabricPoItem(obj_FabricPo);

    }


    function UpdateFabricPoItem() {
        ajaxPostObjectHandler("/TranMcdReceive/SaveTranRevise", obj_FabricPo, function(response) {
            setTimeout(function() {
                hideLoader();
            }, 0);


            if (response.status_Code == success_status_Code) {
                flag = 1;
                alert(flag);

                UpdateFabricPoItem();


            }
            else {
                showErrorAlert("Alert", response.status_Result, "OK", function() {
                });
            }


        }, true, function() { hideLoader(); }, true);
    }

    function UpdateFabricPoItem() {
        ajaxPostObjectHandler("/TranMcdReceive/SaveTranMcdReceive", obj_FabricPo, function(response) {

            setTimeout(function() {
                hideLoader();
            }, 0);


            if (response.status_Code == success_status_Code) {

                LoadDraftReceiveData();
                LoadReceiveData();
                LoadPOApprovalData();

                showSuccessAlert("Success", response.status_Result, "OK", function() {

                    closePopup(); details = [];

                });
            }
            else {
                showErrorAlert("Alert", response.status_Result, "OK", function() {
                });
            }
        }, true, function() { hideLoader(); }, true);
    }
    function SaveFabricPoItem(obj_FabricPo) {
        ajaxPostObjectHandler("/TranMcdReceive/SaveTranMcdReceive", obj_FabricPo, function(response) {

            setTimeout(function() {
                hideLoader();
            }, 0);


            if (response.status_Code == success_status_Code) {

                LoadDraftReceiveData();
                LoadReceiveData();
                LoadPOApprovalData();

                showSuccessAlert("Success", response.status_Result, "OK", function() {
                    closePopup(); details = [];
                });
            }
            else {
                showErrorAlert("Alert", response.status_Result, "OK", function() {
                });
            }
        }, true, function() { hideLoader(); }, true);
    }

    function populateFabricMasterl() {
        var challanNo = $('#del_chalan_no').val();
        var driverName = $('#driver_name').val();
        var vehicalNo = $('#vehical_no').val();
        var tollerence = $('#tollerence').val();
        var delevary_status = $('#modalcontent #ddldeliverystatus1').find('option:selected').val();
        var transaction_mode = $('#modalcontent #ddltranmode1').find('option:selected').val();
        var transport_type = $('#modalcontent #ddlTransport1').find('option:selected').val();
        //var rackVal = $('#modalcontent #modalContainerRack #ddlrackName').val();
        // var rackVal = $('#ddlrackName').val();
        $.each($("#DTTranFabricDetails tbody tr"), function(key, val) {
            var item_new = check_textbox_value($(this).find(".item_id_scm1"));
            genItemMasterIds.push(item_new);
        });
        var unmatchingDetails = tranFabricDetails.filter(function(detail) {
            return !genItemMasterIds.includes(detail.item_id); // Check if gen_item_master_id exists in serverItemIds
        });

        var obj_FabricPo = {
            //
            po_date: $('#modalcontent #po_date').val(),
            // po_id: selectedPO[0].id,
            po_id: poid,
            arrival_date: $('#modalcontent #arrival_date').val(),
            receive_date: $('#modalcontent #receive_date').val(),
            supplier_id: check_value($('#modalcontent #supplierId').val()),
            delivery_unit: check_value($('#modalcontent #delivery_unit_id').val()),

            // delivery_address: check_value($('#modalcontent #delivery_address').val()),
            del_chalan_no: challanNo,
            del_chalan_date: $('#modalcontent #del_chalan_date').val(),
            tran_mode: transaction_mode,
            transport_type: transport_type,
            vehical_no: vehicalNo,
            driver_name: driverName,
            tollerence: tollerence == null || tollerence === "" ? 0 : tollerence,
            delevary_status: delevary_status,
            gen_item_structure_group_id: check_value($('#modalcontent #item_structure_group_id').val()),
            is_submitted: isSubmit,
            List_Files: listAttachments,
            details: tranFabricDetails,
            unmatchingDetails: unmatchingDetails
            //rackDetail: rackDetail
        };
        return { unmatchingDetails, obj_FabricPo };
    }

    function populateFabricDetail() {
        $.each($("#DTTranFabricDetails tbody tr"), function(key, val) {

            var obj = {
                //po_id: selectedPO[0].id,
                po_id: poid,
                gen_item_master_id: $(val).find(".item_id").val(),
                item_id: $(val).find(".item_id").val(),
                po_quantity: $(val).find(".embid").val(),
                item_quantity: $(val).find(".embid").val(),
                measurement_unit: $(val).find(".uom").val(),
                unit: $(val).find(".uom").val(),
                receive_quantity: $(val).find(".recQty").val(),
                mcd_no: $(val).find(".mcdNo").val(),
                chalan_quantity: $(val).find(".chalanQty").val(),
                random_sample_quantity: $(val).find(".randomQty").val(),
                aql: $(val).find(".aql").val(),
                pass_quantity: $(val).find(".passQty").val(),
                fail_quantity: $(val).find(".failQty").val(),
                defect_percentage: $(val).find(".defect").val(),
                measurement_unit_detail_id: $(val).find(".measurement_unit_det_id").val(),
                gen_warehouse_floor_rack_info: $(val).find(".rackName").val(),

                // gen_warehouse_floor_rack_id: $('.floorId').val(), //($("#modalContainerRack #ddlrackName").length > 0 && $("#modalContainerRack #ddlrackName").val() !== null) ? $("#modalContainerRack #ddlrackName").val() : 0,
                remarks: $(val).find(".remarks").val(), //check_textbox_value($(this).find(".remarks"))
            };
            tranFabricDetails.push(obj);

        });
    }
}
