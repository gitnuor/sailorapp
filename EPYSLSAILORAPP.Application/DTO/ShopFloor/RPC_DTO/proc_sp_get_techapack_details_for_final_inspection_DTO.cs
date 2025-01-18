
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_sp_get_techapack_details_for_final_inspection_DTO 
		{

			public String style_item_product_name  { get; set;}

			public Int64? style_item_product_id  { get; set;}

			public Int64? userid  { get; set;}

			public String merchandiser_name { get; set;}

			public String teckpack_style_code  { get; set;}

			public DateTime? techpack_date  { get; set;}

			public Int64? event_id  { get; set;}

			public String event_title  { get; set;}

			public String designer_name  { get; set;}

			public  List<SelectListItem> colors { get; set;}	


		}
}
