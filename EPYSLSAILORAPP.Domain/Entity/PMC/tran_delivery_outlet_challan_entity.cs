
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_delivery_outlet_challan")]

		public class tran_delivery_outlet_challan_entity : DapperExt
		{

 			[Key]
			public Int64 tran_delivery_outlet_challan_id  { get; set;}

 			[Column("delivery_outlet_challan_no")]
			public string? delivery_outlet_challan_no  { get; set;}

 			[Column("delivery_outlet_challan_date")]
			public DateTime? delivery_outlet_challan_date  { get; set;}

 			[Column("del_challan_no")]
			public Int64? del_challan_no  { get; set;}

 			[Column("del_challan_date")]
			public DateTime? del_challan_date  { get; set;}

 			[Column("delivery_from")]
			public string? delivery_from  { get; set;}

 			[Column("delivery_to")]
			public Int64? delivery_to  { get; set;}

 			[Column("delivery_address")]
			public string? delivery_address  { get; set;}

 			[Column("transport_type")]
			public Int64? transport_type  { get; set;}

 			[Column("transport_number")]
			public string? transport_number  { get; set;}

 			[Column("driver_name")]
			public string? driver_name  { get; set;}

 			[Column("driver_contact")]
			public string? driver_contact  { get; set;}

 			[Column("fiscal_year_id")]
			public Int64? fiscal_year_id  { get; set;}

 			[Column("event_id")]
			public Int64? event_id  { get; set;}

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

 			[Column("tran_delivery_outlet_challan_id_detail_json")]
			public String? tran_delivery_outlet_challan_id_detail_json  { get; set;}

 			[Column("remarks")]
			public string? remarks  { get; set;}


		}
}
