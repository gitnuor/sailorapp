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
    public interface ITranChatService
    {
        Task<bool> SaveAsync(tran_chat_entity entity);

        Task<Int64> SaveAndGetNewMessageCount(tran_chat_DTO obj);

        Task<bool> UpdateAsync(tran_chat_entity entity);

        Task<List<tran_chat_entity>> GetAllAsync();

        Task<List<tran_chat_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<List<tran_chat_DTO>> GetAllPagedGroupChatDetailsDataAsync(Chat_DtParameters param);

        Task<List<tran_chat_DTO>> GetAllPagedMessageBox(Chat_DtParameters param);

        Task<tran_chat_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> tran_chat_insert_sp(tran_chat_DTO objtran_chat);
        Task<bool> tran_chat_update_sp(tran_chat_DTO objtran_chat);

    }
}

