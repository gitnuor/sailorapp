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
using EPYSLSAILORAPP.Application.DTO.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranFinishingReceiveService
    {
        Task<List<rpc_tran_finishing_receive_DTO>> GetnFinishingReceivePendingListAsync(Int64 row_index, Int64 page_size, Int64 finishing_receive_id, string actionType, Int64 fiscal_year_id, Int64 event_id);
        Task<rpc_tran_finishing_receive_DTO> GetnFinishingReceiveListByIdAsync(Int64 row_index, Int64 page_size, Int64 finishing_receive_id, string actionType, Int64 fiscal_year_id, Int64 event_id);
        List<rpc_tran_finishing_receive_DTO> GetAllproc_sp_get_colors_by_sewing_output_Id(Int64? techpack_id);
        Task<List<rpc_tran_finishing_receive_DTO>> GetAll_finishing_receive_techpack_wiseAsync(Int64? p_techpack_id, String color_code);
        Task<bool> tran_finishing_receive_insert_sp(tran_finishing_receive_DTO objtran_finishing_receive);
        List<rpc_proc_finishing_process_type_DTO> GetAllFinishingProcessType();
        Task<List<tran_finishing_receive_DTO>> GetFinishingReceive_DraftListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id);
        Task<tran_finishing_receive_DTO> GetSingleAsync(Int64 Id);

    }
}

