using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_supplier_information")]

    public class gen_supplier_information_entity : DapperExt
    {

        [Key]
        public Int64? gen_supplier_information_id { get; set; }

        public String contact_code { get; set; }

        public String name { get; set; }

        public String short_name { get; set; }

        public String display_code { get; set; }

        public String email { get; set; }

        public String office_address { get; set; }

        public String factory_address { get; set; }

        public String fax_no { get; set; }

        [JsonProperty("gen_contact_category")]
        public String gen_contact_category { get; set; }

        public String contact_person { get; set; }

        public Int64? country_id { get; set; }

        public String city { get; set; }

        public String agent_name { get; set; }

        public string? geographical_location_list { get; set; }

        public String vat_bin_number { get; set; }

        public DateTime? vat_bin_number_start_date { get; set; }

        public DateTime? vat_bin_number_expire_date { get; set; }

        public string? vat_bin_documnet { get; set; }

        public String tin_number { get; set; }

        public string? tin_document { get; set; }

        public String trade_license { get; set; }

        public DateTime? trade_license_start_date { get; set; }

        public DateTime? trade_license_expire_date { get; set; }

        public string? trade_license_document { get; set; }

        public string? branch_list { get; set; }

        public string? concern_person_list { get; set; }

        public string? inco_term_list { get; set; }

        public string? mode_of_transction_list { get; set; }

        public string? payment_method_list { get; set; }

        public string? payment_term_list { get; set; }

        public string? bank_account_info_list { get; set; }

        public string? item_sub_group_detail_list { get; set; }
        [JsonProperty("credit_period")]
        public String credit_period { get; set; }

        [JsonProperty("calculation_of_tenure")]
        public String calculation_of_tenure { get; set; }

        public Boolean? active { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
