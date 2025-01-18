
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class style_category_DTO 
		{

			public Int64? style_category_id  { get; set;}

			public Int64? style_master_category_id  { get; set;}

			public String style_category_name  { get; set;}

			public String style_category_code  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
