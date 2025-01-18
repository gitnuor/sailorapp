
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_sp_get_data_for_mcd_receive_DTO
    {

        public Int64? po_id { get; set; }

        public String po_no { get; set; }

        public DateTime? po_date { get; set; }

        public DateTime? delivery_start_date { get; set; }

        public DateTime? delivery_end_date { get; set; }

        public Int64? supplier_id { get; set; }

        public Int64? delivery_unit { get; set; }

        public Int64? delivery_address { get; set; }

        public Int64? currency_id { get; set; }

        public string documents { get; set; }

        public string terms_conditions { get; set; }

        public Int64? is_submitted { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? pr_id { get; set; }

        public Int64? item_structure_group_id { get; set; }

        public Int64? is_approved { get; set; }

        public DateTime? approved_date { get; set; }

        public Int64? approved_by { get; set; }

        public Boolean? is_bill_submitted { get; set; }

        public Int64? po_details_id { get; set; }

        public Int64? item_id { get; set; }

        public Decimal? item_quantity { get; set; }

        public String unit { get; set; }

        public Decimal? unit_price { get; set; }

        public Decimal? total_price { get; set; }

        public String remarks { get; set; }

        public Int64? gen_unit_id { get; set; }

        public String unit_name { get; set; }

        public String unit_address { get; set; }

        public Int64? gen_supplier_information_id { get; set; }

        public String contact_code { get; set; }

        public String name { get; set; }

        public String short_name { get; set; }

        public String display_code { get; set; }

        public String email { get; set; }

        public String office_address { get; set; }

        public String factory_address { get; set; }

        public Boolean? active { get; set; }

        public String list_po_details { get; set; }
        public Decimal? receive_quantity { get; set; }


    }
}
