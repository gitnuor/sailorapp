
using BDO.Core.Base;
using Postgrest.Attributes;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_sewing_allocation_plan_DTO : BaseEntity
    {

        [Required]
        [Column("tran_sewing_allocation_plan_id")]
        public Int64 tran_sewing_allocation_plan_id { get; set; }

        [Column("sewing_allocation_number")]
        public string? sewing_allocation_number { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("sewing_allocation_date")]
        public DateTime? sewing_allocation_date { get; set; }

        [Column("merchandiser_id")]
        public Int64? merchandiser_id { get; set; }
        public string merchandiser_name { get; set; }

        [Column("style_item_product_id")]
        public Int64? style_item_product_id { get; set; }

        [Column("style_item_product_category")]
        public string? style_item_product_category { get; set; }

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


    }

    public class tran_sewing_detail_plan_DTO : BaseEntity
    {


        public Int64 production_line_id { get; set; }


        public string line_name { get; set; }
        public string color_code { get; set; }
        public string batch_no { get; set; }
        public Int64 batch_id { get; set; }
        public Int64 total_allocated_quantity { get; set; }
        public Int64? techpack_id { get; set; }

        public string tran_sewing_detail_plan_size { get; set; }



    }

    public class tran_sewing_detail_plan_size_DTO : BaseEntity
    {


        public Int64 production_line_id { get; set; }
        public Int64 batch_id { get; set; }
        public Int64 style_product_size_group_detid { get; set; }
        public Int64 cutting_quantity { get; set; }
        public Int64 allocated_quantity { get; set; }
        public string size_name { get; set; }




    }
    public class sewing_plan_insert_details_size_dto
    {
        public long style_product_size_group_detid { get; set; }
        public string size_name { get; set; }
        public long cutting_quantity { get; set; }
        public long allocated_quantity { get; set; }
    }

    public class sewing_plan_insert_details_dto
    {
        public long production_line_id { get; set; }
        public string color_code { get; set; }
        public long techpack_id { get; set; }
        public long batch_id { get; set; }
        public string batch_no { get; set; }
        public long total_allocated_quantity { get; set; }
        public List<sewing_plan_insert_details_size_dto> tran_sewing_allocation_plan_details_size { get; set; }
        public string all_size { get; set; }
    }

    public class sewing_plan_insert_dto
    {
        public long techpack_id { get; set; }
        public DateTime sewing_allocation_date { get; set; }
        public long merchandiser_id { get; set; }
        public long style_item_product_id { get; set; }
        public string style_item_product_category { get; set; }
        public List<sewing_plan_insert_details_dto> tran_sewing_allocation_plan_details { get; set; }
        public DateTime? date_added { get; set; }
        public Int64? added_by { get; set; }
        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }
    }
}
