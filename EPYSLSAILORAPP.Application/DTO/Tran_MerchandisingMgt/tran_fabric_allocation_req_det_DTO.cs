
using BDO.Core.Base;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;

using System.ComponentModel.DataAnnotations;
namespace EPYSLSAILORAPP.Domain.DTO
{

    public class tran_fabric_allocation_req_det_DTO : BaseEntity
    {

        public Int64? tran_fabric_allocation_req_det_id { get; set; }

        public Int64? tran_fabric_allocation_req_id {  get; set; }

        public Int64? item_master_id { get; set; }

       // public gen_item_master_DTO? obj_item_master { get; set; }

        public string? item_master { get; set; }

        public Int64? prev_tech_pack_id { get; set; }

       // public tran_tech_pack_DTO? obj_prev_tech_pack { get; set; }

        public string? prev_tech_pack { get; set; }

        public Int64? tech_pack_id { get; set; }

        //public tran_tech_pack_DTO? obj_tech_pack { get; set; }

        public string? tech_pack { get; set; }

        public Int64? measurement_unit_detail_id { get; set; }

        //public gen_measurement_unit_detail_DTO? obj_measurement_unit_detail { get; set; }

        public string? measurement_unit_detail { get; set; }


        public Decimal? booking_quantity { get; set; }

        public String? note { get; set; }

        public Int64? added_by { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_added { get; set; }

        public DateTime? date_updated { get; set; }

        public Int64? current_state { get; set; }
        

    }
}
