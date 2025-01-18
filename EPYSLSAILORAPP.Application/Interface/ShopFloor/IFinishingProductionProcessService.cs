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
    public interface IFinishingProductionProcessService
    {
      Task<List<rpc_tran_finishing_production_DTO>> GetnFinishingProductionPendingListAsync(Int64 row_index, Int64 page_size, Int64 finishing_receive_id, string actionType, Int64 fiscal_year_id, Int64 event_id);
      Task<List<rpc_tran_finishing_production_DTO>> GetnFinishingProductionPendingListByTechpackAsync(Int64 row_index, Int64 page_size, Int64 techpackId, Int64 activeTag, string actionType, Int64 fiscal_year_id, Int64 event_id);
      Task<rpc_tran_finishing_production_DTO> GetnFinishingProductionListByIdAsync(Int64 row_index, Int64 page_size, Int64 finishing_techpack_id, string actionType, Int64 fiscal_year_id, Int64 event_id);
      List<rpc_tran_finishing_production_DTO> GetAllproc_sp_get_colors_by_techpack_Id(Int64? p_techpack_Id);
      Task<List<rpc_tran_finishing_production_DTO>> GetAll_finishing_production_techpack_color_wiseAsync(Int64? p_techpack_id, String color_code , Int64? finishingProcessId);
      Task<bool> tran_finishing_production_process_insert_sp(tran_finishing_production_process_DTO objtran_finishing_receive);
     List<rpc_proc_finishing_process_type_DTO> GetAllFinishingProcessType();
     Task<List<tran_finishing_production_process_DTO>> GetnFinishingProductionListView(Int64 finishing_techpack_id , string activeTab);
     Task<List<tran_finishing_production_detail_process_DTO>> GetDetaiilsProductionIdWise(Int64 finishing_production_process_id);
     List<rpc_tran_finishing_production_DTO> GetAllTechpackList();
     Task<rpc_tran_finishing_receive_DTO> GetnFinishingProductionProcessTab(Int64 finishing_techpack_id);
     Task<tran_finishing_production_process_DTO> GetnFinishingProductionReceiveListByTechpacAsync(Int64 row_index, Int64 page_size, Int64 techpackId, string actionType, Int64 fiscal_year_id, Int64 event_id , Int64 gen_finishing_process_id);


    }
}

