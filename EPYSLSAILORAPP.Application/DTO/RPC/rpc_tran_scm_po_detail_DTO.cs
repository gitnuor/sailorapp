using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_scm_po_detail_DTO
    {


        public Int64? po_id { get; set; }
        public Int64? event_id { get; set; }

        public String po_no { get; set; }
        public String suggested_supplier_name { get; set; }
        public String selected_supplier_name { get; set; }
        public String delivery_unit_name { get; set; }
        public String techpack_number { get; set; }
        public String pr_no { get; set; }

        public DateTime? po_date { get; set; }

        public DateTime? delivery_start_date { get; set; }

        public DateTime? delivery_end_date { get; set; }

        public dropdown_entity ddlsupplier_info { get; set; }
        public Int64? supplier_id { get; set; }
        public Int64? item_structure_group_id { get; set; }
        public Int64? measurement_unit_detail_id_val { get; set; }


        public Int64? delivery_unit { get; set; }


        public Int64? delivery_address { get; set; }


        public Int64? currency_id { get; set; }

        public string documents { get; set; }

        public string terms_conditions { get; set; }

        public int? is_submitted { get; set; }


        public Int32? status { get; set; }

        public Int64? added_by { get; set; }
        public Int64? pr_id { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public int? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approved_date { get; set; }
        // public List<tran_purchase_requisition_dtl_DTO> details { get; set; }
        public string po_details { get; set; }
        public string list_po_details { get; set; }
        public List<tran_scm_po_details_DTO> List_po_details { get; set; }

        public List<file_upload> List_Files { get; set; }
        public List<string> DeleteList { get; set; }

        public string supplier_name { get; set; }
        public string deliveryAddress { get; set; }

        public Decimal? receive_quantity { get; set; }
        // public List<string> documents { get; set; }
        public Decimal? recevQty { get; set; }
        public DateTime? arrival_date { get; set; }

        public DateTime? receive_date { get; set; }
        public String receive_no { get; set; }

        public long gen_item_structure_group_id { get; set; }
        public String del_chalan_no { get; set; }
        public DateTime? del_chalan_date { get; set; }
        public String tran_mode { get; set; }
        public String transport_type { get; set; }

        public long? tollerence { get; set; }

        public String vehical_no { get; set; }

        public String driver_name { get; set; }
        public String delevary_status { get; set; }
        public dropdown_entity ddldeliverystatus { get; set; }
        
        public dropdown_entity ddltranmode { get; set; }
     
        public dropdown_entity ddlTransport { get; set; }
        public String name { get; set; }
        public String unit_name { get; set; }
        public String unit_address { get; set; }
        public String factory_address { get; set; }
        
       public String contact_person { get; set; }

      

    }
}
