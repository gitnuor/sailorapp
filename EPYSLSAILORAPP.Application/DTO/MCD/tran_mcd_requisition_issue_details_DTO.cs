
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_mcd_requisition_issue_details_DTO : BaseEntity
    {

        [Required]
        [Column("tran_mcd_requisition_issue_details_id")]
        public Int64 tran_mcd_requisition_issue_details_id { get; set; }
        [Column("gen_item_master_id")]
        public Int64? gen_item_master_id { get; set; }

        [Column("tran_mcd_requisition_issue_id")]
        public Int64 tran_mcd_requisition_issue_id { get; set; }
        [Column("measurement_unit")]
        public string? measurement_unit { get; set; }

        [Column("measurement_unit_detail_id")]
        public Int64? measurement_unit_detail_id { get; set; }
        [Column("item_id")]
        public Int64 item_id { get; set; }

        [Column("requisition_quantity")]
        public Decimal? requisition_quantity { get; set; }

        [Column("issue_quantity")]
        public Decimal? issue_quantity { get; set; }

        [Column("rejected_quantity")]
        public Decimal? rejected_quantity { get; set; }


        [Column("measurement_unit_id")]
        public Int64 measurement_unit_id { get; set; }
        public Decimal? available_stock_quantity { get; set; }
        public Decimal? booking_quantity { get; set; }

        [Column("measurement_unit_name")]
        public string? measurement_unit_name { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }
        public string gen_item_master { get; set; }

        public Int64? current_state { get; set; }
        public string color_code { get; set; }
    }
}
