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
    public interface ITranEmbellishReceiveService
    {
       Task<bool> SaveAsync(tran_embellish_receive_entity entity);
       Task<bool> tran_embellish_receive_insert_sp(tran_embellish_receive_DTO objtran_embellish_delivery_receive);

        Task<bool> UpdateAsync(tran_embellish_receive_entity entity);

		Task<List<tran_embellish_receive_entity>> GetAllAsync();

        Task<List<tran_embellish_receive_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<tran_embellish_receive_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

            
        Task<bool> tran_embellish_receive_update_sp(tran_embellish_receive_DTO objtran_embellish_receive);

        Task<List<tran_embellish_receive_DTO>> GetTranReceivedChallanAllListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id);

        Task<tran_embellish_receive_DTO> Get_master_detail_tran_emb_delivery_challan_Receive_Async(Int64 embellish_receive_id);

        Task<bool> ApprovalProposedAsync(tran_embellish_receive_DTO entity);
        Task<bool> ApprovedAsync(tran_embellish_receive_DTO entity);



    }
}

