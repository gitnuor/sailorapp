
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_fabric_allocation_req")]
    public class tran_fabric_allocation_req_entity : DapperExt
    {
        [Key]
        public Int64? tran_fabric_allocation_req_id { get; set; }

        public String allocation_number { get; set; }

        public DateTime? allocation_date { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public String remarks { get; set; }

        public String? detl_list { get; set; }

        public Int64? is_submitted { get; set; }

        public Int64? submitted_by { get; set; }

        public Int64? is_approved { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? approved_date { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        public Int64? is_transfer { get; set; }

    }
}
