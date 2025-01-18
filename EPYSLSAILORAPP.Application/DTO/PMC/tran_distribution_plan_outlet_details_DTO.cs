
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_distribution_plan_outlet_details_DTO : BaseEntity
    {

        [Required]
        [Column("tran_distribution_plan_outlet_details_id")]
        public Int64 tran_distribution_plan_outlet_details_id { get; set; }

        [Column("tran_distribution_plan_details_id")]
        public Int64? tran_distribution_plan_details_id { get; set; }

        [Column("tran_distribution_plan_id")]
        public Int64? tran_distribution_plan_id { get; set; }

        [Column("outlet_id")]
        public Int64? outlet_id { get; set; }
        public string? outlet_name { get; set; }

        [Column("received_quantity")]
        public Int64? received_quantity { get; set; }

        [Column("style_item_product_id")]
        public Int64? style_item_product_id { get; set; }

        [Column("mrp")]
        public Decimal? mrp { get; set; }

        [Column("style_product_size_group_detid")]
        public Int64? style_product_size_group_detid { get; set; }

        [Column("style_code")]
        public string? style_code { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("style_product_size")]
        public string? style_product_size { get; set; }

        [Column("style_product_unit")]
        public string? style_product_unit { get; set; }

        [Column("style_product_unit_id")]
        public Int64? style_product_unit_id { get; set; }




    }
}
