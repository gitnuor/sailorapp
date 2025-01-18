
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_email_notification_DTO : BaseEntity
    {

        [Required]
        [Column("tran_email_notification_id")]
        public Int64 tran_email_notification_id { get; set; }

        [Column("sender_email")]
        public string? sender_email { get; set; }

        [Column("to_email")]
        public string? to_email { get; set; }

        [Column("cc_email")]
        public string? cc_email { get; set; }

        public string to_name { get; set; }

        [Column("subject")]
        public string? subject { get; set; }

        [Column("email_body")]
        public string? email_body { get; set; }

        [Required]
        [Column("initiated_by")]
        public Int64 initiated_by { get; set; }

        [Column("initiated_date")]
        public DateTime? initiated_date { get; set; }

        [Column("is_sent")]
        public Int32? is_sent { get; set; }

        [Column("sent_status")]
        public string? sent_status { get; set; }

        [Column("sent_time")]
        public DateTime? sent_time { get; set; }

        [Column("attempt_count")]
        public Int64? attempt_count { get; set; }

        public Int64? tran_email_notification_master_id { get; set; }



    }
}
