
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class vw_style_item_product_DTO
    {
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

        public Int64? fiscal_year_id { get; set; }

        public Int64? style_product_size_group_id { get; set; }


    }
}
