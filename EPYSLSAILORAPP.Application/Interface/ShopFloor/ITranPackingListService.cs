using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranPackingListService
    {
        Task<bool> SaveAsync(tran_packing_list_DTO entity);

        Task<bool> UpdateAsync(tran_packing_list_entity entity);

        Task<List<tran_packing_list_entity>> GetAllAsync();

        Task<List<tran_packing_list_entity>> GetAllPagedDataAsync(DtParameters request);
        Task<List<tran_packing_list_entity>> GetAllSubmittedDataAsync(DtParameters request);
        Task<List<tran_packing_list_entity>> GetAllApprovedDataAsync(DtParameters request);

        Task<rpc_proc_sp_get_data_tran_packing_list_by_id_DTO> GetSingleAsync(Int64 p_tran_packing_list_id);
        Task<rpc_proc_sp_get_data_tran_packing_list_by_id_DTO> GetPackingListForPMC(Int64 p_tran_packing_list_id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> SendForApproval(Int64? Id);
        Task<bool> Approve(tran_packing_list_DTO entity);
        Task<bool> tran_packing_list_update_sp(tran_packing_list_DTO objtran_packing_list);
        Task<List<rpc_tran_packing_list_DTO>> GetAllJoined_TranPackingListAsync(Int64 currnet_page, Int64 page_size, Int64 fiscal_year_id, Int64 event_id);


    }
}

