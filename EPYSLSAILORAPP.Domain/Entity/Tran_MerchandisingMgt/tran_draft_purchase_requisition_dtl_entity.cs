using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
   
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_draft_purchase_requisition_dtl")]

    public class tran_draft_purchase_requisition_dtl_entity : DapperExt
    {

        [Key]
        public Int64? dpr_detail_id { get; set; }

        public Int64? dpr_id { get; set; }

        public Int64? item_id { get; set; }

        public Decimal? item_quantity { get; set; }

        public Decimal? unit_price { get; set; }

        public Decimal? total_price { get; set; }

        public Int64? uom { get; set; }


    }
}
