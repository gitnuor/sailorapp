using EPYSLSAILORAPP.Application.DTO;
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
    public interface ITranPreCostingReviewService
    {
       Task<bool> SaveAsync(tran_pre_costing_review_DTO entity);

		Task<bool> UpdateAsync(tran_pre_costing_review_DTO entity);

        Task<bool> UpdateReviewAck(tran_pre_costing_review_DTO entity);

        Task<List<tran_pre_costing_review_DTO>> GetAllAsync();

        Task<List<tran_pre_costing_review_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<tran_pre_costing_review_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

