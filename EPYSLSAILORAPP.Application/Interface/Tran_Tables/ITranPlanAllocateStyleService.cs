using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranPlanAllocateStyleService
    {
       Task<bool> SaveAsync(tran_plan_allocate_style_entity entity);

        Task<bool> DesignerAssignListAsync(List<tran_plan_allocate_style_entity> list);

        Task<bool> UpdateAsync(tran_plan_allocate_style_entity entity);

		Task<List<tran_plan_allocate_style_entity>> GetAllAsync();

		Task<List<tran_plan_allocate_style_entity>> GetAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);

        Task<tran_plan_allocate_style_entity> GetTotalAddedStyle(Int64 tran_va_plan_detl_id,Int64? tran_va_plan_detl_style_id);

        Task<List<tran_plan_allocate_style_entity>> GetStyleListByPlanDetlID(Int64 Id);

        Task<bool> tran_plan_allocate_style_approve_reject(tran_plan_allocate_style_DTO objtran_plan_allocate_style);
    }
}

