
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_finishing_production_process_details_DTO : BaseEntity
		{

			//[Required]
 			[Column("tran_finishing_production_process_details_id")]
			public Int64 tran_finishing_production_process_details_id  { get; set;}

			//[Required]
 			[Column("tran_finishing_production_process_id")]
			public Int64 tran_finishing_production_process_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("style_product_size_group_detid")]
			public Int64? style_product_size_group_detid  { get; set;}

 			[Column("size_name")]
			public string? size_name  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("finish_receive_qty")]
			public Int64? finish_receive_qty  { get; set;}

 			[Column("production_quantity")]
			public Int64? production_quantity  { get; set;}

 			[Column("bal_qty")]
			public Int64? bal_qty  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

			public List<tran_finishing_production_process_DTO> TranFinishingProductionProcess_List {get; set;}


		}
}
