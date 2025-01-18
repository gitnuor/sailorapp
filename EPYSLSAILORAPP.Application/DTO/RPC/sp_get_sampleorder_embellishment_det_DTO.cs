
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_sampleorder_embellishment_det_DTO
    {

        public Int64? embellishment_id { get; set; }

        public Int64? tran_sample_order_embellishment_id { get; set; }

        public String style_embellishment_name { get; set; }

        public Int64? measurement_unit_id { get; set; }

        public Int64? default_measurement_unit_detail_id { get; set; }

        public Int64? measurement_unit_detail_id { get; set; }

    }
}
