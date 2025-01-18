
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_warehouse_floor_rack_DTO : BaseEntity
		{

			public Int64? gen_warehouse_floor_rack_id  { get; set;}

			public Int64? gen_warehouse_floor_id  { get; set;}

			public String rack_name  { get; set;}

			public String rack_info  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
