using BDO.Core.Base;
using Dapper.Contrib.Extensions;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.GenTables
{

    public class gen_fiscal_year_dto:BaseEntity
    {
        public Int64 fiscal_year_id { set; get; }
        public Int64? year_no { set; get; }
        public  string? year_name { set; get; }
       
       // public DateTime? start_month { set; get; }
        //public DateTime? end_month { set; get; }

        public DateTime? start_date { set; get; }

        
        public DateTime? end_date { set; get; }

        public bool locks { set; get; }
        public bool is_used { set; get; }
        public Int64? added_by { set; get; }
        public DateTime? date_added { set; get; }
        public Int64? update_by { set; get; }
        public DateTime? date_updated { set; get; }
    }
}
