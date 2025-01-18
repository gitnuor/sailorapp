

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_range_plan")]
    public class tran_range_plan_entity : DapperExt
    {
        [Key]
        public Int64? range_plan_id { get; set; }

        public Int64? tran_bp_year_id { get; set; }

        public String remarks { get; set; }

        public bool? is_submitted { get; set; }

       

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public bool? is_approved { get; set; }
        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }
        public string range_plan_details_list_json { get; set; }


    }
}
