using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using System.Numerics;

namespace EPYSLSAILORAPP.Domain.Entity
{
    
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_mcd_requisition_issue_details")]

    public class tran_mcd_requisition_issue_details_entity : DapperExt
    {

        [Key]
        public Int64 tran_mcd_requisition_issue_details_id { get; set; }

        [Column("tran_mcd_requisition_issue_id")]
        public Int64 tran_mcd_requisition_issue_id { get; set; }
        [Column("tran_mcd_requisition_slip_id")]
        public Int64? tran_mcd_requisition_slip_id { get; set; }

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

        [Column("measurement_unit_name")]
        public string? measurement_unit_name { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        public Int64? current_state { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }


    }
}
