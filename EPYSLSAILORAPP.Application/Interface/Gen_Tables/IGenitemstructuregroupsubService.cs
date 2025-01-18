using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenItemStructureGroupSubService
    {
        Task<bool> SaveUpdateAsync(gen_item_structure_group_sub_DTO entity);
        //Task<bool> UpdateAsync(gen_item_structure_group_sub_DTO entity);

        Task<List<gen_item_structure_group_sub_DTO>> GetAllAsync();

        Task<List<gen_item_structure_group_sub_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_item_structure_group_sub_DTO> GetSingleAsync(Int64 Id);

        Task<List<gen_item_structure_group_sub_DTO>> GetAllItemStructureSubGroups(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

