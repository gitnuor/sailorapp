using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_MerchandisingMgt;
using EPYSLSAILORAPP.Domain.Entity.SupplyChain;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranPurchaseRequisitionService
    {
        Task<bool> SaveAsync(tran_purchase_requisition_DTO entity);

        Task<bool> UpdateAsync(tran_purchase_requisition_DTO entity, List<string> DeleteFiles);
        Task<bool> UpdateOpenPRAsync(tran_purchase_requisition_DTO entity, List<string> DeleteFiles);
        Task<bool> ExecuteAcknoledgeAsync(long pr_id, long added_by);

        Task<bool> ExecutePrApprovalAsync(long pr_id);

        Task<tran_purchase_requisition_DTO> GetPR(long pr_id);

        Task<List<tran_purchase_requisition_entity>> GetAllAsync();

        Task<List<tran_purchase_requisition_entity>> GetAllPagedDataAsync(DtParameters request);
        Task<List<tran_purchase_requisition_entity>> GetAllAccesoriesDataAsync(PR_DtParameters param);
        Task<List<tran_purchase_requisition_entity>> GetAllFabricDataAsync(PR_DtParameters param);
        Task<tran_purchase_requisition_DTO> GetSingleRequisitionAsync(Int64 Id);
        Task<tran_purchase_requisition_DTO> GetSingleRequisitionWithoutTechpackAsync(long Id);
        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_purchase_requisition_insert_sp(tran_purchase_requisition_entity objtran_purchase_requisition);
        Task<bool> tran_purchase_requisition_update_sp(tran_purchase_requisition_DTO objtran_purchase_requisition);
        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionAsync(Int64 row_index, Int64 page_size);

        Task<List<tran_purchase_requisition_DTO>> GetPurchaseRequisitionDropDownData(DtParameters param, long group);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionPendingAckAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionMerchandiseFabricAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType, string search);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionMerchantPendingApprovalAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionMerchandise_Approved(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionMerchanAccessories(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType, string search);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionAccessoriesAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType, string search_text);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType, string search_text);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllTranPurchaseRequisitionApprovedAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search);
        Task<List<tran_purchase_requisition_dtl_DTO>> GetItemDetailsByTechpackforAcc(long p_techpack_id,  long gen_item_structure_group_sub_id);
        Task<List<tran_purchase_requisition_dtl_DTO>> GetItemDetailsByTechpackforFabric(long p_techpack_id, long gen_item_structure_group_sub_id);

        Task<List<rpc_tran_purchase_requisition_DTO>> GetAllJoined_TranPurchaseRequisitionAckAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long supplier_id, long delivery_unit_id,string search);

        Task<tran_purchase_requisition_DTO> GetMerchandiserByTechpack(long p_techpack_id);
    }
}

