
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_range_plan_events_DTO
    {

        public Int64? tran_range_plan_event_id { get; set; }

        public Int64? range_plan_id { get; set; }

        public Int64? tran_bp_event_id { get; set; }

        public Decimal? total_mrp_value { get; set; }

        public Decimal? total_cpu_value { get; set; }

        public Int64? total_range_plan_quantity { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public bool? is_finalized { get; set; }
        public string approve_remarks { get; set; }
        public long? approved_by { get; set; }
        public DateTime? approve_date { get; set; }
        public long? is_approved { get; set; }

    }
}
