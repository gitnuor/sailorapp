
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_team_group_DTO 
		{

			public Int64? gen_team_group_id  { get; set;}

			public Int64? department_id  { get; set;}

			public String team_group_name  { get; set;}

			public String team_head_name  { get; set;}

			public String team_head_id_number  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
