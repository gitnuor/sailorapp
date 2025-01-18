
using EPYSLSAILORAPP.Domain.DTO;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_mapped_item_structure_DTO
    {

        public Int64? item_structure_group_id { get; set; }

        public String structure_group_name { get; set; }

        public String sub_group_name { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

        public List<mapped_rpc_item_structure> List { get; set; }

        public List<rpc_sp_get_sampleorder_subgroup_det_DTO> List_sub_group { get; set; }


    }
}
