
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.RPC;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_scm_bill_submission_DTO : BaseEntity
    {

        public Int64? bill_submission_id { get; set; }

        [Required]
        public Int64? po_id { get; set; }
        public String po_no { get; set; }
        public String supplier_name { get; set; }
        public DateTime? po_date { get; set; }
        public DateTime? po_approved_date { get; set; }
        [Required]
        public Int64? supplier_id { get; set; }

        public String bill_no { get; set; }

        public String challan_no { get; set; }

        [Required]
        public DateTime? challan_date { get; set; }

        [Required]
        public DateTime? bill_date { get; set; }

        public Decimal? total_po_amount { get; set; }

        public Decimal? loading_cost_in_percentage { get; set; }

        public Decimal? loading_cost { get; set; }

        public Decimal? transport_cost_in_percentage { get; set; }

        public Decimal? transport_cost { get; set; }

        public Decimal? discount_in_percentage { get; set; }

        public Decimal? discount_amount { get; set; }

        public Decimal? vat_in_percentage { get; set; }

        public Decimal? vat_amount { get; set; }

        public Decimal? total_value { get; set; }

        public Boolean? is_submitted { get; set; }
        public Boolean? is_send_for_approval { get; set; }

        public DateTime? submitted_date { get; set; }

        public Int64? submitted_by { get; set; }

        public Int64? is_approved { get; set; }

        public DateTime? approved_date { get; set; }

        public Int64? approved_by { get; set; }

        public Int32? status { get; set; }

        public JArray documents { get; set; }

        public JArray terms_conditions { get; set; }

        public String remarks { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }
        public List<tran_scm_po_details_DTO> po_details { get; set; }
        public List<file_upload> List_Files { get; set; }

        public Int64? fiscal_year_id { get; set; }
        public Int64? event_id { get; set; }
        public Int64 total_rows { get; set; }


        public DateTime? delivery_start_date { get; set; }

        public DateTime? delivery_end_date { get; set; }
        
        public string? delivery_unit_name { get; set; }
        
        public List<rpc_proc_sp_get_data_tran_scm_po_DTO> detChallan { get; set; }

        public List<rpc_tran_mcd_receive_detail_DTO> detItem { get; set; }

    }
}
