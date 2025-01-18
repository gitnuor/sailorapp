
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("gen_team_department_group_members")]

		public class gen_team_department_group_members_entity : DapperExt
		{

 			[Key]
			public Int64 gen_team_department_group_members_id  { get; set;}

 			[Column("gen_team_department_group_id")]
			public Int64? gen_team_department_group_id  { get; set;}

 			[Column("member_name")]
			public string? member_name  { get; set;}

 			[Column("member_employee_id")]
			public string? member_employee_id  { get; set;}

 			[Column("member_user_id")]
			public Int64? member_user_id  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

 			[Column("is_active")]
			public Int64? is_active  { get; set;}


		}
}
