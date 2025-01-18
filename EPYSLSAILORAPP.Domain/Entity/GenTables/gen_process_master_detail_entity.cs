
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_process_master_detail")]

    public class gen_process_master_detail_entity : DapperExt
    {

        [Key]
        public Int64? gen_process_master_detail_id { get; set; }

        public Int64? gen_process_master_id { get; set; }

        public String sub_process_name { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
