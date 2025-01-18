
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_measurement_unit_details_by_masterid_DTO
    {

        public Int64? gen_measurement_unit_id { get; set; }

        public String unit_name { get; set; }

        public String unit_detail_title { get; set; }

        public String unit_detail_display { get; set; }

        public Decimal? relative_factor { get; set; }

        public Int64? gen_measurement_unit_detail_id { get; set; }

        public string styleplacementinformation_list { get; set; }

    }
}
