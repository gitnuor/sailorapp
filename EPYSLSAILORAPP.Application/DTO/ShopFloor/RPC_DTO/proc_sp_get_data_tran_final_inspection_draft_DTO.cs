
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_proc_sp_get_data_tran_final_inspection_draft_DTO 
		{

			public Int64? tran_final_inspection_id  { get; set;}

			public String final_inspection_no  { get; set;}

			public DateTime? final_inspection_date  { get; set;}

			public Int64? finishing_production_process_id  { get; set;}

			public Int64? techpack_id  { get; set;}

			public String note  { get; set;}

			public Int64? event_id  { get; set;}

			public String event_title  { get; set;}

			public String techpack_number  { get; set;}

			public Int64 total_rows  { get; set;}


		}
}
