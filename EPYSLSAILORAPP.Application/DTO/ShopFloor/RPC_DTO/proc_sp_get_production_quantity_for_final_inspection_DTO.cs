
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_proc_sp_get_production_quantity_for_final_inspection_DTO 
		{

			public String color_code  { get; set;}

			public Int64? production_quantity  { get; set;}

			public Int64? already_inspected_quantity  { get; set;}


		}
}
