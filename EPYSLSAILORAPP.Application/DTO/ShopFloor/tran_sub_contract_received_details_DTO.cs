
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_sub_contract_received_details_DTO : BaseEntity
		{

			[Required]
 			[Column("tran_sub_contract_received_details_id")]
			public Int64 tran_sub_contract_received_details_id  { get; set;}

			[Required]
 			[Column("tran_sub_contract_received_id")]
			public Int64 tran_sub_contract_received_id  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("color_qty")]
			public Int64? color_qty  { get; set;}

 			[Column("production_process_name")]
			public string? production_process_name  { get; set;}

 			[Column("sub_contract_challan_qty")]
			public Int64? sub_contract_challan_qty  { get; set;}

 			[Column("sub_contract_receive_qty")]
			public Int64? sub_contract_receive_qty  { get; set;}

 			[Column("bal_qty")]
			public Int64? bal_qty  { get; set;}

 			[Column("remarks")]
			public string? remarks  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

			public List<tran_sub_contract_received_DTO> TranSubContractReceived_List {get; set;}


		}
}
