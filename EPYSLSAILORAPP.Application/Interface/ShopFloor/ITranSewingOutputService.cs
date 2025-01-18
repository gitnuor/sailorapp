using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranSewingOutputService
    {
      

        Task<bool> UpdateAsync(tran_sewing_output_entity entity);

        Task<List<tran_sewing_output_entity>> GetAllAsync();



        Task<tran_sewing_output_entity> GetSingleAsync(Int64 Id);
        Task<List<rpc_tran_sewing_input_DTO>> GetAllSewingOutputPendingAsync(Int64 row_index, Int64 page_size, long p_fiscal_year_id, long p_event_id);
        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> SaveAsync(tran_sewing_output_DTO objtran_sewing_output);
        Task<bool> tran_sewing_output_update_sp(tran_sewing_output_DTO objtran_sewing_output);
        Task<List<rpc_tran_sewing_output_DTO>> GetTranSewingOutputtedData(Int64 row_index, Int64 page_size, long p_fiscal_year_id, long p_event_id);
        Task<rpc_sewing_input_data_for_output_DTO> GetSewingOutputDataByInputId(long Id);
        Task<List<rpc_sewing_line_wise_input_deatail_DTO>> AddLine(long tran_sewing_allocation_plan_id, long production_line_id);
        Task<List<qc_failed_details_DTO>> GetAllQCParam();
        Task<List<SelectListItem>> GetAllHourList();
    }
}

