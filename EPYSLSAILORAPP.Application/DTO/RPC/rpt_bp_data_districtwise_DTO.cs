
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_rpt_bp_data_districtwise_DTO
    {

        public Decimal? total_gross { get; set; }

        public Decimal? total_gross_discount { get; set; }

        public Decimal? total_gross_net { get; set; }

        public Int64? district_id { get; set; }

        public String name { get; set; }
        
        public string monthly_outlet_gross { get; set; }

        public List<rpc_rpt_bp_data_districtwise_outlet_DTO> OutletList { get; set; }

        public string all_outlets { get; set; }

        public List<rpc_rpt_bp_data_districtwise_outlet_DTO> AllOutletList { get; set; }
    }

    public class rpc_rpt_bp_data_districtwise_outlet_DTO
    {
        public String outlet_name { get; set; }
        public Decimal? outlet_gross_sales { get; set; }
        public Int64? outlet_id { get; set; }
    }
}
