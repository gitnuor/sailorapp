using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IPpMeetingService
    {
       Task<bool> SaveAsync(tran_pp_meeting_DTO entity);

		Task<bool> UpdateAsync(tran_pp_meeting_entity entity);

		Task<List<tran_pp_meeting_DTO>> GetAllAsync();

  
        Task<List<tran_pp_meeting_DTO>> GetAllPagedDataForSelect2(DtParameters param);

		Task<tran_pp_meeting_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);


        Task<List<rpc_tran_pp_meeting_DTO>> GetAllJoined_TranPpMeetingAsync(long row_index, long page_size, long event_id, long fiscal_year_id);

        
    }
}

