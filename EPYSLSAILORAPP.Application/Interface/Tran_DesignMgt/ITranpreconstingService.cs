using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;

namespace EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt
{
    public interface ITranpreconstingService
    {
        Task<bool> SaveAsync(tran_pre_costing_DTO entity);

        Task<bool> UpdateAsync(tran_pre_costing_DTO entity);

        Task<List<tran_pre_costing_DTO>> GetAllAsync();

        Task<tran_pre_costing_DTO> GetAsync(long Id);

        Task<bool> PreCostingReviewAdd(tran_pre_costing_DTO entity);

        Task<bool> PreCostingReviewAction(tran_pre_costing_DTO entity);

        Task<bool> tran_pre_costing_approve_reject(tran_pre_costing_DTO objtran_pre_costing);
    }
}

