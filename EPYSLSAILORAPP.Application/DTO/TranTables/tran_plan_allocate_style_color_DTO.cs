
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_plan_allocate_style_color_DTO
    {

        public Int64? tran_va_plan_detl_style_color_id { get; set; }

        public Int64? tran_va_plan_detl_style_id { get; set; }

        public String color_code { get; set; }

        public Int64? style_color_quantity { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }
        public JArray style_color_size_details { get; set; }

        public List<tran_plan_allocate_style_color_size_DTO> List_ColorSizeInfo { get; set; }

        public string strddlSizeHTML { get; set; }

        public string strddlUnitHTML { get; set; }

        public string txtcolor_code { get; set; }

        public string txtorder_quantity { get; set; }

        public string tran_color_size_detl_json { get; set; }
    }
}
