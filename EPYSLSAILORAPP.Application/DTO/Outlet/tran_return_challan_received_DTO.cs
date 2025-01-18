
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_return_challan_received_DTO : tran_return_challan_DTO
    {

        [Required]
        [Column("tran_return_challan_received_id")]
        public Int64 tran_return_challan_received_id { get; set; }

        //[Column("tran_return_challan_id")]
        //public Int64? tran_return_challan_id { get; set; }

        [Column("return_receive_no")]
        public string? return_receive_no { get; set; }

        [Column("return_receive_date")]
        public DateTime? return_receive_date { get; set; }

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

        public string? tran_return_challan_receive_details_json { get; set; }
        public string? return_challan_receive_details_json { get; set; }

        public List<tran_return_challan_receive_details_DTO> TranReturnChallanReceiveDetails_List { get; set; }



       

       



    }
}
