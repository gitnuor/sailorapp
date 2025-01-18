
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_range_plan_DTO
    {

        public Int64? range_plan_id { get; set; }

        public bool? is_submitted { get; set; }

        public bool? is_approved { get; set; }

        public Int64? tran_bp_year_id { get; set; }

        public String remarks { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public tran_range_plan_events_DTO Event_Detail { get; set; }
        public List<tran_range_plan_details_DTO> DetList { get; set; }
        public JArray range_plan_details_list_json { get; set; }

        public Int64? tran_range_plan_event_id { get; set; }

        public string is_finalized { get; set; }

       
    }
}
