
using EPYSLSAILORAPP.Application.DTO;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_range_plan_getfor_createupdate_DTO
    {
        public Int64? style_product_size_group_id { get; set; }

        public Int64? tran_range_plan_event_id { get; set; }

        public bool? is_finalized { get; set; }

        public bool? is_separate_price {  get; set; }
        public Boolean? is_submitted { get; set; }
        public Boolean? is_approved { get; set; }
        public Int64? approved_by { get; set; }
        public Int64? total_rows { get; set; }
        public Int64? event_id { get; set; }
        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public String remarks { get; set; }

        public Decimal? bp_yearly_gross_sales { get; set; }

        public decimal? total_rangeplan_mrp_value { get; set; }

        public decimal? total_rangeplan_cpu_value { get; set; }
     
        public Int64? total_rangeplan_quantity { get; set; }

        public Decimal? event_gross_sales { get; set; }

        public Int64? range_plan_id { get; set; }

        public Int64? tran_bp_event_id { get; set; }

        public Int64? tran_bp_year_id { get; set; }

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

        public String product_size_json { get; set; }
        public String priority_list_json { get; set; }
        public String stylesize_list_json { get; set; }
    }

    public class Filter_RangePlan_DataTable : DtParameters
    {
        public Int64? primary_id { get; set; }

        public string? str_primary_id { get; set; }
        public Int64? style_item_product_id { get; set; }

        public Int64? style_item_type_id { get; set; }

        public Int64? style_product_type_id { get; set; }

        public Int64? style_item_origin_id { get; set; }

        public Int64? style_gender_id { get; set; }

        public Int64? userid {  get; set; }

        public Int64? style_master_category_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        public Int64? range_plan_id { get; set; }

        public Int64? tran_va_plan_id { get; set; }

        public int? filter_option { get; set; }

        public int? is_ack {  get; set; } 
    }
}
