
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [Table("style_embellishment")]

    public class style_embellishment_entity : BaseModel
    {

        [PrimaryKey("style_embellishment_id", false)]
        public Int64? style_embellishment_id { get; set; }

        public String style_embellishment_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? measurement_unit_id { get; set; }
        public Int64? default_measurement_unit_detail_id { get; set; }


    }
}
