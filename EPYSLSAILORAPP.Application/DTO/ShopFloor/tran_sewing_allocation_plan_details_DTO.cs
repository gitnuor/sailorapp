
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_sewing_allocation_plan_details_DTO : BaseEntity
		{

			[Required]
 			[Column("tran_sewing_allocation_plan_details_id")]
			public Int64 tran_sewing_allocation_plan_details_id  { get; set;}

			[Required]
 			[Column("tran_sewing_allocation_plan_id")]
			public Int64 tran_sewing_allocation_plan_id  { get; set;}

 			[Column("production_line_id")]
			public Int64? production_line_id  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("batch_id")]
			public Int64? batch_id  { get; set;}

 			[Column("batch_no")]
			public string? batch_no  { get; set;}

 			[Column("total_allocated_quantity")]
			public Int64? total_allocated_quantity  { get; set;}

			public JArray? part_details  { get; set;}

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
