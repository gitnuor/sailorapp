
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.RPC;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_va_plan_Filter_DTO
    {
        public Int64? tran_pre_costing_review_id { get; set; }
        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }
        public Int64? tran_sample_order_id { get; set; }
        public Int64? tran_techpack_id { get; set; }
        public Int64? tran_pre_costing_id { get; set; }
        public Int64? tran_designer_review_id { get; set; }
        public Int64? row_index { get; set; }

        public Int64? data_filter_type { get; set; }

        public string style_code { get; set; }

        public DateTime delivery_date { get; set; }

        public string unit_name { get; set; }

        public Int64? delivery_unit_id { get; set; }

        public Int64? order_quantity { get; set; }
        public Int64? tran_va_plan_detl_style_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public Int64? style_quantity { get; set; }
        public Int64? style_product_size_group_id { get; set; }

        public Int64? tran_va_plan_id { get; set; }

        public Int64? tran_va_plan_event_id { get; set; }

        public Int64? tran_range_plan_event_id { get; set; }

        public Int64? range_plan_id { get; set; }
        public Int64? range_plan_detail_id { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? no_of_style { get; set; }

        public Int64? userid { get; set; }

        public String style_code_gen { get; set; }

        public Int64? range_plan_qnty { get; set; }

        public string style_product { get; set; }

        public string style_product_detail { get; set; }

        public string str_style_size { get; set; }

        public Int64? style_master_category_id { get; set; }

        public Int64? is_ack { get; set; }

        public List<rpd_sp_get_style_group_size_by_fiscalyearid_DTO> List_product_size { get; set; }

        public Int64? is_view { get; set; }

        public DtSearch search { get; set; }
    }
}
