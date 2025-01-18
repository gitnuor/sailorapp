using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.DTO
{
    public class tran_cutting_color_DTO
    {
        public long?  tran_cutting_color_id { get; set; }
        public long?  tran_cutting_id { get; set; }
        public string? color_code { get; set; }
        public long?  allocated_unit_id { get; set; }
        public string? allocated_unit { get; set; }
        public long?  total_quantity { get; set; }
        public DateTime? delivery_date { get; set; }
        public long?  total_cut { get; set; }
        public string? remarks { get; set; }
        public long?  added_by { get; set; }
        public DateTime?  date_added { get; set; }
        public long?  updated_by { get; set; }
        public DateTime?  date_updated { get; set; }

        public long?  current_state { get; set; }

        [JsonProperty("tran_cutting_color_batch_json")]
        public string? tran_cutting_color_batch_json { get; set; }
        public List<tran_cutting_color_batch_DTO> batches { get; set; }
    }
    public class tran_cutting_color_batch_DTO
    {
        public long? tran_cutting_color_batch_id { get; set; }
        public long? tran_cutting_color_id { get; set; }
        public DateTime? cutting_date_start { get; set; }
        public DateTime? cutting_date_end { get; set; }
        public string? batch_no { get; set; }
        public long? uom_id { get; set; }
        public string? uom { get; set; }
        public decimal? total_booking_quantity { get; set; }
        public decimal? total_receive_quantity { get; set; }
        public decimal? total_input_quantity { get; set; }
        public long? total_no_of_lay { get; set; }
        public long? is_contrast { get; set; }
        public long? is_hand_cut { get; set; }
        public string? pattern_type { get; set; }
        public string? marker_type { get; set; }
        public string? fabric_details { get; set; }
        public string? part_details { get; set; }
        public string? remarks { get; set; }
        public long? added_by { get; set; }
        public DateTime? date_added { get; set; }
        public long? updated_by { get; set; }
        public DateTime? date_updated { get; set; }

        [JsonProperty("tran_cutting_color_batch_size_summery")]
        public string? tran_cutting_color_batch_size_summery { get; set; }

        public List<tran_cutting_color_batch_size_summery> color_batch_size_summery_List { get; set; } 

        public List<tran_cutting_color_batch_fabric_details_DTO> fabric { get; set; } //ntm
        public List<tran_cutting_color_batch_garment_part_DTO> garment_part { get; set; } //ntm
        public List<tran_cutting_color_batch_garment_part_bundle_DTO> bundle_list { get; set; }

        [JsonProperty("batch_garment_part_bundle_json")]
        public string? batch_garment_part_bundle_json { get; set; }

        [JsonProperty("tran_cutting_color_batch_garment_part")]
        public string? tran_cutting_color_batch_garment_part { get; set; }

        [JsonProperty("tran_cutting_color_batch_fabric_details_json")]
        public string? tran_cutting_color_batch_fabric_details_json { get; set; }

    }
    public class tran_cutting_color_batch_fabric_details_DTO
    {
        public long? tran_cutting_color_batch_fabric_details_id { get; set; }
        public long? tran_cutting_color_batch_id { get; set; }
        public long? gen_item_master_id { get; set; }
        public string? fabric_name { get; set; }
        public long? uom_id { get; set; }
        public string? uom { get; set; }
        public decimal? booking_quantity { get; set; }
        public decimal? receive_quantity { get; set; }
        public decimal? input_quantity { get; set; }
        public long? no_of_lay { get; set; }
        public string? remarks { get; set; }
        public long? added_by { get; set; }
        public DateTime? date_added { get; set; }
        public long? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
    }
    public class tran_cutting_color_batch_garment_part_DTO
    {
        public long? tran_cutting_color_batch_garment_part_id { get; set; }
        public long? tran_cutting_color_batch_id { get; set; }
        public long? gen_part_id { get; set; }
        public long? total_output_quantity { get; set; }
        public string? gen_part { get; set; }
        public string? batch_summary_size_info { get; set; }
        public string? remarks { get; set; }
        public long? added_by { get; set; }
        public DateTime? date_added { get; set; }
        public long? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
        public List<tran_cutting_color_batch_garment_part_details_DTO> garment_part_details { get; set; }
        [JsonProperty("tran_cutting_color_batch_garment_part_details_json")]
        public string? tran_cutting_color_batch_garment_part_details_json {  get; set; }
    }
    public class tran_cutting_color_batch_garment_part_details_DTO
    {
        public long? tran_cutting_color_batch_garment_part_details_id { get; set; }
        public long? tran_cutting_color_batch_garment_part_id { get; set; }
        public string? style_size { get; set; }
        public long? style_product_size_group_detid { get; set; }
        public decimal? ratio { get; set; }
        public long? output_quantity { get; set; }
        public string? remarks { get; set; }
        public long? added_by { get; set; }
        public DateTime? date_added { get; set; }
        public long? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
    }
    public class tran_cutting_color_batch_garment_part_bundle_DTO
    {
        public long? tran_cutting_color_batch_garment_part_bundle_id { get; set; }
        public long? tran_cutting_color_batch_garment_part_id { get; set; }
        public string? bundle_number { get; set; }
        public string? bundle_barcode { get; set; }
        public long? bundle_quantity { get; set; }
        public long? style_product_size_group_detid { get; set; }
        public long? gen_garment_part_id { get; set; }
        public string? batch_no { get; set; }
        public long? tran_cutting_color_batch_id { get; set; }
        public string? remarks { get; set; }
        public long? added_by { get; set; }
        public DateTime? date_added { get; set; }
        public long? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
    }


}
