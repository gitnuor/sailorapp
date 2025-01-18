namespace EPYSLSAILORAPP.Infrastructure.Identity.Configuration
{
    public class CORS
    {
        public bool AllowAnyOrigin { get; set; }
        public required string[] AllowedOrigins { get; set; }
    }
}
