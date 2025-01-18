using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.CommonInterface
{
    [Table("CommonInterfaceMaster")]
    public class CommonInterfaceMaster : DapperBaseEntity
    {
        #region Table Fields
        [Key]
        public int MenuID { get; set; }

        public string TableName { get; set; }

        public string InterfaceName { get; set; }

        public string ApiUrl { get; set; }

        public string ParameterColumns { get; set; }

        public int? MasterRowNum { get; set; }

        public int? MasterColNum { get; set; }

        public bool IsInsertAllow { get; set; }

        public bool IsUpdateAllow { get; set; }

        public bool IsDeleteAllow { get; set; }

        public bool HasGrid { get; set; }

        public string PrimaryKeyColumn { get; set; }

        public string AssemblyName { get; set; }

        public string EntityName { get; set; }

        public string SelectSql { get; set; }

        public string InsertSql { get; set; }

        public string UpdateSql { get; set; }
        public string SaveApiUrl { get; set; }
        #endregion

        #region Additional Fields

        [Write(false)]
        public override bool IsModified => MenuID > 0 || EntityState == EntityState.Modified;

        #endregion Additional Fields

        public CommonInterfaceMaster()
        {
            IsInsertAllow = true;
            IsUpdateAllow = true;
            IsDeleteAllow = true;
            HasGrid = false;
        }
    }
}
