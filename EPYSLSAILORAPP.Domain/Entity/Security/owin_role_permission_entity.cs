
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ 
	[System.ComponentModel.DataAnnotations.Schema.Table("owin_role_permission")]
		public class owin_role_permission_entity : DapperExt
		{

 			[Key]
			public Int64? role_permission_id  { get; set;}

			public Int64? role_id  { get; set;}

			public Int64? menu_action_id  { get; set;}

			public Int64? menu_id  { get; set;}

			public Boolean? is_permitted { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
