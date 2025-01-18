
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_style_product_item_DTO
    {

        public String style_item_product_name { get; set; }

        public String style_item_type_name { get; set; }

        public string style_product_size_group_name { get; set; }


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


    }
}
