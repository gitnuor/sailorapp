
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_gen_arif_DTO
    {
        public Int64? gen_arif_id { get; set; }
        public String bank_name { get; set; }

        public String bank_short_name { get; set; }

        public Boolean? is_used { get; set; }

        public Boolean? is_local { get; set; }

        public JArray? arif_details_1 { get; set; }

        public JArray? arif_details_2 { get; set; }

        public Int64? unit_id { get; set; }

        public Int64? district_id { get; set; }

        public Int64? gen_arif_details1_id { get; set; }

        public String details1 { get; set; }

        public String details2 { get; set; }

        public Int64? current_state { get; set; }

        public Int64? gen_arif_details2_id { get; set; }

        public String details3 { get; set; }

        public String details4 { get; set; }

        public Int64? gen_unit_id { get; set; }

        public String unit_name { get; set; }

        public String remarks { get; set; }

        public String unit_address { get; set; }

        public String name { get; set; }

        public Int64? divisionid { get; set; }


    }
}
