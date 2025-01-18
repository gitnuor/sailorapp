
using BDO.Core.Base;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_garment_part_DTO  : BaseEntity
		{

			public Int64? gen_garment_part_id  { get; set;}

			public String garment_part_name  { get; set;}

			public String short_code  { get; set;}

			public Int64? multiplier  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
