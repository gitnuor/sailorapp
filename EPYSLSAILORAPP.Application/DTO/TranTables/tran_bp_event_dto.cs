using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;

namespace EPYSLSAILORAPP.Application.DTO.TranTables
{
    public class tran_bp_event_dto
    {
        [PrimaryKey("tran_bp_event_id", false)]
        public long tran_bp_event_id { get; set; }

        public long tran_bp_year_id { get; set; }

        public long event_id { get; set; }

        public decimal event_gross_sales { get; set; }

        public decimal event_discount_amount { get; set; }

        public decimal event_net_amount { get; set; }

        public long? readygoods_qnty { get; set; }

        public long? readygoods_value { get; set; }

        public long? readygoods_cpu {  get; set; }

        public long added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public JArray event_month_list_json { get; set; }

        public List<tran_bp_event_month_dto> tran_bp_month_list { get; set; }
    }
}
