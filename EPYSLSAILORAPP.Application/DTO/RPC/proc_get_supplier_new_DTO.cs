
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Domain.DTO;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_proc_get_supplier_new_DTO
    {

        public String gen_contact_category_list { get; set; }

        public String gen_geographical_location_list { get; set; }

        public String gencountry_list { get; set; }

        public String genbank_list { get; set; }

        public String genbankbranch_list { get; set; }

        public String genincoterm_list { get; set; }

        public String genpaymentterm_list { get; set; }

        public String genpaymentmethod_list { get; set; }

        public String genmodeofransaction_list { get; set; }

        public String genitemstructuregroupsub_list { get; set; }

        public String gencreditperiod_list { get; set; }

        public String gencalculationoftenure_list { get; set; }

        public List<gen_contact_category_DTO> List_contact_category { get; set; }

        public List<gen_geographical_location_DTO> List_geographical_location { get; set; }

        public List<gen_country_DTO> List_country { get; set; }

        public List<gen_bank_DTO> List_bank { get; set; }

        public List<gen_bank_branch_DTO> List_bankbranch { get; set; }

        public List<gen_inco_term_DTO> List_incoterm { get; set; }

        public List<gen_payment_term_DTO> List_paymentterm { get; set; }

        public List<gen_payment_method_DTO> List_paymentmethod { get; set; }

        public List<gen_mode_of_transaction_DTO> List_mode_of_transaction { get; set; }

        public List<gen_item_structure_group_sub_DTO> List_itemstructuregroupsub { get; set; }

        public List<gen_credit_period_DTO> List_creditperiod { get; set; }

        public List<gen_calculation_of_tenure_DTO> List_calculationoftenure { get; set; }
    }
}
