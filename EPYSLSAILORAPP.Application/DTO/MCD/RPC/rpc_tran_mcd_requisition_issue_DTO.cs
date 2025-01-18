
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_mcd_requisition_issue_DTO
    {

        public Int64? tran_mcd_requisition_issue_id { get; set; }
        public Int64 total_rows { get; set; }


        public String issue_no { get; set; }

        public DateTime? date_added { get; set; }

        public String techpack_number { get; set; }

        public String requisition_slip_no { get; set; }
        public String name { get; set; }

    }
}
