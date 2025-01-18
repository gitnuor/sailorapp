using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.BusinessPlanning
{
    public class StyleCategoryDTO
    {
       public int style_category_id { set; get; }
        public int style_master_category_id { set; get; }
        public string style_category_name { set; get; }
        public string style_category_code { set; get; }
        public bool has_sleeve { set; get; }
        public bool has_hoody { set; get; }
        public bool has_length { set; get; }
        public bool has_fit { set; get; }
        public bool is_active { set; get; }
        public int added_by { set; get; }
        public DateTime date_added { set; get; }
        public int ? updated_by { set; get; }
        public DateTime ? date_updated { set; get; }
    }
}
