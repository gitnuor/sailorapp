

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_plan_allocate")]
    public class tran_plan_allocate_entity : DapperExt
    {

        [Key]
        public Int64? tran_va_plan_detl_id { get; set; }

        public Int64? range_plan_detail_id { get; set; }

        public Int64? style_item_product_id { get; set; }

        public Int64? no_of_style { get; set; }

        public String style_code_gen { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public JArray style_details { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

    }
}
