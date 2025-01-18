
using BDO.Core.Base;
using EPYSLSAILORAPP.Domain.DTO;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_pre_costing_details_by_masterid_DTO :BaseEntity
    {
        public Int64? row_index { get; set; }

        public Int64? item_structure_group_id { get; set; }
        public String measurement_unit { get; set; }

        public String sub_group_name { get; set; }

        public String structure_group_name { get; set; }

        public string all_segment_text { get; set; }

        public Int64? tran_pre_costing_item_detail_id { get; set; }

        public Int64? tran_pre_costing_id { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

        public Int64? segment_det1_id { get; set; }

        public Int64? segment_det2_id { get; set; }

        public Int64? segment_det3_id { get; set; }

        public Int64? segment_det4_id { get; set; }

        public Int64? segment_det5_id { get; set; }

        public Int64? segment_det6_id { get; set; }

        public Int64? segment_det7_id { get; set; }

        public Int64? segment_det8_id { get; set; }

        public Int64? segment_det9_id { get; set; }

        public Int64? segment_det10_id { get; set; }

        public Int64? segment_det11_id { get; set; }

        public Int64? segment_det12_id { get; set; }

        public Int64? segment_det13_id { get; set; }

        public Int64? segment_det14_id { get; set; }

        public Int64? segment_det15_id { get; set; }

        public String segment_det1_text { get; set; }

        public String segment_det2_text { get; set; }

        public String segment_det3_text { get; set; }

        public String segment_det4_text { get; set; }

        public String segment_det5_text { get; set; }

        public String segment_det6_text { get; set; }

        public String segment_det7_text { get; set; }

        public String segment_det8_text { get; set; }

        public String segment_det9_text { get; set; }

        public String segment_det10_text { get; set; }

        public String segment_det11_text { get; set; }

        public String segment_det12_text { get; set; }

        public String segment_det13_text { get; set; }

        public String segment_det14_text { get; set; }

        public String segment_det15_text { get; set; }

        public Int64? measurement_unit_detail_id { get; set; }

        public Decimal? order_quantity { get; set; }

        public Decimal? wastage { get; set; }

        public Decimal? total_order_quantity { get; set; }

        public Decimal? price_per_unit { get; set; }

        public Decimal? total_price { get; set; }

        public String remarks { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public JArray placement_info { get; set; }
        public decimal? color_quantity { get; set; }
        public string? color_code { get; set; }
        public List<style_placement_information_DTO> List_placement_info { get; set; }
    }
}
