

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ 
	[System.ComponentModel.DataAnnotations.Schema.Table("style_fabric_detl")]

		public class style_fabric_detl_entity : DapperExt
		{

 			[Key]
			public Int64? style_fabric_detl_id  { get; set;}

			public Int64? style_fabric_id  { get; set;}

			public Int64? gen_segment_id  { get; set;}

			public String segment_display_name  { get; set;}

			public Boolean? is_active  { get; set;}

			public Int64? added_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
