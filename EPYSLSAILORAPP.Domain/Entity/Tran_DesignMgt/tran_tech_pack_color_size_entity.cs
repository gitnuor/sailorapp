
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_tech_pack_color_size")]

		public class tran_tech_pack_color_size_entity : DapperExt
		{

 			[Key]
			public Int64? tran_tech_pack_color_size_id  { get; set;}

			public Int64? tran_tech_pack_color_id  { get; set;}

			public Int64? style_product_size_group_detid  { get; set;}

			public Int64? style_color_size_quantity  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
