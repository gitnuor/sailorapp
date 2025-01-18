using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt
{

    public class tran_designer_review_DTO : tran_pre_costing_DTO
    {

        public long? tran_designer_review_id { get; set; }

        public long? tran_pre_costing_id { get; set; }

        [Required(ErrorMessage = "Required")]
        public long? no_review { get; set; }
        [Required(ErrorMessage = "Required")]
        public long? no_confirmation { get; set; }


        [Required(ErrorMessage = "Required")]
        public long? no_confirmation_with_modification { get; set; }

        [Required(ErrorMessage = "Required")]
        public long? no_of_not_confirmed { get; set; }

        public long? status { get; set; }

        public string? remarks { get; set; }

        public bool? is_submitted { get; set; }

        public bool? is_approved { get; set; }

        public long? approved_by { get; set; }

        public DateTime? approve_date { get; set; }

        public string approve_remarks { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }
        public List<file_upload> List_image { get; set; }
        public List<file_upload> List_Files { get; set; }
        public List<string> DeleteList { get; set; }
        public string document { get; set; }

        public Int64? fiscal_year_id { get; set; }

        public Int64? event_id { get; set; }

    }
}
