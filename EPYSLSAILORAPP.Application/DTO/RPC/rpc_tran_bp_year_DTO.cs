
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_bp_year_DTO
    {

        public Int64? tran_bp_year_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? year_no { get; set; }

        public Decimal? gross_sales { get; set; }

        public Boolean? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public String remarks { get; set; }

        public Boolean? is_submitted { get; set; }

        public Boolean? is_current { get; set; }

        public Decimal? yearly_gross_discount { get; set; }

        public Decimal? yearly_gross_net_amount { get; set; }

        public Decimal? due_amount { get; set; }

        public String year_name { get; set; }

        public Int64? total_rows { get; set; }
        public Int64? total_quantity { get; set; }
        public Int64? total_mrp { get; set; }
        public Int64? total_cpu { get; set; }
    }
}
