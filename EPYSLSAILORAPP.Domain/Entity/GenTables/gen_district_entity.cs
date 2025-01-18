
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("gen_district")]

		public class gen_district_entity : DapperExt
		{

 			[Key]
			public Int64 district_id  { get; set;}

 			[Column("name")]
			public string? name  { get; set;}

 			[Column("divisionid")]
			public Int64? divisionid  { get; set;}


		}
}
