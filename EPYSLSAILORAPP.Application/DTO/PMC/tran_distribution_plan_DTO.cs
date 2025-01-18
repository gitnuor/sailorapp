
using BDO.Core.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_distribution_plan_DTO : BaseEntity
    {

        [Required]
        [Column("tran_distribution_plan_id")]
        public Int64 tran_distribution_plan_id { get; set; }

        [Column("distribution_no")]
        public string? distribution_no { get; set; }

        [Column("distribution_date")]
        public DateTime? distribution_date { get; set; }

        [Column("distributed_by")]
        public Int64? distributed_by { get; set; }

        public string? distributed_by_name { get; set; }

        [Column("note")]
        public string? note { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }

        [Column("is_submitted")]
        public Int64? is_submitted { get; set; }
        public Int64? techpack_id { get; set; }

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

        public JArray? tran_distribution_plan_details_json { get; set; }

        public List<tran_distribution_plan_details_DTO> TranDistributionPlanDetails_List { get; set; }

        public List<SelectListItem> techpacks { get; set; }


    }
  
}
