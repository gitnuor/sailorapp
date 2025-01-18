
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_mcd_receive_detail_DTO
    {

        public Int64? receive_detail_id { get; set; }

        public Int64? received_id { get; set; }

        public Int64? gen_item_master_id { get; set; }

        public Decimal? po_quantity { get; set; }

        public String measurement_unit { get; set; }

        public Decimal? receive_quantity { get; set; }

        public Decimal? receive_unit { get; set; }

        public Decimal? chalan_quantity { get; set; }

        public Decimal? exceess_quantity { get; set; }

        public Decimal? shortage_quantity { get; set; }

        public Decimal? random_sample_quantity { get; set; }

        public Decimal? aql { get; set; }

        public Decimal? pass_quantity { get; set; }

        public Decimal? fail_quantity { get; set; }

        public Decimal? defect_percentage { get; set; }

        public String remarks { get; set; }

        public Int64? po_id { get; set; }

        public String item_name { get; set; }
        public String item_spec { get; set; }

        public Int64? measurement_unit_detail_id { get; set; }

        public string? mcd_no { get; set; }

        public String po_unit { get; set; }

        public Decimal? unit_price { get; set; }
        public Decimal? total_price { get; set; }

        public Decimal? loading_cost_in_percentage { get; set; }

        public Decimal? loading_cost { get; set; }

    }
}
