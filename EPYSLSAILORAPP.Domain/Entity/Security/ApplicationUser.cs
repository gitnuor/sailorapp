using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Security
{
    [Table("ApplicationUser")]
    public class ApplicationUser : DapperBaseEntity
    {
        #region Table properties
        [Key]

        public int ApplicationID { get; set; }

        public int UserCode { get; set; }

        public bool? IsDefault { get; set; }

        #endregion

        #region Additional properties
        [Write(false)]
        public override bool IsModified => ApplicationID > 0 || EntityState == EntityState.Modified;

        [Write(false)]
        public virtual Application Application { get; set; }
        #endregion

        public ApplicationUser()
        {
            IsDefault = false;
        }
    }
}
