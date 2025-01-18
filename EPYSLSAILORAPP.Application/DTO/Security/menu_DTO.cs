
using BDO.Core.Base;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

		public class menu_DTO : BaseEntity
		{

        public Int32? menu_id { get; set; }

        public Int32? parent_id { get; set; }

        public String menu_caption { get; set; }

        public String navigate_url { get; set; }

        public String image_url { get; set; }

        public Int32? seq_no { get; set; }

        public Boolean? is_visible { get; set; }

        public Int32? added_by { get; set; }

        public Int32? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }



    }
}
