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
using EPYSLSAILORAPP.Application.DTO.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IProductionProcessDefineService
    {
        Task<List<rpc_tran_production_process_DTO>> GetProductionProcess_PendingListAsync(Int64 row_index, Int64 page_size, Int64 workOrderId, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id);
        Task<rpc_tran_production_process_DTO> GetTechPackInfoForProductionProcess(Int64 row_index, Int64 page_size, Int64 tran_techpack_id, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id);
        Task<rpc_tran_production_process_DTO> GetAll_production_techpack_wiseAsync(Int64? p_techpack_id);
        Task<rpc_tran_production_process_DTO> GetAll_production_techpack_wiseAsync(Int64? p_techpack_id , string colorCode);
        Task<bool> tran_production_process_define_insert_sp(tran_production_process_define_DTO objtran_production_process_define);
        Task<List<tran_production_process_define_DTO>> GetProductionProcess_DraftListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id);
        Task<tran_production_process_define_DTO_exc> GetSingleAsync(Int64 Id);
    }
}

