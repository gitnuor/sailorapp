using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using EPYSLSAILORAPP.Domain.Statics;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;

namespace EPYSLSAILORAPP.Application.DTO.TranTables
{
    public partial class tran_bp_year_dto
    {
        #region Table properties
        [PrimaryKey("tran_bp_year_id", false)]
        public long tran_bp_year_id { get; set; }

        public long fiscal_year_id { get; set; }

        public decimal? gross_sales { get; set; }

        public decimal? yearly_gross_discount { get; set; }

        public decimal? yearly_gross_net { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public long? TotalRecord { get; set; }

        public gen_fiscal_year gen_fiscal_year { get; set; }

        public string remarks { get; set; }
        public string approve_remarks { get; set; }
        public long? approved_by { get; set; }
        public DateTime? approve_date { get; set; }

        public bool? is_approved { get; set; }
        public bool? is_submitted { get; set; }

        public JArray event_list_json { get; set; }

        #endregion Table properties

        #region Additional Columns

        public List<tran_bp_event_dto> tran_bp_event_list { get; set; }


        #endregion

        public tran_bp_year_dto()
        {
        }
    }

    public partial class tran_bp_year_dto_ext
    {
        #region Table properties
       
        public long tran_bp_year_id { get; set; }

        public long fiscal_year_id { get; set; }

        public decimal? gross_sales { get; set; }

        public decimal? yearly_gross_discount { get; set; }

        public decimal? yearly_gross_net { get; set; }

        public long? added_by { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public long? TotalRecord { get; set; }

        public gen_fiscal_year gen_fiscal_year { get; set; }

        public string remarks { get; set; }
        public string approve_remarks { get; set; }
        public long? approved_by { get; set; }
        public DateTime? approve_date { get; set; }

        public bool? is_approved { get; set; }
        public bool? is_submitted { get; set; }

        #endregion Table properties

    }
}
