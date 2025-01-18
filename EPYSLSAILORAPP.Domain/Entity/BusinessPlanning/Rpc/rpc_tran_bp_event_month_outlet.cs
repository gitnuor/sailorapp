using EPYSLSAILORAPP.Domain.Statics;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity.BusinessPlanning
{
    public class rpc_tran_bp_event_month_outlet 
    {
        #region Table properties
        public Int64? tran_bp_event_month_outlet_id { get; set; }
        public Int64? outlet_id { get; set; }

        public Int64? tran_bp_event_month_id { get; set; }

        public decimal? outlet_gross_sales { get; set; }
        public decimal? outlet_discount_amount { get; set; }
        public decimal? outlet_net_amount { get; set; }

        public decimal? yearly_gross_sales { get; set; }
        public decimal? yearly_gross_discount { get; set; }
        public decimal? yearly_gross_net { get; set; }
        public decimal? event_gross_sales { get; set; }
        public decimal? event_discount_amount { get; set; }
        public decimal? event_net_amount { get; set; }
        public decimal? monthly_gross_sales { get; set; }

        public Int64? tran_bp_year_id { get; set; }
        public Int64? tran_bp_event_id { get; set; }

        public Int64? month_id { get; set; }
        public Int64? event_id { get; set; }

        public Int64? readygoods_qnty { get; set; }
        public decimal? readygoods_value { get; set; }

        public decimal? readygoods_cpu { get; set; }

        public Int64? added_by { get; set; }
        public DateTime? date_added { get; set; }

        #endregion Table properties


    }
}
