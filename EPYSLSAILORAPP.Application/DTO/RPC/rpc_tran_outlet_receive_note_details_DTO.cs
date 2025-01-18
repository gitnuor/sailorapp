
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_tran_outlet_receive_note_details_DTO 
		{

			public Int64? tran_outlet_receive_note_details_id  { get; set;}

			public Int64? tran_outlet_receive_note_id  { get; set;}

			public Int64? techpack_id  { get; set;}

			public Int64? style_item_product_id  { get; set;}

			public String techpack_label  { get; set;}

			public String style_code  { get; set;}

			public String color_code  { get; set;}

			public Int64? style_product_size_group_detid  { get; set;}

			public String style_product_size  { get; set;}

			public String style_product_unit  { get; set;}

			public Decimal? mrp  { get; set;}

			public Int64? distribute_size_quantity  { get; set;}

			public Int64? challan_quantity  { get; set;}

			public Int64? receive_quantity  { get; set;}

			public Int64? reject_quantity  { get; set;}

			public String remarks  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
