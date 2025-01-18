
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_proc_sp_get_data_tran_fabric_allocation_req_details_DTO 
		{

			public Int64? tran_fabric_allocation_req_id  { get; set;}

			public String allocation_number  { get; set;}

			public DateTime? allocation_date  { get; set;}

			public String remarks  { get; set;}

			public String detl_list  { get; set;}


		}
}
