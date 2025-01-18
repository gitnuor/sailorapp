
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_designation_DTO : BaseEntity
		{

			[Required]
 			[Column("designation_id")]
			public Int64 designation_id  { get; set;}

 			[Column("designation_name")]
			public string? designation_name  { get; set;}


		}
}
