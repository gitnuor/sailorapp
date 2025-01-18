
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_bank_DTO : BaseEntity
		{

			public Int64? gen_bank_id  { get; set;}

 			[Required]
			public String bank_name  { get; set;}

 			[Required]
			public String bank_short_name  { get; set;}

 			[Required]
			public Boolean? is_used  { get; set;}

 			[Required]
			public Boolean? is_local  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
