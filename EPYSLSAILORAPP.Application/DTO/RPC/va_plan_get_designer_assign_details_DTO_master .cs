
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class va_plan_get_designer_assign_details_DTO_master
    {
        public List<rpc_va_plan_get_designer_assign_details_DTO> List_Products { get; set; }

        public List<rpc_va_plan_get_designer_assign_details_det_DTO> List_Designer_Assign { get; set; }

        public List<rpc_va_plan_get_designer_assign_details_det_DTO> List_Unique_Designer { get; set; }

        public List<owin_user_DTO> List_Designer { get; set; }

    }
}
