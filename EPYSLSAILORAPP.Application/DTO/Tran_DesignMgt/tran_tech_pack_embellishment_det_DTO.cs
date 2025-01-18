
using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_tech_pack_embellishment_det_DTO
    {

        public Int64? tran_tech_pack_embellishment_det_id { get; set; }

        public Int64? tran_tech_pack_embellishment_info_id { get; set; }

        public Int64? gen_garment_part_id { get; set; }

		public string? sub_process_name { get; set; }

		public string? garment_part_name { get; set; }

		public Int64? gen_process_master_detail_id { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        /// <summary>
       // [JsonProperty("embellishment_details")]
        /// </summary>
        public List<tran_tech_pack_embellishment_det_DTO> embellishment_details { get; set; }

        public List<tran_tech_pack_embellishment_det_Color_DTO> Color_Details { get; set; }


    }

    public class tran_tech_pack_embellishment_det_Color_DTO
    {
        public Int64? tran_va_plan_detl_style_color_id { get; set; }
        public string color_code { get; set; }

        public bool status { get; set; }
    }


}
