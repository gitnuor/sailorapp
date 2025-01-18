
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class tran_finishing_production_process_DTO : BaseEntity
		{

			[Required]
 			[Column("tran_finishing_production_process_id")]
			public Int64 tran_finishing_production_process_id  { get; set;}

 			[Column("tran_finish_receive_date")]
			public DateTime? tran_finish_receive_date  { get; set;}

        
            public string? production_process_date { get; set; }

            [Column("techpack_id")]
			public Int64? techpack_id  { get; set;}

 			[Column("style_item_product_category")]
			public string? style_item_product_category  { get; set;}

 			[Column("style_item_product_id")]
			public Int64? style_item_product_id  { get; set;}

 			[Column("gen_finishing_process_id")]
			public Int64? gen_finishing_process_id  { get; set;}

 			[Column("finishing_process_name")]
			public string? finishing_process_name  { get; set;}

        public string? techpack_number { get; set; }

        [Column("is_iron")]
			public Int64? is_iron  { get; set;}

 			[Column("is_hang_tag")]
			public Int64? is_hang_tag  { get; set;}

 			[Column("is_folding")]
			public Int64? is_folding  { get; set;}

 			[Column("is_poly")]
			public Int64? is_poly  { get; set;}

 			[Column("fiscal_year_id")]
			public Int64? fiscal_year_id  { get; set;}

 			[Column("event_id")]
			public Int64? event_id  { get; set;}

 			[Column("added_by")]
			public Int64? added_by  { get; set;}

 			[Column("updated_by")]
			public Int64? updated_by  { get; set;}

 			[Column("date_added")]
			public DateTime? date_added  { get; set;}

 			[Column("date_updated")]
			public DateTime? date_updated  { get; set;}

 			[Column("is_submitted")]
			public Int64? is_submitted  { get; set;}

 			[Column("submitted_by")]
			public Int64? submitted_by  { get; set;}

 			[Column("is_approved")]
			public Int64? is_approved  { get; set;}

 			[Column("approved_by")]
			public Int64? approved_by  { get; set;}

 			[Column("note")]
			public string? note  { get; set;}

			public JArray? tran_finishing_production_process_details  { get; set;}

			public List<tran_finishing_production_process_details_DTO> details {get; set;}
        public string tran_finishing_production_process_details_list { get; set; }

    }
	public class tran_finishing_production_detail_process_DTO 
	{  
        public Int64 tran_finishing_production_process_id { get; set; }
        public Int64 tran_finishing_production_process_details_id { get; set; }
        public Int64? techpack_id { get; set; } 
        public Int64? style_product_size_group_detid { get; set; } 
        public string? size_name { get; set; }     
        public string? color_code { get; set; }   
        public Int64? finish_receive_qty { get; set; }
        public Int64? production_quantity { get; set; }  
        public Int64? bal_qty { get; set; }

    }

}
