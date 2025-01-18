using EPYSLSAILORAPP.Domain.Entity.General;

namespace EPYSLSAILORAPP.Application.DTO.General
{
    public class EntityTypeDTO : EntityType
    {
        #region Additional Columns
        public List<EntityTypeValue> Childs { get; set; }

        #endregion

        public EntityTypeDTO()
        {
            IntegerValue = false;
            IsUsed = false;
            DateAdded = DateTime.Now;
            AddedBy = 0;
            UpdatedBy = 0;
            Childs = new List<EntityTypeValue>();
        }
    }
}
