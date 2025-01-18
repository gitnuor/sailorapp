using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.DTO.TranTables;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface.BusinessPlanning
{
    public interface IBusinessPlanService
    {

        Task<List<rpc_event_tran_data_getall>> rpc_event_tran_data_GetAll(DtParameters param);
        Task<List<tran_bp_year_dto>> GetAll(DtParameters param);
        Task<bool> SaveBusinessPlan(BusinessPlanDTO param);
        Task<bool> UpdateBusinessPlan(BusinessPlanDTO param);

        Task<bool> UpdateApproveReject(tran_bp_year_dto entity);
        Task<List<rpc_tran_bp_year_DTO>> GetAllJoined_TranBpYearAsync(DtParameters param);

        Task<List<tran_bp_year_dto>> get_tran_BP_YearService_GetAll();

    }
}
