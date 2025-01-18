
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_rpt_bp_data_outlet_wise_DTO
    {

        public Decimal? total_gross { get; set; }
        public Decimal? total_gross_discont { get; set; }
        public Decimal? outlet_gross_net { get; set; }

        public Int64? outlet_id { get; set; }

        public String outlet_name { get; set; }

        public string year_name { get; set; }
    }
}
