using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain
{
    [Table("TableReturn")]
    public class TableReturn
    {
       
        public Int64 TotalCount { get; set; }
       

        public TableReturn()
        {
            TotalCount = 0;
          
        }
    }
}
