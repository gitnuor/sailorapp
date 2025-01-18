using Newtonsoft.Json;
using Postgrest.Attributes;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class lookup_trading_DTO 
    {

        [Required]
        [Column("lookup_id")]
        public Int64 lookup_id { get; set; }

        [Column("lookup_master_id")]
        public Int64? lookup_master_id { get; set; }

        [Required]
        [Column("lookup_value")]
        public string lookup_value { get; set; }

        [Required]
        [Column("is_active")]
        public Boolean is_active { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

    }

}
