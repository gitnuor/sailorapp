
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_tech_pack_color")]

    public class tran_tech_pack_color_entity : DapperExt
    {

        [Key]
        public Int64? tran_tech_pack_color_id { get; set; }

        public Int64? tran_tech_pack_id { get; set; }

        public Int64? tran_va_plan_detl_style_color_id { get; set; }

        public String color_code { get; set; }

        public Int64? style_color_quantity { get; set; }

        public JArray size_details { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
