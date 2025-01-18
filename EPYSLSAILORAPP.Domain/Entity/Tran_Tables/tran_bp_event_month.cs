
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.Tran_Tables
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_bp_event_month")]
    public class tran_bp_event_month : DapperExt
    {
        #region Table properties

        [Key]
        public long tran_bp_event_month_id { get; set; }

        public long tran_bp_event_id { get; set; }

        public long month_id { get; set; }

        public decimal monthly_gross_sales { get; set; }

        public long added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public string event_month_outlet_list_json { get; set; }

        #endregion Table properties


    }
}
