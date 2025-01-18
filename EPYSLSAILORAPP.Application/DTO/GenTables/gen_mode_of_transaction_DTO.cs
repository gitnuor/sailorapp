
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_mode_of_transaction_DTO
    {

        public Int64? gen_mode_of_transaction_id { get; set; }

        public String mode_of_transaction { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public bool? is_checked { get; set; }
    }
}
