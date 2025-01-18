
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_mode_of_transaction")]

    public class gen_mode_of_transaction_entity : DapperExt
    {

        [Key]
        public Int64? gen_mode_of_transaction_id { get; set; }

        public String mode_of_transaction { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
