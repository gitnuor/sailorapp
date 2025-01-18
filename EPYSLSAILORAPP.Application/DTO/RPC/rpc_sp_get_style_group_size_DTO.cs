
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_style_group_size_DTO
    {

        public Int64? style_product_size_group_id { get; set; }

        public Int64? style_product_size_group_detid { get; set; }

        public String style_product_size { get; set; }

        public bool? is_separate_price { get; set; }

        public Int64? range_plan_detail_id { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Decimal? size_quantity { get; set; }

        public Decimal? size_per_pc_mrp_value { get; set; }

        public Decimal? size_per_pc_cpu_value { get; set; }

        public Int64? range_plan_detail_size_id { get; set; }


    }
}
