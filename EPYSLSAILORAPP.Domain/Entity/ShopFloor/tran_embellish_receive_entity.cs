
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_embellish_receive")]

		public class tran_embellish_receive_entity : DapperExt
		{

 			[Key]
			public Int64 embellish_receive_id  { get; set;}

 			[Column("embellish_receive_no")]
			public string embellish_receive_no  { get; set;}

 			[Column("embellish_receive_date")]
			public DateTime? embellish_receive_date  { get; set;}

 			[Column("challan_no_receive")]
			public string? challan_no_receive  { get; set;}

 			[Column("challan_receive_date")]
			public DateTime? challan_receive_date  { get; set;}

 			[Column("note")]
			public string? note  { get; set;}

 			[Column("supplier_id")]
			public Int64? supplier_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("service_work_order_id")]
			public Int64? service_work_order_id  { get; set;}

 			[Column("gen_process_master_id")]
			public Int64? gen_process_master_id  { get; set;}

 			[Column("total_qty")]
			public Decimal? total_qty  { get; set;}

 			[Column("total_no_bundle")]
			public Int64? total_no_bundle  { get; set;}

 			[Column("embellish_receive_detail_list")]
			public String? embellish_receive_detail_list  { get; set;}

 			[Column("is_submitted")]
			public Int64? is_submitted  { get; set;}

 			[Column("submitted_by")]
			public Int64? submitted_by  { get; set;}

 			[Column("is_approved")]
			public Int64? is_approved  { get; set;}

 			[Column("approved_by")]
			public Int64? approved_by  { get; set;}

 			[Column("approve_date")]
			public DateTime? approve_date  { get; set;}

 			[Column("approve_remarks")]
			public string? approve_remarks  { get; set;}

 			[Column("added_by")]
			public Int64 added_by  { get; set;}

 			[Column("date_added")]
			public DateTime date_added  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

 			[Column("fiscal_year_id")]
			public Int64? fiscal_year_id  { get; set;}

 			[Column("event_id")]
			public Int64? event_id  { get; set;}


		}
}
