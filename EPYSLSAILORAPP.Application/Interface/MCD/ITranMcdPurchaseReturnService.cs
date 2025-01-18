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
    public interface ITranMcdPurchaseReturnService
    {
        //Task<(bool success, long requisition_slip_id)> SaveAsync(tran_mcd_purchase_return_entity entity, List<tran_mcd_purchase_return_detail_entity> details);
        Task<bool> SaveAsync(tran_mcd_purchase_return_entity entity);

        Task<bool> UpdateAsync(tran_mcd_purchase_return_entity entity);

        Task<List<tran_mcd_purchase_return_DTO>> GetAllAsync();

        Task<List<tran_mcd_purchase_return_DTO>> GetAllPagedDataAsync(DtParameters request);

        Task<List<tran_mcd_purchase_return_DTO>> GetAllPagedDataForSelect2(DtParameters param);

        Task<tran_mcd_purchase_return_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);

        Task<(bool success, long requisition_slip_id)> tran_mcd_purchase_return_insert_sp(tran_mcd_purchase_return_entity objtran_mcd_purchase_return, List<tran_mcd_purchase_return_detail_entity> details);
        Task<(bool success, long requisition_slip_id)> tran_mcd_purchase_return_update_sp(tran_mcd_purchase_return_entity objtran_mcd_purchase_return, List<tran_mcd_purchase_return_detail_entity> details);

        // Task<List<rpc_tran_mcd_purchase_return_DTO>> GetAllJoined_TranMcdPurchaseReturnAsync(Int64 currnet_page,Int64 page_size);

        Task<List<tran_mcd_purchase_return_DTO>> GetAllJoined_TranMcdPurchaseReturnAsync(Int64 row_index, Int64 page_size);

        Task<bool> ProposedAsync(tran_mcd_purchase_return_DTO request);

        Task<bool> ApproveAsync(tran_mcd_purchase_return_DTO request);

    }
}

