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
    public interface ITranMcdRequisitionIssueService
    {
        Task<bool> SaveAsync(tran_mcd_requisition_issue_entity objtran_mcd_requisition_issue, List<tran_mcd_requisition_issue_details_entity> detail);

        Task<bool> UpdateAsync(tran_mcd_requisition_issue_entity objtran_mcd_requisition_issue, List<tran_mcd_requisition_issue_details_entity> detail);

        Task<List<tran_mcd_requisition_issue_entity>> GetAllAsync();

        Task<List<tran_mcd_requisition_issue_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<tran_mcd_requisition_issue_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);

        Task<bool> tran_mcd_requisition_issue_insert_sp(tran_mcd_requisition_issue_DTO objtran_mcd_requisition_issue);
        Task<bool> tran_mcd_requisition_issue_update_sp(tran_mcd_requisition_issue_DTO objtran_mcd_requisition_issue);
        Task<List<tran_mcd_requisition_issue_DTO>> GetAllJoined_TranMcdRequisitionIssueAsync(Int64 row_index, Int64 page_size);

        Task<List<rpc_tran_mcd_requisition_slip_for_issueLanding_DTO>> GetAllMCDRequisitionSlipAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id, string search);
        Task<List<rpc_tran_mcd_requisition_slip_for_issueLanding_DTO>> GetAllMCDRequisitionSlipAccAsync(long row_index, long page_size, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id, string search);
        Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetDraftRequisitionSlipData(long row_index, long page_size, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id, string search);
        Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetDraftRequisitionSlipAcceData(long row_index, long page_size, long fiscal_year_id, long event_id, long p_group_id, long p_sub_group_id,string search);

        Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetSubmittedRequisitionSlipData(long row_index, long page_size, long fiscal_year_id, long event_id, long group_id, long sub_group_id, string search);
        Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetSubmittedRequisitionAccData(long row_index, long page_size, long fiscal_year_id, long event_id, long group_id, long sub_group_id, string search);
        Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetAprrovedRequisitionSlipData(long row_index, long page_size, long fiscal_year_id, long event_id, long group_id, long sub_group_id, string search);
        Task<List<rpc_tran_mcd_requisition_issue_DTO>> GetAprrovedRequisitionSlipDataAccesories(long row_index, long page_size, long fiscal_year_id, long event_id, long group_id, long sub_group_id, string search);
        Task<rpc_proc_sp_get_mcd_requisition_issue_DTO> Get_mcd_requisition_issueAsync(long? p_issue_id);

        Task<bool> ApproveFabricReq(long tran_mcd_requisition_issue_id, long userid);
    }
}

