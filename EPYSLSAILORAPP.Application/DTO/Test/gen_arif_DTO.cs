
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_arif_DTO : BaseEntity
		{

			[Required]
 			[Column("gen_arif_id")]
			public Int64?gen_arif_id  { get; set;}

 			[Required]
 			[Column("bank_name")]
			public string?bank_name  { get; set;}

 			[Required]
 			[Column("bank_short_name")]
			public string?bank_short_name  { get; set;}

 			[Required]
 			[Column("is_used")]
			public Boolean? is_used  { get; set;}

 			[Required]
 			[Column("is_local")]
			public Boolean? is_local  { get; set;}

 			[Column("added_by")]
			public Int64?added_by  { get; set;}

 			[Column("updated_by")]
			public Int64 updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime date_updated  { get; set;}

			public JArray arif_details_1  { get; set;}

			public JArray arif_details_2  { get; set;}

 			[Column("unit_id")]
			public Int64 unit_id  { get; set;}

 			[Column("district_id")]
			public Int64 district_id  { get; set;}


		}
}
