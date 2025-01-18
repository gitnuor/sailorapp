
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{ [System.ComponentModel.DataAnnotations.Schema.Table("tran_tech_pack_embellishment_det")]

		public class tran_tech_pack_embellishment_det_entity : DapperExt
		{

 			[Key]
			public Int64? tran_tech_pack_embellishment_det_id  { get; set;}

			public Int64? tran_tech_pack_embellishment_info_id  { get; set;}

			public Int64? gen_garment_part_id  { get; set;}

			public Int64? gen_process_master_detail_id  { get; set;}

			public Int64? added_by  { get; set;}

			public Int64? updated_by  { get; set;}

			public DateTime? date_added  { get; set;}

			public DateTime? date_updated  { get; set;}


		}
}
