
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("gen_season_event_config")]

		public class gen_season_event_config_entity : DapperExt
		{

 			[Key]
			public Int64 event_id  { get; set;}

 			[Column("season_id")]
			public Int64? season_id  { get; set;}

 			[Column("fiscal_year_id")]
			public Int64? fiscal_year_id  { get; set;}

 			[Column("start_date")]
			public DateTime start_date  { get; set;}

 			[Column("start_month_id")]
			public Int64 start_month_id  { get; set;}

 			[Column("end_month_id")]
			public Int64 end_month_id  { get; set;}

 			[Column("added_by")]
			public Int64 added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime date_added  { get; set;}

 			[Column("event_title")]
			public string? event_title  { get; set;}

 			[Column("is_active")]
			public Boolean? is_active  { get; set;}

 			[Column("event_sequence")]
			public Int64? event_sequence  { get; set;}

 			[Column("end_date")]
			public DateTime? end_date  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}


		}
}
