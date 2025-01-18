
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class style_product_size_group_details_DTO
    {

        public Int64? style_product_size_group_detid { get; set; }

        public Int64? style_product_size_group_id { get; set; }

        public String style_product_size { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? current_state { get; set; }
    }
}
