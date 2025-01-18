
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_sp_get_mcd_requisition_issue_DTO
    {

        public Int64? requisition_slip_id { get; set; }
        public Int64? tran_mcd_requisition_issue_id { get; set; }
        public String requisition_slip_no { get; set; }

        public String issue_no { get; set; }

        public DateTime? requisition_date { get; set; }

        public String remarks { get; set; }

        public Int64? gen_item_structure_group_id { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

        public Int64? techpack_id { get; set; }
        public DateTime? issue_date { get; set; }
        public String techpack_number { get; set; }

        public String requisitioner_name { get; set; }

        public String location_name { get; set; }
        public string issuer_name { get; set; }
        public bool is_submitted { get; set; }

        public Boolean? is_approved { get; set; }
        public bool is_full_issued { get; set; }

        public String tran_mcd_requisition_issue_detail_list { get; set; }

        public String sub_group_dropdown_list { get; set; }
        public String color_code { get; set; }


    }
}
