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
    public interface ITranrangeplanService
    {
       Task<bool> SaveAsync(tran_range_plan_entity entity);

		Task<bool> UpdateAsync(tran_range_plan_entity entity);

		Task<List<tran_range_plan_entity>> GetAllAsync();

        Task<List<tran_range_plan_entity>> GetAllByRangePlanIDAsync(Int64 RangePlanID);


        Task<List<tran_range_plan_entity>> GetAsync(Int64 Id);

        Task<bool> SaveMasterDetailsAsync(tran_range_plan_DTO entity);
    }
}

