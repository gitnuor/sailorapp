using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;



namespace EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt
{
    [Postgrest.Attributes.Table("tran_transport")]
    public class tran_transport: BaseModel
    {
        [PrimaryKey("transport_id", false)]
        public Int64? transport_id { get; set; }
        public string? transport_type { get; set; }
    }



}
