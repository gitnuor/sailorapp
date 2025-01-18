using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.GenTables
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_contact_category")]

    public class gen_contact_category_entity : DapperExt
    {

        [Key]
        public long? gen_contact_category_id { get; set; }

        public string contact_category { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
