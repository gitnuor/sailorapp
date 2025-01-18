
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_chat_group_DTO : BaseEntity
    {

        [Required]
        [Column("chat_group_id")]
        public Int64 chat_group_id { get; set; }

        [Required]
        [Column("chat_group_name")]
        public string chat_group_name { get; set; }

        [Required]
        public string group_users { get; set; }

        [Column("added_by")]
        public Int64 added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        public List<gen_chat_user> det_list { get; set; }


    }

    public class gen_chat_user
    {
        public string user_name {  get; set; }

        public string name { get; set; }

        public string emp_pic { get; set; }
    }


}
