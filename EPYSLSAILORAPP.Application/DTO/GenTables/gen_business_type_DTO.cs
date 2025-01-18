using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.GenTables
{

    public class gen_business_type_DTO
    {

        public long? gen_business_type_id { get; set; }

        public string business_type { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
