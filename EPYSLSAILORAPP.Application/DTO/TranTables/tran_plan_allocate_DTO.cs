
using EPYSLSAILORAPP.Domain.RPC;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{
    public class tran_plan_allocate_DTO
    {
        public Int64? tran_va_plan_detl_id { get; set; }

        public Int64? tran_va_plan_id { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }
        public Int64? range_plan_detail_id { get; set; }
        public Int64? tran_range_plan_event_id { get; set; }

        public Int64? range_plan_id { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? no_of_style { get; set; }

        public String style_code_gen { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public List<tran_plan_allocate_style_DTO> List_style { get; set; }

        public JArray style_details { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        public List<owin_user_DTO> List_team_members { get; set; }

        public List<rpd_sp_get_style_group_size_by_fiscalyearid_DTO> List_product_size { get; set; }

        public List<rpc_va_plan_get_designer_assign_details_det_DTO> List_Designer_Assign { get; set; }


    }
}
