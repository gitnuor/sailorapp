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
    public interface ITranScmBillSubmissionService
    {
        Task<bool> SaveAsync(tran_scm_bill_submission_entity entity);

        Task<bool> UpdateAsync(tran_scm_bill_submission_entity entity);

        Task<List<tran_scm_bill_submission_DTO>> GetAllAsync();

        Task<List<tran_scm_bill_submission_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<List<tran_scm_bill_submission_entity>> GetAll_Bill_Submission_SubmittedPagedDataAsync(DtParameters param);

        Task<List<tran_scm_bill_submission_entity>> Get_Bill_Approval_Pending_AllPagedDataAsync(DtParameters param);

        Task<tran_scm_bill_submission_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_scm_bill_submission_insert_sp(tran_scm_bill_submission_DTO objtran_scm_bill_submission);

        Task<bool> tran_scm_bill_submission_update_sp(tran_scm_bill_submission_DTO objtran_scm_bill_submission);

        Task<List<tran_scm_bill_submission_DTO>> GetAllJoined_TranScmBillSubmissionAsync(Int64 row_index, Int64 page_size);

        Task<bool> tran_scm_bill_submission_insert_sp_and_po_id_update(tran_scm_bill_submission_DTO objtran_scm_bill_submission);

        Task<bool> Update_Bill_Approval_Async(tran_scm_bill_submission_entity entity);

        Task<List<tran_scm_bill_submission_DTO>> GetAll_Pending_Bill_Submission_Async(Int64 fiscal_year_id, Int64 event_id, Int64 row_index, Int64 page_size,string search);
        Task<List<tran_scm_bill_submission_DTO>> GetAll_Bill_Submission_Async(Int64 fiscal_year_id, Int64 event_id, Int64 action_type, Int64 row_index, Int64 page_size,string search);


    }
}

