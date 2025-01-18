
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_shop_floor_return_details_DTO : BaseEntity
    {

        [Required]
        [Column("tran_shop_floor_return_details_id")]
        public Int64 tran_shop_floor_return_details_id { get; set; }

        [Required]
        [Column("tran_mcd_requisition_issue_id")]
        public Int64 tran_mcd_requisition_issue_id { get; set; }

        [Required]
        [Column("item_id")]
        public Int64 item_id { get; set; }

        [Column("issue_quantity")]
        public Decimal? issue_quantity { get; set; }

        [Column("return_quantity")]
        public Decimal? return_quantity { get; set; }

        [Required]
        [Column("measurement_unit_id")]
        public Int64 measurement_unit_id { get; set; }

        [Column("measurement_unit_name")]
        public string? measurement_unit_name { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }
        public string? item_name { get; set; }

        [Column("tran_shop_floor_return_id")]
        public Int64? tran_shop_floor_return_id { get; set; }



    }
}
