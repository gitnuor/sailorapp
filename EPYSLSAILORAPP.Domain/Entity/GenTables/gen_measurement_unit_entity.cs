
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity
{
   
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_measurement_unit")]

    public class gen_measurement_unit_entity : DapperExt
    {

        public Int64? gen_measurement_unit_id { get; set; }

        public String unit_name { get; set; }

        public String remarks { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
