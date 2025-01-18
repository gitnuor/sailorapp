
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_term_and_conditions_DTO : BaseEntity
    {

        [Required]
        [Column("gen_term_and_conditions_id")]
        public Int64 gen_term_and_conditions_id { get; set; }

        [Required]
        [Column("term_condition_name")]
        public string term_condition_name { get; set; }

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

        [Required]
        [Column("menu_id")]
        public Int64 menu_id { get; set; }

        public List<gen_term_and_conditions_details_DTO> GenTermAndConditionsDetails_List { get; set; }


    }


   

}
