namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_sewing_input_DTO
    {

        public Int64? tran_sewing_input_id { get; set; }

        public Int64? tran_sewing_allocation_plan_id { get; set; }

        public Int64? techpack_id { get; set; }

        public String sewing_allocation_number { get; set; }
        public String merchandiser_name { get; set; }

        public String techpack_number { get; set; }
        public DateTime sewing_allocation_date { get; set; }

        public Int64 total_rows { get; set; }

    }


    public class rpc_sewing_allocation_data_DTO
    {
        public long tran_sewing_allocation_plan_id { get; set; }
        public string sewing_allocation_number { get; set; }
        public DateTime sewing_allocation_date { get; set; }
        public long techpack_id { get; set; }
        public string techpack_number { get; set; }
        public string merchandiser_name { get; set; }
        public string style_item_product_category { get; set; }
        public string ddl_line { get; set; } // Store JSONB as a string
    }

    public class rpc_sewing_input_data_for_output_DTO
    {
        public long tran_sewing_allocation_plan_id { get; set; }

        public long techpack_id { get; set; }
        public string techpack_number { get; set; }
        public string merchandiser_name { get; set; }
        public string style_item_product_category { get; set; }
        public string ddl_line { get; set; } // Store JSONB as a string
    }
    public class rpc_sewing_line_wise_deatail_DTO  
    {
        public long production_line_id { get; set; }
        public string line_name { get; set; }
        public string color_code { get; set; }
        public string size_name { get; set; }
        public long style_product_size_group_detid { get; set; }
        public long batch_id { get; set; }
        public string batch_no { get; set; }
        public long allocated_quantity { get; set; }
        public long already_inputed { get; set; }
        public long tran_sewing_allocation_plan_id { get; set; }
    }
    public class rpc_sewing_line_wise_input_deatail_DTO
    {
        public long production_line_id { get; set; }
        public string line_name { get; set; }
        public string color_code { get; set; }
        public string size_name { get; set; }
        public long style_product_size_group_detid { get; set; }

        public long input_quantity { get; set; }
     
    }
}
