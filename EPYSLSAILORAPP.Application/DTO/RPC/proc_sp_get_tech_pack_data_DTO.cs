
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_sp_get_tech_pack_data_DTO
    {
        public Int64? tran_designer_review_id {  get; set; }
        public Int64? tran_sample_order_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public String tran_sample_order_number { get; set; }

        public DateTime? order_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public Int64? delivery_unit_id { get; set; }

        public Int64? order_quantity { get; set; }

        public Int64? designer_member_id { get; set; }

        public string? sample_photos { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public String style_code { get; set; }

        public Int64? style_quantity { get; set; }

        public Int64? no_of_color { get; set; }

        public String color_code_gen { get; set; }

        public Int64? style_item_product_sub_category_id { get; set; }

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

        public string gen_garment_partlist { get; set; }

        public Int64? style_master_category_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? style_product_size_group_id { get; set; }

        //public String designer_list { get; set; }

        public String mapping_structure_list { get; set; }

        public String gen_itemstructure_groupsub_list { get; set; }

        public String gen_unit_list { get; set; }

        public String style_product_unit_list { get; set; }

        public String style_product_sizegroupdetails_list { get; set; }

        public String sample_order_embellishmentlist { get; set; }

        public String sample_order_detaillist { get; set; }

        public String sampleorder_subgroup_list { get; set; }

        public String color_detl_size_list { get; set; }

        public String name { get; set; }

        public String gen_process_master_list { get; set; }

        public DateTime? pre_costing_date { get; set; }

        public Decimal? total_raw_material_cost { get; set; }

        public Decimal? total_raw_material_percentage { get; set; }

        public Decimal? factory_overhead_cost { get; set; }

        public Decimal? sales_marketing_distribution_cost { get; set; }

        public Decimal? depreciation_amortization_cost { get; set; }

        public Decimal? total_overhead_cost { get; set; }

        public Decimal? total_production_cost { get; set; }

        public Decimal? floor_price_percentage { get; set; }

        public Decimal? floor_price_per_pc { get; set; }

        public Decimal? desired_markup_percentage { get; set; }

        public Decimal? estimated_markup_price { get; set; }

        public Decimal? desired_markup_price { get; set; }

        public Decimal? final_mrp { get; set; }

        public Decimal? total_style_quantity_mrp { get; set; }

        public Decimal? suggested_mrp_with_cost { get; set; }

        public String smv { get; set; }

        public String remarks { get; set; }

        public String precosting_colorwisesize_quantity { get; set; }

        public Int64? pre_costing_quantity { get; set; }

        public String pre_costing_detail_list { get; set; }

        public String pre_costing_embellishment_list { get; set; }

        public String pre_costing_subcontract_list { get; set; }

        public String style_color_details { get; set; }

        public String style_embellishment_ids { get; set; }

        public List<file_upload> List_SampleOrderFiles { get; set; }

        public string style_color_size_details { get; set; }

        public string gen_process_master_detail_list { get; set; }


        public string style_product_composition_list { get; set; }
        public string styles_leeve_info_list { get; set; }

        public List<style_product_composition_DTO> List_style_product_composition { get; set; }
        public List<style_sleeve_info_DTO> List_style_sleeve_info { get; set; }

        public string fit_name { get; set; }
        public string pattern { get; set; }

        public style_fit_info_DTO objt_fit_name { get; set; }
        public style_pattern_DTO obj_pattern { get; set; }
    }
}
