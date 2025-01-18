
using BDO.Core.Base;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt
{
    public class tran_sample_order_DTO : BaseEntity
    {
        public long? row_index { get; set; }

        public long? tran_va_plan_detl_style_id { get; set; }
        public long? tran_sample_order_id { get; set; }
        public long? tran_va_plan_detl_id { get; set; }
        public string tran_sample_order_number { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime? order_date { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime? delivery_date { get; set; }
        public string style_code { get; set; }

        [Required(ErrorMessage = "Required")]
        public long? delivery_unit_id { get; set; }
        public string str_delivery_unit { get; set; }
        public long? order_quantity { get; set; }
        public long? designer_member_id { get; set; }

        public string team_member_marketing_name { get; set; }
        public string? remarks { get; set; }

        public bool? is_submit { get; set; }

        public bool? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? date_approved { get; set; }

        public long? added_by { get; set; }

        public string proc_sp_get_pre_costing_New { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }
        public string sample_photos { get; set; }

        public string designer { get; set; }

        public string style_product { get; set; }

        public string style_product_details { get; set; }

        public string style_embellishment_ids { get; set; }

        public List<rpc_sp_get_mapped_item_structure_DTO> List_Mapped_Structure { get; set; }

        public List<gen_item_structure_group_sub_DTO> List_Structure_det { get; set; }

        public List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO> List_Color_detl_size { get; set; }

        public List<SelectListItem> List_Unit { get; set; }


        public List<file_upload> List_base64String_File { get; set; }

        public List<tran_sample_order_subgroup_DTO> List_SubGroup { get; set; }

        public List<tran_sample_order_detl_DTO> List_Detl { get; set; }

        public List<tran_sample_order_embellishment_DTO> List_Embellishmet { get; set; }

        public string fit_name { get; set; }

        public string pattern { get; set; }

        public style_fit_info_DTO obj_fit_name { get; set; }

        public style_pattern_DTO obj_pattern { get; set; }
        public Int64? fiscal_year_id { get; set; }
        public Int64? event_id { get; set; }

        public string so_detl { get; set; }
        public string embellishment_detl { get; set; }
        public string subgroup_detl { get; set; }

    }
}
