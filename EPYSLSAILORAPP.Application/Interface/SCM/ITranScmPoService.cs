using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranScmPoService
    {
        Task<bool> SaveAsync(tran_scm_po_entity entity, List<tran_scm_po_details_entity> details, List<file_upload> files);

        Task<bool> UpdateAsync(tran_scm_po_DTO entity);

        Task<bool> SendForApproval(tran_scm_po_DTO entity);

        Task<bool> UpdateApproval(long po_id, string status_remarks);
        Task<bool> UpdateRejectApproval(long po_id, string status_remarks);

        Task<List<tran_scm_po_entity>> GetAllAsync();

        Task<List<tran_scm_po_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_scm_po_DTO> GetSingleAsync(Int64 Id);

        Task<tran_scm_po_DTO> GetPurchaseOrder(long po_id);
        Task<rpc_tran_scm_po_detail_DTO> Get_data_tran_scm_po_Async(Int64 po_id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_scm_po_insert_sp(tran_scm_po_entity objtran_scm_po);
        Task<bool> tran_scm_po_update_sp(tran_scm_po_entity objtran_scm_po);
        Task<List<rpc_tran_scm_po_DTO>> GetAllJoined_TranScmPoAsync(Int64 row_index, Int64 page_size);

        Task<List<tran_scm_po_DTO>> GetAllPOApprovaData_Fro_Bill_Submission_Async(DtParameters param);

        Task<List<rpc_tran_scm_po_DTO>> GetAllAccessoriesPoAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, Int64 listType,string search);

        Task<List<rpc_tran_scm_po_DTO>> GetAllFabricsPoAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,
          Int64 listType,string search);

        Task<List<rpc_tran_scm_po_DTO>> GetAllOpenPoAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,string search);

        Task<List<rpc_tran_scm_po_DTO>> GetSubmittedPOData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search);

        Task<List<rpc_tran_scm_po_DTO>> GetOpenPOApprovedData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, string search);

        Task<List<rpc_tran_scm_po_DTO>> GetAllOpenPoPendingApprovalAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,string search);

        Task<List<rpc_tran_scm_po_DTO>> GetAllPoApprovalAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id ,DtSearch search);

        Task<List<rpc_tran_scm_po_DTO>> GetAllPoPendingAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,string search);

        Task<bool> ApprovedAsync(tran_scm_po_DTO entity);
        Task<bool> SaveRevisePoAsync(tran_mcd_receive_DTO obj_revised_po);

        Task<List<concern_person>> GetConcernPersonsAsync(long supplierId);
        Task<List<rpc_tran_scm_po_DTO>> GetAllFabricsPoReviseAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,
         Int64 listType);
        Task<tran_scm_po_DTO> GetSingleReviseAsync(Int64 Id);

        Task<List<rpc_tran_scm_po_DTO>> GetAllPoRejectAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id, DtSearch search);
        Task<List<rpc_tran_scm_po_DTO>> GetOpenPORejectData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id, long delivery_unit_id,string search);
        Task<List<rpc_proc_sp_get_data_tran_scm_po_DTO>> GetBillChallanDetailAsync(Int64 id);
   //     Task<List<rpc_tran_mcd_receive_detail_DTO>> GetBillChallanItemDetailAsync(Int64 poid , Int64 receivedid);
        Task<List<tran_scm_po_details_DTO>> GetBillChallanItemDetailAsync(Int64 poid, Int64 receivedid);
    }
}

