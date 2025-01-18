using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.Entity.GenTables
{
    
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_legal_information")]

    public class gen_legal_information_entity : DapperExt
    {

        [PrimaryKey("gen_legal_information_id", false)]
        public long? gen_legal_information_id { get; set; }

        public string vat_bin_number { get; set; }

        public DateTime? vat_bin_number_start_date { get; set; }

        public DateTime? vat_bin_number_expire_date { get; set; }

        public string tin_number { get; set; }

        public string trade_license { get; set; }

        public DateTime? trade_license_start_date { get; set; }

        public DateTime? trade_license_expire_date { get; set; }

        public long? gen_general_information_id { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
