using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using System.Text;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IShopFloorReturnService
    {
        Task<bool> SaveAsync(tran_shop_floor_return_DTO objtran_shop_floor_return);

        Task<bool> UpdateAsync(tran_shop_floor_return_entity entity, List<tran_shop_floor_return_details_entity> details);

        Task<List<tran_shop_floor_return_DTO>> GetAllAsync();

        Task<List<tran_shop_floor_return_DTO>> GetAllPagedDataAsync(DtParameters request);

        Task<List<tran_shop_floor_return_DTO>> GetAllPagedDataForSelect2(DtParameters param);

        Task<tran_shop_floor_return_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
        Task<bool> tran_shop_floor_return_update_sp(tran_shop_floor_return_entity objtran_shop_floor_return);
        Task<List<rpc_tran_shop_floor_return_DTO>> GetAllJoined_ShopFloorReturnAsync(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id);
        Task<List<rpc_tran_shop_floor_return_DTO>> GetSubmittedShopFloorReturnData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id , string search);
        Task<List<rpc_tran_shop_floor_return_DTO>> GetApprovedShopFloorReturnData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id, string search);
        Task<List<rpc_tran_shop_floor_return_DTO>> GetRejectedShopFloorReturnData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id);
        Task<rpc_proc_sp_get_mcd_requisition_issue_DTO> Get_mcd_requisition_issueAsync(Int64? p_issue_id);
        Task<tran_shop_floor_return_DTO> GetSingleAsyncByReturnId(Int64? tran_shop_floor_return_id);
        Task<bool> AckonwledgementUpdate(tran_shop_floor_return_entity entity);
        Task<bool> Ackonwledge(tran_shop_floor_return_DTO entity);
        Task<bool> SendForApproval(long in_tran_shop_floor_return_id);
    }
}

