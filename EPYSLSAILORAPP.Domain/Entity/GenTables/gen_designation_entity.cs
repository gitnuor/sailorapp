
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ 
	[System.ComponentModel.DataAnnotations.Schema.Table("gen_designation")]

		public class gen_designation_entity : DapperExt
		{

 			[Key]
			public Int64 designation_id  { get; set;}

 			[Column("designation_name")]
			public string? designation_name  { get; set;}


		}
}
