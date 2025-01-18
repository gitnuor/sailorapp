
using BDO.Core.Base;
using EPYSLSAILORAPP.Domain.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace EPYSLSAILORAPP.Domain.DTO
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_inco_term")]
    public class gen_inco_term_DTO : DapperExt
    {
        [Key]
        [Required]
        public Int64? gen_inco_term_id { get; set; }

        public String inco_term { get; set; }

        [Required]
        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; } 
        public bool? is_checked { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
