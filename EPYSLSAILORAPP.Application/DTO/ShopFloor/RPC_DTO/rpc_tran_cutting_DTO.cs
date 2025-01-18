namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_cutting_DTO 
		{
			public long tran_cutting_id { get; set; }

			public String style_code  { get; set;}

			public String all_processes  { get; set;}

			public String remarks  { get; set;}

			public String techpack_number  { get; set;}

        public Int64 total_rows { get; set; }
    }
}
