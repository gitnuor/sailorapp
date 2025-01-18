namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_scm_po_DTO
    {

        public Int64? po_id { get; set; }
        public Int64 total_rows { get; set; }

        public String po_no { get; set; }

        public DateTime? po_date { get; set; }

        public String supplier_name { get; set; }

        public String unit_name { get; set; }

        public String pr_no { get; set; }

        public String event_title { get; set; }

        public string supplier_concern_person { get; set; }
        public string supplier_address { get; set; }
        public string status_remarks { get; set; }




    }
}
