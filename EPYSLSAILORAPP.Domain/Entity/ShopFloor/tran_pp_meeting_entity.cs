
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_pp_meeting")]

    public class tran_pp_meeting_entity : DapperExt
    {

        [Key]
        public Int64 tran_pp_meeting_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("meeting_conducted_by")]
        public Int64? meeting_conducted_by { get; set; }

        [Column("meeting_conducted_date")]
        public DateTime? meeting_conducted_date { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        public string imagePath { get; set; }


    }
}
