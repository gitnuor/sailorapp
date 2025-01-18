
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_range_plan_details_size_DTO
    {

        public Int64? range_plan_detail_size_id { get; set; }

        public Int64? range_plan_detail_id { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? style_product_size_group_detid { get; set; }

        public Int64? size_quantity { get; set; }

        public Decimal? size_per_pc_mrp_value { get; set; }

        public Decimal? size_per_pc_cpu_value { get; set; }


        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
