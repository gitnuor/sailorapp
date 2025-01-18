
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_barcode_DTO
    {

        public Int64? tran_barcode_id { get; set; }

        public String techpack_number { get; set; }

        public String color_code { get; set; }
        public String size { get; set; }

        public Int64? total_rows { get; set; }

        public String barcode { get; set; }
       
        public Int64? fiscal_year_id { get; set; }

    
        public Int64? event_id { get; set; }


    }
}
