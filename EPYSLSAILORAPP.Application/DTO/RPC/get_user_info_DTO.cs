
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.RPC
{

    public class rpc_get_user_info_DTO
    {

        public Int64? userid { get; set; }

        public String name { get; set; }

        public String user_name { get; set; }

        public String password { get; set; }

        public String email { get; set; }

        public String email_password { get; set; }

        public Boolean? is_super_user { get; set; }

        public Boolean? is_admin { get; set; }

        public Boolean? is_active { get; set; }

        public Boolean? is_loggedin { get; set; }

        public DateTime? logon_time { get; set; }

        public Int64? employee_code { get; set; }

        public Int64? role_id { get; set; }

        public Int64? gen_team_group_id { get; set; }

        public String role_name { get; set; }

        public String team_group_name { get; set; }

        public string emp_pic { get; set; }



    }
}
