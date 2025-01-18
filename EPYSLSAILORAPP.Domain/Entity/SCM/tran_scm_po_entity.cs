using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_scm_po")]

    public class tran_scm_po_entity : DapperExt
    {

        [Key]
        public Int64? po_id { get; set; }
        public Int64? pr_id { get; set; }
        public Int64? event_id { get; set; }
        public Int64? fiscal_year_id { get; set; }
        public Int64? item_structure_group_id { get; set; }

        [Column("po_no")]
        public String po_no { get; set; }

        public DateTime? po_date { get; set; }

        public DateTime? delivery_start_date { get; set; }

        public DateTime? delivery_end_date { get; set; }

        public Int64? supplier_id { get; set; }

        public Int64? delivery_unit { get; set; }

        public Int64? delivery_address { get; set; }

        public Int64? currency_id { get; set; }

        public string documents { get; set; }
        [JsonProperty("po_details")]
        public string po_details { get; set; }

        public String terms_conditions { get; set; }

        public int? is_submitted { get; set; }

        public Int32? status { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


        public int? is_approved { get; set; }
        public int? is_bill_submitted { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approved_date { get; set; }


        public Decimal vat_amount { get; set; }
        public Decimal discount_amount { get; set; }
        public Decimal final_amount { get; set; }

        public string supplier_concern_person { get; set; }
        public string supplier_address { get; set; }
        public string? status_remarks { get; set; }
    }
}
