
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_return_challan_details_DTO : BaseEntity
    {

        [Required]
        [Column("tran_return_challan_details_id")]
        public Int64 tran_return_challan_details_id { get; set; }

        [Column("tran_return_challan_id")]
        public Int64? tran_return_challan_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }
        public string? techpack_number { get; set; }
        [Column("style_code")]
        public string? style_code { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("style_product_size_group_detid")]
        public Int64? style_product_size_group_detid { get; set; }

        [Column("style_product_size")]
        public string? style_product_size { get; set; }

        [Column("barcode")]
        public string? barcode { get; set; }

        [Column("style_product_unit_id")]
        public Int64? style_product_unit_id { get; set; }

        [Column("style_product_unit")]
        public string? style_product_unit { get; set; }

        [Column("mrp")]
        public Decimal? mrp { get; set; }

        [Column("return_quantity")]
        public Int64? return_quantity { get; set; }

        [Column("reject_quantity")]
        public Int64? reject_quantity { get; set; }
        public Int64? already_return_quantity { get; set; }

        [Column("note")]
        public string? note { get; set; }




    }
}
