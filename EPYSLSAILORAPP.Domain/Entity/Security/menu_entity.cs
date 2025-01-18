using Dapper.Contrib.Extensions;
//using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{

    [System.ComponentModel.DataAnnotations.Schema.Table("menu")]
    public class menu_entity : DapperExt
		{

        
        [Key]
        public Int64? menu_id { get; set; }

        public Int64? parent_id { get; set; }

        public String menu_caption { get; set; }

        public String navigate_url { get; set; }

        public String image_url { get; set; }

        public Int64? seq_no { get; set; }

        public Boolean? is_visible { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }
        

    }
}
