
using BDO.Core.Base;
using Postgrest.Attributes;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_mcd_requisition_issue_DTO : BaseEntity
    {


        [Column("tran_mcd_requisition_issue_id")]
        public Int64? tran_mcd_requisition_issue_id { get; set; }


        [Column("issue_no")]
        public string issue_no { get; set; }
        public string requisitioner_name { get; set; }
        public string location_name { get; set; }
        public string issuer_name { get; set; }
        public string requisition_slip_no { get; set; }

        [Column("requisition_slip_id")]
        public Int64? requisition_slip_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("is_submitted")]
        public Boolean? is_submitted { get; set; }

        [Column("is_full_issued")]
        public Boolean? is_full_issued { get; set; }

        [Column("is_closed")]
        public Boolean? is_closed { get; set; }

        [Column("is_approved")]
        public Boolean? is_approved { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

        [Column("approve_date")]
        public DateTime? approve_date { get; set; }
        public DateTime? issue_date { get; set; }
        public DateTime? requisition_date { get; set; }

        [Column("approve_remarks")]
        public string? approve_remarks { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }
        public String techpack_number { get; set; }
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

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        public String? mcd_requisition_issue_details { get; set; }

        public List<tran_mcd_requisition_issue_details_DTO> details { get; set; }




    }

    public class requisition_issue_DTO
    {



        public Int64? tran_mcd_requisition_issue_id { get; set; }


        [Column("requisition_slip_id")]
        public Int64? requisition_slip_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("is_full_issued")]
        public Boolean? is_full_issued { get; set; }
        public List<tran_mcd_requisition_issue_details_DTO> details { get; set; }

        [Column("gen_item_structure_group_id")]
        public Int64? gen_item_structure_group_id { get; set; }

        [Column("gen_item_structure_group_sub_id")]
        public Int64? gen_item_structure_group_sub_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }


















    }
}
