namespace EPYSLSAILORAPP.Application.DTO.Season
{
    public class season_dto
    {
        public int season_id { get; set; }
        public string season_name { get; set; }
        public string short_name { get; set; }
        public bool is_active { get; set; }
        public int added_by { get; set; }
        public DateTime date_added { get; set; }
        public int? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
    }
}
