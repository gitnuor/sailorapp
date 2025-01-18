
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_division_DTO : BaseEntity
    {

        [Required]
        [Column("division_id")]
        public Int64 division_id { get; set; }

        [Column("name")]
        public string? name { get; set; }

        public List<gen_district_DTO> GenDistrict_List { get; set; }


    }
}
