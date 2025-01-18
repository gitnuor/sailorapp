
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_supplier_information_DTO : BaseEntity
    {

        public Int64? gen_supplier_information_id { get; set; }

        public String? contact_code { get; set; }

        public String name { get; set; }

        public String? short_name { get; set; }

        public String? display_code { get; set; }

        public String? email { get; set; }

        public String? office_address { get; set; }

        public String? factory_address { get; set; }

        public String? fax_no { get; set; }

        public String? contact_person { get; set; }

        public Int64? country_id { get; set; }

        public String? city { get; set; }

        public String? agent_name { get; set; }

        public String? geographical_location_list { get; set; }

        public String? vat_bin_number { get; set; }

        public DateTime? vat_bin_number_start_date { get; set; }

        public DateTime? vat_bin_number_expire_date { get; set; }

        public String? vat_bin_documnet { get; set; }

        public String? tin_number { get; set; }

        public String? tin_document { get; set; }

        public String? trade_license { get; set; }

        public DateTime? trade_license_start_date { get; set; }

        public DateTime? trade_license_expire_date { get; set; }

        public String? trade_license_document { get; set; }

        public String? branch_list { get; set; }

        public String? concern_person_list { get; set; }

        public String?  inco_term_list { get; set; }

        public String? mode_of_transction_list { get; set; }

        public String? payment_method_list { get; set; }

        public String? payment_term_list { get; set; }

        public String? bank_account_info_list { get; set; }

        public String? item_sub_group_detail_list { get; set; }

        public String? contact_category { get; set; }

        public String? credit_period { get; set; }

        public String? calculation_of_tenure { get; set; }

        public gen_contact_category_DTO? obj_contact_category { get; set; }

        public gen_credit_period_DTO? obj_credit_period { get; set; }

        public gen_calculation_of_tenure_DTO? obj_calculation_of_tenure { get; set; }

        public Boolean? active { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

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

        public List<concern_person> List_concern_person { get; set; }

        public List<branch> List_branch { get; set; }

        public List<bank_account_info> List_bank_account_info { get; set; }

        public List<item_subgroup_info> List_item_subgroup_info { get; set; }

        public List<file_upload> List_vat_bin_docs { get; set; }
        public List<file_upload> List_tin_docs { get; set; }
        public List<file_upload> List_tradelincense_docs { get; set; }

        public List<gen_credit_period_DTO> List_creditperiod { get; set; }

        public List<gen_calculation_of_tenure_DTO> List_calculationoftenure { get; set; }

    }

    public class concern_person
    {
        public String? name { get; set; }
        public String? address { get; set; }
        public String? designation { get; set; }
        public String? phone { get; set; }
        public String? email { get; set; }

    }

    public class branch
    {
        public String? branch_name { get; set; }
        public String? address { get; set; }

    }

    public class bank_account_info
    {
        public gen_bank_DTO gen_bank { get; set; }
        public gen_bank_branch_DTO gen_bankbranch { get; set; }

       
        public String acno { get; set; }
        public String? routingno { get; set; }
        public String? swiftcode { get; set; }

        public String? is_default { get; set; }

    }

    public class item_subgroup_info
    {
        public gen_item_structure_group_sub_DTO item_structure_group { get; set; }
      
        public String? supplier_type { get; set; }
        public String? is_active { get; set; }


    }
}
