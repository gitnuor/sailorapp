
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_item_stock_master_det_DTO : BaseEntity
		{

			[Required]
 			[Column("gen_item_stock_master_det")]
			public Int64 gen_item_stock_master_det  { get; set;}

			[Required]
 			[Column("item_master_id")]
			public Int64 item_master_id  { get; set;}

			[Required]
 			[Column("measurement_unit_detail_id")]
			public Int64 measurement_unit_detail_id  { get; set;}

 			[Column("measurement_unit_detail")]
			public string? measurement_unit_detail  { get; set;}

 			[Required]
 			[Column("opening_quantity")]
			public Decimal opening_quantity  { get; set;}

 			[Required]
 			[Column("total_received_quantity")]
			public Decimal total_received_quantity  { get; set;}

 			[Required]
 			[Column("total_allocated_quantity")]
			public Decimal total_allocated_quantity  { get; set;}

 			[Required]
 			[Column("total_issued_quantity")]
			public Decimal total_issued_quantity  { get; set;}

 			[Required]
 			[Column("total_failed_quantity")]
			public Decimal total_failed_quantity  { get; set;}

 			[Required]
 			[Column("total_floor_return_quantity")]
			public Decimal total_floor_return_quantity  { get; set;}

 			[Required]
 			[Column("total_quarantine_quantity")]
			public Decimal total_quarantine_quantity  { get; set;}

 			[Column("gen_warehouse_floor_rack_id")]
			public Int64? gen_warehouse_floor_rack_id  { get; set;}

 			[Column("gen_warehouse_floor_rack_info")]
			public string? gen_warehouse_floor_rack_info  { get; set;}

 			[Column("table_name")]
			public string? table_name  { get; set;}

 			[Column("primary_key_value")]
			public Int64? primary_key_value  { get; set;}

 			[Column("added_by")]
			public Int64 added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}


		}
}
