using EPYSLSAILORAPP.Application.DTO.GenTables;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Domain.Statics;
using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.BusinessPlanning
{
    public class RangePlanDTO
    {
      
        public int range_plan_id { get; set; }


        public int fiscal_year_id { get; set; }


        public int season_id { get; set; }


        public int style_label_id { get; set; }

        public int style_gender_id { get; set; }

        public int style_master_category_id { get; set; }

        public int style_category_id { get; set; }

        public int plan_qty { get; set; }

        public decimal own_production_percent { get; set; }

        public decimal sub_contact_percent { get; set; }

        public decimal import_percent { get; set; }

        public decimal own_production_qty { get; set; }

        public decimal sub_contact_qty { get; set; }

        public decimal import_qty { get; set; }

        public int added_by { get; set; }

        public DateTime date_added { get; set; }

        public int? updated_by { get; set; }

        public DateTime? date_updated { get; set; }



        #region Additional Columns

        //
        //public override bool IsModified => EntityState == EntityState.Modified || RangePlanID > 0;

        public string year_name { get; set; }


        public string season_name { get; set; }

        public string style_gender_name { get; set; }

        public string style_label_name { get; set; }

        public string master_category_name { get; set; }

        public string style_category_name { get; set; }

        public IEnumerable<Select2OptionModel> fiscal_year_list { get; set; }

        public IEnumerable<Select2OptionModel> season_list { get; set; }

        public IEnumerable<Select2OptionModel> style_gender_list { get; set; }

        public IEnumerable<Select2OptionModel> style_label_list { get; set; }

        public IEnumerable<Select2OptionModel> style_master_category_list { get; set; }

        public IEnumerable<Select2OptionModel> style_category_list { get; set; }

        public StyleGenderDTO style_gender { get; set; }
        public season_dto season_master { get; set; }
        public gen_fiscal_year_dto fiscal_year { get; set; }
        public StyleLabelDTO style_label { get; set; }
        public StyleMasterCategoryDTO style_master_category { get; set; }
        public StyleCategoryDTO style_category { get; set; }
        #endregion Additional Columns

    }
}
