
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("gen_division")]

		public class gen_division_entity : DapperExt
		{

 			[Column("division_id")]
			public Int64 division_id  { get; set;}

 			[Column("name")]
			public string? name  { get; set;}


		}
}
