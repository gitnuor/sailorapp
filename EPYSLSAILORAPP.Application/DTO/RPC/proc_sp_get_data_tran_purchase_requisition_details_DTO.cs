
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

		public class rpc_proc_sp_get_data_tran_purchase_requisition_details_DTO 
		{

			public Int64? pr_id  { get; set;}

			public String pr_no  { get; set;}

			public DateTime? pr_date  { get; set;}

			public DateTime? delivery_date  { get; set;}

			public Int64? event_id  { get; set;}

			public Int64? techpack_id  { get; set;}

			public Int64? designer_id  { get; set;}

			public Int64? merchandiser_id  { get; set;}

			public Int64? currency_id  { get; set;}

			public Int64? delivery_unit_id  { get; set;}

			public String delivery_unit_address  { get; set;}

			public String remarks  { get; set;}

			public Int64? supplier_id  { get; set;}

			public String supplier_address  { get; set;}

			public String supplier_concern_person  { get; set;}

			public String terms_condition_list  { get; set;}

			public String test_requirements_list  { get; set;}

			public String document_list  { get; set;}

			public Int64? approved_by  { get; set;}

			public DateTime? approve_date  { get; set;}

			public String approve_remarks  { get; set;}

			public Int64? gen_item_structure_group_id  { get; set;}

			public Boolean? is_acknowledged  { get; set;}

			public Int64? acknowledged_by  { get; set;}

			public DateTime? acknowledged_date  { get; set;}

			public String acknowledged_remarks  { get; set;}

			public Int64? is_submitted  { get; set;}

			public Int64? is_approved  { get; set;}

			public String det_list  { get; set;}

			public String delivery_unit_name  { get; set;}

			public String techpack_number  { get; set;}

			public String suggested_supplier_name  { get; set;}

			public String tran_purchase_requisition_dtl_list  { get; set;}


		}
}
