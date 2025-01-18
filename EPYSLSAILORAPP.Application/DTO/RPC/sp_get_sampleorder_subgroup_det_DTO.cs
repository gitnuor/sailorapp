
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_sampleorder_subgroup_det_DTO
    {

        public Int64? item_structure_group_id { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

        public String sub_group_name { get; set; }

        public Int64? tran_sample_order_subgroup_id { get; set; }

        public Int64? measurement_unit_id { get; set; }
        public Int64? default_measurement_unit_detail_id { get; set; }

    }
}
