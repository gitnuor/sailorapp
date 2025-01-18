
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class menu_action_DTO : BaseEntity
		{

			public Int64? menu_action_id  { get; set;}

			public Int64? menu_id  { get; set;}

 			[Required]
			public String action_url  { get; set;}

 			[Required]
			public String method_name  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
