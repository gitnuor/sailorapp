
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_final_inspection_details_DTO : BaseEntity
    {

        [Required]
        [Column("tran_final_inspection_details_id")]
        public Int64 tran_final_inspection_details_id { get; set; }

        [Column("tran_final_inspection_id")]
        public Int64? tran_final_inspection_id { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("color_quantity")]
        public Int64? color_quantity { get; set; }
        public Int64? production_qty { get; set; }
        [Column("inspection_qty")]
        public Int64? inspection_qty { get; set; }
        public Int64? already_inspected_quantity { get; set; }
        [Column("balance_qty")]
        public Int64? balance_qty { get; set; }

        [Column("proposed_mrp")]
        public Decimal? proposed_mrp { get; set; }

        [Column("final_mrp")]
        public Decimal? final_mrp { get; set; }

        [Column("note")]
        public string? note { get; set; }



    }
}
