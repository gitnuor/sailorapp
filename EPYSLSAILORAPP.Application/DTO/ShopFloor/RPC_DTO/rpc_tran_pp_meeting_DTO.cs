
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_pp_meeting_DTO
    {

        public Int64? tran_pp_meeting_id { get; set; }
        public Int64? tran_cutting_id { get; set; }

        public Int64? techpack_id { get; set; }

        public String remarks { get; set; }

        public Int64? meeting_conducted_by { get; set; }

        public DateTime? meeting_conducted_date { get; set; }

        public Int64? event_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public String techpack_number { get; set; }

        public String conducted_by { get; set; }
        public String event_title { get; set; }


        public Int64 total_rows { get; set; }

    }



}
