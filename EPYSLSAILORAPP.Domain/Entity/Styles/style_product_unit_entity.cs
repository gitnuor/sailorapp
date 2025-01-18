
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("style_product_unit")]

    public class style_product_unit_entity : DapperExt
    {

        [Key]
        public Int64? style_product_unit_id { get; set; }

        public String style_product_unit_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? unit_type { get; set; }
    }
}
