using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt
{
    
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_sample_order_detl")]

    public class tran_sample_order_detl_entity : DapperExt
    {

        [Key]
        public long? tran_sample_order_detl_id { get; set; }

        public long? tran_sample_order_id { get; set; }

        public string color_code { get; set; }

        public long? style_product_size_group_detid { get; set; }

        public long? order_quantity { get; set; }

        public long? style_product_unit_id { get; set; }

        public string remarks { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
