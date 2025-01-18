
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_scm_po_test")]

    public class tran_scm_po_entity_test : DapperExt
    {

        [Key]
        public Int64 po_id { get; set; }

        [Column("po_no")]
        public string? po_no { get; set; }

        [Column("po_date")]
        public DateTime? po_date { get; set; }

        [Column("delivery_start_date")]
        public DateTime? delivery_start_date { get; set; }

        [Column("delivery_end_date")]
        public DateTime? delivery_end_date { get; set; }

        [Column("supplier_id")]
        public Int64? supplier_id { get; set; }

        [Column("delivery_unit")]
        public Int64? delivery_unit { get; set; }

        [Column("delivery_address")]
        public Int64? delivery_address { get; set; }

        [Column("currency_id")]
        public Int64? currency_id { get; set; }

        [Column("documents")]
        public String? documents { get; set; }

        [Column("terms_conditions")]
        public String? terms_conditions { get; set; }

        [Column("status")]
        public Int64? status { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("pr_id")]
        public Int64? pr_id { get; set; }

        [Column("item_structure_group_id")]
        public Int64? item_structure_group_id { get; set; }

        [Column("approved_date")]
        public DateTime? approved_date { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

        [Column("is_bill_submitted")]
        public Boolean? is_bill_submitted { get; set; }

        [Column("po_details")]
        public String? po_details { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("is_submitted")]
        public Int64? is_submitted { get; set; }

        [Column("is_approved")]
        public Int64? is_approved { get; set; }


    }
}
