using EPYSLSAILORAPP.Application.DTO.General;
using EPYSLSAILORAPP.Domain.Statics;

namespace EPYSLSAILORAPP.Application.Interface.Common
{
    public interface IEntityTypeService
    {
        #region EntityType
        Task<List<EntityTypeDTO>> GetEntityTypeAsync(PaginationInfo paginationInfo);
        Task<EntityTypeDTO> GetAsync(int id);
        Task SaveEntityTypeAsync(EntityTypeDTO entity);
        #endregion EntityType
    }
}
