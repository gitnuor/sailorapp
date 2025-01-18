using BDO.Core.Base;
using EPYSLSAILORAPP.Domain.DTO;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt
{

    public class tran_pre_costing_item_detail_DTO : BaseEntity
    {
        public long? row_index { get; set; }
        public long? tran_pre_costing_item_detail_id { get; set; }

        public long? gen_item_structure_group_sub_id { get; set; }

        public long? tran_pre_costing_id { get; set; }

        public long? segment_det1_id { get; set; }

        public long? segment_det2_id { get; set; }

        public long? segment_det3_id { get; set; }

        public long? segment_det4_id { get; set; }

        public long? segment_det5_id { get; set; }

        public long? segment_det6_id { get; set; }

        public long? segment_det7_id { get; set; }

        public long? segment_det8_id { get; set; }

        public long? segment_det9_id { get; set; }

        public long? segment_det10_id { get; set; }

        public long? segment_det11_id { get; set; }

        public long? segment_det12_id { get; set; }

        public long? segment_det13_id { get; set; }

        public long? segment_det14_id { get; set; }

        public long? segment_det15_id { get; set; }

        public string? segment_det1_text { get; set; }

        public string? segment_det2_text { get; set; }

        public string? segment_det3_text { get; set; }

        public string? segment_det4_text { get; set; }

        public string? segment_det5_text { get; set; }

        public string? segment_det6_text { get; set; }

        public string? segment_det7_text { get; set; }

        public string? segment_det8_text { get; set; }

        public string? segment_det9_text { get; set; }

        public string? segment_det10_text { get; set; }

        public string? segment_det11_text { get; set; }

        public string? segment_det12_text { get; set; }

        public string? segment_det13_text { get; set; }

        public string? segment_det14_text { get; set; }

        public string? segment_det15_text { get; set; }

        public long? measurement_unit_detail_id { get; set; }

        public decimal? order_quantity { get; set; }

        public decimal? wastage { get; set; }

        public decimal? total_order_quantity { get; set; }

        public decimal? price_per_unit { get; set; }

        public decimal? total_price { get; set; }

        public string? remarks { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public JArray? placement_info { get; set; }

        public List<style_placement_information_DTO> List_placement_info { get; set; }
        public long? item_structure_group_id { get; set; }
        public String structure_group_name { get; set; }
        public string? sub_group_name { get; set; }

        public string? str_placement_info { get; set; }

        public string? all_segment_text { get; set; }

        public string? measurement_unit { get; set; }

        public string? construction_id { get; set; }

        public string? composition_id { get; set; }
        public string? color_code { get; set; }
        public decimal? color_quantity { get; set; }

    }
}
