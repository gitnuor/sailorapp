

using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Postgrest.Models;
using Dapper.Contrib.Extensions;
namespace EPYSLSAILORAPP.Domain.Entity
{
    [System.ComponentModel.DataAnnotations.Schema.Table("gen_tran_transport")]

    public class gen_tran_transport_entity : DapperExt
    {

        [Key]

        public Int64? transport_id { get; set; }
        public String transport_type { get; set; }


    }

    [System.ComponentModel.DataAnnotations.Schema.Table("gen_transaction_mode")]
    public class gen_transaction_mode_entity : DapperExt
    {

        [Key]

        public Int64? transaction_mode_id { get; set; }
        public String transaction_mode_type { get; set; }

    }



    [System.ComponentModel.DataAnnotations.Schema.Table("gen_delivery_status")]
    public class gen_delivery_status_entity : DapperExt
    {

        [Key]

        public Int64? delivery_status_id { get; set; }
        public String delivery_status_type { get; set; }

    }



}
