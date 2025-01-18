using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.Statics;
using Microsoft.AspNetCore.Mvc;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity.BusinessPlanning
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_season_event_config")]
    public class gen_season_event_config : DapperExt
    {
        #region Table properties

        [Key]
        public long? event_id { get; set; }
        public Int64 fiscal_year_id { get; set; }
        public Int64 season_id { get; set; }
        public Int64 start_month_id { get; set; }
        public Int64 end_month_id { get; set; }

      
        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }
        public bool? is_active { get; set; }
        public string event_title { get; set; }
        public Int64? added_by { get; set; }
        public DateTime? date_added { get; set; }
        public Int64? updated_by { get; set; }
        public DateTime? date_updated { get; set; }

        public Int64? event_sequence { get; set; }

        #endregion Table properties

    }


    public class gen_season_event_config_ext: gen_season_event_config
    {
        public string season_name { get; set; }

        public List<gen_season_event_config_ext> seasonEventConficList { get; set; }
    }
}
