

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
namespace EPYSLSAILORAPP.Domain.Entity
{ 
	[System.ComponentModel.DataAnnotations.Schema.Table("style_placement_information")]

		public class style_placement_information_entity : DapperExt
		{

 			[Key]
			public Int64? style_placement_information_id  { get; set;}

			public String placement  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
