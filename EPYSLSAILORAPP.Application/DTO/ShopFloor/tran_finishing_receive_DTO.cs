
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_finishing_receive_DTO : BaseEntity
    {

        //[Required]
        [Column("tran_finish_receive_id")]
        public Int64 tran_finish_receive_id { get; set; }

        //[Required]
        [Column("tran_finish_receive_no")]
        public string tran_finish_receive_no { get; set; }

        [Column("tran_finish_receive_date")]
        public DateTime? tran_finish_receive_date { get; set; }

        [Column("tran_sewing_output_id")]
        public Int64? tran_sewing_output_id { get; set; }

        [Column("tran_sewing_allocation_plan_id")]
        public Int64? tran_sewing_allocation_plan_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }
        public string? techpack_number { get; set; }

        [Column("style_item_product_id")]
        public Int64? style_item_product_id { get; set; }

        [Column("style_item_product_category")]
        public string? style_item_product_category { get; set; }

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

        [Column("is_submitted")]
        public Int64? is_submitted { get; set; }
        [Column("is_approved")]
        public Int64? is_approved { get; set; }

        public string? tran_finish_receive_details { get; set; }
        public string? finishing_process_name { get; set; }
        public string? finish_receive_details { get; set; }

        public List<tran_finishing_receive_details_DTO> details { get; set; }

        public List<tran_finishing_process_DTO> finishingProcessdetails { get; set; }

        public Int64? gen_finishing_process_id { get; set; }


    }
    public class tran_finishing_process_DTO
    {

        public Int64? gen_finishing_process_id { get; set; }

        public string finishing_process_name { get; set; }


    }

}
