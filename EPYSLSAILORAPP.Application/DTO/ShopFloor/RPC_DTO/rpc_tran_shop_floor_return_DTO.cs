
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_shop_floor_return_DTO
    {

        public Int64? tran_shop_floor_return_id { get; set; }

        public String return_no { get; set; }

        public String requisition_slip_no { get; set; }

        public String issue_no { get; set; }
        public Int64 total_rows { get; set; }

        public String techpack_number { get; set; }


    }
}
