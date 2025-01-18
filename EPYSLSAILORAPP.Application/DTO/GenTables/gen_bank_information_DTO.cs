using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.GenTables
{

    public class gen_bank_information_DTO
    {

        public long? gen_bank_information_id { get; set; }

        public string banke_name { get; set; }

        public string ac_number { get; set; }

        public string swift_code { get; set; }

        public string routing_no { get; set; }

        public string bftin_no { get; set; }

        public bool? is_default { get; set; }

        public long? gen_general_information_id { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
