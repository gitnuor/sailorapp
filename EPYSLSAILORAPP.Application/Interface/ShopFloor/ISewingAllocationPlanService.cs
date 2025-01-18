using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ISewingAllocationPlanService
    {
        Task<bool> SaveAsync(sewing_plan_insert_dto entity);

        Task<bool> UpdateAsync(tran_sewing_allocation_plan_entity entity);

        Task<List<tran_sewing_allocation_plan_entity>> GetAllAsync();

        Task<List<rpc_tran_sewing_allocation_plan_DTO>> GetAllPagedDataAsync(Int64 fiscal_year_id, Int64 event_id, Int64 currnet_page, Int64 page_size);

        Task<tran_sewing_allocation_plan_entity> GetSingleAsync(Int64 Id);
        Task<rpc_sewing_allocation_data_DTO> GetSewingInputDataByAllocationId(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_sewing_allocation_plan_insert_sp(tran_sewing_allocation_plan_DTO objtran_sewing_allocation_plan);
        Task<bool> tran_sewing_allocation_plan_update_sp(tran_sewing_allocation_plan_DTO objtran_sewing_allocation_plan);


        Task<List<rpc_sewing_line_wise_deatail_DTO>> AddLine(long tran_sewing_allocation_plan_id, long production_line_id);
    }
}

