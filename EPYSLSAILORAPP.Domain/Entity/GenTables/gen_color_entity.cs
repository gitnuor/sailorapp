
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("gen_color")]

		public class gen_color_entity : DapperExt
		{

 			[Key]
			public Int64 gen_color_id  { get; set;}

 			[Column("color_name")]
			public string color_name  { get; set;}

 			[Column("color_code")]
			public string color_code  { get; set;}

 			[Column("added_by")]
			public Int64 added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}


		}
}
