using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.Item
{
    [Table("ItemSubGroupMailSetup")]
    public class ItemSubGroupMailSetup : DapperBaseEntity
    {
        #region Table properties

        [Key]
        public int SetupID { get; set; }

        public int ItemSubGroupID { get; set; }

        public int SetupForID { get; set; }

        public string ToMailID { get; set; }

        public string CCMailID { get; set; }

        public string BCCMailID { get; set; }

        #endregion Table properties

        #region Additional Columns

        [Write(false)]
        public override bool IsModified => EntityState == EntityState.Modified || SetupID > 0;

        #endregion Additional Columns

        public ItemSubGroupMailSetup()
        {
            ToMailID = "";
            CCMailID = "";
            BCCMailID = "";
        }

    }
}
