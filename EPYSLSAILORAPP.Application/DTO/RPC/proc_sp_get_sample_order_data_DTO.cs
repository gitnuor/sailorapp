
using EPYSLSAILORAPP.Domain.DTO;
using Postgrest.Attributes;
using Postgrest.Models;
namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_sp_get_sample_order_data_DTO
    {

        public String style_product_size_group_name { get; set; }

        public String style_item_product_name { get; set; }

        public String style_item_type_name { get; set; }

        public String style_product_type_name { get; set; }

        public String style_item_origin_name { get; set; }

        public String style_gender_name { get; set; }

        public String master_category_name { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? style_item_type_id { get; set; }

        public Int64? style_product_type_id { get; set; }

        public Int64? style_item_origin_id { get; set; }

        public Int64? style_gender_id { get; set; }

        public Int64? style_master_category_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? style_product_size_group_id { get; set; }

        public String designer_list { get; set; }

        public String mapping_structure_list { get; set; }

        public String gen_itemstructure_groupsub_list { get; set; }

        public String gen_unit_list { get; set; }

        public String style_product_unit_list { get; set; }

        public String style_product_sizegroupdetails_list { get; set; }

        public String colordetlsize_list { get; set; }

        public List<rpc_sp_get_color_detl_size_by_vaplandetlid_DTO> color_detl_size_List { get; set; }

        public List<owin_user_DTO> team_members_List { get; set; }

        public List<rpc_gen_itemstructure_group_sub> genitemstructuregroupsub_List { get; set; }

        public List<mapped_rpc_item_structure> mapped_structure_List { get; set; }

        public List<gen_unit_DTO> gen_unit_office_List { get; set; }

        public List<style_product_unit_DTO> style_product_unit_List { get; set; }

        public List<style_product_size_group_details_DTO> style_productsize_groupdetails_List { get; set; }

        public string style_fit_info_list { get; set; }
        public string style_pattern_list { get; set; }
        public List<style_fit_info_DTO> obj_style_fit_info_list { get; set; }
        public List<style_pattern_DTO> obj_style_pattern_list { get; set; }

    }

    public class mapped_rpc_item_structure
    {

        public Int64? item_structure_group_id { get; set; }

        public String structure_group_name { get; set; }

        public String sub_group_name { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

    }

    public class rpc_gen_itemstructure_group_sub
    {
        public Int64? gen_item_structure_group_sub_id { get; set; }
        public Int64? item_structure_group_id { get; set; }
        public string sub_group_name { get; set; }
        public Int64? measurement_unit_id { get; set; }
        public Int64? default_measurement_unit_detail_id { get; set; }
        public string unit_name { get; set; }
        public string unit_detail_title { get; set; }
        public string unit_detail_display { get; set; }
    }
}
