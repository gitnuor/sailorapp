
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_mcd_receive_DTO : BaseEntity
    {

        public long? received_id { get; set; }

        public String receive_no { get; set; }

        public Int64? po_id { get; set; }
        public string? po_no { get; set; }
        [Required]
        public DateTime? po_date { get; set; }

        public Int64? supplier_id { get; set; }

        public string delivery_unit { get; set; }

        public string factory_address { get; set; }
        public string delivery_address { get; set; }
        [Required]
        public DateTime? arrival_date { get; set; }
        [Required]
        public DateTime? receive_date { get; set; }

        public String del_chalan_no { get; set; }
        [Required]
        public DateTime? del_chalan_date { get; set; }
        public String tran_mode { get; set; }
        public String transport_type { get; set; }

        public long? tollerence { get; set; }

        public String vehical_no { get; set; }

        public String driver_name { get; set; }
        public String delevary_status { get; set; }

        public String lc_no { get; set; }

        public DateTime? lc_date { get; set; }

        public String invoice_no { get; set; }

        public DateTime? invoice_date { get; set; }
        [JsonProperty("documents")]
        public string documents { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public long? approved_by { get; set; }

        public DateTime? date_approved { get; set; }

        public long gen_item_structure_group_id { get; set; }
        public long item_structure_group_id { get; set; }

        public String po_status { get; set; }
        public List<file_upload> List_Files { get; set; }
        public List<tran_mcd_receive_detail_DTO> details { get; set; }
        public List<gen_item_stock_master_det_DTO> rackDetail { get; set; }
        public string supplier_name { get; set; }
        [Required]
        public dropdown_entity ddldeliverystatus { get; set; }
        [Required]
        public dropdown_entity ddltranmode { get; set; }
        [Required]
        public dropdown_entity ddlTransport { get; set; }

        public decimal po_quantity { get; set; }
        public String po_unit { get; set; }
        public String contact_person { get; set; }

        public Int64? event_id { get; set; }
        public Int64? fiscal_year_id { get; set; }
        public Int64? is_submitted { get; set; }
        public Int64? is_acknowledged { get; set; }

        public String name { get; set; }

        public List<string> DeleteList { get; set; }

        public String? tran_mcd_receive_json { get; set; }

        public String? unit_name { get; set; }
        public String unit_address { get; set; }
        public String? item_name { get; set; }
        public String? item_spec { get; set; }

        public string delivery_status_type { get; set; }
        public string transaction_mode_type { get; set; }
        public List<tran_mcd_receive_detail_DTO> unmatchingDetails { get; set; }

        public string? tran_revise_po_details { get; set; }
       


    }

    public class tran_mcd_receive_transport_DTO
    {
        public long? transport_id { get; set; }
        public String transport_type { get; set; }

    }

    public class gen_transaction_mode_DTO
    {
        public Int64? transaction_mode_id { get; set; }
        public String transaction_mode_type { get; set; }

    }

    public class gen_delivery_status_DTO
    {
        public Int64? delivery_status_id { get; set; }
        public String delivery_status_type { get; set; }

    }




}
