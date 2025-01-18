
using BDO.Core.Base;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt
{

    public class tran_sample_order_detl_DTO : BaseEntity
    {

        public long? tran_sample_order_detl_id { get; set; }

        public long? tran_sample_order_id { get; set; }

        public string color_code { get; set; }

        public long? style_product_size_group_detid { get; set; }

        public long? order_quantity { get; set; }

        public long? style_product_unit_id { get; set; }

        public string remarks { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }

        public string strddlSizeHTML { get; set; }

        public string strddlUnitHTML { get; set; }

        public string txtcolor_code { get; set; }

        public string txtorder_quantity { get; set; }

        public string btnAddDeleteRowHTML { get; set; }

        public string style_product_size { get; set; }

    }
}
