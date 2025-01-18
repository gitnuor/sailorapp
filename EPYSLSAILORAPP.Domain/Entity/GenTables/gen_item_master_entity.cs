
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_item_master")]

    public class gen_item_master_entity : DapperExt
    {

        [Key]
        public Int64? gen_item_master_id { get; set; }

        [Column("item_name")]
        public String item_name { get; set; }
        public String item_spec { get; set; }

        public Int64? item_structure_group_id { get; set; }

        public Int64? item_structure_sub_group_id { get; set; }

        public Int64? measurement_unit_detail_id { get; set; }

        public Int64? segment_det1_id { get; set; }

        public Int64? segment_det2_id { get; set; }

        public Int64? segment_det3_id { get; set; }

        public Int64? segment_det4_id { get; set; }

        public Int64? segment_det5_id { get; set; }

        public Int64? segment_det6_id { get; set; }

        public Int64? segment_det7_id { get; set; }

        public Int64? segment_det8_id { get; set; }

        public Int64? segment_det9_id { get; set; }

        public Int64? segment_det10_id { get; set; }

        public Int64? segment_det11_id { get; set; }

        public Int64? segment_det12_id { get; set; }

        public Int64? segment_det13_id { get; set; }

        public Int64? segment_det14_id { get; set; }

        public Int64? segment_det15_id { get; set; }

        public string? segment_det1_text { get; set; }

        public string? segment_det2_text { get; set; }

        public string? segment_det3_text { get; set; }

        public string? segment_det4_text { get; set; }

        public string? segment_det5_text { get; set; }

        public string? segment_det6_text { get; set; }

        public string? segment_det7_text { get; set; }

        public string? segment_det8_text { get; set; }

        public string? segment_det9_text { get; set; }

        public string? segment_det10_text { get; set; }

        public string? segment_det11_text { get; set; }

        public string? segment_det12_text { get; set; }

        public string? segment_det13_text { get; set; }

        public string? segment_det14_text { get; set; }

        public string? segment_det15_text { get; set; }


        public String remarks { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
