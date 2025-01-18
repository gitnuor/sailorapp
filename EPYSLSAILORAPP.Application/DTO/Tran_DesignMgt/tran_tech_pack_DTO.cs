using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using System.Security.Cryptography.X509Certificates;

namespace EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt
{

    public class tran_tech_pack_DTO : tran_pre_costing_DTO
    {
      

        public long? tran_techpack_id { get; set; }
        public long? style_product_unit_id { get; set; }

        public long? tran_designer_review_id { get; set; }

        public string techpack_number { get; set; }
        public string style_product_unit_name { get; set; }

        public DateTime? techpack_date { get; set; }

        public decimal? costing_smv { get; set; }

        public string teckpack_style_code { get; set; }

        public string aop_style { get; set; }

        public long? merchandiser_id { get; set; }

        public string? production_availability_path { get; set; }

        public string? vat { get; set; }

        public string? photoshoot { get; set; }

        public string? e_com { get; set; }

        public string? sample_ok { get; set; }

        public string? follow_style { get; set; }

        public string? need_production_approval { get; set; }

        public string? iron { get; set; }
        public string? fabric_allocation { get; set; }
        public string? additional_comments { get; set; }
        public string photos { get; set; }

        public Int64? is_submitted { get; set; }

        public Int64? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approve_date { get; set; }
        public string approve_remarks { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public DateTime? delivery_date { get; set; }

        public List<tran_plan_allocate_style_color_DTO> Color_List { get; set; }
        public List<tran_tech_pack_embellishment_info_DTO> TechPack_EmbellishmentList { get; set; }

        public List<tran_tech_pack_color_DTO> TechPack_ColorList { get; set; }
        public List<tran_tech_pack_color_size_DTO> List_ColorSize { get; set; }

        public List<style_product_size_group_details_DTO> Color_Group_Details { get; set; }

        public List<file_upload> List_base64String_Techpack_File { get; set; }

        public string color_wise_size_quantity { get; set; }
        public Int64? tech_pack_costing_quantity { get; set; }

        public long? is_ack { get; set; }

        public DateTime? ack_date { get; set; }

        public rpc_proc_sp_get_tech_pack_data_edit_DTO rpc_proc_sp_get_tech_pack_data_edit_DTO { get; set; }
        public string? spi { get; set; }
        public string? product_composition { get; set; }
        public string? sleeve_details { get; set; }
        public style_product_composition_DTO obj_product_composition { get; set; }

        public style_sleeve_info_DTO obj_sleeve_details { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        public long? supplier_id { get; set; }
        public string supplier_concern_person { get; set; }

        public long? delivery_unit_id { get; set; }
        public string? delivery_unit_name { get; set; }

        public string delivery_unit_address { get; set; }

        public string process_name { get; set; }
        public Int64? gen_process_master_id { get; set; }
        public string techpack_embellishment_list_detail { get; set; }


        public string name { get; set; }

        public string contact_person { get; set; }


        public string supplier_info { get; set; }

        public dropdown_entity ddlsupplier_info { get; set; }
        public String color_code { get; set; }
        public Int64? style_color_size_quantity { get; set; }
        public long tran_techpack_id1 { get; set; }

        public string? style_item_product_name { get; set; }

        public Int64? style_item_product_id { get; set; }



    }
    public class rpc_proc_sp_get_techapack_details_for_sewing_DTO
    {

        public Int64? style_item_product_id { get; set; }

        public String style_item_product_name { get; set; }

        public Int64? userid { get; set; }

        public String name { get; set; }


    }
    public class rpc_proc_sp_get_colors_by_techpack_DTO
    {

        public String color_code { get; set; }


    }
}
