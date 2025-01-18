
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_gen_email_template_DTO 
		{

			public Int64? gen_email_template_id  { get; set;}

			public String email_template_html  { get; set;}

			public Int64? gen_email_template_category_id  { get; set;}


		}
}
