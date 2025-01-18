
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_va_plan_get_designer_assign_details_det_DTO
    {

        public Int64? style_item_product_id { get; set; }

        public Int64? designer_member_id { get; set; }

        public string team_member_marketing_name { get; set; }

        public String name { get; set; }

        public Int64? no_of_style { get; set; }

        public Int64? total_style_quantity { get; set; }

        public Int64? sum_no_of_style { get; set; }

        public Int64? sum_total_style_quantity { get; set; }


    }
}
