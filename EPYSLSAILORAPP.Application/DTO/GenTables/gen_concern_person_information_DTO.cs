
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_concern_person_information_DTO 
		{

			public Int64? gen_concern_person_information_id  { get; set;}

			public String name  { get; set;}

			public String designation  { get; set;}

			public String phone_no  { get; set;}

			public String fax  { get; set;}

			public String email  { get; set;}

			public String address  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
