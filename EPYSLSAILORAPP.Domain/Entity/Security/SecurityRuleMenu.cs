using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Security
{
    [Table("SecurityRuleMenu")]
    public class SecurityRuleMenu : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public int MasterId { get; set; }
        public int MenuId { get; set; }
        public int SecurityRuleCode { get; set; }

        public bool? CanSelect { get; set; }

        public bool? CanInsert { get; set; }

        public bool? CanUpdate { get; set; }

        public bool? CanDelete { get; set; }

        public int AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        #endregion

        #region Additional properties
        [Write(false)]
        public string MenuName { get; set; }
        [Write(false)]
        public int ApplicationID { get; set; }
        [Write(false)]
        public override bool IsModified => MenuId > 0 || EntityState == EntityState.Modified;

        [Write(false)]
        public virtual Menu Menu { get; set; }

        //[Write(false)]
        //public virtual SecurityRule SecurityRule { get; set; }
        #endregion

        public SecurityRuleMenu()
        {
            CanSelect = false;
            CanInsert = false;
            CanUpdate = false;
            CanDelete = false;
        }
    }
}
