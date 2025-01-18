
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_chat_group")]

    public class gen_chat_group_entity : DapperExt
    {

        [Key]
        public Int64 chat_group_id { get; set; }

        [Column("chat_group_name")]
        public string chat_group_name { get; set; }

        [Column("group_users")]
        [JsonProperty("group_users")]
        public string group_users { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }


    }
}
