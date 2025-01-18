using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_final_inspection_DTO : BaseEntity
    {

        [Required]
        [Column("tran_final_inspection_id")]
        public Int64 tran_final_inspection_id { get; set; }

        [Column("final_inspection_no")]
        public string? final_inspection_no { get; set; }

        [Column("final_inspection_date")]
        public DateTime? final_inspection_date { get; set; }

        [Column("finishing_production_process_id")]
        public Int64? finishing_production_process_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }
        public Int64? style_item_product_id { get; set; }
        public String teckpack_style_code { get; set; }
        [Column("note")]
        public string? note { get; set; }
        public string? techpack_number { get; set; }
        public DateTime? techpack_date { get; set; }
        public String event_title { get; set; }
        public String style_item_product_name { get; set; }
        public String designer_name { get; set; }
        public String merchandiser_name { get; set; }
        [Column("is_draft")]
        public Int64? is_draft { get; set; }

        [Column("is_submitted")]
        public Int64? is_submitted { get; set; }

        [Column("submitted_by")]
        public Int64? submitted_by { get; set; }

        [Column("submitted_date")]
        public DateTime? submitted_date { get; set; }

        [Column("is_approved")]
        public Int64? is_approved { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

        [Column("approved_date")]
        public DateTime? approved_date { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        public JArray? tran_final_inspection_details { get; set; }

        public List<tran_final_inspection_details_DTO> TranFinalInspectionDetails_List { get; set; }

        public string inspection_details { get; set; }


    }
}
