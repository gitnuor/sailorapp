using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranReturnChallanReceivedService
    {
        Task<bool> SaveAsync(tran_return_challan_received_DTO entity);

        Task<bool> UpdateAsync(tran_return_challan_received_DTO entity);

        Task<List<tran_return_challan_received_DTO>> GetAllAsync();

        Task<List<tran_return_challan_received_DTO>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_return_challan_received_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_return_challan_received_insert_sp(tran_return_challan_received_DTO objtran_return_challan_received);
        Task<bool> tran_return_challan_received_update_sp(tran_return_challan_received_DTO objtran_return_challan_received);
        //Task<List<rpc_tran_return_challan_received_DTO>> GetAllJoined_TranReturnChallanReceivedAsync(Int64 currnet_page,Int64 page_size);

        Task<tran_return_challan_received_DTO> GetReturnChallanData(Int64 p_tran_return_challan_id);


        Task<List<tran_return_challan_received_DTO>> GetAllPendingReturnChallanReceivedAsync(long p_fiscal_year_id, long p_event_id, Int64 row_index, Int64 page_size);
        Task<List<tran_return_challan_received_DTO>> GetAllReturnChallanReceivedAsync(long p_fiscal_year_id, long p_event_id, Int64 row_index, Int64 page_size);


    }
}

