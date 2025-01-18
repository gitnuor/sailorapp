
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_sewing_input_details_DTO : BaseEntity
		{

			[Required]
 			[Column("tran_sewing_input_details_id")]
			public Int64 tran_sewing_input_details_id  { get; set;}

 			[Column("tran_sewing_input_id")]
			public Int64? tran_sewing_input_id  { get; set;}

 			[Column("tran_sewing_allocation_plan_id")]
			public Int64? tran_sewing_allocation_plan_id  { get; set;}

 			[Column("production_line_id")]
			public Int64? production_line_id  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("style_product_size_group_detid")]
			public Int64? style_product_size_group_detid  { get; set;}

 			[Column("batch_id")]
			public Int64? batch_id  { get; set;}

 			[Column("allocated_quantity")]
			public Int64? allocated_quantity  { get; set;}
			public Int64? input_quantity { get; set; }





    }
}
