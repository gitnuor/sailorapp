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
    public interface ITranFabricAllocationReqService
    {
       Task<bool> SaveAsync(tran_fabric_allocation_req_DTO entity);

		Task<bool> UpdateAsync(tran_fabric_allocation_req_DTO entity);

        Task<bool> ApproveRejectAsync(tran_fabric_allocation_req_DTO entity);

        Task<List<tran_fabric_allocation_req_entity>> GetAllAsync();

        Task<List<tran_fabric_allocation_req_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<tran_fabric_allocation_req_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

