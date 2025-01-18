
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_proc_get_filter_items_DTO 
		{

			public String? genfiscalyear_list  { get; set;}

			public String? genseasoneventconfig_list  { get; set;}

			public String? styleitemtype_list  { get; set;}

			public String? styleproducttype_list  { get; set;}

			public String? stylegender_list  { get; set;}

			public String? styleitemorigin_list  { get; set;}


		}
}
