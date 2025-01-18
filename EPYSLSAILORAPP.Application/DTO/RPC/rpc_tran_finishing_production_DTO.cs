using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.RPC
{
    public class rpc_tran_finishing_production_DTO
    {
        public Int64? tran_finish_receive_id { get; set; }
        public Int64? techpack_id { get; set; }
        public Int64? style_item_product_id { get; set; }
        public String style_item_product_category { get; set; }
        public String merchandiser_name { get; set; }
        public String techpack_number { get; set; }
        public DateTime tran_finish_receive_date { get; set; }
        public String color_code { get; set; }
        public Int64? qc_pass_quantity { get; set; }
        public String size_name { get; set; }
        public Int64? finish_receive_qty { get; set; }

        public DateTime production_process_date { get; set; }
        public String finishing_process_name { get; set; }
        public Int64? tran_finishing_production_process_id { get; set; }
        public Int64? production_quantity { get; set; }

    }

    //public class rpc_proc_finishing_process_type_DTO
    //{
    //    public Int64? gen_finishing_process_id { get; set; }
    //    public String finishing_process_name { get; set; }
    //}

}
