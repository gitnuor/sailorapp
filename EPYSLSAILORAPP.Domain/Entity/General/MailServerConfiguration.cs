using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.General
{
    [Table("MailServerConfiguration")]
    public class MailServerConfiguration : DapperBaseEntity
    {
        #region Table properties

        [Key]
        public int ConfigurationID { get; set; }

        public string ConfigurationName { get; set; }

        public string SMTPServerIP { get; set; }

        public int SMTPServerPort { get; set; }

        public string SMTPMailID { get; set; }

        public string SMTPMailDisplayName { get; set; }

        public string SMTPMailPassword { get; set; }

        public bool IsActive { get; set; }

        #endregion Table properties

        public override bool IsModified => EntityState == EntityState.Modified || ConfigurationID > 0;

        public MailServerConfiguration()
        {

        }

    }
}
