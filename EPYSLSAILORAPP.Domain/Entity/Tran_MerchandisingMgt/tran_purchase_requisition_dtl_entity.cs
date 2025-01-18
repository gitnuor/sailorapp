using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.SupplyChain
{
    
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_draft_purchase_requisition_dtl")]

    public class tran_purchase_requisition_dtl_entity : DapperExt
    {

        [Key]
        public long? pr_detail_id { get; set; }

        public long? pr_id { get; set; }

        public long? item_id { get; set; }

        public decimal? item_quantity { get; set; }
        public decimal? suggested_unit_price { get; set; }

        public string? color_code { get; set; }

        public decimal? total_price { get; set; }

        public long? uom { get; set; }


    }
}
