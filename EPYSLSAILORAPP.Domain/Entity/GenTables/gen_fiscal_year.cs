using Dapper.Contrib.Extensions;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity.GenTables
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_fiscal_year")]
    public class gen_fiscal_year :DapperExt
    {

        [Key]
        public Int64 fiscal_year_id { set; get; }
        public Int64? year_no { set; get; }
        public string? year_name { set; get; }
        //public Int64? period_no { set; get; }
        //public DateTime? start_month { set; get; }
        //public DateTime? end_month { set; get; }
        public DateTime?  start_date { set; get; }
        public DateTime? end_date { set; get; }
       
        public bool locks { set; get; }
        public bool is_used { set; get; }
        public Int64? added_by { set; get; }
        public DateTime? date_added { set; get; }
        public Int64? update_by { set; get; }
        public DateTime? date_updated { set; get; }
    }
}
