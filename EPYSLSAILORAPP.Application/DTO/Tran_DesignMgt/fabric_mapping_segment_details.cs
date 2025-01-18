
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class fabric_mapping_segment_details_for_fabricbooking_dto
    {



        public Int64? gen_item_structure_group_id { get; set; }
        public Int64? gen_item_structure_group_sub_id { get; set; }


        public List<rpc_sp_get_mapped_segment_by_gen_item_structure_group_sub_id_DTO> mapping_item { get; set; }

    }
}
