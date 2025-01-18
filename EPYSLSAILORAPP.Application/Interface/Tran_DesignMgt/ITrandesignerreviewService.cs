using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.Tran_DesignMgt;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface.Tran_DesignMgt
{
    public interface ITranDesignerReviewService
    {
        Task<bool> SaveAsync(tran_designer_review_entity entity, List<file_upload> List_base64String_File);

        Task<bool> UpdateAsync(tran_designer_review_entity entity, List<file_upload> List_base64String_File, List<string> DeleteList);

        Task<List<tran_designer_review_entity>> GetAllAsync();
         Task<bool> ApproveAsync(tran_designer_review_entity entity);
        Task<tran_designer_review_DTO> GetFullAsyncDesinerData(long Id);
        Task<tran_designer_review_entity> GetAsync(long Id);

        Task<List<rpc_sp_get_data_for_designer_review_DTO>> GetAllsp_get_data_for_designer_reviewAsync
            (Filter_RangePlan_DataTable param);
        Task<List<rpc_sp_get_data_for_designer_review_DTO>> GetAllsp_get_data_for_designer_review_dataAsync
            (Filter_RangePlan_DataTable param, Int64 actionType);
    }
}

