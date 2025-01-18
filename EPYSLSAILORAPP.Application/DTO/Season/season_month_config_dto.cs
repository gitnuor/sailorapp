namespace EPYSLSAILORAPP.Application.DTO.Season
{
    public class season_month_config_dto
    {
        public int season_month_config_id { get; set; }
        public int season_id { get; set; }
        public int fiscal_year_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int start_month_id { get; set; }
        public int end_month_id { get; set; }
        public bool is_active { get; set; }
        public int added_by { get; set; }
        public DateTime date_added { get; set; }
        public int? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
    }
}
