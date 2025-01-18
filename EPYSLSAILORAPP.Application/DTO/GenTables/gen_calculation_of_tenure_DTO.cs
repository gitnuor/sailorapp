using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.GenTables
{

    public class gen_calculation_of_tenure_DTO
    {

        public long? gen_calculation_of_tenure_id { get; set; }

        public string calculation_of_tenure { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
