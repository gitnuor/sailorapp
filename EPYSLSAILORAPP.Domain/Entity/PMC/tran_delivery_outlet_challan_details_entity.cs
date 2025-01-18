
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_delivery_outlet_challan_details")]

		public class tran_delivery_outlet_challan_details_entity : DapperExt
		{

 			[Key]
			public Int64 tran_delivery_outlet_challan_details_id  { get; set;}

 			[Column("tran_delivery_outlet_challan_id")]
			public Int64? tran_delivery_outlet_challan_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("style_item_product_id")]
			public Int64? style_item_product_id  { get; set;}

 			[Column("techpack_label")]
			public string? techpack_label  { get; set;}

 			[Column("style_code")]
			public string? style_code  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("style_product_size_group_detid")]
			public Int64? style_product_size_group_detid  { get; set;}

 			[Column("style_product_size")]
			public string? style_product_size  { get; set;}

 			[Column("barcode")]
			public string? barcode  { get; set;}

 			[Column("style_product_unit")]
			public string? style_product_unit  { get; set;}

 			[Column("mrp")]
			public Decimal? mrp  { get; set;}

 			[Column("allocate_quantity")]
			public Int64? allocate_quantity  { get; set;}

 			[Column("challan_quantity")]
			public Int64? challan_quantity  { get; set;}

 			[Column("total_value")]
			public Int64? total_value  { get; set;}

 			[Column("remarks")]
			public string? remarks  { get; set;}


		}
}
