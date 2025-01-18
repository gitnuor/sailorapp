
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_sewing_output_details")]

		public class tran_sewing_output_details_entity : DapperExt
		{

 			[Key]
			public Int64 tran_sewing_output_details_id  { get; set;}

 			[Column("tran_sewing_output_id")]
			public Int64? tran_sewing_output_id  { get; set;}

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

 			[Column("input_quantity")]
			public Int64? input_quantity  { get; set;}

 			[Column("transfer_quantity")]
			public Int64? transfer_quantity  { get; set;}

 			[Column("production_quantity")]
			public Int64? production_quantity  { get; set;}

 			[Column("qc_pass_quantity")]
			public Int64? qc_pass_quantity  { get; set;}

 			[Column("qc_failed_quantity")]
			public Int64? qc_failed_quantity  { get; set;}

 			[Column("transfer_out")]
			public Int64? transfer_out  { get; set;}

 			[Column("qc_parameter_json")]
			public String? qc_parameter_json  { get; set;}


		}
}
