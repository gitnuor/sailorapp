
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_mapped_segment_by_gen_item_structure_group_sub_id_DTO
    {

        public Int64? gen_item_structure_group_sub_segment_mapping_id { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

        public Int64? gen_segment_id { get; set; }

        public String gen_segment_name { get; set; }

        public String sub_group_name { get; set; }

        public string styleplacementinformation_list { get; set; }

    }
}
