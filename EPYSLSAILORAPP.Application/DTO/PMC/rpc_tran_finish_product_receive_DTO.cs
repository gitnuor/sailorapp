
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_tran_finish_product_receive_DTO 
		{

			public Int64? tran_finish_product_receive_id  { get; set;}

			public Int64? tran_packing_list_id  { get; set;}

			public String finish_product_receive_no  { get; set;}

			public DateTime? finish_product_receive_date  { get; set;}

			public String vehicle_type  { get; set;}

			public String vehicle_number  { get; set;}

			public String driver_name  { get; set;}

			public String driver_contact_no  { get; set;}

			public String note  { get; set;}

			public Int64? fiscal_year_id  { get; set;}

			public Int64? event_id  { get; set;}

			public JArray? tran_finish_product_receive_details_json  { get; set;}


		}
}
