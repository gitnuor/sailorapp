
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_chat_DTO : DapperExt
    {

        [Required]
        [Column("tran_chat_id")]
        public Int64 tran_chat_id { get; set; }

        [Required]
        [Column("from_user_id")]
        public Int64 from_user_id { get; set; }

        [Required]
        [Column("from_user_name")]
        public string from_user_name { get; set; }

        [Required]
        [Column("to_user_id")]
        public Int64 to_user_id { get; set; }

        [Required]
        [Column("to_user_name")]
        public string to_user_name { get; set; }

        [Column("message")]
        public string? message { get; set; }
        [JsonProperty("message_json")]
        public string? message_json { get; set; }

        [Column("is_view")]
        public Int64? is_view { get; set; }

        [Column("date_viewed")]
        public DateTime? date_viewed { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("emp_pic")]
        public string emp_pic { get; set; }

        [Column("last_message")]
        public string last_message { get; set; }

        public Int64 is_group { get; set; }

        [Column("to_chat_group_id")]
        public Int64 to_chat_group_id { get; set; }

        [Column("to_chat_group_name")]
        public string to_chat_group_name { get; set; }

        [Column("to_chat_group_users")]
        public string to_chat_group_users { get; set; }

    }

}
