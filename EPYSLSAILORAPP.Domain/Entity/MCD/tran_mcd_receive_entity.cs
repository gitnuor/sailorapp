
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_mcd_receive")]

    public class tran_mcd_receive_entity : DapperExt
    {

        [Key]
        public Int64? received_id { get; set; }

        public String receive_no { get; set; }

        public Int64? po_id { get; set; }

        public DateTime? po_date { get; set; }

        public Int64? supplier_id { get; set; }

        public Int64? delivery_unit { get; set; }

        public Int64? delivery_address { get; set; }

        public DateTime? arrival_date { get; set; }

        public String del_chalan_no { get; set; }

        public DateTime? del_chalan_date { get; set; }

        public String tran_mode { get; set; }

        public String transport_type { get; set; }

        public Int64? tollerence { get; set; }

        public String vehical_no { get; set; }

        public String driver_name { get; set; }

        public String delevary_status { get; set; }

        public String lc_no { get; set; }

        public DateTime? lc_date { get; set; }

        public String invoice_no { get; set; }

        public DateTime? invoice_date { get; set; }

        public String documents { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? approved_by { get; set; }

        public DateTime? date_approved { get; set; }

        public long? gen_item_structure_group_id { get; set; }

        public String po_status { get; set; }
        public Int64? event_id { get; set; }
        public Int64? fiscal_year_id { get; set; }

        public Int64? is_submitted { get; set; }
        public Int64? is_acknowledged { get; set; }
        public String? tran_mcd_receive_json { get; set; }


    }
}
