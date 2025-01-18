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
    public interface ITranPlanAllocateService
    {
       Task<bool> SaveAsync(tran_plan_allocate_DTO entity);

		Task<bool> UpdateAsync(tran_plan_allocate_DTO entity);


        Task<List<tran_plan_allocate_entity>> GetAllAsync();

		Task<List<tran_plan_allocate_entity>> GetAsync(Int64 Id);
    }
}

