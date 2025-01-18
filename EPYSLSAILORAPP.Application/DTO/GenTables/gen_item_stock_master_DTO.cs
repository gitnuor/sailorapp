
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_item_stock_master_DTO : BaseEntity
    {


        [Column("item_stock_master_id")]
        public Int64 item_stock_master_id { get; set; }


        [Column("item_master_id")]
        public Int64 item_master_id { get; set; }

        [Column("tran_techpack_id")]
        public Int64? tran_techpack_id { get; set; }


        [Column("measurement_unit_detail_id")]
        public Int64 measurement_unit_detail_id { get; set; }


        [Column("opening_quantity")]
        public Decimal opening_quantity { get; set; }


        [Column("total_received_quantity")]
        public Decimal total_received_quantity { get; set; }


        [Column("total_allocated_quantity")]
        public Decimal total_allocated_quantity { get; set; }

        public Decimal total_unapproved_allocated_quantity { get; set; }


        [Column("total_issued_quantity")]
        public Decimal total_issued_quantity { get; set; }


        [Column("total_failed_quantity")]
        public Decimal total_failed_quantity { get; set; }


        [Column("total_floor_return_quantity")]
        public Decimal total_floor_return_quantity { get; set; }


        [Column("total_quarantine_quantity")]
        public Decimal total_quarantine_quantity { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("available_stock_quantity")]
        public Int64? available_stock_quantity { get; set; }

        [Column("total_stock_quantity")]
        public Int64? total_stock_quantity { get; set; }


    }
}
