

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
namespace EPYSLSAILORAPP.Domain.Entity
{ 
	[System.ComponentModel.DataAnnotations.Schema.Table("style_sleeve_info")]

		public class style_sleeve_info_entity : DapperExt
		{

 			[Key]
			public Int64? style_sleeve_info_id  { get; set;}

			public String sleeve_info { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
