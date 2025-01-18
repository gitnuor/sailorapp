namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_purchase_requisition_DTO 
		{

			public Int64? pr_id  { get; set;}
			public Int64 total_rows  { get; set; }

        public String pr_no  { get; set;}

			public DateTime? pr_date  { get; set;}

			public DateTime? delivery_date  { get; set;}

			public String event_title  { get; set;}

			public String designer_name  { get; set;}

			public String unit_name  { get; set;}

			public String supplier_name  { get; set;}


		}
    public class rpc_draft_purchase_requisition_DTO
    {

        public Int64? dpr_id { get; set; }

        public String dpr_no { get; set; }

        public DateTime? dpr_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public String event_title { get; set; }

        public String designer_name { get; set; }

        public String unit_name { get; set; }

        public String supplier_name { get; set; }


    }
}
