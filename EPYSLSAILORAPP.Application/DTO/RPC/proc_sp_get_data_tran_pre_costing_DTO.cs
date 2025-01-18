
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_sp_get_data_tran_pre_costing_DTO
    {

        public Int64? tran_pre_costing_id { get; set; }

        public Int64? tran_sample_order_id { get; set; }

        public DateTime? pre_costing_date { get; set; }

        public Decimal? total_raw_material_cost { get; set; }

        public Decimal? total_raw_material_percentage { get; set; }

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
        [JsonProperty("color_wise_size_quantity")]
        public String color_wise_size_quantity { get; set; }

        public Int64? pre_costing_quantity { get; set; }

        public Int64? is_submitted { get; set; }

        public Int64? submitted_by { get; set; }

        public Int64? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public String item_detl_list { get; set; }

        public String embellishment_det_listl { get; set; }

        public String subcontract_det_list { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        [JsonProperty("tran_pre_costing_item_detail_list")]
        public String tran_pre_costing_item_detail_list { get; set; }

        [JsonProperty("tran_pre_costing_item_embellishment_detail_list")]
        public String tran_pre_costing_item_embellishment_detail_list { get; set; }

        [JsonProperty("tran_pre_costing_item_subcontract_detail_list")]
        public String tran_pre_costing_item_subcontract_detail_list { get; set; }


    }
}
