using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranFinalInspectionService
    {
        Task<bool> SaveAsync(tran_final_inspection_DTO entity);

        Task<bool> UpdateAsync(tran_final_inspection_DTO entity);

        Task<List<tran_final_inspection_entity>> GetAllAsync();

        Task<List<tran_final_inspection_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_final_inspection_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

       
        Task<List<rpc_tran_final_inspection_DTO>> GetAllJoined_TranFinalInspectionAsync( Int64 currnet_page, Int64 page_size, long fiscal_year_id, long event_id);

        Task<List<rpc_proc_sp_get_production_quantity_for_final_inspection_DTO>> GetProductionQuantityForFinalInspection(Int64? p_techpack_id, String p_color_code);
        Task<List<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>> GetTranFinalInspectionDraftedData(Int64 row_index, Int64 page_size, long fiscal_year_id, long event_id);
        Task<List<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>> GetTranFinalInspectionSubmittedData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id);
        Task<List<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>> GetTranFinalInspectionApprovedData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id);
        Task<bool> SendForApproval(tran_final_inspection_DTO objtran_final_inspection);
        Task<bool> ApproveInspection(tran_final_inspection_DTO objtran_final_inspection);
    }
}

