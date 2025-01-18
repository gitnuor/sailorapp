

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_warehouse_floor_rack")]

    public class gen_warehouse_floor_rack_entity : DapperExt
    {

        [Key]
        public Int64? gen_warehouse_floor_rack_id { get; set; }

        public Int64? gen_warehouse_floor_id { get; set; }

        public String rack_name { get; set; }

        public String rack_info { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
