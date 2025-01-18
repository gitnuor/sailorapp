
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

	public class tran_service_work_order_DTO : BaseEntity
	{

		//[Required]
		[Column("service_work_order_id")]
		public Int64 service_work_order_id { get; set; }

		//[Required]
		[Column("service_work_order_no")]
		public string service_work_order_no { get; set; }

		[Column("service_work_date")]
		public DateTime? service_work_date { get; set; }

		[Column("gen_process_master_id")]
		public Int64? gen_process_master_id { get; set; }

		[Column("techpack_id")]
		public Int64? techpack_id { get; set; }

		[Column("tran_tech_pack_embellishment_info_id")]
		public Int64? tran_tech_pack_embellishment_info_id { get; set; }

		[Column("delivery_date")]
		public DateTime? delivery_date { get; set; }

		[Column("delivery_unit_id")]
		public Int64? delivery_unit_id { get; set; }

		[Column("delivery_unit_address")]
		public string delivery_unit_address { get; set; }

		[Column("remarks")]
		public string? remarks { get; set; }

		[Column("supplier_id")]
		public Int64? supplier_id { get; set; }

		[Column("supplier_address")]
		public string? supplier_address { get; set; }

		[Column("supplier_concern_person")]
		public string? supplier_concern_person { get; set; }

		[Column("supplier_referrence")]
		public string? supplier_referrence { get; set; }

		public string? terms_condition_list { get; set; }

		public JArray? test_requirements_list { get; set; }

		public JArray? work_order_detail_list { get; set; }

		[Column("is_submitted")]
		public Int64? is_submitted { get; set; }

		[Column("submitted_by")]
		public Int64? submitted_by { get; set; }

		[Column("is_approved")]
		public Int64? is_approved { get; set; }

		[Column("approved_by")]
		public Int64? approved_by { get; set; }

		[Column("approve_date")]
		public DateTime? approve_date { get; set; }

		[Column("approve_remarks")]
		public string? approve_remarks { get; set; }

		[Column("added_by")]
		public Int64 added_by { get; set; }

		[Column("date_added")]
		public DateTime date_added { get; set; }

		[Column("updated_by")]
		public Int64? updated_by { get; set; }

		[Column("date_updated")]
		public DateTime? date_updated { get; set; }

		[Column("fiscal_year_id")]
		public Int64? fiscal_year_id { get; set; }

		[Column("event_id")]
		public Int64? event_id { get; set; }

		public List<tran_service_work_order_detail_DTO> details { get; set; }
		public string supplier_name { get; set; }

		public string techpack_number { get; set; }
		public string tran_service_work_order_detail_list { get; set; }
		public string unit_name { get; set; }
		public string unit_address { get; set; }
		public string gen_unit_id { get; set; }
		public string name { get; set; }
		public string process_name { get; set; }
		public Int64? embellish_delivery_challan_id { get; set; }
		public string embellish_delivery_challan_no { get; set; }
		public decimal order_quantity { get; set; }
        public Int64 service_work_order_id1 { get; set; }

        public List<TermConditionDetail> terms_conditions_list { get; set; }
        public List<TermConditionGrouped> terms_and_conditions_list { get; set; }

       

    }

	public class tran_cutting_batch_wise_DTO
	{
		public long tran_cutting_color_id { get; set; }
		public long tran_cutting_id { get; set; }
		public string color_code { get; set; }
		public long allocated_unit_id { get; set; }
		public string allocated_unit { get; set; }
		public long total_quantity { get; set; }
		public DateTime delivery_date { get; set; }
		public long total_cut { get; set; }
		public string remarks { get; set; }
		public long added_by { get; set; }
		public DateTime date_added { get; set; }
		public long updated_by { get; set; }
		public DateTime date_updated { get; set; }

		public long tran_cutting_color_batch_id { get; set; }
		public string techpack_number { get; set; }
		public string batch_no { get; set; }

		public List<tran_cutting_color_batch_DTO> batches { get; set; }
	}

	public class tran_bundle_DTO
	{
		public string batch_no { get; set; }
		public long gen_garment_part_id { get; set; }
		public string bundle_number { get; set; }
		public string bundle_barcode { get; set; }
		public long bundle_quantity { get; set; }
		public string tran_cutting_color_batch_id { get; set; }
		public string color_code { get; set; }
		public string garment_part_name { get; set; }
		public string style_size { get; set; }
		public string tran_cutting_color_batch_garment_part_bundle_id { get; set; }
	}

	public class rpc_proc_sp_get_colors_by_work_order_DTO
	{
		public String color_code { get; set; }
	}
}
