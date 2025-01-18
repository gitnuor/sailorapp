
using Dapper.Contrib.Extensions;
using Postgrest.Attributes;
using Postgrest.Models;
//using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_item_structure_group_sub")]

    public class gen_item_structure_group_sub_entity : DapperExt
    {

        [Key]
        public Int64? gen_item_structure_group_sub_id { get; set; }

        public Int64? item_structure_group_id { get; set; }

        public String sub_group_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? measurement_unit_id { get; set; }
        public Int64? default_measurement_unit_detail_id { get; set; }

        public Int64? secondary_measurement_unit_id { get; set; }

        public Int64? secondary_measurement_unit_detail_id { get; set; }

        public String measurement_unit { get; set; }
        public String default_measurement_unit_detail { get; set; }
        public String secondary_measurement_unit { get; set; }
        public String secondary_measurement_unit_detail { get; set; }
    }
}
