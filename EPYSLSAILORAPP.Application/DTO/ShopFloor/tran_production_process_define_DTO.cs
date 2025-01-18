
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_production_process_define_DTO : BaseEntity
    {

        //[Required]
        [Column("tran_production_process_id")]
        public Int64 tran_production_process_id { get; set; }

        [Column("tran_production_process_date")]
        public DateTime? tran_production_process_date { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }
        public string? techpack_number { get; set; }

        [Column("techpack_date")]
        public Int64? techpack_date { get; set; }

        [Column("style_item_product_category")]
        public string? style_item_product_category { get; set; }

        [Column("style_item_product_id")]
        public Int64? style_item_product_id { get; set; }

        [Column("merchandiser_name")]
        public string? merchandiser_name { get; set; }

        [Column("designer_name")]
        public string? designer_name { get; set; }

        [Column("style_qty")]
        public decimal? style_qty { get; set; }

        [Column("supplier_id")]
        public Int64? supplier_id { get; set; }
        public string? supplier_name { get; set; }

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

        [Column("submitted_by")]
        public Int64? submitted_by { get; set; }

        [Column("is_approved")]
        public Int64? is_approved { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

        public JArray? tran_production_process_details { get; set; }
        
       

        public List<tran_production_process_define_details_DTO> details { get; set; }


    }

    public class tran_production_process_define_DTO_exc : BaseEntity
    {
        [Column("tran_production_process_id")]
        public Int64 tran_production_process_id { get; set; }

        [Column("tran_production_process_date")]
        public DateTime? tran_production_process_date { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }
        public string? techpack_number { get; set; }

        [Column("techpack_date")]
        public Int64? techpack_date { get; set; }

        [Column("style_item_product_category")]
        public string? style_item_product_category { get; set; }

        [Column("style_item_product_id")]
        public Int64? style_item_product_id { get; set; }

        [Column("merchandiser_name")]
        public string? merchandiser_name { get; set; }

        [Column("designer_name")]
        public string? designer_name { get; set; }

        [Column("style_qty")]
        public decimal? style_qty { get; set; }

        [Column("supplier_id")]
        public Int64? supplier_id { get; set; }
        public string? supplier_name { get; set; }

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

        [Column("submitted_by")]
        public Int64? submitted_by { get; set; }

        [Column("is_approved")]
        public Int64? is_approved { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

        public string? production_process_details { get; set; }
        public string? production_process_name { get; set; }



        public List<tran_production_process_define_details_DTO> details { get; set; }
    }
}
