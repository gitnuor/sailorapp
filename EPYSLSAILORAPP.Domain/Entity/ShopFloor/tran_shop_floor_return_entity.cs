
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_shop_floor_return")]

    public class tran_shop_floor_return_entity : DapperExt
    {

        [Key]
        public Int64 tran_shop_floor_return_id { get; set; }

        [Column("return_no")]
        public string return_no { get; set; }

        [Column("requisition_slip_id")]
        public Int64? requisition_slip_id { get; set; }

        [Column("tran_mcd_requisition_issue_id")]
        public Int64? tran_mcd_requisition_issue_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("is_submitted")]
        public Int32? is_submitted { get; set; }

        [Column("is_acknowledged")]
        public Int32? is_acknowledged { get; set; }

        [Column("acknowledged_by")]
        public Int64? acknowledged_by { get; set; }

        [Column("acknowledged_date")]
        public DateTime? acknowledged_date { get; set; }

        [Column("acknowledged_remarks")]
        public string? acknowledged_remarks { get; set; }

        [Column("gen_item_structure_group_id")]
        public Int64? gen_item_structure_group_id { get; set; }

        [Column("gen_item_structure_group_sub_id")]
        public Int64? gen_item_structure_group_sub_id { get; set; }

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
        [Column("shop_floor_return_details_json")]
        public String? shop_floor_return_details_json { get; set; }


    }
}
