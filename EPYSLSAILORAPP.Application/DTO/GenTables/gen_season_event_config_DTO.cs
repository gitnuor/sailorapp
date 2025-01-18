
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_season_event_config_DTO : BaseEntity
    {

        [Required]
        [Column("event_id")]
        public Int64 event_id { get; set; }

        [Column("season_id")]
        public Int64? season_id { get; set; }
        public string season_name { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        public string year_name { get; set; }

        [Required]
        [Column("start_date")]
        public DateTime start_date { get; set; }

        [Required]
        [Column("start_month_id")]
        public Int64 start_month_id { get; set; }

        [Required]
        [Column("end_month_id")]
        public Int64 end_month_id { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("event_title")]
        public string? event_title { get; set; }

        [Column("is_active")]
        public Boolean? is_active { get; set; }

        [Column("event_sequence")]
        public Int64? event_sequence { get; set; }

        [Column("end_date")]
        public DateTime? end_date { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        public List<tran_shop_floor_return_DTO> TranShopFloorReturn_List { get; set; }

        public List<tran_pp_meeting_DTO> TranPpMeeting_List { get; set; }

        //public List<tran_purchase_requisition_DTO> TranPurchaseRequisition_List {get; set;}

        //public List<tran_bp_event_DTO> TranBpEvent_List {get; set;}

        public List<tran_cutting_DTO> TranCutting_List { get; set; }

        public List<tran_mcd_requisition_issue_DTO> TranMcdRequisitionIssue_List { get; set; }

        public List<gen_season_dto> GenSeason_List { get; set; }

        public List<gen_fiscal_year_dto> GenFiscalYear_List { get; set; }


    }
}
