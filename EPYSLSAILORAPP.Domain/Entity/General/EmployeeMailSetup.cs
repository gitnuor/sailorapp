using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.General
{
    [Table("EmployeeMailSetup")]
    public class EmployeeMailSetup : DapperBaseEntity
    {
        #region Table properties

        [Key]
        public int SetupID { get; set; }

        public int EmployeeCode { get; set; }

        public int SetupForID { get; set; }

        public required string ToMailID { get; set; }

        public string? CCMailID { get; set; }

        public string? BCCMailID { get; set; }

        #endregion Table properties

        #region Additional Columns

        [Write(false)]
        public override bool IsModified => EntityState == EntityState.Modified || SetupID > 0;

        #endregion Additional Columns


    }
}
