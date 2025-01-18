
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Domain.Security
{
    [System.ComponentModel.DataAnnotations.Schema.Table("owin_user")]
    public class owin_user_entity : DapperExt
    {
        #region Table properties
        [Key]
        public Int64? userid { get; set; }

        [Column("user_name")]
        public string user_name { get; set; }
        [Column("name")]
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string email_password { get; set; }
        public bool? is_super_user { get; set; }
        public bool? is_admin { get; set; }
        public bool? is_active { get; set; }
        public bool? is_loggedin { get; set; }
        public DateTime? logon_time { get; set; }
        public Int64? employee_code { get; set; }
        public int added_by { get; set; }
        public DateTime date_added { get; set; }
        public Int64? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
        public Int64? role_id { get; set; }
        public Int64? gen_team_group_id { get; set; }

        public string emp_pic { get; set; }

        public Int64? designation_id { get; set; }
        #endregion


    }

    public class owin_user_entity_Ext: owin_user_entity
    {
        public string designation_name { get; set; }
    }

}
