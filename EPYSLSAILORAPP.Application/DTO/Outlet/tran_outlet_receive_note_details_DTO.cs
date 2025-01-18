
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_outlet_receive_note_details_DTO : BaseEntity
		{

			//[Required]
 			[Column("tran_outlet_receive_note_details_id")]
			public Int64 tran_outlet_receive_note_details_id  { get; set;}

 			[Column("tran_outlet_receive_note_id")]
			public Int64? tran_outlet_receive_note_id  { get; set;}

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

 			[Column("style_product_unit")]
			public string? style_product_unit  { get; set;}

 			[Column("mrp")]
			public Decimal? mrp  { get; set;}

 			[Column("distribute_size_quantity")]
			public Int64? distribute_size_quantity  { get; set;}

 			[Column("challan_quantity")]
			public Int64? challan_quantity  { get; set;}

 			[Column("receive_quantity")]
			public Int64? receive_quantity  { get; set;}

 			[Column("reject_quantity")]
			public Int64? reject_quantity  { get; set;}

 			[Column("remarks")]
			public string? remarks  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}
        public string? style_item_product_name { get; set; }
        public string? techpack_number { get; set; }
        public string? barcode { get; set; }

        public Decimal? total_value { get; set; }

        public List<tran_outlet_receive_note_DTO> TranOutletReceiveNote_List {get; set;}


		}
}
