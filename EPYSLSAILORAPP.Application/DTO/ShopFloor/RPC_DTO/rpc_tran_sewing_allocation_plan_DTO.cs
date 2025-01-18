
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_sewing_allocation_plan_DTO
    {

        public string sewing_allocation_number { get; set; }
        public string style_item_product_category { get; set; }
        public string techpack_number { get; set; }
        public string merchandiser_name { get; set; }
        public DateTimeOffset sewing_allocation_date { get; set; }
        public long tran_sewing_allocation_plan_id { get; set; }
        public Int64 total_rows { get; set; }

    }
}
