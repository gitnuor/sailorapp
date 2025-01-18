
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class style_item_type_DTO 
		{

			public Int64? style_item_type_id  { get; set;}

			public String style_item_type_name  { get; set;}

			public String style_item_type_code  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
