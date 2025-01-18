
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_tran_mcd_requisition_slip_for_issueLanding_DTO
    {

			public Int64? requisition_slip_id  { get; set;}
			public Int64 total_rows { get; set; }

        public String requisition_slip_no  { get; set;}

			public DateTime? requisition_date  { get; set;}

			public Int64? requisition_by  { get; set;}

			public String techpack_number  { get; set;}

			public String name  { get; set;}


		}
}
