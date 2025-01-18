using BDO.Core.Base;
using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_item_master_DTO : BaseEntity
    {
        public Int64? gen_item_master_id { get; set; }

        [Required]
        public String item_name { get; set; }
        public String item_spec { get; set; }

        [Required]
        public Int64? item_structure_group_id { get; set; }

        [Required]
        public Int64? item_structure_sub_group_id { get; set; }

        [Required]
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

        public String remarks { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
