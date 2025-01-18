
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

	public class tran_mcd_requisition_slip_detail_DTO : BaseEntity
	{

		[Required]
		[Column("requisition_slip_detail_id")]
		public Int64? requisition_slip_detail_id { get; set; }

		[Column("requisition_slip_id")]
		public Int64? requisition_slip_id { get; set; }

		[Column("gen_item_master_id")]
		public Int64? gen_item_master_id { get; set; }

		[Column("measurement_unit_detail_id")]
		public Int64? measurement_unit_detail_id { get; set; }

		[Column("measurement_unit")]
		public string? measurement_unit { get; set; }

		[Column("available_stock_quantity")]
		public Decimal? available_stock_quantity { get; set; }

		[Column("booking_quantity")]
		public Decimal? booking_quantity { get; set; }

		[Column("gen_item_structure_group_sub_id")]
		public Int64? gen_item_structure_group_sub_id { get; set; }

		[Column("requisition_quantity")]
		public Decimal? requisition_quantity { get; set; }

		[Column("mcd_no")]
		public string? mcd_no { get; set; }

		[Column("remarks")]
		public string? remarks { get; set; }

		[Column("tran_type")]
		public Int64? tran_type { get; set; }

		[Column("added_by")]
		public Int64? added_by { get; set; }

		[Column("date_added")]
		public DateTime? date_added { get; set; }

		[Column("updated_by")]
		public Int64? updated_by { get; set; }

		[Column("date_updated")]
		public DateTime? date_updated { get; set; }

		public string  gen_item_master {get;set;}
        public String color_code { get; set; }


    }
}
