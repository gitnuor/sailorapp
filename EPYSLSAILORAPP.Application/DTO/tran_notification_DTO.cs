
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_notification_DTO : BaseEntity
    {

        public Int64? new_notification_id_ret { get; set; }

        [Required]
        [Column("tran_notification_id")]
        public Int64? tran_notification_id { get; set; }

        [Column("to_user_id")]
        public Int64? to_user_id { get; set; }

        [Required]
        [Column("to_user_name")]
        public string? to_user_name { get; set; }

        [Column("message")]
        public string? message { get; set; }

        public string? message_json { get; set; }

        [Column("is_view")]
        public Int64? is_view { get; set; }

        [Column("date_viewed")]
        public DateTime? date_viewed { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("module_id")]
        public Int64? module_id { get; set; }

        [Column("notifacation_link")]
        public string? notifacation_link { get; set; }
        public Int64? total_notification { get; set; }


    }

}
