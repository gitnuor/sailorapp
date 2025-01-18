
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_measurement_unit_detail_DTO 
		{

			public Int64? gen_measurement_unit_detail_id  { get; set;}

			public Int64? gen_measurement_unit_id  { get; set;}

			public String unit_detail_title  { get; set;}

			public String unit_detail_display  { get; set;}

			public Decimal? relative_factor  { get; set;}

			public Boolean? no_fraction  { get; set;}

			public String remarks  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
