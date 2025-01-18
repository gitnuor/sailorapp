
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_tran_outlet_receive_note_DTO 
		{

			public Int64? tran_outlet_receive_note_id  { get; set;}

			public String outlet_receive_no  { get; set;}

			public DateTime? outlet_receive_date  { get; set;}

			public Int64? del_challan_no  { get; set;}

			public DateTime? del_challan_date  { get; set;}

			public String delivery_from  { get; set;}

			public Int64? delivery_to  { get; set;}

			public String delivery_address  { get; set;}

			public Int64? transport_type  { get; set;}

			public String transport_number  { get; set;}

			public String driver_name  { get; set;}

			public String driver_contact  { get; set;}

			public Int64? fiscal_year_id  { get; set;}

			public Int64? event_id  { get; set;}

			public Int64? is_draft  { get; set;}

			public Int64? is_submitted  { get; set;}

			public Int64? submitted_by  { get; set;}

			public DateTime? submitted_date  { get; set;}

			public Int64? is_approved  { get; set;}

			public Int64? approved_by  { get; set;}

			public DateTime? approved_date  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}

			public JArray? tran_outlet_receive_note_detail_json  { get; set;}

			public String barcode  { get; set;}


		}
}
