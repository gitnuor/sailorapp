
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_packing_list_DTO : BaseEntity
    {

        [Required]
        [Column("tran_packing_list_id")]
        public Int64 tran_packing_list_id { get; set; }

        [Column("packing_list_no")]
        public string? packing_list_no { get; set; }

        [Column("packing_list_date")]
        public DateTime? packing_list_date { get; set; }

        [Column("note")]
        public string? note { get; set; }

        [Column("is_draft")]
        public Int64? is_draft { get; set; }

        [Column("draft_date")]
        public DateTime? draft_date { get; set; }

        [Column("is_submitted")]
        public Int64? is_submitted { get; set; }

        [Column("submitted_date")]
        public DateTime? submitted_date { get; set; }

        [Column("submitted_by")]
        public Int64? submitted_by { get; set; }

        [Column("is_approved")]
        public Int64? is_approved { get; set; }

        [Column("approved_date")]
        public DateTime? approved_date { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

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

        public JArray? tran_packing_list_details_json { get; set; }

        public List<tran_packing_list_details_DTO> tran_packing_list_details { get; set; }


    }
    public class rpc_proc_sp_get_data_techpack_for_distribution_DTO
    {
        public Int64? techpack_id { get; set; }
        public String techpack_number { get; set; }

    }
}
