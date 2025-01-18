using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.GenTables
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_calculation_of_tenure")]

    public class gen_calculation_of_tenure_entity : DapperExt
    {

        [Key]
        public long? gen_calculation_of_tenure_id { get; set; }

        public string calculation_of_tenure { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
