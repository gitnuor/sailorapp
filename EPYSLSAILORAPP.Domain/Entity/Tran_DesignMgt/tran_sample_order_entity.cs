
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_sample_order")]

    public class tran_sample_order_entity : DapperExt
    {

        [Key]
        public long? tran_sample_order_id { get; set; }

        public long? tran_va_plan_detl_style_id { get; set; }

        public long? tran_va_plan_detl_id { get; set; }

        public string tran_sample_order_number { get; set; }

        public DateTime? order_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public long? delivery_unit_id { get; set; }

        public long? order_quantity { get; set; }

        public long? designer_member_id { get; set; }

        public string remarks { get; set; }

        public bool? is_submit { get; set; }

        public bool? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? date_approved { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public string sample_photos { get; set; }

        public string fit_name { get; set; }

        public string pattern { get; set; }
        public Int64? fiscal_year_id { get; set; }
        public Int64? event_id { get; set; }

    }
}
