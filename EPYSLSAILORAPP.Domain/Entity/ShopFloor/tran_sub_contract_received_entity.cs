
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_sub_contract_received")]

		public class tran_sub_contract_received_entity : DapperExt
		{

 			[Key]
			public Int64 tran_sub_contract_received_id  { get; set;}

 			[Column("tran_sub_contract_received_date")]
			public DateTime? tran_sub_contract_received_date  { get; set;}

 			[Column("tran_sub_contract_request_id")]
			public Int64? tran_sub_contract_request_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("techpack_date")]
			public DateTime? techpack_date  { get; set;}

 			[Column("style_item_product_category")]
			public string? style_item_product_category  { get; set;}

 			[Column("style_item_product_id")]
			public Int64? style_item_product_id  { get; set;}

 			[Column("merchandiser_name")]
			public string? merchandiser_name  { get; set;}

 			[Column("designer_name")]
			public string? designer_name  { get; set;}

 			[Column("supplier_id")]
			public Int64? supplier_id  { get; set;}

 			[Column("supplier_concern_person")]
			public string? supplier_concern_person  { get; set;}

 			[Column("supplier_address")]
			public string? supplier_address  { get; set;}

 			[Column("fiscal_year_id")]
			public Int64? fiscal_year_id  { get; set;}

 			[Column("event_id")]
			public Int64? event_id  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

 			[Column("is_submitted")]
			public Int64? is_submitted  { get; set;}

 			[Column("submitted_by")]
			public Int64? submitted_by  { get; set;}

 			[Column("is_approved")]
			public Int64? is_approved  { get; set;}

 			[Column("approved_by")]
			public Int64? approved_by  { get; set;}

 			[Column("tran_sub_contract_received_details")]
			public String? tran_sub_contract_received_details  { get; set;}


		}
}
