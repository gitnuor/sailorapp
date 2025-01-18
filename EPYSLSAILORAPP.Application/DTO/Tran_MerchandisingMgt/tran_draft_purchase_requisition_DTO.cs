
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Application.DTO;
using Newtonsoft.Json.Linq;

using System.ComponentModel.DataAnnotations;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_draft_purchase_requisition_DTO
    {
        public long? dpr_id { get; set; }
        public long item_structure_group_id { get; set; }

        [Required]
        public string dpr_no { get; set; }

        public DateTime? dpr_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public long? event_id { get; set; }

        public long? techpack_id { get; set; }

        public long? designer_id { get; set; }
        public string designer_name { get; set; }

        public long? merchandiser_id { get; set; }
        public string merchandiser_name { get; set; }
        public string techpack_name { get; set; }

        public long? currency_id { get; set; }

        public long? delivery_unit_id { get; set; }
        public string? delivery_unit_name { get; set; }

        public string delivery_unit_address { get; set; }

        public string remarks { get; set; }

        public long? supplier_id { get; set; }

        public string supplier_address { get; set; }
        public string supplier_name { get; set; }

        public string supplier_concern_person { get; set; }

        public string terms_condition_list { get; set; }

        public string test_requirements_list { get; set; }

        public string document_list { get; set; }

        public bool? is_submitted { get; set; }

        public bool? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public string approve_remarks { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }
        public long gen_item_structure_group_id { get; set; }
        public DateTime? date_updated { get; set; }
        public dropdown_entity ddlsupplier_info { get; set; }
        public dropdown_TechpackEntity ddlTechpack_info { get; set; }
        public List<file_upload> List_Files { get; set; }
        public List<tran_purchase_requisition_dtl_DTO> details { get; set; }
        public List<string> DeleteList { get; set; }

        public bool? is_acknowledged { get; set; }
        public long? acknowledged_by { get; set; }
        public DateTime? acknowledged_date { get; set; }
        public string acknowledged_remarks { get; set; }

        public string detl_list { get; set; }

    }
}
