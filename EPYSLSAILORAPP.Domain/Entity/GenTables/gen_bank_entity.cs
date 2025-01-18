
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_bank")]

    public class gen_bank_entity : DapperExt
    {

        [Key]
        public Int64? gen_bank_id { get; set; }

        public String bank_name { get; set; }

        public String bank_short_name { get; set; }

        public Boolean? is_used { get; set; }

        public Boolean? is_local { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
