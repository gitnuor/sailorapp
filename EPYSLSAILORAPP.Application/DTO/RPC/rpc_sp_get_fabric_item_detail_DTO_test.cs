
using EPYSLSAILORAPP.Domain.DTO;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_pre_costing_item_detail_DTO
    {

        public Int64? tran_pre_costing_item_detail_id { get; set; }

        public Int64? tran_pre_costing_id { get; set; }
        public Int64? techpack_id { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

        public Int64? measurement_unit_detail_id { get; set; }

        public Decimal? order_quantity { get; set; }

        public Int64? gen_item_master_id { get; set; }

        public String all_segment_text { get; set; }
        public String segment_det1_text { get; set; }
        public String segment_det2_text { get; set; }
        public String segment_det3_text { get; set; }
        public String segment_det4_text { get; set; }

        public String measurement_unit { get; set; }
        public String available_stock_quantity { get; set; }
        public String booking_quantity { get; set; }

        public String sub_group_name { get; set; }
        public string remarks { get; set; }
        public string remarksDtl { get; set; }
        public Decimal requisition_quantity { get; set; }
        public DateTime requisition_date { get; set; }
        public Int64 requisition_by { get; set; }

        public List<tran_mcd_requisition_slip_detail_DTO> details { get; set; }


    }
}
