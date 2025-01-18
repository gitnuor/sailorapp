
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_mcd_receive_detail_DTO : BaseEntity
    {

        public long? receive_detail_id { get; set; }

        public long? received_id { get; set; }
        public long? po_id { get; set; }

        public long? gen_item_master_id { get; set; }
        public long? item_id { get; set; }

        public Decimal? po_quantity { get; set; }
        public Decimal? item_quantity { get; set; }

        //public String po_unit  { get; set;}
        public String measurement_unit { get; set; }
        public long? measurement_unit_detail_id { get; set; }
        public String unit { get; set; }

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
        public String gen_warehouse_floor_rack_id { get; set; }
        public String gen_warehouse_floor_rack_info { get; set; }


        public Int64? tran_type { get; set; }
        public Int64? added_by { get; set; }
        public DateTime? date_added { get; set; }
        public Int64? updated_by { get; set; }
        public DateTime? date_updated { get; set; }

        public Int64? current_state { get; set; }
        public String item_name { get; set; }
        public String item_spec { get; set; }
        public String mcd_no { get; set; }
        public Int64? revise_status { get; set; }


    }
}
