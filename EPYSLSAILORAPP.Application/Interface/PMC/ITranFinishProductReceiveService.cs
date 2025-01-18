using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranFinishProductReceiveService
    {
        Task<bool> SaveAsync(tran_finish_product_receive_DTO entity);

        Task<bool> UpdateAsync(tran_finish_product_receive_DTO entity);

        Task<List<tran_finish_product_receive_DTO>> GetAllAsync();

        Task<List<tran_finish_product_receive_DTO>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_finish_product_receive_DTO> GetSingleAsync(Int64 p_tran_finish_product_receive_id);

        Task<bool> DeleteAsync(Int64? Id);

       
        Task<bool> tran_finish_product_receive_update_sp(tran_finish_product_receive_DTO objtran_finish_product_receive);
        Task<List<rpc_tran_finish_product_receive_DTO>> GetAllJoined_TranFinishProductReceiveAsync(Int64 currnet_page, Int64 page_size, Int64 fiscal_year_id, Int64 event_id);


    }
}

