using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenItemMasterService
    {
       Task<Int64> SaveAsync(gen_item_master_entity entity);

		Task<bool> UpdateAsync(gen_item_master_entity entity);

		Task<List<gen_item_master_DTO>> GetAllAsync();

        Task<long> CheckAndSaveItemMasterAsync(gen_item_master_DTO dto, long? usid);

        Task<List<gen_item_master_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<gen_item_master_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

