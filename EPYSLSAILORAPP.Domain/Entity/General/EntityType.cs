using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Domain.General;

namespace EPYSLSAILORAPP.Domain.Entity.General
{
    [Table("EntityType")]
    public class EntityType : DapperBaseEntity
    {
        #region Table properties
        [Key]
        public int EntityTypeID { get; set; }
        public string EntityTypeName { get; set; }
        public bool IntegerValue { get; set; }
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

        public EntityType()
        {
            IntegerValue = false;
            IsUsed = false;
            DateAdded = DateTime.Now;
            AddedBy = 0;
            UpdatedBy = 0;
        }
    }
}
