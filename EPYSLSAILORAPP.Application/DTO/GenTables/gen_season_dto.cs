using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.GenTables
{
    public class gen_season_dto
    {
        public Int64 season_id { set; get; }
        public  string season_name { set; get; }
        public string short_name { set; get; }
        public bool is_active { set; get; }

        public Int64? update_by { set; get; }
        public Int64 added_by { set; get; }
        public DateTime date_added { set; get; }
        public Int64? sequence { set; get; }
        public DateTime? date_updated { set; get; }
    }
}
