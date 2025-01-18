
using EPYSLSAILORAPP.Application.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_tech_pack_embellishment_info_DTO
    {

        public Int64? tran_tech_pack_embellishment_info_id { get; set; }

        public Int64? tran_tech_pack_id { get; set; }

        public Int64? gen_process_master_id { get; set; }

        public Int64? supplier_id { get; set; }

        public string supplier_info { get; set; }

        public dropdown_entity ddlsupplier_info { get; set; }

        public String is_garment_form { get; set; }

		public String garment_part_name { get; set; }

		public String sub_process_name { get; set; }

        //[JsonProperty("embellishment_details")]
		public string embellishment_details { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public List<tran_tech_pack_embellishment_det_DTO> EmbelshmentDet_List { get; set; }
        public string process_name { get; set; }
        public String color_code { get; set; }
		
		public string style_color_size_quantity { get; set; }
        public string color_wise_size_quantity { get; set; }

        public List<tran_tech_pack_color_size_DTO> List_ColorSize { get; set; }
        public string contact_person { get; set; }
        public Int64? is_workorder { get; set; }
        public string gen_garment_part_id { get; set; }

    }
}
