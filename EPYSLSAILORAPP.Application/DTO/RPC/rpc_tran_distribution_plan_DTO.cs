using EPYSLSAILORAPP.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.RPC
{
    public class rpc_tran_distribution_plan_DTO
    {

        public Int64 tran_distribution_plan_details_id { get; set; }
        public Int64? tran_distribution_plan_id { get; set; }
        public Int64? techpack_id { get; set; }
        public string? style_code { get; set; } 
        public string? color_code { get; set; }
        public Int64? style_product_size_group_detid { get; set; }
        public string? style_product_size { get; set; }
        public Int64? style_product_unit { get; set; }
        public Decimal? mrp { get; set; }
        public Int64? size_quantity { get; set; }
        public Int64? available_quantity { get; set; }
        public Int64? distributed_quantity { get; set; }
        public string? techpack_number { get; set; }
    }

    public class rpc_tran_distribution_DTO
    {

        public Int64? tran_distribution_plan_id { get; set; }

        public String distribution_no { get; set; }

        public DateTime? distribution_date { get; set; }

        public String name { get; set; }

        public String event_title { get; set; }


    }


}
