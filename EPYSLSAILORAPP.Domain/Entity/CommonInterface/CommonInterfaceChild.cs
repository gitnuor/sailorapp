using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.CommonInterface
{
    [Table("CommonInterfaceChild")]
    public class CommonInterfaceChild : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public int ChildID { get; set; }
        public int MenuID { get; set; }

        public string ColumnName { get; set; }

        public string Label { get; set; }

        public string EntryType { get; set; }

        public int Scale { get; set; }

        public bool IsSys { get; set; }

        public bool IsRequired { get; set; }

        public bool IsHidden { get; set; }

        public bool IsEnabled { get; set; }

        public string IdPrefix { get; set; }

        public decimal? Seq { get; set; }

        public string Tooltip { get; set; }

        public bool HasFinder { get; set; }

        public string FinderApiUrl { get; set; }

        public string FinderHeaderColumns { get; set; }

        public string FinderDisplayColumns { get; set; }

        public string FinderColumnAligns { get; set; }

        public string FinderColumnWidths { get; set; }

        public string FinderColumnSortings { get; set; }

        public string FinderFilterColumns { get; set; }

        public string FinderValueColumn { get; set; }

        public string FinderDisplayOthersColumn { get; set; }

        public bool HasSelectionChangeMethod { get; set; }

        public string SelectApiUrl { get; set; }

        public string SelectSql { get; set; }

        public bool HasDependentColumn { get; set; }

        public string DependentColumnName { get; set; }

        public bool HasDefault { get; set; }

        public string DefaultValue { get; set; }

        public string ParameterValue { get; set; }

        public int MaxLength { get; set; }
        public bool HasGetValueByID { get; set; }
        public string GetValueApiUrl { get; set; }
        public string GetValueSql { get; set; }
        public string FinderSql { get; set; }
        #endregion

        #region Additional properties

        [Write(false)]
        public override bool IsModified => ChildID > 0 || EntityState == EntityState.Modified;
        #endregion

        public CommonInterfaceChild()
        {
            Scale = 0;
            IsSys = false;
            IsRequired = false;
            IsHidden = true;
            IsEnabled = true;
            HasFinder = false;
            HasSelectionChangeMethod = false;
            HasDefault = false;
        }
    }
}
