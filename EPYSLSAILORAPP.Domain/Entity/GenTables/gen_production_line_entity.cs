
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("gen_production_line")]

		public class gen_production_line_entity : DapperExt
		{

 			[Key]
			public Int64 production_line_id  { get; set;}

 			[Column("floor_id")]
			public Int64 floor_id  { get; set;}

 			[Column("line_desc")]
			public string? line_desc  { get; set;}

 			[Column("after_wash_status")]
			public Int64? after_wash_status  { get; set;}

 			[Column("auto_sewing_output")]
			public Int64? auto_sewing_output  { get; set;}

 			[Column("seq_no")]
			public Int64 seq_no  { get; set;}

 			[Column("auto_iron_output")]
			public Int64? auto_iron_output  { get; set;}

 			[Column("auto_hang_tag_output")]
			public Int64? auto_hang_tag_output  { get; set;}

 			[Column("auto_folding_output")]
			public Int64? auto_folding_output  { get; set;}

 			[Column("auto_poly_output")]
			public Int64? auto_poly_output  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("date_added")]
			public DateTime date_added  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

 			[Column("line_name")]
			public string? line_name  { get; set;}


		}
}
