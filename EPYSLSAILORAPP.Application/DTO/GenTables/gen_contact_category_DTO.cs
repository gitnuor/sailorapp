using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.GenTables
{

    public class gen_contact_category_DTO
    {

        public long? gen_contact_category_id { get; set; }

        public string contact_category { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
