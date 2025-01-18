
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Statics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{
    public class rpc_sp_get_color_detl_size_by_vaplandetlid_Root_DTO
    {
        public string style_product_size_details { get; set; }
    }
   
    public class rpc_sp_get_color_detl_size_by_vaplandetlid_DTO
    {

        public String style_item_product_name { get; set; }

        public String style_product_size {  get; set; }

        [JsonProperty("style_embellishment_ids")]
        public String style_embellishment_ids { get; set; }//

        public Int64? tran_va_plan_detl_style_color_size_id { get; set; }

        public Int64? designer_member_id { get; set; }

        public Int64? style_item_product_sub_category_id { get; set; }

        public string sub_category_name { get; set; }

        public Int64? tran_va_plan_detl_style_color_id { get; set; }

        public Int64? style_product_size_group_detid { get; set; }

        public Int64? style_color_size_quantity { get; set; }

        public Int64? style_color_quantity { get; set; }

        public String color_code { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public String color_code_gen { get; set; }

        public Int64? no_of_color { get; set; }

        public Int64? style_quantity { get; set; }

        public String style_code { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public String style_code_gen { get; set; }

        public Int64? no_of_style { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? range_plan_detail_id { get; set; }

        public Int64? tran_va_plan_event_id { get; set; }

        public Int64? is_submitted { get; set; }
        public Int64? is_approved { get; set; }
        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String? approve_remarks { get; set; }

        public Int64? tran_sample_order_id { get; set; }
        public Int64? trading_type { get; set; }

        public String? trading_type_name { get; set; }


    }

    public class rpc_style_det
    {
        public string style_product_size_details { get; set; }
    }
}
