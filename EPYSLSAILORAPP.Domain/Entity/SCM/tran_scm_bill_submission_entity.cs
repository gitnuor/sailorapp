
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_scm_bill_submission")]

    public class tran_scm_bill_submission_entity : DapperExt
    {

        [Key]
        public Int64? bill_submission_id { get; set; }

        public Int64? po_id { get; set; }

        public Int64? supplier_id { get; set; }

        public String bill_no { get; set; }

        public String challan_no { get; set; }

        public DateTime? challan_date { get; set; }

        public DateTime? bill_date { get; set; }

        public Decimal? total_po_amount { get; set; }

        public Decimal? loading_cost_in_percentage { get; set; }

        public Decimal? loading_cost { get; set; }

        public Decimal? transport_cost_in_percentage { get; set; }

        public Decimal? transport_cost { get; set; }

        public Decimal? discount_in_percentage { get; set; }

        public Decimal? discount_amount { get; set; }

        public Decimal? vat_in_percentage { get; set; }

        public Decimal? vat_amount { get; set; }

        public Decimal? total_value { get; set; }

        public Boolean? is_submitted { get; set; }
        public Boolean? is_send_for_approval { get; set; }

        public DateTime? submitted_date { get; set; }

        public Int64? submitted_by { get; set; }

        public Int64? is_approved { get; set; }

        public DateTime? approved_date { get; set; }

        public Int64? approved_by { get; set; }

        public Int32? status { get; set; }

        public String documents { get; set; }

        public String terms_conditions { get; set; }

        public String remarks { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
