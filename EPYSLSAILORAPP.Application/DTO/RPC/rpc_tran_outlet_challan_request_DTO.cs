using EPYSLSAILORAPP.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.RPC
{
    public class rpc_tran_outlet_challan_request_DTO
    {
        public string? del_outlet_challan_no { get; set; }
        public DateTime? del_outlet_challan_date { get; set; }
        public Int64? outlet_id { get; set; }
        public Int64? transport_id { get; set; }
        public string vehicle_number { get; set; }
        public string driver_name { get; set; }
        public string driver_contact_no { get; set; }
        public string delivery_address{ get; set; }
        public string note { get; set; }
        public string SailorFactory { get; set; } = "Sailor Factory";
        public String merchandiser_name { get; set; }
        public String? style_item_product_name { get; set; }
        public String techpack_number { get; set; }
        public Int64? tran_techpack_id { get; set; }
        public Int64? techpack_id { get; set; }
        public string challan_details { get; set; }
        public string? color_code { get; set; }
        public string? style_code { get; set; }
        public Int64? style_product_size_group_detid { get; set; }
        public string? style_product_size { get; set; }
        public string? style_product_unit { get; set; }
        public string? distributed_quantity { get; set; }
        public string? mrp { get; set; }
        
        public Int64? style_item_product_id { get; set; }
        public string? barcode { get; set; }
    }

   
}
