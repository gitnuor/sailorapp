
using EPYSLSAILORAPP.Application.DTO;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{
    [Serializable]
    public class rpc_sp_get_data_for_sampleorder_DTO
    {
        public Int64? row_index {  get; set; }
        public string str_delivery_unit { get; set; }
        public Int64? tran_va_plan_detl_style_id { get; set; }
        public Int64? total_sample_count { get; set; }

        public string style_code { get; set; }
        public Int64? style_quantity { get; set; }
        public Int64? no_of_color { get; set; }

        public string color_code_gen { get; set; }

        public Int64? total_assigned_quantity { get; set; }

        public Int64? tran_va_plan_event_id { get; set; }

        public Int64? tran_sample_order_id { get; set; }

        public Int64? tran_va_plan_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public Int64? no_of_style { get; set; }

        public String style_code_gen { get; set; }

        public Boolean? is_separate_price { get; set; }

        public Int64? event_id { get; set; }

        public string? tran_sample_order_number { get; set; }

        public Int64? tran_range_plan_event_id { get; set; }

        public Boolean? is_finalized { get; set; }

        public Int64? tran_bp_event_id { get; set; }

        public Int64? tran_bp_year_id { get; set; }

        public Decimal? total_rangeplan_mrp_value { get; set; }

        public Int64? total_rangeplan_quantity { get; set; }

        public Decimal? total_rangeplan_cpu_value { get; set; }

        public Int64? style_product_size_group_id { get; set; }

        public Boolean? is_submitted { get; set; }

        public Boolean? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public String remarks { get; set; }

        public Decimal? bp_yearly_gross_sales { get; set; }

        public Decimal? event_gross_sales { get; set; }

        public Int64? range_plan_id { get; set; }

        public Int64? range_plan_detail_id { get; set; }

        public Int64? range_plan_quantity { get; set; }

        public Decimal? mrp_per_pc_value { get; set; }

        public Decimal? mrp_value { get; set; }

        public Decimal? cpu_per_pc_value { get; set; }

        public Decimal? cpu_value { get; set; }

        public Int64? priority_id { get; set; }

        public Int64? prev_year_quantity { get; set; }

        public Decimal? prev_year_efficiency { get; set; }

        public String style_item_product_name { get; set; }

        public String style_item_type_name { get; set; }

        public String style_product_type_name { get; set; }

        public String style_item_origin_name { get; set; }

        public String style_gender_name { get; set; }

        public String master_category_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? style_item_type_id { get; set; }

        public Int64? style_product_type_id { get; set; }

        public Int64? style_item_origin_id { get; set; }

        public Int64? style_gender_id { get; set; }

        public Int64? style_master_category_id { get; set; }

        public string sample_photos { get; set; }

        public List<file_upload> sample_photos_List { get; set; }

        public string sample_order_details { get; set; }
    }
}
