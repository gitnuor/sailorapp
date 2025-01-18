

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_warehouse_floor")]

    public class gen_warehouse_floor_entity : DapperExt
    {

        [Key]

        public Int64? gen_warehouse_floor_id { get; set; }

        public Int64? gen_warehouse_id { get; set; }

        public String floor_name { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
