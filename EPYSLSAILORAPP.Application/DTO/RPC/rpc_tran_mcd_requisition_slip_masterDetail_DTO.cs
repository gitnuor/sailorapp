
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class rpc_tran_mcd_requisition_slip_masterDetail_DTO : BaseEntity
    {

        [Required]
        [Column("requisition_slip_id")]
        public Int64? requisition_slip_id { get; set; }

        [Required]
        [Column("requisition_slip_no")]
        public string? requisition_slip_no { get; set; }

        [Column("requisition_date")]
        public DateTime? requisition_date { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        public string techpack_number { get; set; }
        public string name { get; set; }

        [Column("requisition_by")]
        public Int64? requisition_by { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        public JArray? document_list { get; set; }

        [Column("is_submitted")]
        public Int64? is_submitted { get; set; }

        [Column("is_approved")]
        public Int64? is_approved { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

        [Column("approve_date")]
        public DateTime? approve_date { get; set; }

        [Column("approve_remarks")]
        public string? approve_remarks { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("gen_item_structure_group_id")]
        public Int64? gen_item_structure_group_id { get; set; }

        [Column("gen_item_structure_group_sub_id")]
        public Int64? gen_item_structure_group_sub_id { get; set; }

        [Column("is_acknowledged")]
        public Boolean? is_acknowledged { get; set; }

        [Column("acknowledged_by")]
        public Int64? acknowledged_by { get; set; }

        [Column("acknowledged_date")]
        public DateTime? acknowledged_date { get; set; }

        [Column("acknowledged_remarks")]
        public string? acknowledged_remarks { get; set; }
        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }
        public List<tran_mcd_requisition_slip_detail_DTO> details { get; set; }
        public string location_name { get; set; }

        public string tran_mcd_requisition_slip_detail_list { get; set; }
        public string sub_group_dropdown_list { get; set; }
        public dropdown_entity sub_group_dropdown { get; set; }
        public String color_code { get; set; }


    }
}
