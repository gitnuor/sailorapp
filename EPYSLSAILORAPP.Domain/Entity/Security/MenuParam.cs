using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Security
{
    [Table("MenuParam")]
    public class MenuParam : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public int MenuId { get; set; }
        public string ParamType { get; set; }

        public string ParamValue { get; set; }

        public int SeqNo { get; set; }

        #endregion

        #region Additional properties
        [Write(false)]
        public override bool IsModified => MenuId > 0 || EntityState == EntityState.Modified;
        [Write(false)]
        public virtual Menu Menu { get; set; }
        #endregion

        public MenuParam()
        {
        }
    }
}
