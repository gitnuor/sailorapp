
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_gen_season_event_config_DTO 
		{

			public Int64? event_id  { get; set;}

			public Int64? season_id  { get; set;}

			public Int64? fiscal_year_id  { get; set;}

			public DateTime? start_date  { get; set;}

			public Int64? start_month_id  { get; set;}

			public Int64? end_month_id  { get; set;}

			public String event_title  { get; set;}

			public Boolean? is_active  { get; set;}

			public Int64? event_sequence  { get; set;}

			public DateTime? end_date  { get; set;}


		}
}
