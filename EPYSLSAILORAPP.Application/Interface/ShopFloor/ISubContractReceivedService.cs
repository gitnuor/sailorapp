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
using Microsoft.Extensions.Configuration;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface ISubContractReceivedService
    {
        Task<List<rpc_tran_sub_contract_received_DTO>> GetSubContractReceivedPendingListAsync(Int64 row_index, Int64 page_size, Int64 tran_techpack_id, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id);
        Task<rpc_tran_sub_contract_received_DTO> GetTechPackInfoForSubContractReceived(Int64 row_index, Int64 page_size, Int64 p_tran_sub_contract_request_id, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id);
        List<rpc_tran_sub_contract_received_DTO> GetAll_subcontract_color_Async(Int64? p_tran_sub_contract_request_id);
        Task<List<rpc_tran_sub_contract_received_DTO>> GetAll_subcontract_techpack_color_wiseAsync(Int64? p_techpack_id, String color_code);
        Task<rpc_tran_production_process_DTO> GetAll_production_techpack_wiseAsync(Int64? p_techpack_id , string colorCode);
        Task<bool> tran_sub_contract_received_insert_sp(tran_sub_contract_received_DTO objtran_sub_contract_received);
        Task<List<rpc_tran_sub_contract_received_DTO>> GetAllSupplier(Int64 p_tran_sub_contract_request_id);
        Task<List<tran_sub_contract_received_DTO>> GetSubContractReceived_DraftListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id);
        Task<tran_sub_contract_received_DTO> GetSingleAsync(Int64 Id);

        Task<bool> ApprovalProposedAsync(tran_sub_contract_received_DTO request);



         Task<bool> ApprovedAsync(tran_sub_contract_received_DTO request);
        
    }
}

