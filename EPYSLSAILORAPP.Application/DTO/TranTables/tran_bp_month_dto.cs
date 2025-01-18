using Newtonsoft.Json.Linq;
using Postgrest.Attributes;

namespace EPYSLSAILORAPP.Application.DTO.TranTables
{
    public class tran_bp_event_month_dto
    {
        [PrimaryKey("tran_bp_event_month_id", false)]
        public long tran_bp_event_month_id { get; set; }

        public long tran_bp_event_id { get; set; }

        public long month_id { get; set; }

        public decimal monthly_gross_sales { get; set; }

        public decimal monthly_discount_amount { get; set; }

        public decimal monthly_net_amount { get; set; }

        public long added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public JArray event_month_outlet_list_json { get; set; }

        public List<tran_bp_event_month_outlet_dto> tran_bp_event_month_outlet_list { get; set; }
    }
}
