
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_mcd_receive_detail")]

    public class tran_mcd_receive_detail_entity : DapperExt
    {

        [Key]
        public Int64? receive_detail_id { get; set; }

        public Int64? received_id { get; set; }
        public Int64? po_id { get; set; }

        public Int64? gen_item_master_id { get; set; }

        public Decimal? po_quantity { get; set; }
        public Int64? measurement_unit_detail_id { get; set; }
        //public String po_unit { get; set; }

        //public String po_unit  { get; set;}
        public String measurement_unit { get; set; }
        //public long? measurement_unit_detail_id { get; set; }

			public Decimal? receive_quantity  { get; set;}
        public Decimal? chalan_quantity { get; set; }

        public Decimal? random_sample_quantity { get; set; }

        public Decimal? aql { get; set; }

        public Decimal? pass_quantity { get; set; }

        public Decimal? fail_quantity { get; set; }

        public Decimal? defect_percentage { get; set; }

        public String remarks { get; set; }
       

        public String gen_warehouse_floor_rack_info { get; set; }
        public string gen_warehouse_floor_rack_id { get; set; }
        public Int64? tran_type { get; set; }
        public Int64? added_by { get; set; }
        public DateTime? date_added { get; set; }
        public Int64? updated_by { get; set; }
        public DateTime? date_updated { get; set; }

        public Int64? current_state { get; set; }
        public string? mcd_no { get; set; }


    }
}
