
using Dapper.Contrib.Extensions;
using Postgrest.Attributes;
using Postgrest.Models;
//using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_item_structure_group")]

    public class gen_item_structure_group_entity : DapperExt
    {

        [Key]
        public Int64? item_structure_group_id { get; set; }

        public String structure_group_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
