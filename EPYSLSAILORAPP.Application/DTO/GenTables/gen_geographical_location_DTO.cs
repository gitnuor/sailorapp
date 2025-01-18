
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_geographical_location_DTO : BaseEntity
    {

        public Int64? gen_geographical_location_id { get; set; }

        public String location_title { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public bool? is_checked { get; set; }


        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
