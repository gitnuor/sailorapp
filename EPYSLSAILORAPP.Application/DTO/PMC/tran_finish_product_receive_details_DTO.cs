
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_finish_product_receive_details_DTO : BaseEntity
    {

        [Required]
        [Column("tran_finish_product_receive_details_id")]
        public Int64 tran_finish_product_receive_details_id { get; set; }

        [Column("tran_finish_product_receive_id")]
        public Int64? tran_finish_product_receive_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("style_code")]
        public string? style_code { get; set; }
        public string? techpack_number { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("style_product_size_group_detid")]
        public Int64? style_product_size_group_detid { get; set; }

        [Column("style_product_size")]
        public string? style_product_size { get; set; }

        [Column("barcode")]
        public string? barcode { get; set; }

        [Column("style_product_unit")]
        public Int64? style_product_unit_id { get; set; }
        public string style_product_unit { get; set; }

        [Column("mrp")]
        public Decimal? mrp { get; set; }

        [Column("order_quantity")]
        public Int64? order_quantity { get; set; }

        [Column("packing_quantity")]
        public Int64? packing_quantity { get; set; }

        [Column("receive_quantity")]
        public Int64? receive_quantity { get; set; }

        [Column("reject_quantity")]
        public Int64? reject_quantity { get; set; }

        [Column("note")]
        public string? note { get; set; }

        [Column("total_mrp_value")]
        public Decimal? total_mrp_value { get; set; }



    }
}
