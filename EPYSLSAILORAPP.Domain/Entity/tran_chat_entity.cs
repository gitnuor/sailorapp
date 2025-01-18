
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_chat")]

    public class tran_chat_entity : DapperExt
    {

        [Key]
        public Int64 tran_chat_id { get; set; }

        [Column("from_user_id")]
        public Int64 from_user_id { get; set; }

        [Column("from_user_name")]
        public string from_user_name { get; set; }

        [Column("to_user_id")]
        public Int64 to_user_id { get; set; }

        [Column("to_user_name")]
        public string to_user_name { get; set; }

        [Column("message")]
        public string? message { get; set; }

        [Column("message_json")]
        [JsonProperty("message_json")]
        public string? message_json { get; set; }

        [Column("is_view")]
        public Int64? is_view { get; set; }

        [Column("date_viewed")]
        public DateTime? date_viewed { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        public Int64 is_group { get; set; }

        [Column("to_chat_group_id")]
        public Int64 to_chat_group_id { get; set; }

        [Column("to_chat_group_name")]
        public string to_chat_group_name { get; set; }

        [Column("to_chat_group_users")]
        [JsonProperty("to_chat_group_users")]
        public string to_chat_group_users { get; set; }

    }

    
}
