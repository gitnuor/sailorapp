
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_packing_list")]

		public class tran_packing_list_entity : DapperExt
		{

 			[Key]
			public Int64 tran_packing_list_id  { get; set;}

 			[Column("packing_list_no")]
			public string? packing_list_no  { get; set;}

 			[Column("packing_list_date")]
			public DateTime? packing_list_date  { get; set;}

 			[Column("note")]
			public string? note  { get; set;}

 			[Column("is_draft")]
			public Int64? is_draft  { get; set;}

 			[Column("draft_date")]
			public DateTime? draft_date  { get; set;}

 			[Column("is_submitted")]
			public Int64? is_submitted  { get; set;}

 			[Column("submitted_date")]
			public DateTime? submitted_date  { get; set;}

 			[Column("submitted_by")]
			public Int64? submitted_by  { get; set;}

 			[Column("is_approved")]
			public Int64? is_approved  { get; set;}

 			[Column("approved_date")]
			public DateTime? approved_date  { get; set;}

 			[Column("approved_by")]
			public Int64? approved_by  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

 			[Column("fiscal_year_id")]
			public Int64? fiscal_year_id  { get; set;}

 			[Column("event_id")]
			public Int64? event_id  { get; set;}
			public long total_rows { get; set; }

        [Column("tran_packing_list_details_json")]
			public String? tran_packing_list_details_json  { get; set;}


		}
}
