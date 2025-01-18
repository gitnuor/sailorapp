
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_color_DTO : BaseEntity
		{

			[Required]
 			[Column("gen_color_id")]
			public Int64 gen_color_id  { get; set;}

 			[Required]
 			[Column("color_name")]
			public string color_name  { get; set;}

 			[Required]
 			[Column("color_code")]
			public string color_code  { get; set;}

			public Int64? added_by { get; set; }

			//[Column("added_by")]
			//public Int64 added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}


		}
}
