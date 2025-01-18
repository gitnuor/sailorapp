
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_plan_allocate_style_DTO
    {

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public Int64? designer_member_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public String? style_code { get; set; }
        public String? trading_type_name { get; set; }

        public Int64? style_quantity { get; set; }

        public JArray? style_embellishment_ids { get; set; }

        public Int64? no_of_color { get; set; }

        public String? color_code_gen { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }
        public Int64? trading_type { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? is_delete { get; set; }

        public Int64? style_item_product_sub_category_id { get; set; }
        public List<tran_plan_allocate_style_color_DTO> List_ColorInfo{ get; set; }

        public JArray style_color_details { get; set; }

        public List<SelectListItem> List_ProductSubCategory { get; set; }

        public List<dropdown_entity> List_Embellishment {  get; set; }

        public Int64? is_submitted { get; set; }
        public Int64? is_approved { get; set; }
        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String? approve_remarks { get; set; }

        public Int64? tran_sample_order_id { get; set; }
    }
}
