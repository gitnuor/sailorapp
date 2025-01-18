
using EPYSLSAILORAPP.Domain.DTO;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_gen_item_structure_group_sub_edit_DTO
    {
        public Int64? gen_item_master_id { get; set; }
        public Int64? gen_item_structure_group_sub_id { get; set; }

        public Int64? item_structure_group_id { get; set; }

        public String sub_group_name { get; set; }

        public Int64? measurement_unit_id { get; set; }

        public Int64? default_measurement_unit_detail_id { get; set; }

        public Int64? secondary_measurement_unit_id { get; set; }

        public Int64? secondary_measurement_unit_detail_id { get; set; }

        public String measurement_unit { get; set; }

        public String default_measurement_unit_detail { get; set; }

        public String secondary_measurement_unit { get; set; }

        public String secondary_measurement_unit_detail { get; set; }

        public String gen_item_structure_group_list { get; set; }

        public String gen_measurement_unit_list { get; set; }

        public String gen_measurement_unit_detail_list { get; set; }

        public String gen_segment_mapp_list { get; set; }

        public List<gen_item_structure_group_DTO> List_gen_item_structure_group { get; set; }

        public List<gen_measurement_unit_DTO> List_gen_measurement_unit { get; set; }

        public List<gen_measurement_unit_detail_DTO> List_measurement_unit_detail { get; set; }

        public List<gen_item_structure_group_sub_segment_mapping_DTO> List_item_structure_group_sub_segment_mapping { get; set; }


    }
}
