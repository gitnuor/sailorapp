
using EPYSLSAILORAPP.Application.DTO;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_data_for_techpack_DTO
    {
        public string techpack_number { get; set; }

        public DateTime? techpack_date { get; set; }
        public Int64? tran_techpack_id { get; set; }

        public Int64? tran_designer_review_id { get; set; }

        public Int64? tran_pre_costing_id { get; set; }

        public Int64? tran_sample_order_id { get; set; }

        public String tran_sample_order_number { get; set; }

        public DateTime? order_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public Int64? delivery_unit_id { get; set; }

        public String unit_name { get; set; }

        public Int64? order_quantity { get; set; }

        public Int64? style_quantity { get; set; }

        public String style_code { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public Int64? tran_va_plan_event_id { get; set; }

        public Int64? tran_va_plan_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public Int64? no_of_style { get; set; }

        public String style_code_gen { get; set; }

        public Boolean? is_separate_price { get; set; }


        public Int64? style_product_size_group_id { get; set; }

        public Boolean? is_submitted { get; set; }

        public Boolean? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public String remarks { get; set; }

       
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

        public Int64? is_ack { get; set; }
        public DateTime? ack_date { get; set; }

        public List<file_upload> sample_photos_List { get; set; }

        public String sample_photos { get; set; }

        public Int64? total_rows { get; set; }

        public string merchant_name {  get; set; }

        public String designer_name { get; set; }
    }
}
