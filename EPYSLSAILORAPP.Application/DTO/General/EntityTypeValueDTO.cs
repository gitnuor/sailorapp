using EPYSLSAILORAPP.Domain.Entity.General;

namespace EPYSLSAILORAPP.Application.DTO.General
{
    public class EntityTypeValueDTO : EntityTypeValue
    {

        #region Additional Columns
        #endregion

        public EntityTypeValueDTO()
        {
            IsUsed = false;
            DateAdded = DateTime.Now;
            UpdatedBy = 0;
        }
    }
}
