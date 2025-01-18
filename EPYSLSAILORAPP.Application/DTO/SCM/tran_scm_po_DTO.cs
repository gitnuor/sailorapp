using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Domain.RPC;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_scm_po_DTO : BaseEntity
    {

        public Int64? po_id { get; set; }
        public Int64? event_id { get; set; }
        public Int64? fiscal_year_id { get; set; }

        public String? po_no { get; set; }
        public String? suggested_supplier_name { get; set; }
        public String? selected_supplier_name { get; set; }
        public String? delivery_unit_name { get; set; }
        public String? techpack_number { get; set; }
        public String? pr_no { get; set; }

        public DateTime? po_date { get; set; }

        public DateTime? delivery_start_date { get; set; }

        public DateTime? delivery_end_date { get; set; }

        public dropdown_entity ddlsupplier_info { get; set; }
        public Int64? supplier_id { get; set; }
        public Int64? item_structure_group_id { get; set; }
        public Int64? measurement_unit_detail_id_val { get; set; }

        public Int64? gen_item_structure_group_id { get; set; }
        public Int64? delivery_unit { get; set; }


        public Int64? delivery_address { get; set; }


        public Int64? currency_id { get; set; }

        public string documents { get; set; }

        public string terms_conditions { get; set; }

        public int? is_submitted { get; set; }


        public Int32? status { get; set; }

        public Int64? added_by { get; set; }
        public Int64? pr_id { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public int? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approved_date { get; set; }
        // public List<tran_purchase_requisition_dtl_DTO> details { get; set; }
        public string po_details { get; set; }
        //public string list_po_details { get; set; }
        public List<tran_scm_po_details_DTO> List_po_details { get; set; }

        public List<gen_term_and_conditions_DTO> List_terms_conditions { get; set; }
        public List<TermConditionDetail> terms_conditions_list { get; set; }
        public List<concern_person> concern_Person { get; set; }


        public List<TermConditionGrouped> terms_and_conditions_list { get; set; }

        public List<file_upload> List_Files { get; set; }
        public List<string> DeleteList { get; set; }

        public string? supplier_name { get; set; }
        public string? deliveryAddress { get; set; }

        public Decimal? receive_quantity { get; set; }
        // public List<string> documents { get; set; }



        public Decimal vat_amount { get; set; }
        public Decimal discount_amount { get; set; }
        public Decimal final_amount { get; set; }

        public string? supplier_address { get; set; }


        public string? supplier_concern_person { get; set; }
        public string? revise_status { get; set; }
        public string? status_remarks { get; set; }
        public string del_chalan_no { get; set; }
        public DateTime? del_chalan_date { get; set; }
        public List<rpc_proc_sp_get_data_tran_scm_po_DTO> detChallan { get; set; }
    }

    public class TermConditionDetail
    {
        public Int64 gen_term_and_conditions_details_id { get; set; }
        public Int64? gen_term_and_conditions_id { get; set; }

        public string term_condition_name { get; set; }
        public string? description { get; set; }
    }

    public class TermConditionGrouped
    {
        public Int64? gen_term_and_conditions_id { get; set; }
        public string term_condition_name { get; set; }
        public List<TermConditionDetailGrouped> Details { get; set; }
    }

    public class TermConditionDetailGrouped
    {
        public Int64 gen_term_and_conditions_details_id { get; set; }
        public string description { get; set; }
    }

    



    public class tran_scm_po_DTO_Ext
    {
        public Int64? po_id { get; set; }
        public int? is_approved { get; set; }

        public long? approved_by { get; set; }

        public int? is_submitted { get; set; }
        public DateTime? approved_date { get; set; }

        public Int64? updated_by { get; set; }


        public DateTime? date_updated { get; set; }
    }
}





