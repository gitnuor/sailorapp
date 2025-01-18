using BDO.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.DTO.TranTables
{
    public class tran_transport_dto: BaseEntity
    {
        public int? transport_id { get; set; }
        public string? transport_type { get; set; }
    }
}
