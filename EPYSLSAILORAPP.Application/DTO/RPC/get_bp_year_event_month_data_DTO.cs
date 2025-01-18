
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_get_bp_year_event_month_data_DTO 
		{

			public Int64? tran_bp_event_month_id  { get; set;}

			public Int64? tran_bp_event_id  { get; set;}

			public Int64? month_id  { get; set;}

			public Decimal? monthly_gross_sales  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
