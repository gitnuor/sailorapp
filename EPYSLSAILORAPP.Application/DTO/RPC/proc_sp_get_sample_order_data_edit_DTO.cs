
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_sp_get_sample_order_data_edit_DTO
    {
        public Int64? tran_sample_order_id {  get; set; }
        public Int64? tran_va_plan_detl_id { get; set; }

        public String tran_sample_order_number { get; set; }

        public DateTime? order_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public Int64? delivery_unit_id { get; set; }

        public Int64? order_quantity { get; set; }

        public Int64? designer_member_id { get; set; }

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

        public Int64? style_master_category_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? style_product_size_group_id { get; set; }

        public String designer_list { get; set; }

        public String mapping_structure_list { get; set; }

        public String gen_itemstructure_groupsub_list { get; set; }

        public String gen_unit_list { get; set; }

        public String style_product_unit_list { get; set; }

        public String style_product_sizegroupdetails_list { get; set; }

        public String sample_order_embellishmentlist { get; set; }

        public String sample_order_detaillist { get; set; }

        [JsonProperty("sample_photos")]
        public string sample_photos { get; set; }

        public List<file_upload>  List_SampleOrderFiles { get; set; }

        [JsonProperty("sampleorder_subgroup_list")]
        public string sampleorder_subgroup_list {  get; set; }

        [JsonProperty("style_fit_info_list")]
        public string style_fit_info_list { get; set; }

        [JsonProperty("style_pattern_list")]
        public string style_pattern_list { get; set; }

        public List<style_fit_info_DTO> obj_style_fit_info_list { get; set; }
        public List<style_pattern_DTO> obj_style_pattern_list { get; set; }

        public style_fit_info_DTO obj_style_fit_info { get; set; }
        public style_pattern_DTO obj_style_pattern { get; set; }

        public string pattern {  get; set; }
        public string fit_name { get;  set; }

    }

    public class rpc_proc_sp_get_sample_order_embellishment_DTO
    {

        public Int64? embellishment_id { get; set; }

        public Int64? tran_sample_order_embellishment_id { get; set; }

        public String process_name { get; set; }

        public Int64? gen_measurement_unit_id { get; set; }

        public Int64? measurement_unit_detail_id { get; set; }
    }
}
