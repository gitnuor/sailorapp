
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class gen_team_department_group_members_DTO
    {


        [Column("group_members_id")]
        public Int64 group_members_id { get; set; }

        [Column("group_id")]
        public Int64? group_id { get; set; }

        [Column("member_name")]
        public string? member_name { get; set; }

        [Column("member_employee_id")]
        public string? member_employee_id { get; set; }

        [Column("member_user_id")]
        public Int64? member_user_id { get; set; }

        [Column("added_by")]
        public Int64? added_by { get; set; }

        [Column("updated_by")]
        public Int64? updated_by { get; set; }

        [Column("date_added")]
        public DateTime? date_added { get; set; }

        [Column("date_updated")]
        public DateTime? date_updated { get; set; }

        [Column("is_active")]
        public Int64? is_active { get; set; }

       

        public string? emp_pic { get; set; }
        public string? designation_name { get; set; }
        public string? employee_code { get; set; }
        public string? name { get; set; }
        public string? new_emp_pic { get; set; }

     

    }
}
