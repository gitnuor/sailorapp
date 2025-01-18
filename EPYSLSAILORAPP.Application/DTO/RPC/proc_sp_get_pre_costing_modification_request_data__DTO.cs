
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_sp_get_pre_costing_modification_request_data__DTO
    {
        public Int64? tran_pre_costing_review_id { get; set; }
        public Int64? tran_pre_costing_id { get; set; }
        public Int64? tran_sample_order_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public String tran_sample_order_number { get; set; }

        public DateTime? order_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public Int64? delivery_unit_id { get; set; }

        public Int64? order_quantity { get; set; }

        public Int64? designer_member_id { get; set; }
        [JsonProperty("sample_photos")]
        public string? sample_photos { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public String style_code { get; set; }

        public Int64? style_quantity { get; set; }

        public string style_product { get; set; }

        public string style_product_details { get; set; }

        public Int64? no_of_color { get; set; }

        public String color_code_gen { get; set; }

        public Int64? style_item_product_sub_category_id { get; set; }

        public String style_product_size_group_name { get; set; }

        public String style_item_product_name { get; set; }

        public String style_item_type_name { get; set; }

        public String style_product_type_name { get; set; }

        public String style_item_origin_name { get; set; }

        public String style_gender_name { get; set; }

        public String master_category_name { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? style_item_type_id { get; set; }

        public Int64? style_product_type_id { get; set; }

        public Int64? style_item_origin_id { get; set; }

        public Int64? style_gender_id { get; set; }

        public Int64? style_master_category_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? style_product_size_group_id { get; set; }

        public String designer_list { get; set; }

        public String mapping_structure_list { get; set; }

        public String gen_itemstructure_groupsub_list { get; set; }

        public String gen_unit_list { get; set; }

        public String style_product_unit_list { get; set; }

        public String style_product_sizegroupdetails_list { get; set; }

        public String sample_order_embellishmentlist { get; set; }

        public String sample_order_detaillist { get; set; }

        public String sampleorder_subgroup_list { get; set; }

        public String color_detl_size_list { get; set; }

        public String name { get; set; }

        public String gen_process_master_list { get; set; }

        public DateTime? pre_costing_date { get; set; }

        public Decimal? total_raw_material_cost { get; set; }

        public Decimal? total_raw_material_percentage { get; set; }

        public string team_member_marketing_name {  get; set; }

        public Decimal? factory_overhead_cost { get; set; }

        public Decimal? sales_marketing_distribution_cost { get; set; }

        public Decimal? depreciation_amortization_cost { get; set; }

        public Decimal? total_overhead_cost { get; set; }

        public Decimal? total_production_cost { get; set; }

        public Decimal? floor_price_percentage { get; set; }

        public Decimal? floor_price_per_pc { get; set; }

        public Decimal? desired_markup_percentage { get; set; }

        public Decimal? estimated_markup_price { get; set; }

        public Decimal? desired_markup_price { get; set; }

        public Decimal? final_mrp { get; set; }

        public Decimal? total_style_quantity_mrp { get; set; }

        public Decimal? suggested_mrp_with_cost { get; set; }

        public String smv { get; set; }

        public String remarks { get; set; }

        public String color_wise_size_quantity { get; set; }

        public Int64? pre_costing_quantity { get; set; }
        [JsonProperty("pre_costing_detail_list")]
        public String pre_costing_detail_list { get; set; }

        public String pre_costing_embellishment_list { get; set; }

        public String pre_costing_subcontract_list { get; set; }

        public DateTime? pre_costing_date_old { get; set; }

        public Decimal? total_raw_material_cost_old { get; set; }

        public Decimal? total_raw_material_percentage_old { get; set; }

        public Decimal? factory_overhead_cost_old { get; set; }

        public Decimal? sales_marketing_distribution_cost_old { get; set; }

        public Decimal? depreciation_amortization_cost_old { get; set; }

        public Decimal? total_overhead_cost_old { get; set; }

        public Decimal? total_production_cost_old { get; set; }

        public Decimal? floor_price_percentage_old { get; set; }

        public Decimal? floor_price_per_pc_old { get; set; }

        public Decimal? desired_markup_percentage_old { get; set; }

        public Decimal? estimated_markup_price_old { get; set; }

        public Decimal? desired_markup_price_old { get; set; }

        public Decimal? final_mrp_old { get; set; }

        public Decimal? total_style_quantity_mrp_old { get; set; }

        public Decimal? suggested_mrp_with_cost_old { get; set; }

        public String smv_old { get; set; }

        public String remarks_old { get; set; }

        public String color_wise_size_quantity_old { get; set; }

        public Int64? pre_costing_quantity_old { get; set; }

        public String pre_costing_detail_list_old { get; set; }

        public String pre_costing_embellishment_list_old { get; set; }

        public String pre_costing_subcontract_list_old { get; set; }

        public string tpc_new_data { get; set; }

        public string tpc_old_data { get; set; }


        public tran_pre_costing_entity obj_tpc_new_data { get; set; }

        public tran_pre_costing_entity obj_tpc_old_data { get; set; }

        public List<file_upload> List_SampleOrderFiles { get; set; }

        public List<SelectListItem> List_Unit { get; set; }

        public List<rpc_sp_get_mapped_item_structure_DTO> List_Mapped_Structure { get; set; }

        public List<gen_item_structure_group_sub_DTO> List_Structure_det { get; set; }

        public List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO> List_Color_detl_size { get; set; }

        
        public List<file_upload> List_base64String_File { get; set; }

        public List<tran_sample_order_subgroup_DTO> List_SubGroup { get; set; }

        public List<tran_sample_order_detl_DTO> List_Detl { get; set; }

        public List<tran_sample_order_embellishment_DTO> List_Embellishmet { get; set; }


        public List<rpc_sp_get_pre_costing_details_by_masterid_DTO> List_PreCostingDet_rpc { get; set; }
        public List<tran_pre_costing_item_embellishment_detail_DTO> List_EmbellishmentDetl { get; set; }
        public List<tran_pre_costing_item_subcontract_detail_DTO> List_SubContractDetl { get; set; }

        public List<rpc_sp_get_pre_costing_details_by_masterid_DTO> List_PreCostingDet_rpc_old { get; set; }
        public List<tran_pre_costing_item_embellishment_detail_DTO> List_EmbellishmentDetl_old { get; set; }
        public List<tran_pre_costing_item_subcontract_detail_DTO> List_SubContractDetl_old { get; set; }

        public List<tran_plan_allocate_style_DTO> List_plan_detl_style { get; set; }
        public List<tran_plan_allocate_style_color_DTO> List_detl_style_color { get; set; }
        public List<tran_plan_allocate_style_color_DTO> List_detl_style_color_Old { get; set; }

        public DateTime? designer_approve_date { get; set; }
        public string? designer_approve_remarks { get; set; }
        public Int64? designer_approved_by { get; set; }
        public Int64? is_approved_by_designer { get; set; }
        public Int64? is_ack_by_merchant { get; set; }
        public Int64? is_submitted { get; set; }
        public Int64? merchant_ack_by { get; set; }
        public DateTime? merchant_ack_date { get; set; }
        public string? merchant_ack_remarks { get; set; }

    }
}
