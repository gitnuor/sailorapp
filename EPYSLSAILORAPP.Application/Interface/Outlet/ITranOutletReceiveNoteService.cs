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
    public interface ITranOutletReceiveNoteService
    {
       Task<bool> SaveAsync(tran_outlet_receive_note_DTO entity);

		Task<bool> UpdateAsync(tran_outlet_receive_note_DTO entity);

		Task<List<tran_outlet_receive_note_DTO>> GetAllAsync();

        Task<List<tran_outlet_receive_note_DTO>> GetAllPagedDataAsync(DtParameters request);

		Task<tran_outlet_receive_note_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

             Task<bool> tran_outlet_receive_note_insert_sp(tran_outlet_receive_note_DTO objtran_outlet_receive_note);
     Task<bool> tran_outlet_receive_note_update_sp(tran_outlet_receive_note_DTO objtran_outlet_receive_note);
     
        Task<tran_outlet_receive_note_DTO> GetAllJoined_TranOutletReceiveNoteAsync(Int64 outlet_challan_receive_id);


    }
}

