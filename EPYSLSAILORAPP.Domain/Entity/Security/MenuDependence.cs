using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Security
{
    [Table("MenuDependence")]
    public class MenuDependence : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public int DependentID { get; set; }

        public int MenuId { get; set; }

        public int DependentMenuId { get; set; }

        public int SeqNo { get; set; }

        public string RefNo { get; set; }

        public bool UserWise { get; set; }

        public string IntervalType { get; set; }

        public int? LeadTimeValue { get; set; }

        #endregion

        #region Additional properties
        [Write(false)]
        public override bool IsModified => DependentID > 0 || EntityState == EntityState.Modified;

        public virtual Menu Menu { get; set; }
        #endregion

        public MenuDependence()
        {
            SeqNo = 0;
            RefNo = "";
            UserWise = false;
        }
    }
}
