
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_pre_costing_review_DTO : BaseEntity
    {

        public Int64? tran_pre_costing_review_id { get; set; }

        public Int64? tran_pre_costing_id { get; set; }

        public string? version_no { get; set; }

        public string? obj_old_data { get; set; }

        public string? obj_new_data { get; set; }

        public string? old_data { get; set; }

        public string? new_data { get; set; }

        public Int64? is_submitted { get; set; }

        public Int64? submitted_by { get; set; }

        public Int64? is_approved_by_designer { get; set; }

        public Int64? designer_approved_by { get; set; }

        public DateTime? designer_approve_date { get; set; }

        public String designer_approve_remarks { get; set; }

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
