
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.Entity
{

    [System.ComponentModel.DataAnnotations.Schema.Table("tran_notification")]

    public class tran_notification_entity : DapperExt
    {

        [Key]
        public Int64 tran_notification_id { get; set; }

        [Column("to_user_id")]
        public Int64? to_user_id { get; set; }

        [Column("to_user_name")]
        public string to_user_name { get; set; }

        [Column("message")]
        public string? message { get; set; }

        [Column("message_json")]
        [JsonProperty("message_json")]
        public String? message_json { get; set; }

        [Column("is_view")]
        public Int64? is_view { get; set; }

        [Column("date_viewed")]
        public DateTime? date_viewed { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("module_id")]
        public Int64? module_id { get; set; }

        [Column("notifacation_link")]
        public string? notifacation_link { get; set; }


    }
}
