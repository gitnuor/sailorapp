using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.GenTables
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_garment_part")]

    public class gen_garment_part_entity : DapperExt
    {

        [Key]
        public long? gen_garment_part_id { get; set; }

        public string garment_part_name { get; set; }

        public string short_code { get; set; }

        public long? multiplier { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
