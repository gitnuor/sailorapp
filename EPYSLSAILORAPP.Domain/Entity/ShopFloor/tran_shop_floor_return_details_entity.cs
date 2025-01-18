
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
   
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_shop_floor_return_details")]

    public class tran_shop_floor_return_details_entity : DapperExt
    {

        [Key]
        public Int64 tran_shop_floor_return_details_id { get; set; }

        [Column("tran_mcd_requisition_issue_id")]
        public Int64 tran_mcd_requisition_issue_id { get; set; }

        [Column("item_id")]
        public Int64 item_id { get; set; }

        [Column("issue_quantity")]
        public Decimal? issue_quantity { get; set; }

        [Column("return_quantity")]
        public Decimal? return_quantity { get; set; }

        [Column("measurement_unit_id")]
        public Int64 measurement_unit_id { get; set; }

        [Column("measurement_unit_name")]
        public string? measurement_unit_name { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("tran_shop_floor_return_id")]
        public Int64? tran_shop_floor_return_id { get; set; }
        public Int64? current_state { get; set; }


    }
}
