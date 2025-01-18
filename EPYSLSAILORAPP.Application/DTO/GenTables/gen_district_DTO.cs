
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_district_DTO : BaseEntity
    {

        [Required]
        [Column("district_id")]
        public Int64 district_id { get; set; }

        [Column("name")]
        public string? name { get; set; }

        [Column("divisionid")]
        public Int64? divisionid { get; set; }

        public List<gen_division_DTO> GenDivision_List { get; set; }


    }
}
