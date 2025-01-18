
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [Table("tran_va_plan_events")]

    public class tran_va_plan_events_entity : BaseModel
    {

        [PrimaryKey("tran_va_plan_event_id", false)]
        public Int64? tran_va_plan_event_id { get; set; }

        public Int64? tran_va_plan_id { get; set; }

        public Int64? tran_range_plan_event_id { get; set; }

        public bool? is_finalised { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }
        public JArray plan_detl_details { get; set; }
    }
}
