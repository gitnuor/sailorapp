
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_embellish_delivery_challan")]

		public class tran_embellish_delivery_challan_entity : DapperExt
		{

 			[Key]
			public Int64 embellish_delivery_challan_id  { get; set;}

 			[Column("embellish_delivery_challan_no")]
			public string embellish_delivery_challan_no  { get; set;}

 			[Column("embellish_delivery_challan_date")]
			public DateTime? embellish_delivery_challan_date  { get; set;}

 			[Column("service_work_order_id")]
			public Int64? service_work_order_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("gen_unit_id")]
			public Int64? gen_unit_id  { get; set;}

 			[Column("unit_address")]
			public string? unit_address  { get; set;}

 			[Column("unit_name")]
			public string? unit_name  { get; set;}

 			[Column("embellish_delivery_challan_detail_list")]
			public JArray? embellish_delivery_challan_detail_list  { get; set;}

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
