
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using Microsoft.AspNetCore.Mvc.Rendering;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class style_item_product_DTO
    {
        [PrimaryKey("style_item_product_id", false)]
        public Int64? style_item_product_id { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public String style_item_product_name { get; set; }

        public Int64? style_item_type_id { get; set; }

        public Int64? style_product_type_id { get; set; }

        public Int64? style_item_origin_id { get; set; }

        public Int64? style_gender_id { get; set; }

        public Int64? style_master_category_id { get; set; }

        public Int64? style_product_size_group_id { get; set; }

        public string product_sub_category_json { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public List<style_item_product_entity> List { get; set; }

        public List<SelectListItem> style_item_type_List { get; set; }

        public List<SelectListItem> style_product_type_List { get; set; }

        public List<SelectListItem> style_item_origin_List { get; set; }

        public List<SelectListItem> style_gender_List { get; set; }

        public List<SelectListItem> style_master_category_List { get; set; }

        public List<SelectListItem> style_product_size_group_List { get; set; }

        public List<SelectListItem> gen_fiscal_year_List { get; set; }

        public style_item_type_DTO style_item_type { get; set; }

        public style_product_type_DTO style_product_type { get; set; }

        public style_item_origin_DTO style_item_origin { get; set; }

        public style_gender_DTO style_gender { get; set; }

        public style_master_category_DTO style_master_category { get; set; }

        public style_product_size_group_DTO style_product_size_group { get; set; }
        public Int64? RecordTotal { get; set; }

        public List<style_item_product_sub_category_DTO> DetList { get; set; }
    }
}
