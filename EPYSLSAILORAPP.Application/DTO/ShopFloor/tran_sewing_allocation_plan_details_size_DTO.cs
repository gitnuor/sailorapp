
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_sewing_allocation_plan_details_size_DTO : BaseEntity
		{

			[Required]
 			[Column("tran_sewing_allocation_plan_details_size_id")]
			public Int64 tran_sewing_allocation_plan_details_size_id  { get; set;}

			[Required]
 			[Column("tran_sewing_allocation_plan_details_id")]
			public Int64 tran_sewing_allocation_plan_details_id  { get; set;}

			[Required]
 			[Column("tran_sewing_allocation_plan_id")]
			public Int64 tran_sewing_allocation_plan_id  { get; set;}

 			[Column("production_line_id")]
			public Int64? production_line_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("batch_id")]
			public Int64? batch_id  { get; set;}

 			[Column("style_product_size_group_detid")]
			public Int64? style_product_size_group_detid  { get; set;}

 			[Column("size_name")]
			public string? size_name  { get; set;}

 			[Column("cutting_quantity")]
			public Int64? cutting_quantity  { get; set;}

 			[Column("allocated_quantity")]
			public Int64? allocated_quantity  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}


		}
}
