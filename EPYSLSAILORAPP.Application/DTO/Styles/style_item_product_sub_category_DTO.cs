
using BDO.Core.Base;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class style_item_product_sub_category_DTO:BaseEntity
		{

			public Int64? style_item_product_sub_category_id  { get; set;}

			public Int64? style_item_product_id  { get; set;}

			public String sub_category_name  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
