using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.GenTables
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_credit_period")]

    public class gen_credit_period_entity : DapperExt
    {

        [Key]
        public long? gen_credit_period_id { get; set; }

        public string credit_period { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
