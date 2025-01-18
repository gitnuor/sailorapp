
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_sp_get_data_tran_scm_po_DTO
    {

        public Int64? po_id { get; set; }

        public String po_no { get; set; }

        public DateTime? po_date { get; set; }

        public DateTime? delivery_start_date { get; set; }

        public DateTime? delivery_end_date { get; set; }

        public Int64? supplier_id { get; set; }

        public Int64? delivery_unit { get; set; }

        public Int64? delivery_address { get; set; }

        public Int64? currency_id { get; set; }

        public String documents { get; set; }

        public String terms_conditions { get; set; }

        public Int64? status { get; set; }

        public Int64? pr_id { get; set; }

        public Int64? item_structure_group_id { get; set; }

        public DateTime? approved_date { get; set; }

        public Int64? approved_by { get; set; }

        public Int64? is_bill_submitted { get; set; }

        public String po_details { get; set; }

        public Int64? event_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? is_submitted { get; set; }

        public Int64? is_approved { get; set; }

        public String tran_scm_po_details_list { get; set; }

        public string delivery_unit_name { get; set; }

        public string suggested_supplier_name { get; set; }

        public string pr_no { get; set; }
        public string techpack_number { get; set; }

        public Decimal vat_amount { get; set; }
        public Decimal discount_amount { get; set; }
        public Decimal final_amount { get; set; }

        public string term_and_conditions_details {  get; set; }

        public string supplier_concern_person { get; set; }
        public string supplier_address { get; set; }
        public string status_remarks { get; set; }
        public string del_chalan_no { get; set; }
        public string del_chalan_date { get; set; }
        public Int64? received_id { get; set; }
    }
}
