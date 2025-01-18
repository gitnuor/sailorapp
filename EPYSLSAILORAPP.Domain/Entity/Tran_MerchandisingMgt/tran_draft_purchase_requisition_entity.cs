using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_draft_purchase_requisition_dtl")]

    public class tran_draft_purchase_requisition_entity : DapperExt
    {

        [Key]
        public Int64? dpr_id { get; set; }

        [Column("dpr_no")]
        public String dpr_no { get; set; }

        public DateTime? dpr_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public Int64? event_id { get; set; }

        public Int64? techpack_id { get; set; }

        public Int64? designer_id { get; set; }

        public Int64? merchandiser_id { get; set; }

        public Int64? currency_id { get; set; }

        public Int64? delivery_unit_id { get; set; }

        public String delivery_unit_address { get; set; }

        public String remarks { get; set; }

        public Int64? supplier_id { get; set; }

        public String supplier_address { get; set; }

        public String supplier_concern_person { get; set; }

        public String terms_condition_list { get; set; }

        public String test_requirements_list { get; set; }

        public string document_list { get; set; }

        public Boolean? is_submitted { get; set; }

        public Boolean? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public String approve_remarks { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? gen_item_structure_group_id { get; set; }

        public Boolean? is_acknowledged { get; set; }

        public Int64? acknowledged_by { get; set; }

        public DateTime? acknowledged_date { get; set; }

        public String acknowledged_remarks { get; set; }

        public string detl_list { get; set; }


    }
}
