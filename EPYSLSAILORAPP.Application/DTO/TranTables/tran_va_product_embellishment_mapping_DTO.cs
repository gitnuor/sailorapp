
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_va_product_embellishment_mapping_DTO 
		{

			public Int64? tran_va_product_embellishment_mapping_id  { get; set;}

			public Int64? style_item_product_id  { get; set;}

			public Int64? style_embelishment_id  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
