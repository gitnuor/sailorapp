
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_tech_pack_embellishment_info")]

    public class tran_tech_pack_embellishment_info_entity : DapperExt
    {

        [Key]
        public Int64? tran_tech_pack_embellishment_info_id { get; set; }

        public Int64? tran_tech_pack_id { get; set; }

        public Int64? gen_process_master_id { get; set; }

		public string process_name { get; set; }

		public Int64? supplier_id { get; set; }

        [JsonProperty("supplier_info")]
        public string supplier_info { get; set; }

        public String is_garment_form { get; set; }

		[JsonProperty("embellishment_details")]
		public string embellishment_details { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }
        public Int64? is_workorder { get; set; }


    }
}
