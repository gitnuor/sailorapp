using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt
{

    [System.ComponentModel.DataAnnotations.Schema.Table("tran_tech_pack")]

    public class tran_tech_pack_entity : DapperExt
    {

        [Key]
        public long? tran_techpack_id { get; set; }

        public long? tran_designer_review_id { get; set; }

        public string techpack_number { get; set; }

        public DateTime? techpack_date { get; set; }

        public decimal? costing_smv { get; set; }

        public string teckpack_style_code { get; set; }

        public string aop_style { get; set; }

        public long? merchandiser_id { get; set; }

        public string production_availability_path { get; set; }

        public string vat { get; set; }

        public string photoshoot { get; set; }

        public string photos { get; set; }

        public string e_com { get; set; }

        public string sample_ok { get; set; }

        public string follow_style { get; set; }

        public string need_production_approval { get; set; }

        public string iron { get; set; }

        public string fabric_allocation { get; set; }

        public string additional_comments { get; set; }

        public Int64? is_submitted { get; set; }

        public Int64? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public DateTime? delivery_date { get; set; }

        public string approve_remarks { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        [JsonProperty("color_wise_size_quantity")]
        public string color_wise_size_quantity { get; set; }
        public Int64? tech_pack_costing_quantity { get; set; }

        public long? is_ack { get; set; }

        public DateTime? ack_date { get; set; }

        public string spi { get; set; }
        public string product_composition { get; set; }
        public string sleeve_details { get; set; }
        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

        [JsonProperty("embellishment_detl")]
        public string embellishment_detl { get; set; }

        public string? style_item_product_name { get; set; }

        public Int64? style_item_product_id { get; set; }
    }
}
