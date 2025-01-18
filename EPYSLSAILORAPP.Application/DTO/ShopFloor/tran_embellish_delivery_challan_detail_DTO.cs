
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_embellish_delivery_challan_detail_DTO : BaseEntity
		{

			[Required]
 			[Column("embellish_delivery_challan_detail_id")]
			public Int64 embellish_delivery_challan_detail_id  { get; set;}

			[Required]
 			[Column("embellish_delivery_challan_id")]
			public Int64 embellish_delivery_challan_id  { get; set;}

 			[Column("bundle_number")]
			public string? bundle_number  { get; set;}

 			[Column("bundle_barcode")]
			public string? bundle_barcode  { get; set;}

 			[Column("bundle_quantity")]
			public Int64? bundle_quantity  { get; set;}

 			[Column("batch_no")]
			public string? batch_no  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("style_size")]
			public string? style_size  { get; set;}

 			[Column("garment_part_name")]
			public string? garment_part_name  { get; set;}

 			[Column("process_name")]
			public string? process_name  { get; set;}

 			[Column("tran_cutting_color_batch_id")]
			public Int64? tran_cutting_color_batch_id  { get; set;}

 			[Column("tran_cutting_color_batch_garment_part_bundle_id")]
			public Int64? tran_cutting_color_batch_garment_part_bundle_id  { get; set;}

 			[Column("gen_garment_part_id")]
			public Int64? gen_garment_part_id  { get; set;}

 			[Column("gen_process_master_id")]
			public Int64? gen_process_master_id  { get; set;}

 			[Column("added_by")]
			public Int64 added_by  { get; set;}

 			[Column("date_added")]
			public DateTime date_added  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

 			[Column("remarks")]
			public string? remarks  { get; set;}

			public List<tran_embellish_delivery_challan_DTO> TranEmbellishDeliveryChallan_List {get; set;}


		}
}
