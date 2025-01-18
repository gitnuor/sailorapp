
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_scm_po_details_DTO : BaseEntity
    {

        public Int64? po_details_id { get; set; }

        [Required]
        public Int64? po_id { get; set; }

        [Required]
        public Int64? pr_id { get; set; }
        public string? item_name { get; set; }
        public string? item_spec { get; set; }
        public string? sub_group_name { get; set; }
        //[Required]
        public Int64? item_id { get; set; }

        public Decimal? item_quantity { get; set; }
        public Decimal? receive_quantity { get; set; }
        public Decimal? recevQty { get; set; }

        public String unit { get; set; }

        public Decimal? unit_price { get; set; }

        public Decimal? total_price { get; set; }

        public String remarks { get; set; }
        public string? uomText { get; set; }
        public long? uom { get; set; }
        public Int64? measurement_unit_detail_id { get; set; }
        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }
        public String revise_status { get; set; }
        public Int64? gen_item_master_id { get; set; }
        public string measurement_unit { get; set; }
        



    }
}
