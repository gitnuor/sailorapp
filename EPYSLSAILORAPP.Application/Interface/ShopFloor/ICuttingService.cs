using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ICuttingService
    {
        //Task<bool> SaveAsync(tran_cutting_entity entity);
        Task<bool> SaveAsync(tran_cutting_insert_DTO objtran_cutting);
        Task<bool> UpdateAsync(tran_cutting_entity entity);

        Task<List<tran_cutting_DTO>> GetAllAsync();

        Task<List<tran_cutting_DTO>> GetAllPagedDataAsync(DtParameters request);

        //Task<List<tran_cutting_DTO>> GetAllPagedDataForSelect2(DtParameters param);

        Task<tran_cutting_DTO> GetSingleAsync(Int64 Id);
        Task<tran_cutting_DTO> GetTechPackInfoForCutting(long Id);
        Task<cutting_details_DTO> GetCuttingDetails(long Id, string color_code);
        Task<List<fabric_details_DTO>> GetFabricDetaiils(long Id);

        Task<bool> DeleteAsync(Int64 Id);

        Task<List<rpc_tran_cutting_DTO>> GetAllJoined_CuttingAsync(Int64 row_index, Int64 page_size);
        Task<List<rpc_tran_pp_meeting_DTO>> GetPendingCuttingPlans(long row_index, long page_size, long event_id, long fiscal_year_id);
        Task<List<rpc_tran_pp_meeting_DTO>> GetRunningCuttingPlans(long row_index, long page_size, long event_id, long fiscal_year_id);
        Task<List<rpc_tran_pp_meeting_DTO>> GetCompletedCuttingPlans(long row_index, long page_size, long event_id, long fiscal_year_id);
        Task<List<tran_cutting_batch_dropdown>> GetAllproc_sp_get_color_wise_batchAsync(Int64? p_techpack_id, String p_color_code);

    }
}

