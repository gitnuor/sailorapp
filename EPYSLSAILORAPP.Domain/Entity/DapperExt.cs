using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    public class DapperExt
    {
        [Computed]
        public Int64? total_rows { get; set; }
        [Computed]
        public Int64? row_index { get; set; }
        [Computed]
        public Int64? page_size { get; set; }
        [Computed]
        public string? search_text { get; }
    }
}
