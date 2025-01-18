
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_sample_order_details_DTO
    {

        public Int64? tran_sample_order_id { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public String tran_sample_order_number { get; set; }

        public DateTime? order_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public Int64? delivery_unit_id { get; set; }

        public string str_delivery_unit { get; set; }

        public Int64? row_index { get; set; }

        public Int64? order_quantity { get; set; }

        public String subgroup_json { get; set; }

        public String embellishment_json { get; set; }


    }
}
