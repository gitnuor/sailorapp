
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_sp_get_tran_va_plan_summary_DTO 
		{

			public Int64? tran_va_plan_id  { get; set;}

			public String year_name  { get; set;}

			public Int64? fiscal_year_id  { get; set;}

			public Decimal? yearly_gross_sales  { get; set;}

			public Decimal? yearly_total_mrp  { get; set;}

			public Decimal? yearly_total_cpu  { get; set;}

			public Int64? yearly_total_quantity  { get; set;}

			public Int64? range_plan_id  { get; set;}

			public Int64? tran_bp_year_id  { get; set;}

			public String remarks  { get; set;}

			public Boolean? is_submitted  { get; set;}

			public Boolean? is_approved  { get; set;}

			public Int64? approved_by  { get; set;}

			public DateTime? approve_date  { get; set;}

			public String approve_remarks  { get; set;}


		}
}
