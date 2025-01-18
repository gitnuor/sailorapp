
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_email_template_category_dto : BaseEntity
		{

			[Required]
 			[Column("gen_email_template_category_id")]
			public Int64 gen_email_template_category_id  { get; set;}

 			[Column("category_name")]
			public string? category_name  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}


		}
}
