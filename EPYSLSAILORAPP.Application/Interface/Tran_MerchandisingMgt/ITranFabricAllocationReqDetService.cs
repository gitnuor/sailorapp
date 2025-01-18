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
    public interface ITranFabricAllocationReqDetService
    {
       Task<bool> SaveAsync(tran_fabric_allocation_req_det_entity entity);

		Task<bool> UpdateAsync(tran_fabric_allocation_req_det_entity entity);

		Task<List<tran_fabric_allocation_req_det_entity>> GetAllAsync();

        Task<List<tran_fabric_allocation_req_det_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<tran_fabric_allocation_req_det_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

