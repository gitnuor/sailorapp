
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_email_notification_master_DTO : BaseEntity
    {

        [Required]
        [Column("tran_email_notification_master_id")]
        public Int64 tran_email_notification_master_id { get; set; }

        [Column("sender_email")]
        public string? sender_email { get; set; }

        [Column("to_email")]
        public string? to_email { get; set; }

        [Column("cc_email")]
        public string? cc_email { get; set; }

        [Column("subject")]
        public string? subject { get; set; }

        [Column("email_body")]
        public string? email_body { get; set; }

        [Column("email_attchement1")]
        public string? email_attchement1 { get; set; }

        [Column("email_attchement2")]
        public string? email_attchement2 { get; set; }

        [Column("email_attchement3")]
        public string? email_attchement3 { get; set; }

        [Column("email_attchement4")]
        public string? email_attchement4 { get; set; }

        [Column("email_attchement5")]
        public string? email_attchement5 { get; set; }

        [Column("email_attchement6")]
        public string? email_attchement6 { get; set; }

        [Column("email_attchement7")]
        public string? email_attchement7 { get; set; }

        [Column("email_attchement8")]
        public string? email_attchement8 { get; set; }

        [Column("email_attchement9")]
        public string? email_attchement9 { get; set; }

        [Column("email_attchement10")]
        public string? email_attchement10 { get; set; }

        [Required]
        [Column("initiated_by")]
        public Int64 initiated_by { get; set; }

        [Column("initiated_date")]
        public DateTime? initiated_date { get; set; }

        public string detl_list { get; set; }

        public List<tran_email_notification_DTO> TranEmailNotification_List { get; set; }

        public Int64? email_template_id { get; set; }

    }
}
