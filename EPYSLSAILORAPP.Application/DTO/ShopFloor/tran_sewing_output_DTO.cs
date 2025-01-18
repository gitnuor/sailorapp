
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_sewing_output_DTO : BaseEntity
    {

        [Required]
        [Column("tran_sewing_output_id")]
        public Int64 tran_sewing_output_id { get; set; }

        [Column("tran_sewing_input_id")]
        public Int64? tran_sewing_input_id { get; set; }

        [Column("tran_sewing_allocation_plan_id")]
        public Int64? tran_sewing_allocation_plan_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }
        public string? techpack_number { get; set; }
        [Column("output_date")]
        public DateTime? output_date { get; set; }
        public string? style_item_product_category { get; set; }
        [Column("note")]
        public string? note { get; set; }
        public string merchandiser_name { get; set; }
        [Column("hour_output")]
        public string? hour_output { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        //public JArray? tran_sewing_output_details { get; set; }

     public List<tran_sewing_output_details_DTO> tran_sewing_output_details { get; set; }




    }

    public class qc_failed_details_DTO : BaseEntity
    {
        public string? qc_parameter_name { get; set; }
        public Int64? qc_failed_quantity { get; set; } = 0;

    }
}