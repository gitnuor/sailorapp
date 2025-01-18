
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("gen_term_and_conditions")]

		public class gen_term_and_conditions_entity : DapperExt
		{

 			[Key]
			public Int64 gen_term_and_conditions_id  { get; set;}

 			[Column("term_condition_name")]
			public string term_condition_name  { get; set;}

 			[Column("description")]
			public string? description  { get; set;}

 			[Column("created_by")]
			public Int64? created_by  { get; set;}

 			[Column("created_date")]
			public DateTime? created_date  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("updated_date")]
			public DateTime? updated_date  { get; set;}

 			[Column("menu_id")]
			public Int64 menu_id  { get; set;}


		}
}
