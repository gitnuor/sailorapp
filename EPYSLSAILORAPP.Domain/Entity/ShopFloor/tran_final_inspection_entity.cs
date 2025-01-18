
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_final_inspection")]

		public class tran_final_inspection_entity : DapperExt
		{

 			[Key]
			public Int64 tran_final_inspection_id  { get; set;}

 			[Column("final_inspection_no")]
			public string? final_inspection_no  { get; set;}

 			[Column("final_inspection_date")]
			public DateTime? final_inspection_date  { get; set;}

 			[Column("finishing_production_process_id")]
			public Int64? finishing_production_process_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("note")]
			public string? note  { get; set;}

 			[Column("is_draft")]
			public Int64? is_draft  { get; set;}

 			[Column("is_submitted")]
			public Int64? is_submitted  { get; set;}

 			[Column("submitted_by")]
			public Int64? submitted_by  { get; set;}

 			[Column("submitted_date")]
			public DateTime? submitted_date  { get; set;}

 			[Column("is_approved")]
			public Int64? is_approved  { get; set;}

 			[Column("approved_by")]
			public Int64? approved_by  { get; set;}

 			[Column("approved_date")]
			public DateTime? approved_date  { get; set;}

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

 			[Column("tran_final_inspection_details")]
			public String? tran_final_inspection_details  { get; set;}


		}
}
