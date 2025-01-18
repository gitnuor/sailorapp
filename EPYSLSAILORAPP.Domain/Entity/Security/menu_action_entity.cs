
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ 
	[System.ComponentModel.DataAnnotations.Schema.Table("menu_action")]

		public class menu_action_entity : DapperExt
		{

 			[Key]
			public Int64? menu_action_id  { get; set;}

			public Int64? menu_id  { get; set;}

			public String action_url  { get; set;}

			public String method_name  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
