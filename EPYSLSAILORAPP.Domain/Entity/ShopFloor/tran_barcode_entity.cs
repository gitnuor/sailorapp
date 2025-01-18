
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_barcode")]

    public class tran_barcode_entity : DapperExt
    {

        [Key]
        public Int64 tran_barcode_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("style_product_size_group_detid")]
        public Int64? style_product_size_group_detid { get; set; }

        [Column("barcode")]
        public string? barcode { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }


    }
}
