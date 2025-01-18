
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_packing_list_DTO
    {

        public Int64? tran_packing_list_id { get; set; }

        public String packing_list_no { get; set; }

        public DateTime? packing_list_date { get; set; }
        public Int64 total_rows { get; set; }
        public String note { get; set; }

        public Int64? is_draft { get; set; }

        public DateTime? draft_date { get; set; }

        public Int64? is_submitted { get; set; }

        public DateTime? submitted_date { get; set; }

        public Int64? submitted_by { get; set; }

        public Int64? is_approved { get; set; }

        public DateTime? approved_date { get; set; }

        public Int64? approved_by { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        public JArray? tran_packing_list_details_json { get; set; }


    }
    public class rpc_proc_sp_get_data_tran_packing_list_by_id_DTO
    {

        public String packing_list_no { get; set; }

        public DateTime? packing_list_date { get; set; }

        public String note { get; set; }

        public String packing_details { get; set; }
        public Int64? is_draft { get; set; }
        public Int64? is_submitted { get; set; }
        public Int64? is_approved { get; set; }

    }
}
