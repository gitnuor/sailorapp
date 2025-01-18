
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_service_work_order_detail")]

    public class tran_service_work_order_detail_entity : DapperExt
    {

        [Key]
        public Int64 service_work_order_detail_id { get; set; }

        [Column("service_work_order_id")]
        public Int64 service_work_order_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("gen_process_master_id")]
        public Int64? gen_process_master_id { get; set; }

        [Column("gen_garment_part_id")]
        public Int64? gen_garment_part_id { get; set; }

        [Column("process_name")]
        public string? process_name { get; set; }

        [Column("measurement_unit_id")]
        public Int64? measurement_unit_id { get; set; }

        [Column("tran_va_plan_detl_style_color_id")]
        public Int64? tran_va_plan_detl_style_color_id { get; set; }

        [Column("tran_tech_pack_embellishment_info_id")]
        public Int64? tran_tech_pack_embellishment_info_id { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("color_quantity")]
        public Decimal? color_quantity { get; set; }

        [Column("order_quantity")]
        public Decimal? order_quantity { get; set; }

        [Column("rate")]
        public Decimal? rate { get; set; }

        [Column("total_value")]
        public Decimal? total_value { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }


    }
}
