using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt
{
  
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_pre_costing_item_subcontract_detail")]
    public class tran_pre_costing_item_subcontract_detail_entity : DapperExt
    {

        [Key]
        public long? tran_pre_costing_item_subcontract_detail_id { get; set; }

        public long? tran_pre_costing_id { get; set; }

        public long? process_master_id { get; set; }

        public string? process_name { get; set; }

        public long? measurement_unit_detail_id { get; set; }

        public decimal? order_quantity { get; set; }

        public decimal? wastage { get; set; }

        public decimal? total_order_quantity { get; set; }

        public decimal? price_per_unit { get; set; }

        public decimal? total_price { get; set; }

        public string remarks { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? current_state { get; set; }

    }
}
