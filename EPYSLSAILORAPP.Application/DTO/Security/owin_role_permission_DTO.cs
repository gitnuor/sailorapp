
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class owin_role_permission_DTO : BaseEntity
		{

			public Int64? role_permission_id  { get; set;}

 			[Required]
			public Int64? role_id  { get; set;}

 			[Required]
			public Int64? menu_action_id  { get; set;}

 			[Required]
			public Int64? menu_id  { get; set;}

 			[Required]
			public Boolean? is_permitted { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
