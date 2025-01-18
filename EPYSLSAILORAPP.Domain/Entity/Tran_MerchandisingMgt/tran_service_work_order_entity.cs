using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_service_work_order")]

    public class tran_service_work_order_entity : DapperExt
    {

        [Key]
        public Int64 service_work_order_id { get; set; }

        [Column("service_work_order_no")]
        public string service_work_order_no { get; set; }

        [Column("service_work_date")]
        public DateTime? service_work_date { get; set; }

        [Column("gen_process_master_id")]
        public Int64? gen_process_master_id { get; set; }

        [Column("techpack_id")]
        public Int64? techpack_id { get; set; }

        [Column("tran_tech_pack_embellishment_info_id")]
        public Int64? tran_tech_pack_embellishment_info_id { get; set; }

        [Column("delivery_date")]
        public DateTime? delivery_date { get; set; }

        [Column("delivery_unit_id")]
        public Int64? delivery_unit_id { get; set; }

        [Column("delivery_unit_address")]
        public string? delivery_unit_address { get; set; }

        [Column("remarks")]
        public string? remarks { get; set; }

        [Column("supplier_id")]
        public Int64? supplier_id { get; set; }

        [Column("supplier_address")]
        public string? supplier_address { get; set; }

        [Column("supplier_concern_person")]
        public string? supplier_concern_person { get; set; }

        [Column("supplier_referrence")]
        public string? supplier_referrence { get; set; }

        [Column("terms_condition_list")]
        public String? terms_condition_list { get; set; }

        [Column("test_requirements_list")]
        public String? test_requirements_list { get; set; }

        [Column("work_order_detail_list")]
        //public String? work_order_detail_list  { get; set;}
        public JArray work_order_detail_list { get; set; }

        [Column("is_submitted")]
        public Int64? is_submitted { get; set; }

        [Column("submitted_by")]
        public Int64? submitted_by { get; set; }

        [Column("is_approved")]
        public Int64? is_approved { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

        [Column("approve_date")]
        public DateTime? approve_date { get; set; }

        [Column("approve_remarks")]
        public string? approve_remarks { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }


        


    }
}
