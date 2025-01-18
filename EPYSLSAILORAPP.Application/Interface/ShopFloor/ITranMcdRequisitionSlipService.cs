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
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranMcdRequisitionSlipService
    {
        Task<bool> SaveAsync(tran_mcd_requisition_slip_DTO entity);
        Task<bool> UpdateAsync(tran_mcd_requisition_slip_DTO entity);

        Task<List<tran_mcd_requisition_slip_entity>> GetAllAsync();

        Task<List<tran_mcd_requisition_slip_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_mcd_requisition_slip_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);

        Task<bool> tran_fabric_requisition_slip_insert_sp(tran_mcd_requisition_slip_DTO objtran_fabric_requisition_slip);
        Task<bool> tran_fabric_requisition_slip_update_sp(tran_mcd_requisition_slip_DTO objtran_fabric_requisition_slip);
        Task<bool> ProposedAsync(tran_mcd_requisition_slip_DTO request);
        Task<bool> ApproveAsync(tran_mcd_requisition_slip_DTO request);
    }
}

