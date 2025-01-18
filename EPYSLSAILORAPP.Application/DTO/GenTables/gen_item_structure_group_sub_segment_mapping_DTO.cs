
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{
    public class gen_item_structure_group_sub_segment_mapping_DTO
    {

        public Int64? gen_item_structure_group_sub_segment_mapping_id { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

        public Int64? gen_segment_id { get; set; }

        public string segment_text { get; set; }
        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
