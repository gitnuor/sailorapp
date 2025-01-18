
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_tran_final_inspection_DTO 
		{

			public Int64? tran_final_inspection_id  { get; set;}

			public String final_inspection_no  { get; set;}

			public DateTime? final_inspection_date  { get; set;}

			public Int64? finishing_production_process_id  { get; set;}

			public Int64? techpack_id  { get; set;}

			public String note  { get; set;}

			public Int64? is_draft  { get; set;}

			public Int64? is_submitted  { get; set;}

			public Int64? submitted_by  { get; set;}

			public DateTime? submitted_date  { get; set;}

			public Int64? is_approved  { get; set;}

			public Int64? approved_by  { get; set;}

			public DateTime? approved_date  { get; set;}

        public Int64 total_rows { get; set; }
    }
}
