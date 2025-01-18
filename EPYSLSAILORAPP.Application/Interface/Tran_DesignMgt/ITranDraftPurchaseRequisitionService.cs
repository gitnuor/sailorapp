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
    public interface ITranDraftPurchaseRequisitionService
    {
        Task<bool> SaveAsync(tran_draft_purchase_requisition_entity entity);

        Task<bool> UpdateAsync(tran_draft_purchase_requisition_entity entity);

        Task<List<tran_draft_purchase_requisition_entity>> GetAllAsync();

        Task<List<tran_draft_purchase_requisition_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_draft_purchase_requisition_DTO> GetSingleAsync(Int64 Id);

        Task<List<rpc_draft_purchase_requisition_DTO>> GetAllDraftTranPurchaseRequisitionAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_draft_purchase_requisition_insert_sp(tran_draft_purchase_requisition_DTO objtran_draft_purchase_requisition);
        Task<bool> tran_draft_purchase_requisition_update_sp(tran_draft_purchase_requisition_DTO objtran_draft_purchase_requisition);
      

    }
}

