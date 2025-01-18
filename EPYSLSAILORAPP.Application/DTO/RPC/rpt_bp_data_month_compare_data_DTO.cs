
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_rpt_bp_data_month_compare_data_DTO
    {

        public String year_name { get; set; }

        public String month_name { get; set; }

        public Int64? month_id { get; set; }

        public Int64? year { get; set; }

        public Decimal? total_gross { get; set; }

        public Decimal? total_gross_discount { get; set; }

        public Decimal? total_gross_net { get; set; }


    }
}
