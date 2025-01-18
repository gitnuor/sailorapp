using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.Statics;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using System.Numerics;

namespace EPYSLSAILORAPP.Domain.Entity.Tran_Tables
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_bp_year")]

    public class tran_bp_year
    {
        #region Table properties

        [Key]
        public long tran_bp_year_id { get; set; }

        public long fiscal_year_id { get; set; }

        public decimal? gross_sales { get; set; }

        public decimal? yearly_gross_discount { get; set; }
        public decimal? yearly_gross_net { get; set; }
        //public BigInteger totalrows { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }
        public string remarks { get; set; }
        public string approve_remarks { get; set; }
        public Int64? approved_by { get; set; }
        public DateTime? approve_date { get; set; }
        public bool? is_approved { get; set; }
        public bool? is_submitted { get; set; }

        public JArray event_list_json { get; set; }
        #endregion Table properties


    }

    public class tran_bp_year_ext: tran_bp_year
    {
        public long? year_no { get; set; }

        public Int64 TotalCount { get; set; }
    }
}
