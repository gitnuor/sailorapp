using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranReturnChallanService
    {
        Task<bool> SaveAsync(tran_return_challan_DTO entity);

        Task<bool> UpdateAsync(tran_return_challan_DTO entity);

        Task<List<tran_return_challan_DTO>> GetAllAsync();

        Task<List<tran_return_challan_DTO>> GetAllPagedDataAsync(DtParameters request);
        Task<List<tran_delivery_outlet_challan_DTO>> GetPrendingReturnData(DtParameters request);     

        Task<bool> DeleteAsync(Int64? Id);
        Task<tran_return_challan_DTO> GetOutletChallanReturnData(Int64 p_tran_return_challan_id);

        Task<bool> tran_return_challan_update_sp(tran_return_challan_DTO objtran_return_challan);
        //Task<List<rpc_tran_return_challan_DTO>> GetAllJoined_TranReturnChallanAsync(Int64 currnet_page,Int64 page_size);
        Task<tran_return_challan_DTO> GetOutletReceiveData(Int64 p_tran_outlet_receive_note_id);

        

    }
}

