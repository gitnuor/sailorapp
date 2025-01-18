
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class gen_tran_transport_DTO : BaseEntity
		{

			[Required]
 			[Column("transport_id")]
			public Int64 transport_id  { get; set;}

 			[Column("transport_type")]
			public string? transport_type  { get; set;}


		}
}
