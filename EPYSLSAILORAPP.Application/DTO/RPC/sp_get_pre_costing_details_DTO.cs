
using BDO.Core.Base;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_pre_costing_details_DTO : BaseEntity
    {

        public Int64? tran_pre_costing_id { get; set; }

        public Int64? tran_sample_order_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public String tran_sample_order_number { get; set; }

        public DateTime? order_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public Int64? delivery_unit_id { get; set; }

        public Int64? order_quantity { get; set; }

        public String str_delivery_unit { get; set; }

        
    }
}
