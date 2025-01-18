
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_email_template")]

    public class gen_email_template_entity : DapperExt
    {

        [Key]
        public Int64 gen_email_template_id { get; set; }
        [Column("email_template_name")]
        public string email_template_name { get; set; }

        [Column("email_template_html")]
        public string? email_template_html { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("gen_email_template_category_id")]
        public Int64? gen_email_template_category_id { get; set; }


    }
}
