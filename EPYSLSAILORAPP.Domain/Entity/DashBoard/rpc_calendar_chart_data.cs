using EPYSLSAILORAPP.Domain.Statics;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity.DashBoard
{
    public class rpc_calendar_chart_data 
    {
        public int row_number { get; set; }
        public string event_title { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }


    }
}
