
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.SupplyChain
{

    [System.ComponentModel.DataAnnotations.Schema.Table("tran_purchase_requisition")]
    public class tran_purchase_requisition_entity : DapperExt
    {

        [Key]
        public long? pr_id { get; set; }
        public long? gen_item_structure_group_id { get; set; }

        [Column("pr_no")]
        public string pr_no { get; set; }

        public DateTime? pr_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public long? event_id { get; set; }

        public long? techpack_id { get; set; }

        public long? designer_id { get; set; }

        public long? merchandiser_id { get; set; }

        public long? currency_id { get; set; }

        public long? delivery_unit_id { get; set; }

        public string delivery_unit_address { get; set; }

        public string remarks { get; set; }

        public long? supplier_id { get; set; }

        public string supplier_address { get; set; }

        public string supplier_concern_person { get; set; }

        public string terms_condition_list { get; set; }

        public string test_requirements_list { get; set; }

        public string document_list { get; set; }

        public int? is_submitted { get; set; }

        public int? is_approved { get; set; }
      

        public long? approved_by { get; set; }

        
        public DateTime? approve_date { get; set; }
       

        public string approve_remarks { get; set; }
       
        

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }
        public bool? is_acknowledged { get; set; }
        public long? acknowledged_by { get; set; }
        public DateTime? acknowledged_date { get; set; }
        public string acknowledged_remarks { get; set; }
        public string del_list { get; set; }

    }
}
