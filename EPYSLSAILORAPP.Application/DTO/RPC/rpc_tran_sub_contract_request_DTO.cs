using EPYSLSAILORAPP.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.RPC
{
    public class rpc_tran_sub_contract_request_DTO
    {
       
        public Int64? tran_techpack_id { get; set; }
        public Int64? techpack_id { get; set; }
        public Int64? style_item_product_id { get; set; }
        public String style_item_product_category { get; set; }
        public String techpack_number { get; set; }
        public DateTime? techpack_date { get; set; }
        public String merchandiser_name { get; set; }
        public String designer_name { get; set; }
        public Int64? styleQuantity { get; set; }
        public String emb_Sub_process_type { get; set; }

        public List<size_wise_color_quantity_DTO> size_wise_color { get; set; }
        public string size_wise_color_quantity { get; set; }
        public List<color_quantity_DTO> color_quantity { get; set; }
        public string embellishment_detl { get; set; }
        public string supplier_info { get; set; }

        public dropdown_entity ddlsupplier_info { get; set; }

        public List<supplier_info_DTO> supplier_info_list { get; set; }

        public Int64 tran_production_process_id { get; set; }
        public DateTime? tran_production_process_date { get; set; }

        public Int64? supplier_id { get; set; }
        public string? name { get; set; }
        public string? factory_address { get; set; }
        public string? office_address { get; set; }
        public string? contact_person { get; set; }
        public string? color_code { get; set; }
        public decimal? color_qty { get; set; }
        public string? production_process_name { get; set; }
    }

   
}
