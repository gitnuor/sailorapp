using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EPYSLSAILORAPP.Application.DTO.BusinessPlanning
{
    public class StyleMasterCategoryDTO
    {
        public int style_master_category_id { set; get; }
        public string master_category_name { set; get; }
        public string master_category_code { set; get; }
        public bool is_active { set; get; }
        public int added_by { set; get; }
        public DateTime date_added { set; get; }
        public int ? updated_by { set; get; }
        public DateTime ? date_updated { set; get; }
    }
}
