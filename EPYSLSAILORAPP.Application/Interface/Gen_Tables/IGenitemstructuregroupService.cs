using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenItemStructureGroupService
    {
       Task<bool> SaveAsync(gen_item_structure_group_entity entity);

		Task<bool> UpdateAsync(gen_item_structure_group_entity entity);

		Task<List<gen_item_structure_group_entity>> GetAllAsync();

		Task<List<gen_item_structure_group_entity>> GetAsync(Int64 Id);

        Task<List<gen_item_structure_group_DTO>> GetAllAccessoriesGroups();
    }
}

