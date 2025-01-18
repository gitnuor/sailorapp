using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Domain.Statics;

namespace EPYSLSAILORAPP.Application.DTO.BusinessPlanning
{
    [Serializable]
    public class BusinessPlanDTO
    {
      
        public Int64? business_plan_id { get; set; }

        public Int64? tran_bp_year_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public decimal? gross_sales { get; set; }

        public decimal? yearly_gross_discount { get; set; }

        public decimal? yearly_gross_net { get; set; }

        public string remarks { get; set; }
        public string approve_remarks { get; set; }
        public Int64? approved_by { get; set; }
        public DateTime? approve_date { get; set; }

        public bool? is_approved { get; set; }
        public bool? is_submitted { get; set; }
        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public List<BpEventOutletDTO> event_monthly_outlet_list { get; set; }

    }

    [Serializable]
    public class BpEventOutletDTO
    {
        public Int64? event_id { get; set; }
        public Int64? month_id { get; set; }

        public Int64? outlet_id { get; set; }
        public decimal? outlet_gross_sales { get; set; }

        public decimal? outlet_gross_discount { get; set; }
        public decimal? outlet_gross_net { get; set; }
        public Int64? readygoods_qnty { get; set; }

        public Int64? readygoods_value { get; set; }

        public Int64? readygoods_cpu { get; set; }

        public decimal? event_gross_sales { get; set; }

        public decimal? event_gross_discount { get; set; }

        public decimal? event_gross_net { get; set; }

        public Int64? tran_bp_event_month_outlet_id { get; set; }
       
        public Int64? tran_bp_event_month_id { get; set; }

        public decimal? yearly_gross_sales { get; set; }

        public decimal? yearly_gross_net { get; set; }

        public decimal? yearly_gross_discount { get; set; }

        public decimal? monthly_gross_sales { get; set; }

        public decimal? monthly_discount_amount { get; set; }

        public decimal? monthly_net_amount { get; set; }

        public Int64? tran_bp_year_id { get; set; }
        public Int64? tran_bp_event_id { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

    }
}
