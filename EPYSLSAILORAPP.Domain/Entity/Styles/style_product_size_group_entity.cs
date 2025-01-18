
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("style_product_size_group")]

    public class style_product_size_group_entity : DapperExt
    {

        [Key]
        public Int64? style_product_size_group_id { get; set; }

        public String style_product_size_group_name { get; set; }

        public bool? is_separate_price { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public String size_group_details_json { get; set; }

    }
}
