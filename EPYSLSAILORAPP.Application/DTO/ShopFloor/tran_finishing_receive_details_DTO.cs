
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_finishing_receive_details_DTO : BaseEntity
		{

			//[Required]
 			[Column("tran_finish_receive_details_id")]
			public Int64 tran_finish_receive_details_id  { get; set;}

 			[Column("tran_finish_receive_id")]
			public Int64? tran_finish_receive_id  { get; set;}

 			[Column("tran_sewing_output_id")]
			public Int64? tran_sewing_output_id  { get; set;}

 			[Column("tran_sewing_allocation_plan_id")]
			public Int64? tran_sewing_allocation_plan_id  { get; set;}

 			[Column("style_product_size_group_detid")]
			public Int64? style_product_size_group_detid { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("size_name")]
			public string? size_name  { get; set;}

 			[Column("color_quantity")]
			public Int64? color_quantity  { get; set;}

 			[Column("qc_pass_quantity")]
			public Int64? qc_pass_quantity  { get; set;}

 			[Column("finish_receive_qty")]
			public Int64? finish_receive_qty  { get; set;}

			public List<tran_finishing_receive_DTO> TranFinishingReceive_List {get; set;}

		}


}
