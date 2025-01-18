
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_bank_branch_DTO : BaseEntity
		{

			public Int64? gen_bank_branch_id  { get; set;}

 			[Required]
			public Int32? gen_bank_id  { get; set;}

 			[Required]
			public String branch_name  { get; set;}

 			[Required]
			public String branch_display_name  { get; set;}

			public String branch_address  { get; set;}

			public String current_account  { get; set;}

			public String margin_account  { get; set;}

			public String swift_code  { get; set;}

			public String phone_no  { get; set;}

			public String routing_no  { get; set;}

			public Int32? branch_type_id  { get; set;}

 			[Required]
			public Boolean? is_used  { get; set;}

 			[Required]
			public Boolean? is_trader  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
