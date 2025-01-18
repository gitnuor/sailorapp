
using EPYSLSAILORAPP.Domain.Entity;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_va_plan_DTO
    {

        public Int64? tran_va_plan_id { get; set; }

        public Int64? tran_range_plan_id { get; set; }

        public string remarks { get; set; }

        public Boolean? is_submitted { get; set; }

        public Boolean? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public string approve_remarks { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


        public JArray plan_event_details { get; set; }

        public List<tran_va_plan_events_DTO> List_Events { get; set; }

      
    }
}
