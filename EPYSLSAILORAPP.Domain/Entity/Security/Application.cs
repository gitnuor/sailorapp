using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Security
{
    [Table("Application")]
    public class Application : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public int ApplicationID { get; set; }

        public string ApplicationName { get; set; }

        public string ApplicationDescription { get; set; }

        public bool IsDefault { get; set; }

        public byte[] ApplicationLogo { get; set; }

        public string ApplicationLogoPath { get; set; }

        public int SequenceNo { get; set; }

        public bool IsInUse { get; set; }

        public bool HasMultipleDb { get; set; }

        #endregion

        #region Additional properties
        [Write(false)]
        public override bool IsModified => ApplicationID > 0 || EntityState == EntityState.Modified;

        [Write(false)]
        public virtual List<ApplicationUser> ApplicationUsers { get; set; }

        public virtual List<Menu> Menus { get; set; }

        #endregion

        public Application()
        {
            IsDefault = false;
            SequenceNo = 0;
            IsInUse = false;
            HasMultipleDb = false;
            ApplicationUsers = new List<ApplicationUser>();
            Menus = new List<Menu>();
        }
    }
}
