
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt
{

    public class tran_purchase_requisition_dtl_DTO : BaseEntity
    {

        public long? pr_detail_id { get; set; }

        public long? pr_id { get; set; }
        public string? item_name { get; set; }
        public string? item_spec { get; set; }
        public string? color_code { get; set; }

        public long? item_id { get; set; }

        [Required]
        public decimal? item_quantity { get; set; }

        public decimal? suggested_unit_price { get; set; }

        public decimal? total_price { get; set; }

        public long? uom { get; set; }
        public string? uomText { get; set; }
        public string? sub_group_name { get; set; }
        public long? gen_item_structure_group_sub_id { get; set; }


    }
}
