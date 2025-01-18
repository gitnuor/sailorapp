
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class style_master_category_structure_subgroup_mapping_DTO 
		{

			public Int64? master_category_structure_subgroup_mapping_id  { get; set;}

			public Int64? style_master_category_id  { get; set;}

			public Int64? gen_item_structure_group_sub_id  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
