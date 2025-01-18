
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_range_plan_details")]

    public class tran_range_plan_details_entity : DapperExt
    {

        [Key]
        public Int64? range_plan_detail_id { get; set; }

        public Int64? range_plan_id { get; set; }

        public Int64? tran_bp_event_id { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? range_plan_quantity { get; set; }

        public Decimal? mrp_per_pc_value { get; set; }

        public Decimal? mrp_value { get; set; }

        public Decimal? cpu_per_pc_value { get; set; }

        public Decimal? cpu_value { get; set; }

        public Int64? priority_id { get; set; }

        public Int64? prev_year_quantity { get; set; }

        public Decimal? prev_year_efficiency { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
