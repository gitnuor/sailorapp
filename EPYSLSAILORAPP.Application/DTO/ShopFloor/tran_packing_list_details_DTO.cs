
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_packing_list_details_DTO : BaseEntity
		{

			[Required]
 			[Column("tran_packing_list_details_id")]
			public Int64 tran_packing_list_details_id  { get; set;}

 			[Column("tran_packing_list_id")]
			public Int64? tran_packing_list_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}
			public Int64? style_item_product_id { get; set; }

			 [Column("style_code")]
			public string? style_code  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}
			public string? techpack_number { get; set; }
			public string? style_item_product_name { get; set; }

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
			public string? barcode  { get; set; }

			public Decimal? final_mrp { get; set; }
			public long? already_receive { get; set; }
		

    }
}
