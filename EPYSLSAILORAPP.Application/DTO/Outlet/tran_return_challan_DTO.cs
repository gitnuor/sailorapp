
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_return_challan_DTO : BaseEntity
    {

        [Required]
        [Column("tran_return_challan_id")]
        public Int64 tran_return_challan_id { get; set; }

        [Column("tran_outlet_challan_id")]
        public Int64? tran_outlet_challan_id { get; set; }

        [Column("tran_outlet_challan_receive_id")]
        public Int64? tran_outlet_challan_receive_id { get; set; }

        [Column("return_no")]
        public string? return_no { get; set; }
        public string? transport_type { get; set; }
        public string? transport_type_name { get; set; }

        [Column("return_date")]
        public DateTime? return_date { get; set; }
        public DateTime? outlet_receive_date { get; set; }

        [Column("transport_id")]
        public Int64? transport_id { get; set; }
        public Int64? return_to { get; set; }
        public string return_to_name { get; set; }
        public string outlet_receive_no { get; set; }
        public Int64? return_from { get; set; }
        public string return_from_name { get; set; }

        [Column("vehicle_number")]
        public string? vehicle_number { get; set; }

        [Column("driver_name")]
        public string? driver_name { get; set; }

        [Column("driver_contact_no")]
        public string? driver_contact_no { get; set; }

        [Column("note")]
        public string? note { get; set; }
        public string? receive_details { get; set; }

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

        public JArray? tran_return_challan_details_json { get; set; }

        public List<tran_return_challan_details_DTO> TranReturnChallanDetails_List { get; set; }


    }
}
