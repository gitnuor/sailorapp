using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenItemStructureGroupSubSegmentMappingService
    {
       Task<bool> SaveAsync(gen_item_structure_group_sub_segment_mapping_entity entity);

		Task<bool> UpdateAsync(gen_item_structure_group_sub_segment_mapping_entity entity);

		Task<List<gen_item_structure_group_sub_segment_mapping_entity>> GetAllAsync();

		Task<List<gen_item_structure_group_sub_segment_mapping_entity>> GetAsync(Int64 Id);
    }
}

