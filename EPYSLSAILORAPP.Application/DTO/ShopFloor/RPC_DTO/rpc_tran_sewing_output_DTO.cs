
using Newtonsoft.Json.Linq;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_sewing_output_DTO
    {

        public Int64? tran_sewing_output_id { get; set; }

        public Int64? tran_sewing_input_id { get; set; }

        public Int64? tran_sewing_allocation_plan_id { get; set; }
        public string sewing_allocation_number { get; set; }
        public string techpack_number { get; set; }

        public Int64? techpack_id { get; set; }

        public DateTime? output_date { get; set; }

        public String note { get; set; }

        public String hour_output { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }
        public Int64 total_rows { get; set; }
        public JArray? tran_sewing_output_details { get; set; }


    }
}
