

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_pre_costing_review")]

    public class tran_pre_costing_review_entity : DapperExt
    {

        [Key]
        public Int64? tran_pre_costing_review_id { get; set; }

        public Int64? tran_pre_costing_id { get; set; }

        public string? version_no { get; set; }
       
        public JObject? old_data { get; set; }

        public JObject? new_data { get; set; }

        public String? remarks { get; set; }

        public Int64? is_submitted { get; set; }

        public Int64? submitted_by { get; set; }

        public Int64? is_approved_by_designer { get; set; }

        public Int64? designer_approved_by { get; set; }

        public DateTime? designer_approve_date { get; set; }

        public String? designer_approve_remarks { get; set; }

        public Int64? is_ack_by_merchant { get; set; }

        public Int64? merchant_ack_by { get; set; }

        public DateTime? merchant_ack_date { get; set; }

        public String merchant_ack_remarks { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
