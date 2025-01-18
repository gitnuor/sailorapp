using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Domain.Statics;
using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.BusinessPlanning
{
    public class GenSeasonEventConfigurationDTO
    {

       public long event_id { get; set; }

        public Int64 fiscal_year_id { get; set; }

        public Int64 season_id { get; set; }

        public Int64 range_plan_id { get; set; }

        public Int64 tran_va_plan_event_id { get; set; }

        public Int64 start_month_id { get; set; }

        public Int64 end_month_id { get; set; }

        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }

        public string str_start_date { get; set; }
        public string str_end_date { get; set; }

        public bool? is_active { get; set; }

        public string event_title { get; set; }

        public Int64? event_sequence { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public string str_event_id { get; set; }

        public string str_fiscal_year_id { get; set; }

        public string str_season_id { get; set; }

        public string str_range_plan_id { get; set; }

        public string str_tran_va_plan_event_id { get; set; }



        #region Additional Columns

        //
        //public override bool IsModified => EntityState == EntityState.Modified || RangePlanID > 0;

        public string year_name { get; set; }

        public string season_name { get; set; }

        public season_dto gen_season { get; set; }
        public gen_fiscal_year_dto gen_fiscal_year { get; set; }


        

        #endregion Additional Columns

    }


    public class SeasonEventConfigCopy
    {
       
    }

}
