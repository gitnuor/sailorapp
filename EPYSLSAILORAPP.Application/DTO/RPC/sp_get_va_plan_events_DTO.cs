
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_sp_get_va_plan_events_DTO 
		{

			public Int64? tran_va_plan_event_id  { get; set;}

			public Int64? tran_range_plan_event_id  { get; set;}

			public Int64? range_plan_id  { get; set;}

			public Decimal? yearly_gross_sales  { get; set;}

			public Int64? tran_bp_event_id  { get; set;}

			public Decimal? event_gross_sales  { get; set;}

			public Int64? readygoods_qnty  { get; set;}

			public Decimal? readygoods_value  { get; set;}

			public Int64? event_id  { get; set;}

			public Int64? fiscal_year_id  { get; set;}

			public String event_title  { get; set;}
	

        public Int64? total_range_plan_quantity  { get; set;}

			public Decimal? total_mrp_value  { get; set;}

			public Decimal? total_cpu_value  { get; set;}

			public Boolean? is_finalised  { get; set;}

			public Int64? added_product  { get; set;}


		}
}
