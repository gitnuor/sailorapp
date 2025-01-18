
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_process_master_DTO 
		{

			public Int64? gen_process_master_id  { get; set;}

			public String process_name  { get; set;}

			public Boolean? has_sub_process  { get; set;}

			public Boolean? has_embellishment  { get; set;}

			public Int64? measurement_unit_detail_id  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
