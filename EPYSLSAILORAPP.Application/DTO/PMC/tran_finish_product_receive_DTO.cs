
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_finish_product_receive_DTO : BaseEntity
    {

        [Required]
        [Column("tran_finish_product_receive_id")]
        public Int64 tran_finish_product_receive_id { get; set; }

        [Column("tran_packing_list_id")]
        public Int64? tran_packing_list_id { get; set; }

        [Column("finish_product_receive_no")]
        public string? finish_product_receive_no { get; set; }

        [Column("finish_product_receive_date")]
        public DateTime? finish_product_receive_date { get; set; }


        public long transport_id { get; set; }

        [Column("vehicle_number")]
        public string? vehicle_number { get; set; }

        [Column("driver_name")]
        public string? driver_name { get; set; }

        [Column("driver_contact_no")]
        public string? driver_contact_no { get; set; }

        [Column("note")]
        public string? note { get; set; }

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

        public string? tran_finish_product_receive_details_json { get; set; }
       
        public List<tran_finish_product_receive_details_DTO> TranFinishProductReceiveDetails_List { get; set; }

        public string packing_list_no { get; set; }
        public string transport_type { get; set; }


        public string finish_details { get; set; }
        public string techpack_number { get; set; }
    }


}
