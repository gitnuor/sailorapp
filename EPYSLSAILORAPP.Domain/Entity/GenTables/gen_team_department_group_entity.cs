
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_team_department_group")]

    public class gen_team_department_group_entity : DapperExt
    {

        [Key]

        [Column("group_id")]
        public Int64? group_id { get; set; }

        [Column("team_group_name")]
        public string? team_group_name { get; set; }

        [Column("team_head_name")]
        public string? team_head_name { get; set; }

        [Column("team_head_id_number")]
        public string? team_head_id_number { get; set; }

        [Column("team_head_user_id")]
        public Int64? team_head_user_id { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("team_group")]
        public string? team_group { get; set; }

        [Column("team_group_id")]
        public Int64? team_group_id { get; set; }


    }
}
