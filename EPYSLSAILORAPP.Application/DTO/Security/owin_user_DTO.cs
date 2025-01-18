using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Domain.DTO
{
    public partial class owin_user_DTO
    {
        public Int64? userid { get; set; }
        [Required]
        public string user_name { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        public string email { get; set; }
        public string email_password { get; set; }
        public bool? is_super_user { get; set; }
        public bool? is_admin { get; set; }
        [Required]
        public bool? is_active { get; set; }
        public bool? is_loggedin { get; set; }
        public DateTime? logon_time { get; set; }
        public Int64? employee_code { get; set; }
        public Int64? added_by { get; set; }
        public DateTime date_added { get; set; }
        public Int64? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
        [Required]
        public Int64? role_id { get; set; }
        [Required]
        public string short_name { get; set; }
        public Int64? gen_team_group_id { get; set; }


        public string team_group_name {  get; set; }

        public string emp_pic { get; set; }

        public string new_emp_pic { get; set; }

        public string new_emp_pic_extension { get; set; }

        public Int64? designation_id { get; set; }

        public string newPassword { get; set; }

        public string designation_name { get; set; }

    }
}
