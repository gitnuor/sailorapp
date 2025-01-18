
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_sewing_input")]

    public class tran_sewing_input_entity : DapperExt
    {

        [Key]
        public Int64 tran_sewing_input_id { get; set; }

        [Column("tran_sewing_allocation_plan_id")]
        public Int64? tran_sewing_allocation_plan_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }


    }
}
