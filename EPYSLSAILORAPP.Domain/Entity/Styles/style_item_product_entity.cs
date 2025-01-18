
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{

    [System.ComponentModel.DataAnnotations.Schema.Table("style_item_product")]
    public class style_item_product_entity : DapperExt
    {

        [Key]
        public Int64? style_item_product_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public String style_item_product_name { get; set; }

        public Int64? style_item_type_id { get; set; }

        public Int64? style_product_type_id { get; set; }

        public Int64? style_item_origin_id { get; set; }

        public Int64? style_gender_id { get; set; }

        public Int64? style_master_category_id { get; set; }

        public Int64? style_product_size_group_id { get; set; }

        public string product_sub_category_json { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }
    }
}
