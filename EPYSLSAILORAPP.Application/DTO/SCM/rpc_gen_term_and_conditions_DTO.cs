
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_gen_term_and_conditions_DTO 
		{

			public Int64? gen_term_and_conditions_id  { get; set;}

			public String term_condition_name  { get; set;}

			public String description  { get; set;}

			public Int64? created_by  { get; set;}

			public DateTime? created_date  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? updated_date  { get; set;}

			public Int64? menu_id  { get; set;}


		}
}
