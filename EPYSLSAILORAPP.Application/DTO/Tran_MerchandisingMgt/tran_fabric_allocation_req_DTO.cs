
using BDO.Core.Base;
using EPYSLSAILORAPP.Domain.RPC;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_fabric_allocation_req_DTO : BaseEntity
    {

        public Int64? tran_fabric_allocation_req_id { get; set; }
        public string? allocation_number { get; set; }
        public DateTime? allocation_date { get; set; }
        public Int64? added_by { get; set; }
        public Int64? updated_by { get; set; }
        public DateTime? date_added { get; set; }
        public DateTime? date_updated { get; set; }
        public string? remarks { get; set; }
        public Int64? is_submitted { get; set; }

        public Int64? submitted_by { get; set; }

        public Int64? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approved_date { get; set; }

        public String? detl_list { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        public Int64? is_transfer { get; set; }
        public List<rpc_sp_get_mapped_segment_by_gen_item_structure_group_sub_id_DTO>?  ListCombination {  get; set; }
        public List<tran_fabric_allocation_req_det_DTO>? List_Det { get; set; }
    }
}
