
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_distribution_plan_details_DTO : BaseEntity
    {

        [Required]
        [Column("tran_distribution_plan_details_id")]
        public Int64 tran_distribution_plan_details_id { get; set; }

        [Column("tran_distribution_plan_id")]
        public Int64? tran_distribution_plan_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("style_code")]
        public string? style_code { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("style_product_size_group_detid")]
        public Int64? style_product_size_group_detid { get; set; }

        [Column("style_product_size")]
        public string? style_product_size { get; set; }

        [Column("mrp")]
        public Decimal? mrp { get; set; }

        [Column("distributed_quantity")]
        public Int64? distributed_quantity { get; set; }
        public Int64? receive_quantity { get; set; }
        public Int64? order_quantity { get; set; }

        [Column("style_product_unit")]
        public string? style_product_unit { get; set; }

        [Column("style_item_product_id")]
        public Int64? style_item_product_id { get; set; }
        public string? style_item_product_name { get; set; }
        [Column("style_product_unit_id")]
        public Int64? style_product_unit_id { get; set; }
        public long already_distributed { get; set; }
        public string? techpack_number { get; set; }
        public JArray? tran_distribution_plan_outlet_details { get; set; }

        public List<tran_distribution_plan_outlet_details_DTO> TranDistributionPlanOutletDetails_List { get; set; }


    }
}
