
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_packing_list_details")]

		public class tran_packing_list_details_entity : DapperExt
		{

 			[Key]
			public Int64 tran_packing_list_details_id  { get; set;}

 			[Column("tran_packing_list_id")]
			public Int64? tran_packing_list_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("style_code")]
			public string? style_code  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("style_product_size_group_detid")]
			public Int64? style_product_size_group_detid  { get; set;}

 			[Column("style_product_size")]
			public string? style_product_size  { get; set;}

 			[Column("style_product_unit")]
			public string? style_product_unit  { get; set;}

 			[Column("order_quantity")]
			public Int64? order_quantity  { get; set;}

 			[Column("packing_quantity")]
			public Int64? packing_quantity  { get; set;}

 			[Column("note")]
			public string? note  { get; set;}


		}
}
