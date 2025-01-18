
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class style_label_DTO 
		{

			public Int64? style_label_id  { get; set;}

			public String style_label_name  { get; set;}

			public String short_name  { get; set;}

			public String label_code  { get; set;}

			public String label_description  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
