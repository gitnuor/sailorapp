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
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ITranServiceWorkOrderService
    {
      
       Task<bool> SaveAsync(tran_service_work_order_entity entity, List<tran_service_work_order_detail_entity> details);
        Task<bool> UpdateAsync(tran_service_work_order_DTO entity);
        Task<List<tran_tech_pack_DTO>> GetTranServiceOrderPendingListAsync(Int64 row_index, Int64 page_size, string query_type, Int64 receivedID, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id,string search);
        Task<tran_tech_pack_DTO> GetEmbellishmentByTechpackAsync(Int64 currnet_page, Int64 page_size, string query_type, Int64 receivedID, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id);
        Task<bool> ProposedAsync(tran_service_work_order_DTO request);
        Task<bool> ApproveAsync(tran_service_work_order_DTO request);
        Task<List<tran_service_work_order_DTO>> GetTranServiceWorkOrderDraftListAsync(Int64 row_index, Int64 page_size, Int64 actionType, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id,string search);
        Task<tran_service_work_order_DTO> Get_master_detail_tran_service_order_Async(Int64 service_work_order_id);
        List<rpc_proc_sp_get_colors_by_work_order_DTO> GetAllproc_sp_get_colors_by_work_order(Int64? p_work_order_id);
    }
}

