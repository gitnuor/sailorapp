
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_va_plan_detl_duration_plan")]
    public class tran_va_plan_detl_duration_plan_entity : DapperExt
    {

        [Key]
        public Int64? tran_va_plan_detl_duration_plan_id { get; set; }

        public Int64? tran_va_plan_detl_id { get; set; }

        public DateTime? from_date { get; set; }

        public DateTime? to_date { get; set; }

        public Int64? duration_quantity { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
