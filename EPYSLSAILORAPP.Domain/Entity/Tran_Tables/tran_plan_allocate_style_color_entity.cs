
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_plan_allocate_style_color")]

    public class tran_plan_allocate_style_color_entity : DapperExt
    {

        [Key]
        public Int64? tran_va_plan_detl_style_color_id { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public String color_code { get; set; }

        public Int64? style_color_quantity { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public JArray style_color_size_details { get; set; }


    }
}
