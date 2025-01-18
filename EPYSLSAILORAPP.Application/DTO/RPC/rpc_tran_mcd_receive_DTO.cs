
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_tran_mcd_receive_DTO
    {

        public Int64? received_id { get; set; }
        public Int64 total_rows { get; set; }
        public String receive_no { get; set; }

        public DateTime? arrival_date { get; set; }
        public DateTime? del_chalan_date { get; set; }

        public DateTime? po_date { get; set; }
        public string del_chalan_no { get; set; }

        public String delevary_status { get; set; }

        public JArray? documents { get; set; }

        public String po_status { get; set; }

        public String po_no { get; set; }

        public String supplier_name { get; set; }

        public String office_address { get; set; }

        public String factory_address { get; set; }

        public String contact_person { get; set; }

        public String unit_name { get; set; }

        public String unit_address { get; set; }
        public String tollerence { get; set; }
        public String tran_mode { get; set; }
        public String transport_type { get; set; }
        public String driver_name { get; set; }
        public String vehical_no { get; set; }
        public decimal po_quantity { get; set; }
        

        public DateTime? return_date { get; set; }
        public Int64? po_id { get; set; }
        public Int64? supplier_id { get; set; }

        //public String po_unit { get; set; }
        public String measurement_unit { get; set; }
        public String receive_quantity { get; set; }

        public Int64? event_id { get; set; }
        public Int64? fiscal_year_id { get; set; }

        public Int64? is_submitted { get; set; }
        public Int64? is_acknowledged { get; set; }

       public string  delivery_status_type { get; set; }
       public string transaction_mode_type { get; set; }

        public string receive_status { get; set; }




    }
}
