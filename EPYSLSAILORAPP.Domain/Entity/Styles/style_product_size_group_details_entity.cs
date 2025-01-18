

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("style_product_size_group_details")]

    public class style_product_size_group_details_entity : DapperExt
    {

        [Key]
        public Int64? style_product_size_group_detid { get; set; }

        public Int64? style_product_size_group_id { get; set; }


        public String style_product_size { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
