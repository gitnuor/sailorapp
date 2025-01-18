using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.GenTables
{

    public class gen_credit_period_DTO
    {

        public long? gen_credit_period_id { get; set; }

        public string credit_period { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
