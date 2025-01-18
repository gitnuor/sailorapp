
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_tran_range_plan_summary_DTO
    {

        public Decimal? yearly_gross_sales { get; set; }

        public Decimal? yearly_total_mrp { get; set; }

        public Decimal? yearly_total_cpu { get; set; }

        public Int64? yearly_total_quantity { get; set; }

        public Int64? yearly_total_product { get; set; }

        public Int64? range_plan_id { get; set; }

        public Int64? tran_bp_year_id { get; set; }

        public string remarks { get; set; }

        public Int64? fiscal_year_id { get; set; }  

        public bool? is_approved { get; set; }
        public bool? is_submitted { get; set; }
        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public String year_name { get; set; }


    }
}
