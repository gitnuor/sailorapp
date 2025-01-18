
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_mcd_requisition_slip_DTO
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

        public String measurement_unit { get; set; }
        public String available_stock_quantity { get; set; }
        
        public String booking_quantity { get; set; }

        public String sub_group_name { get; set; }
        public string remarks { get; set; }
        public string remarksDtl { get; set; }
        public Decimal requisition_quantity { get; set; }
        public Decimal total_requisition_quantity { get; set; }
        public Decimal available_stock_quantity_requsition { get; set; }
        public Decimal total_received_quantity { get; set; }
        public String color_code { get; set; }

        public DateTime requisition_date { get; set; }
        public Int64 requisition_by { get; set; }

        public List<tran_mcd_requisition_slip_detail_DTO> details { get; set; }

        public String segment_header1 { get; set; }
        public String segment_header2 { get; set; }
        public String segment_header3 { get; set; }
        public String segment_header4 { get; set; }
        public String segment_header5 { get; set; }
        public String segment_header6 { get; set; }
        public String segment_header7 { get; set; }
        public String segment_header8 { get; set; }
        public String segment_header9 { get; set; }
        public String segment_header10 { get; set; }
        public String segment_header111 { get; set; }
        public String segment_header12 { get; set; }
        public String segment_header13 { get; set; }

        public String segment_header14 { get; set; }

        public String segment_header15 { get; set; }

        public String item_name { get; set; }


    }
}
