using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranChallanOutletService
    {
        Task<List<rpc_tran_outlet_challan_request_DTO>> GetOutletDetailList(Int64 techpack_id);
        Task<List<tran_delivery_outlet_challan_DTO>> GetAllPagedDataAsync(DtParameters request);
        Task<List<tran_delivery_outlet_challan_DTO>> GetAllPagedDataPendingAsync(DtParameters request);
        Task<bool> tran_delivery_outlet_challan_insert_sp(tran_delivery_outlet_challan_DTO objtran_delivery_outlet_challan);
        Task<List<tran_delivery_outlet_challan_DTO>> GetTranServiceoutlet_challan_landing_approval_data(Int64 row_index, Int64 page_size, Int64 actionType, Int64 fiscal_year_id, Int64 event_id,string search);
        Task<tran_delivery_outlet_challan_DTO> Get_data_tran_delivery_outlet_challan_Async(Int64 outlet_challan_id);
        Task<bool> ApproveAsync(tran_delivery_outlet_challan_DTO request);
        Task<bool> ProposedAsync(tran_delivery_outlet_challan_DTO request);
    }
       
}
