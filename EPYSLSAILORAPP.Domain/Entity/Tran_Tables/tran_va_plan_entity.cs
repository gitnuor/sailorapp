
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [Table("tran_va_plan")]

    public class tran_va_plan_entity : BaseModel
    {

        [PrimaryKey("tran_va_plan_id", false)]
        public Int64? tran_va_plan_id { get; set; }

        public Int64? tran_range_plan_id { get; set; }

        public String remarks { get; set; }

        public Boolean? is_submitted { get; set; }

        public Boolean? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public JArray plan_event_details { get; set; }
    }
}
