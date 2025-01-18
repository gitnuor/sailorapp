
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_payment_method_DTO
    {

        public Int64? gen_payment_method_id { get; set; }

        public String payment_method { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public bool? is_checked { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
