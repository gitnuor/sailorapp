
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_production_process_define_details_DTO : BaseEntity
    {

        //[Required]
        [Column("tran_production_process_details_id")]
        public Int64 tran_production_process_details_id { get; set; }

        [Required]
        [Column("tran_production_process_id")]
        public Int64 tran_production_process_id { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("color_qty")]
        public decimal? color_qty { get; set; }



        [Column("production_process_uit_id")]
        public Int64? production_process_uit_id { get; set; }

        [Column("production_process_uit_name")]
        public string? production_process_uit_name { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }
        public JArray? production_process_name { get; set; }
        public List<tran_production_process_define_DTO> details { get; set; }
        public List<tran_production_process_name> process_name_detail { get; set; }


    }


    public class tran_production_process_define_details_DTO_exc : BaseEntity
    {

        //[Required]
        [Column("tran_production_process_details_id")]
        public Int64 tran_production_process_details_id { get; set; }

        [Required]
        [Column("tran_production_process_id")]
        public Int64 tran_production_process_id { get; set; }

        [Column("color_code")]
        public string? color_code { get; set; }

        [Column("color_qty")]
        public decimal? color_qty { get; set; }



        [Column("production_process_uit_id")]
        public Int64? production_process_uit_id { get; set; }

        [Column("production_process_uit_name")]
        public string? production_process_uit_name { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }
        public string? production_process_name { get; set; }
        public List<tran_production_process_define_DTO> details { get; set; }
        public List<tran_production_process_name> process_name_detail { get; set; }


    }

    public class tran_production_process_name
    {
        public string? process_name { get; set; }
    }

}

