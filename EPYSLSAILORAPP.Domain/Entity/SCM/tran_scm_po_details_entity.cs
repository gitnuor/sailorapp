

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ 
	[System.ComponentModel.DataAnnotations.Schema.Table("tran_scm_po_details")]

		public class tran_scm_po_details_entity : DapperExt
		{

 			[Key]
			public Int64? po_details_id  { get; set;}

			public Int64? po_id  { get; set;}

			public Int64? pr_id  { get; set;}

			public Int64? item_id  { get; set;}

			public Decimal? item_quantity  { get; set;}

			public String unit  { get; set;}

			public Decimal? unit_price  { get; set;}

			public Decimal? total_price  { get; set;}

			public String remarks  { get; set;}


		}
}
