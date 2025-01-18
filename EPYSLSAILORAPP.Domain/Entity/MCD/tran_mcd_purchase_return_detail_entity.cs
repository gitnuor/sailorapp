
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_mcd_purchase_return_detail")]

    public class tran_mcd_purchase_return_detail_entity : DapperExt
    {

        [Key]
        public Int64 purchase_return_detail_id { get; set; }

        [Column("purchase_return_id")]
        public Int64? purchase_return_id { get; set; }

        [Column("gen_item_master_id")]
        public Int64? gen_item_master_id { get; set; }

        [Column("po_quantity")]
        public Decimal? po_quantity { get; set; }

        [Column("measurement_unit_detail_id")]
        public Int64? measurement_unit_detail_id { get; set; }

        [Column("measurement_unit")]
        public string? measurement_unit { get; set; }

        [Column("receive_quantity")]
        public Decimal? receive_quantity { get; set; }

        [Column("receive_unit")]
        public Decimal? receive_unit { get; set; }

        [Column("reject_quantity")]
        public Decimal? reject_quantity { get; set; }

        [Column("return_quantity")]
        public Decimal? return_quantity { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("po_id")]
        public Int64? po_id { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }
        public Int64? current_state { get; set; }


    }
}
