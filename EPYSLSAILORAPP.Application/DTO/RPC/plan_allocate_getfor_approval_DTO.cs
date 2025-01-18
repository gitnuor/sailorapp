
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_plan_allocate_getfor_approval_DTO
    {

        public Int64? tran_va_plan_detl_id { get; set; }

        public Int64? no_of_style { get; set; }

        public String style_code_gen { get; set; }

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

        public String style_code { get; set; }

        public Int64? style_quantity { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public string stylesize_list_json { get; set; }

        public Int64? total_rows { get; set; }
        public string trading_type_name { get; set; }
       

    }
}
