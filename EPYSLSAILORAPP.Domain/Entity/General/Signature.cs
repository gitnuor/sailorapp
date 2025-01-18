using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.General
{
    [Table("Signature")]
    public class Signature : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public string Field { get; set; }
        public System.DateTime Dates { get; set; }
        public decimal LastNumber { get; set; }
        public string CompanyId { get; set; }
        public string SiteId { get; set; }
        #endregion 

        #region Additional Columns
        [Write(false)]
        public override bool IsModified => Field != "" || EntityState == EntityState.Modified;

        #endregion

        public Signature()
        {
            LastNumber = 1m;
            CompanyId = "1";
            SiteId = "1";
        }
    }

}
