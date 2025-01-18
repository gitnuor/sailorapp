using Dapper.Contrib.Extensions;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity.GenTables
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_season")]
    public class gen_season
    {
        [Key]
        public Int64 season_id { set; get; }
        public string season_name { set; get; }
        public string short_name { set; get; }
        public bool is_active { set; get; }

       
        public Int64 added_by { set; get; }
        public DateTime date_added { set; get; }

        public Int64? update_by { set; get; }
        public DateTime? date_updated { set; get; }

        public Int64? sequence { set; get; }
    }
}
