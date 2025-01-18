
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_distribution_plan_details")]

		public class tran_distribution_plan_details_entity_test : DapperExt
		{

 			[Key]
			public Int64 tran_distribution_plan_details_id  { get; set;}

 			[Column("tran_distribution_plan_id")]
			public Int64? tran_distribution_plan_id  { get; set;}

 			[Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("style_code")]
			public string? style_code  { get; set;}

 			[Column("color_code")]
			public string? color_code  { get; set;}

 			[Column("style_product_size_group_detid")]
			public Int64? style_product_size_group_detid  { get; set;}

 			[Column("style_product_size")]
			public string? style_product_size  { get; set;}

 			[Column("style_product_unit")]
			public Int64? style_product_unit  { get; set;}

 			[Column("mrp")]
			public Decimal? mrp  { get; set;}

 			[Column("size_quantity")]
			public Int64? size_quantity  { get; set;}

 			[Column("available_quantity")]
			public Int64? available_quantity  { get; set;}

 			[Column("distributed_quantity")]
			public Int64? distributed_quantity  { get; set;}
		}

    public class tran_distribution_plan_details_ext : tran_distribution_plan_details_entity_test
    {
        public string? techpack_number { get; set; }
    }
}
