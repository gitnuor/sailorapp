
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_mcd_purchase_return")]

    public class tran_mcd_purchase_return_entity : DapperExt
    {

        [Key]
        public Int64 purchase_return_id { get; set; }

        [Column("purchase_return_no")]
        public string? purchase_return_no { get; set; }

        [Column("received_id")]
        public Int64? received_id { get; set; }

        [Column("receive_date")]
        public DateTime? receive_date { get; set; }

        [Column("po_id")]
        public Int64? po_id { get; set; }

        [Column("po_date")]
        public DateTime? po_date { get; set; }

        [Column("supplier_id")]
        public Int64? supplier_id { get; set; }

        [Column("del_chalan_no")]
        public string? del_chalan_no { get; set; }

        [Column("del_chalan_date")]
        public DateTime? del_chalan_date { get; set; }

        [Column("transport_type")]
        public string? transport_type { get; set; }

        [Column("vehical_no")]
        public string? vehical_no { get; set; }

        [Column("driver_name")]
        public string? driver_name { get; set; }

        [Column("driver_contact_no")]
        public string? driver_contact_no { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("date_approved")]
        public DateTime? date_approved { get; set; }

        [Column("gen_item_structure_group_id")]
        public Int64? gen_item_structure_group_id { get; set; }

        
        //public String? purchase_return_detail  { get; set;}


        [Column("is_submitted")]
        public Int64? is_submitted { get; set; }

        [Column("submitted_by")]
        public Int64? submitted_by { get; set; }

        [Column("is_approved")]
        public Int64? is_approved { get; set; }

        [Column("approved_by")]
        public Int64? approved_by { get; set; }

        [Column("fiscal_year_id")]
        public Int64? fiscal_year_id { get; set; }

        [Column("event_id")]
        public Int64? event_id { get; set; }
        [Column("purchase_return_detail")]
        public String purchase_return_detail { get; set; }


    }
}
