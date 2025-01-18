
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class style_product_unit_DTO
    {

        public Int64? style_product_unit_id { get; set; }

        public String style_product_unit_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? unit_type { get; set; }
    }
}
