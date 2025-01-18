using EPYSLSAILORAPP.Domain.Statics;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity.BusinessPlanning
{
    public class rpc_event_tran_data_getall :  DapperExt
    {
        #region Table properties
        public string event_title { get; set; }
        public string year_name { get; set; }
        public string season_name { get; set; }
        public Int64? event_id { get; set; }
        public Int64? sequence { get; set; }
        public Int64? season_id { get; set; }
        
        public Int64? fiscal_year_id { get; set; }

        public DateTime? start_date { get; set; }
        
        public DateTime? end_date { get; set; }

        public Int64? start_month_id {  get; set; }

        public Int64? end_month_id { get; set; }

        public decimal? gross_sales { get; set; }
        public decimal? yearly_gross_discount { get; set; }
        public decimal? yearly_gross_net_amount { get; set; }
        public decimal? event_gross_sales { get; set; }
        public decimal? event_discount_amount { get; set; }
        public decimal? event_net_amount { get; set; }
        public Int64? readygoods_qnty { get; set; }

        public decimal? readygoods_value { get; set; }

        public decimal? readygoods_cpu { get; set; }

        public Int64? tran_bp_event_id {  get; set; }  

        public Int64 added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime date_added { get; set; }

        public DateTime? date_updated { get; set; }


        public bool? is_active { get; set; }

        public string monthlist {  get; set; }

        #endregion Table properties


    }
}
