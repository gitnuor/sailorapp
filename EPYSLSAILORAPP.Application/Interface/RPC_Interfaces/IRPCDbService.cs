using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.DashBoard;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IRPCDbService
    {

        Task<List<rpc_range_plan_getfor_createupdate_DTO>> GetAll_Range_plan_getfor_CreateUpdate(Filter_RangePlan_DataTable param);
        Task<List<rpc_get_outlet_sales_data>> GetAllfn_get_outlet_salesAsync(Int64? year_id);
        Task<List<rpc_range_plan_getfor_view_DTO>> GetAll_Range_plan_getfor_View(Filter_RangePlan_DataTable param);

        Task<List<rpc_sp_get_style_group_size_DTO>> GetAll_style_group_sizeAsync();

        Task<List<rpd_sp_get_style_group_size_by_fiscalyearid_DTO>> GetAll_style_group_size_by_fiscalyearid(Int64? fiscal_year_id, Int64? evet_id);

        Task<List<rpc_sp_get_tran_range_plan_events_DTO>> GetAll_Tran_range_plan_eventsAsync(Int64? fiscal_year_id,string search,Int64? primary_id);

        Task<List<rpc_sp_get_tran_range_plan_summary_DTO>> GetAll_Tran_range_plan_summaryAsync();

        Task<List<rpc_sp_get_tran_va_plan_summary_DTO>> GetAll_VA_plan_summaryAsync();

        Task<List<rpc_sp_get_va_plan_events_DTO>> GetAllsp_get_va_plan_eventsAsync(Int64? fiscal_year_id);
        
        Task<get_table_data_count_for_select2_DTO> GetCountForSelect2Async();
        Task<List<rpc_plan_allocate_getfor_createupdate_DTO>> GetAllva_plan_getfor_createupdateAsync(Filter_RangePlan_DataTable param);

        Task<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>> GetAllsp_get_color_detl_size_by_vaplandetlidAsync(Int64? va_plan_detl_id_filter);

        Task<List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO>> GetAllsp_get_color_detl_size_by_style_id(Int64? va_plan_detl_id_filter);

        Task<List<rpc_va_plan_get_designer_assign_details_DTO>> GetAllva_plan_get_designer_assign_detailsAsync( Int64 fiscal_id_filter, Int64 event_id_filter);

        Task<List<rpc_va_plan_get_designer_assign_details_det_DTO>> GetAllva_plan_get_designer_assign_details_detAsync(Int64 fiscal_id_filter, Int64 event_id_filter);

        Task<List<rpc_va_plan_get_designer_assign_details_det_DTO>> GetAllva_plan_get_designer_assign_details_det_by_detidAsync(Int64 planning_detl_id_filter);

        Task<List<rpc_calendar_chart_data>> get_calendar_chart_data(long year_id);
        Task<List<rpc_sp_get_style_product_item_DTO>> GetAllsp_get_style_product_itemAsync(
         
            Int64? style_master_category_id_filter,
            Int64? style_gender_id_filter,
            Int64? style_item_origin_id_filter,
            Int64? style_item_type_id_filter,
            Int64? style_product_type_id_filter, string search
            );

        Task<List<rpc_sp_get_data_for_sampleorder_DTO>> GetAllsp_get_data_for_sampleorderAsync(
           Int64 fiscal_year_id, Int64 event_id_filter, Int64? userid_filter
            );

        Task<List<rpc_sp_get_mapped_item_structure_DTO>> GetAllsp_get_mapped_item_structureAsync(Int64 style_master_category_id_filter);

        Task<List<rpc_sp_get_sample_order_details_DTO>> GetAllsp_get_sample_order_detailsAsync(Int64 designer_member_id_filter);

        Task<List<rpc_sp_get_sampleorder_subgroup_det_DTO>> GetAllsp_get_sampleorder_subgroup_detAsync(Int64 tran_sample_order_id_filter);

        Task<List<rpc_sp_get_sampleorder_embellishment_det_DTO>> GetAllsp_get_sampleorder_embellishment_detAsync(Int64 tran_sample_order_id_filter);

        Task<List<rpc_sp_get_pre_costing_details_DTO>> GetAllsp_get_pre_costing_detailsAsync(Int64 designer_member_id_filter);

        Task<List<rpc_sp_get_data_for_pre_costing_DTO>> GetAllsp_get_data_for_pre_costingAsync(Filter_RangePlan_DataTable request);

        Task<List<rpc_sp_get_mapped_segment_by_gen_item_structure_group_sub_id_DTO>> GetAllsp_get_mapped_segment_by_gen_item_structure_group_sub_idAsync(Int64 gen_item_structure_group_sub_id_filter);

        Task<List<rpc_sp_get_measurement_unit_details_by_masterid_DTO>> GetAllsp_get_measurement_unit_details_by_masteridAsync(Int64 gen_measurement_unit_id_filter);

        Task<List<rpc_sp_get_event_details_allphase_DTO>> GetAllsp_get_event_details_allphaseAsync(Int64? _fiscal_year_id_filter, Int64? _event_id_filter);

        Task<List<rpc_sp_get_data_for_designer_review_DTO>> GetAllsp_get_data_for_designer_reviewAsync(Filter_RangePlan_DataTable request);
        Task<List<rpc_sp_get_data_for_designer_review_DTO>> GetAllsp_get_data_for_designer_Approval_reviewAsync(Filter_RangePlan_DataTable request);

        Task<List<rpc_sp_get_pre_costing_details_by_masterid_DTO>> GetAllsp_get_pre_costing_details_by_masteridAsync(Int64 pre_costing_id_filter);

        Task<List<rpc_sp_get_data_for_techpack_DTO>> GetAllsp_get_data_for_techpackAsync(Filter_RangePlan_DataTable param);

        Task<List<rpc_sp_get_data_for_techpack_DTO>> GetAllsp_get_data_for_techpack_ackAsync
         (Filter_RangePlan_DataTable param);

        Task<List<tran_bp_event_month_dto>> GetAllget_bp_year_event_month_dataAsync(Int64 p_tran_bp_year_id);

        Task<rpc_proc_sp_get_sample_order_data_DTO> GetAllproc_sp_get_sample_order_dataAsync(Int64 fiscal_year_id_filter, Int64 style_item_product_id_filter, Int64 team_category_id_filter, Int64 tran_va_plan_detl_id_filter);

        Task<rpc_proc_sp_get_sample_order_data_edit_DTO> GetAllproc_sp_get_sample_order_data_editAsync(Int64? tran_sample_order_id_filter
        , Int64? fiscal_year_id_filter, Int64? style_item_product_id_filter
        , Int64? team_category_id_filter);

        Task<rpc_proc_sp_get_pre_costing_data_DTO> GetAllproc_sp_get_pre_costing_dataAsync(Int64? tran_sample_order_id_filter
        ,Int64? style_item_product_id_filter,Int64? fiscal_year_id_filter,Int64? team_category_id_filter
        );

        Task<rpc_proc_sp_get_pre_costing_data_edit_DTO> GetAllproc_sp_get_pre_costing_data_editAsync(Int64? tran_pre_costing_id_filter
        , Int64? tran_sample_order_id_filter
        , Int64? style_item_product_id_filter
        , Int64? fiscal_year_id_filter
        , Int64? team_category_id_filter
        );

        Task<rpc_proc_sp_get_tech_pack_data_DTO> GetAllproc_sp_get_tech_pack_dataAsync(Int64? tran_pre_costing_id_filter
        , Int64? tran_sample_order_id_filter
        , Int64? style_item_product_id_filter
        , Int64? fiscal_year_id_filter
        );

        Task<rpc_proc_sp_get_tech_pack_data_edit_DTO> GetAllproc_sp_get_tech_pack_data_editAsync(Int64? tran_tech_pack_id_filter
        , Int64? tran_pre_costing_id_filter
        , Int64? tran_sample_order_id_filter
        , Int64? style_item_product_id_filter
        , Int64? fiscal_year_id_filter
        , Int64? team_category_id_filter
        );

        Task<rpc_get_user_info_DTO> GetAllget_user_infoAsync(String user_name_flter);

        Task<rpc_proc_get_filter_items_DTO> GetAllproc_get_filter_itemsAsync(Int64? fiscal_year_id_filter);

        Task<rpc_proc_get_supplier_new_DTO> GetAllproc_get_supplier_newAsync();

        Task<List<rpc_rpt_bp_data_outlet_wise_DTO>> GetAllrpt_bp_data_outlet_wiseAsync(Int64? fiscal_year_filter);

        Task<List<rpc_rpt_bp_data_month_wise_DTO>> GetAllrpt_bp_data_month_wiseAsync(Int64? fiscal_year_filter);

        Task<rpc_proc_gen_item_structure_group_sub_new_DTO> GetAllproc_gen_item_structure_group_sub_newAsync();

        Task<rpc_proc_gen_item_structure_group_sub_edit_DTO> GetAllproc_gen_item_structure_group_sub_editAsync(Int64? gen_item_structure_group_sub_id_filter);

        Task<List<gen_item_master_entity>> GetAllget_gen_item_master_by_techpack_idAsync(Int64? tran_techpack_id_filter, Int64? item_structure_sub_group_id);

        Task<List<rpc_sp_get_data_for_pre_costing_review_DTO>> GetAllsp_get_data_for_pre_costing_reviewAsync(Int64? _fiscal_year_id_filter,
            Int64? _event_id_filter, Int64? designer_userid_filter, Int64? merchant_userid_filter, Int64? data_filter_type,string search);

        Task<List<rpc_sp_get_data_for_pre_costing_for_review_approval_DTO>> GetAllsp_get_data_for_pre_costing_for_review_approvalAsync(Int64? _fiscal_year_id_filter, 
            Int64? _event_id_filter , Int64? designer_userid_filter, Int64? merchant_userid_filter, Int64? data_filter_type);

        Task<rpc_proc_sp_get_pre_costing_modification_request_data__DTO> GetAllproc_sp_get_pre_costing_modification_request_data_Async(Int64? tran_pre_costing_review_id_filter
            , Int64? tran_pre_costing_id_filter
            , Int64? tran_sample_order_id_filter
            , Int64? style_item_product_id_filter
            , Int64? fiscal_year_id_filter
            , Int64? team_category_id_filter
            );

        Task<rpc_sp_get_data_for_mcd_receive_DTO> GetAllsp_get_data_for_mcd_receiveAsync(Int64? po_id_filter);
        Task<rpc_tran_mcd_receive_DTO> GetAllJoined_TranMcdReceiveAsync(Int64 row_index, Int64 page_size ,string query_type, Int64 receivedID, Int64 actionType, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, DtSearch search);
        Task<List<rpc_tran_mcd_receive_detail_DTO>> GetAllJoined_TranMcdReceiveDetailAsync(Int64 received_id_filter);
        Task<List<rpc_tran_mcd_receive_detail_DTO>> GetAllJoined_TranMcdReceiveDetailFailAsync(Int64 received_id_filter);
        Task<List<rpc_tran_mcd_receive_DTO>> GetAllJoined_TranMcdReceiveListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 actionType, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id , DtSearch search);
        //Task<rpc_tran_mcd_receive_DTO> Get_sp_get_data_for_mcd_receive_by_ID_Async(Int64? received_id);
        Task<List<rpc_tran_mcd_requisition_slip_DTO>> GetAllsp_get_techPack_by_gen_item_structure_group_sub_idAsync(Int64 teckPackId, Int64 gen_item_structure_group_sub_id_filter);
        Task<List<rpc_tran_mcd_requisition_slip_DTO>> GetAll_gen_item_structure_group_sub_idAsync_by_requisition_slip(long requisition_slip_id, long sub_group_id);
        Task<List<tran_mcd_requisition_slip_DTO>> GetAllJoined_FabricRequisitionSlipListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 requisitionSlipid, Int64 genGroupId, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id);
        Task<List<rpc_plan_allocate_getfor_approval_DTO>> GetAllplan_allocate_getfor_approvalAsync(Filter_RangePlan_DataTable param);
        Task<List<tran_mcd_requisition_slip_DTO>> GetAllJoined_FabricRequisitionSlipProposedListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 requisitionSlipid, Int64 gen_groupid, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id);
        Task<List<tran_mcd_requisition_slip_DTO>> GetAllJoined_FabricRequisitionSlipApprovedListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 requisitionSlipid, Int64 gen_groupid, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id);
        Task <rpc_tran_mcd_requisition_slip_masterDetail_DTO> Get_master_detail_fabric_requisition_slipAsync(Int64 requisitionSlipid);
        Task<List<rpc_tran_plan_allocate_getfor_designer_distribution_DTO>> GetAlltran_plan_allocate_getfor_designer_distribution(Filter_RangePlan_DataTable param);
        Task<List<rpc_tran_sample_order_getfor_create_DTO>> GetAlltran_sample_order_getfor_createAsync(Filter_RangePlan_DataTable request);
        Task<List<rpc_tran_sample_order_getfor_create_DTO>> GetAlltran_sample_order_getfor_create_with_trading(Filter_RangePlan_DataTable request);
        Task<List<rpc_tran_mcd_receive_DTO>> GetTranMcdRejectListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id,string search);
        Task<List<tran_mcd_purchase_return_DTO>> GetTranMcdRejectDraftListAsync(Int64 row_index, Int64 page_size, Int64 actionType, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id,string search);
        Task<tran_mcd_purchase_return_DTO> Get_master_detail_tran_purchase_return_slipAsync(Int64 purchase_return_id);
        Task<List<rpc_rpt_bp_data_month_compare_data_DTO>> GetAllrpt_bp_data_month_compare_dataAsync(Int64? fiscal_year_filter, Int64? fiscal_year_compare_with_filter);
        Task<List<rpc_rpt_bp_data_districtwise_DTO>> GetAllrpt_bp_data_districtwiseAsync(Int64? fiscal_year_filter);
        Task<List<rpc_rpt_bp_data_month_wise_outlet_DTO>> GetAllrpt_bp_data_month_wise_outletAsync(Int64? fiscal_year_filter);
        Task<rpc_proc_sp_get_data_tran_scm_po_DTO> GetAllproc_sp_get_data_tran_scm_poAsync(Int64? po_id_filter);
        Task<rpc_proc_sp_get_data_tran_purchase_requisition_details_DTO> GetAllproc_sp_get_data_tran_purchase_requisition_detailsAsync(Int64? pr_id_filter);
        Task<rpc_proc_sp_get_data_tran_scm_po_DTO> GetAllproc_sp_get_data_tran_scm_revise_poAsync(Int64? po_id_filter);
        Task<List<lookup_trading_DTO>> GetAllTradingData(Int64 p_lookup_master_id);


    }
}

