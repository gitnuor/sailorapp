
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class style_item_origin_DTO 
		{

			public Int64? style_item_origin_id  { get; set;}

			public String style_item_origin_name  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
