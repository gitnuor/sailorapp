using Postgrest.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPYSLSAILORAPP.Application.DTO.TranTables
{
    public class tran_bp_event_month_outlet_dto
    {
        [PrimaryKey("tran_bp_event_month_outlet_id", false)]
        public long tran_bp_event_month_outlet_id { get; set; }

        public long tran_bp_event_month_id { get; set; }

        public long outlet_id { get; set; }

        public decimal outlet_gross_sales { get; set; }

        public decimal outlet_discount_amount { get; set; }

        public decimal outlet_net_amount { get; set; }

        public long added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime date_added { get; set; }

        public DateTime? date_updated { get; set; }
    }
}
