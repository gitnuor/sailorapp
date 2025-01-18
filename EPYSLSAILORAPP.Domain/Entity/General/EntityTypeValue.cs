using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.General
{
    [Table("EntityTypeValue")]
    public class EntityTypeValue : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public int ValueID { get; set; }
        public string ValueName { get; set; }
        public int EntityTypeID { get; set; }
        public bool IsUsed { get; set; }
        public int AddedBy { get; set; }
        public DateTime DateAdded { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        #endregion

        #region Additional Columns
        [Write(false)]
        public override bool IsModified => EntityTypeID > 0 || EntityState == EntityState.Modified;
        #endregion

        public EntityTypeValue()
        {
            IsUsed = false;
            DateAdded = DateTime.Now;
            UpdatedBy = 0;
            EntityState = EntityState.Added;
        }
    }
}
