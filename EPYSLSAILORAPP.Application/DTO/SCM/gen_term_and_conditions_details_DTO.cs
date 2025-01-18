
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_term_and_conditions_details_DTO : BaseEntity
    {

        [Required]
        [Column("gen_term_and_conditions_details_id")]
        public Int64 gen_term_and_conditions_details_id { get; set; }

        [Column("gen_term_and_conditions_id")]
        public Int64? gen_term_and_conditions_id { get; set; }

        [Column("description")]
        public string? description { get; set; }

        [Column("created_by")]
        public Int64? created_by { get; set; }

        [Column("created_date")]
        public DateTime? created_date { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("updated_date")]
        public DateTime? updated_date { get; set; }

        public String? term_condition_name { get; set; }


    }
}
