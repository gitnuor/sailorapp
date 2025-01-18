
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_payment_term_DTO
    {

        public Int64? gen_payment_term_id { get; set; }

        public String payment_term { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public bool? is_checked { get; set; }
    }
}
