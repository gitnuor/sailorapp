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
    public interface ITranPlanAllocateStyleColorSizeService
    {
       Task<bool> SaveAsync(tran_plan_allocate_style_color_size_entity entity);

		Task<bool> UpdateAsync(tran_plan_allocate_style_color_size_entity entity);

		Task<List<tran_plan_allocate_style_color_size_entity>> GetAllAsync();

		Task<List<tran_plan_allocate_style_color_size_entity>> GetAsync(Int64 Id);
    }
}

