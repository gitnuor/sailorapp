using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranDistributionPlanService
    {
        Task<bool> SaveAsync(tran_distribution_plan_DTO entity);

        Task<bool> UpdateAsync(tran_distribution_plan_DTO entity);

        Task<List<tran_distribution_plan_DTO>> GetAllAsync();

        Task<List<rpc_tran_distribution_DTO>> GetAllPagedDataAsync(DtParameters param);

        Task<tran_distribution_plan_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<List<tran_distribution_plan_details_DTO>> GetDistributionByTechpack(long p_techpack_id);
        
        Task<bool> tran_distribution_plan_update_sp(tran_distribution_plan_DTO objtran_distribution_plan);


    }
}

