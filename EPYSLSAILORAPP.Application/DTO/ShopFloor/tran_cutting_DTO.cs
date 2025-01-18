
using BDO.Core.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_cutting_DTO : BaseEntity
    {

        [Required]
        [Column("tran_cutting_id")]
        public Int64 tran_cutting_id { get; set; }

        [Column("tran_pp_meeting_id")]
        public Int64? tran_pp_meeting_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("style_code")]
        public string? style_code { get; set; }

        [Column("all_processes")]
        public string? all_processes { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }
        public string? techpack_number { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }
        public DateTime delivery_date { get; set; }
        public string teckpack_style_code { get; set; }
        public string size_wise_color_quantity { get; set; }

        public List<size_wise_color_quantity_DTO> size_wise_color { get; set; }
        public List<color_quantity_DTO> color_quantity { get; set; }
        //for insertion 
        public tran_cutting_color_DTO cutting_Color { get; set; }

        public List<tran_cutting_color_DTO> cutting_color_List { get; set; }

        [JsonProperty("tran_cutting_color_json")]
        public string tran_cutting_color_json { get; set; }
        public Int64 total_rows { get; set; }
    }
    public class size_wise_color_quantity_DTO
    {
        public decimal quantity { get; set; }
        public string color_code { get; set; }
        public string size { get; set; }

    }
    public class color_quantity_DTO
    {
        public decimal quantity { get; set; }
        public string color_code { get; set; }


    }
    public class tran_cutting_insert_DTO : BaseEntity
    {

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }
        public Int64? tran_pp_meeting_id { get; set; }
        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("style_code")]
        public string? style_code { get; set; }

        [Column("all_processes")]
        public string? all_processes { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }
        public string? techpack_number { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }
        public tran_insert_cutting_color_DTO cutting_Color { get; set; }
        public tran_insert_batch_DTO batch_info { get; set; }
        public List<tran_insert_bundle_DTO> bundles { get; set; }
        public List<tran_insert_garment_part_DTO> garment_parts { get; set; }
        public List<tran_insert_garment_part_details_DTO> garment_part_details { get; set; }
        public List<tran_cutting_color_batch_size_summery> tran_cutting_color_batch_size_summery { get; set; }

        public JArray bundlesJarry { get; set; }
        public JArray FabricJarry { get; set; }
        public JArray garment_partsJarry { get; set; }
        public JArray garment_part_detailsJarry { get; set; }
        public JArray tran_cutting_color_batch_size_summeryJarry { get; set; }
    }

    public class fabric_details_DTO
    {
        public string item_name { get; set; }
        public long gen_item_master_id { get; set; }
        public long measurement_unit_detail_id { get; set; }
        public string measurement_unit { get; set; }
        public decimal booking_quantity { get; set; }
        public decimal issue_quantity { get; set; }
    }

    public class cutting_details_DTO
    {
        public Int64? techpack_id { get; set; }
        public string techpack_garment_parts { get; set; }
        public List<garment_part> techpack_garment_part_list { get; set; }

        public string all_garment_parts { get; set; }
        public string size_text { get; set; }
        public List<sizes> size_list { get; set; }

    }

    public class garment_part
    {
        public long gen_garment_part_id { get; set; }
        public string garment_part_name { get; set; }

    }
    public class sizes
    {
        public string? techpack_number { get; set; }
        public long style_product_size_group_detid { get; set; }
        public string size { get; set; }
        public long total_output_quantity { get; set; }

    }

    public class fabric_detail
    {
        public string item_id { get; set; }
        public string item_name { get; set; }
        public string measurement_unit_detail_id { get; set; }
        public string measurement_unit { get; set; }
        public string booking_quantity { get; set; }
        public string issue_quantity { get; set; }
        public string input_quantity { get; set; }
        public string no_of_lay { get; set; }
    }

    public class tran_insert_batch_DTO
    {
        public long batch_no { get; set; }
        public long measurement_unit_detail_id { get; set; }
        public string measurement_unit { get; set; }
        public DateTime cutting_date_start { get; set; }
        public DateTime cutting_date_end { get; set; }
        public decimal total_booking_quantity { get; set; }
        public decimal total_receive_quantity { get; set; }
        public decimal total_input_quantity { get; set; }
        public long total_no_of_lay { get; set; }
        public long is_contrast { get; set; }
        public long is_hand_cut { get; set; }
        public string pattern_type { get; set; }
        public string marker_type { get; set; }
        public List<fabric_detail> details { get; set; }

    }
    public class tran_cutting_batch_dropdown
    {
        public string batch_no { get; set; }
        public string batch_summery_string{ get; set; }
        public List<tran_cutting_color_batch_size_summery> batch_summery{ get; set; }
        public long tran_cutting_color_batch_id { get; set; }
    }

    public class tran_cutting_color_batch_size_summery
    {
        public long style_product_size_group_detid { get; set; }
        public long total_quantity { get; set; }
        public string size_name { get; set; }

    }
    public class tran_insert_cutting_color_DTO
    {
        public string color_code { get; set; }
        public long allocated_unit_id { get; set; }
        public string allocated_unit { get; set; }
        public long total_quantity { get; set; }
        public long total_cut { get; set; }
    }

    public class tran_insert_garment_part_DTO
    {
        public long gen_garment_part_id { get; set; }
        public string garment_part_name { get; set; }
        public long batch_no { get; set; }
    }
    public class tran_insert_bundle_DTO
    {
        public long batch_no { get; set; }
        public long gen_garment_part_id { get; set; }
        public long style_product_size_group_detid { get; set; }
        public string bundle_number { get; set; }
        public long bundle_quantity { get; set; }
    }
    public class tran_insert_garment_part_details_DTO
    {
        public long batch_no { get; set; }
        public long gen_garment_part_id { get; set; }
        public string style_size { get; set; }
        public long style_product_size_group_detid { get; set; }
        public decimal ratio { get; set; }
        public long output_quantity { get; set; }
    }




}
