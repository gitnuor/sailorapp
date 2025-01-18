using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_designer_review")]

    public class tran_designer_review_entity : DapperExt
    {

        [Key]
        public long? tran_designer_review_id { get; set; }

        public long? tran_pre_costing_id { get; set; }

        public long? no_review { get; set; }

        public long? no_confirmation { get; set; }

        public long? no_confirmation_with_modification { get; set; }

        public long? no_of_not_confirmed { get; set; }

        public long? status { get; set; }

        public string remarks { get; set; }

        public bool? is_submitted { get; set; }

        public bool? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public string approve_remarks { get; set; }

        public long? added_by { get; set; }
    
        public String document { get; set; }
        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }
    }
}
