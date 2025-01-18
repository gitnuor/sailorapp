using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.BusinessPlanning
{
    public class StyleGenderDTO
    {
        
        public int style_gender_id { get; set; }
        public string style_gender_name { get; set; }
        public string style_gender_code { get; set; }
        public bool is_active { get; set; }
        public int added_by { get; set; }
        public DateTime? date_added { get; set; }       
        public int? updated_by { get; set; }
        public DateTime? date_updated { get; set; }
    }
}
