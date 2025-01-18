using Dapper.Contrib.Extensions;
using Postgrest.Attributes;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_cutting")]

    public class tran_cutting_entity : DapperExt
    {

        [Key]
        public Int64 tran_cutting_id { get; set; }

        [Column("tran_pp_meeting_id")]
        public Int64? tran_pp_meeting_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("style_code")]
        public string? style_code { get; set; }

        [Column("all_processes")]
        public string? all_processes { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("tran_cutting_color_json")]
        public String? tran_cutting_color_json { get; set; }


    }
}
