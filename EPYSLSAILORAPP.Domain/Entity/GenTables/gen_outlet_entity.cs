
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_outlet")]

		public class gen_outlet_entity : DapperExt
		{

 			[Key]
			public Int64? outlet_id  { get; set;}

			public String outlet_code  { get; set;}

			public String outlet_name  { get; set;}

			public String outlet_description  { get; set;}

			public Int64? district_id  { get; set;}

			public Int64? division_id  { get; set;}

			public String outlet_address  { get; set;}

			public Boolean? is_active  { get; set;}

			public String outlet_logo_path  { get; set;}

			public Int64? sequence_no  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
