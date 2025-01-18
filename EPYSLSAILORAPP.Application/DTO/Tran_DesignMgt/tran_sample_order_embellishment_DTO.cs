
using BDO.Core.Base;
using Postgrest.Attributes;
using Postgrest.Models;

namespace EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt
{

    public class tran_sample_order_embellishment_DTO : BaseEntity
    {

        public long? tran_sample_order_embellishment_id { get; set; }

        public long? tran_sample_order_id { get; set; }

        public long? embellishment_id { get; set; }

        public string remarks { get; set; }

        public long? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public long? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
