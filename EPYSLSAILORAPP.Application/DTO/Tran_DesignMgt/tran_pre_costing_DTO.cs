using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt
{

    public class tran_pre_costing_DTO : tran_sample_order_DTO
    {
        public long? row_index { get; set; }
        public long? tran_pre_costing_id { get; set; }

        public long? style_quantity { get; set; }

        public long? item_structure_group_id { get; set; }
        public String structure_group_name { get; set; }

        public String measurement_unit { get; set; }

        public String sub_group_name { get; set; }
        public decimal? estimated_markup_price { get; set; }

        public DateTime? pre_costing_date { get; set; }

        public decimal? total_raw_material_cost { get; set; }

        public decimal? total_raw_material_percentage { get; set; }

        public decimal? factory_overhead_cost { get; set; }

        public decimal? sales_marketing_distribution_cost { get; set; }

        public decimal? depreciation_amortization_cost { get; set; }

        public decimal? total_overhead_cost { get; set; }

        public decimal? total_production_cost { get; set; }

        public decimal? floor_price_percentage { get; set; }

        public decimal? floor_price_per_pc { get; set; }

        public decimal? desired_markup_percentage { get; set; }

        public decimal? desired_markup_price { get; set; }

        public decimal? final_mrp { get; set; }

        public string smv { get; set; }

        public decimal? total_style_quantity_mrp { get; set; }

        public decimal? suggested_mrp_with_cost { get; set; }

        public tran_sample_order_DTO sample_order { get; set; }

        public List<tran_pre_costing_item_detail_DTO> List_PreCostingDet { get; set; }

        public List<rpc_sp_get_pre_costing_details_by_masterid_DTO> List_PreCostingDet_rpc { get; set; }
        public List<tran_pre_costing_item_embellishment_detail_DTO> List_EmbellishmentDetl { get; set; }
        public List<tran_pre_costing_item_subcontract_detail_DTO> List_SubContractDetl { get; set; }

        public List<tran_plan_allocate_style_DTO> List_plan_detl_style { get; set; }

        public List<tran_plan_allocate_style_color_DTO> List_detl_style_color { get; set; }

        public string color_wise_size_quantity { get; set; }
        public Int64? pre_costing_quantity { get; set; }

        public Int64? is_submitted { get; set; }
        public Int64? submitted_by { get; set; }
        public Int64? is_approved { get; set; }
        public Int64? approved_by { get; set; }
        public DateTime? approve_date { get; set; }
        public string? approve_remarks { get; set; }

        public JArray item_detl_list { get; set; }
        public JArray embellishment_det_listl { get; set; }
        public JArray subcontract_det_list { get; set; }
        public tran_pre_costing_review_DTO pre_costing_review { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        public Int64? is_reviewed { get; set; }

        public Int64? tran_pre_costing_review_id { get; set; }

        public string? version_no { get; set; }
    }
}
