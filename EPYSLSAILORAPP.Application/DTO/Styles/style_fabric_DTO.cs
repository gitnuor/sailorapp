
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class style_fabric_DTO 
		{

			public Int64? style_fabric_id  { get; set;}

			public String style_fabric_name  { get; set;}

			public Boolean? is_active  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
