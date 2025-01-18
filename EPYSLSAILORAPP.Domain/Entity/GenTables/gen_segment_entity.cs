
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_segment")]

    public class gen_segment_entity : DapperExt
    {

        [Key]
        public Int64? gen_segment_id { get; set; }

        public String gen_segment_name { get; set; }

        public String display_name { get; set; }

        public Boolean? is_item_segment { get; set; }

        public Boolean? is_active { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public String segment_detl_json { get; set; }


    }
}
