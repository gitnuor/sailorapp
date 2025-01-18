
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;

namespace EPYSLSAILORAPP.Domain.Entity
{
   
    [System.ComponentModel.DataAnnotations.Schema.Table("tran_sample_order_subgroup")]

    public class tran_sample_order_subgroup_entity : DapperExt
    {

        [Key]
        public Int64? tran_sample_order_subgroup_id { get; set; }

        public Int64? tran_sample_order_id { get; set; }

        public Int64? gen_item_structure_group_sub_id { get; set; }

        public String remarks { get; set; }

        public Int64? added_by { get; set; }

        public DateTime? date_added { get; set; }

        public Int64? updated_by { get; set; }

        public DateTime? date_updated { get; set; }


    }
}
