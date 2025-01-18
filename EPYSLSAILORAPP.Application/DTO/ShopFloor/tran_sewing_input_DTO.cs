
using BDO.Core.Base;
using Postgrest.Attributes;
using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_sewing_input_DTO : BaseEntity
    {

        [Required]
        [Column("tran_sewing_input_id")]
        public Int64 tran_sewing_input_id { get; set; }

        [Column("tran_sewing_allocation_plan_id")]
        public Int64? tran_sewing_allocation_plan_id { get; set; }
        public string? sewing_allocation_number { get; set; }
        public DateTime? sewing_allocation_date { get; set; }
        public string merchandiser_name { get; set; }
        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }
        public string? techpack_number { get; set; }
        public string? style_item_product_category { get; set; }
        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }
        public List<tran_sewing_input_details_DTO> tran_sewing_input_details { get; set; }
        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }


    }
}
