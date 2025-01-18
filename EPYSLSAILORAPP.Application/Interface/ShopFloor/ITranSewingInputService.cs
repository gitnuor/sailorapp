using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranSewingInputService
    {
       Task<bool> SaveAsync(tran_sewing_input_DTO entity);

		Task<bool> UpdateAsync(tran_sewing_input_entity entity);

		Task<List<tran_sewing_input_entity>> GetAllAsync();

        Task<List<tran_sewing_input_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<tran_sewing_input_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

     
     Task<bool> tran_sewing_input_update_sp(tran_sewing_input_DTO objtran_sewing_input);
        Task<List<rpc_tran_sewing_input_DTO>> GetAllSewingInputtedAsync(Int64 row_index, Int64 page_size, long p_fiscal_year_id, long p_event_id);
        Task<List<rpc_tran_sewing_input_DTO>> GetAllPendingSewingInputAsync(Int64 row_index, Int64 page_size, long p_fiscal_year_id, long p_event_id);


    }
}

