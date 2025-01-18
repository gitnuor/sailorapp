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
    public interface IGenArifService
    {
        Task<bool> SaveAsync(gen_arif_entity entity);

        Task<bool> UpdateAsync(gen_arif_entity entity);

        Task<List<gen_arif_entity>> GetAllAsync();

        Task<List<gen_arif_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_arif_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);

        Task<bool> gen_arif_insert_sp(gen_arif_DTO objgen_arif);
        Task<bool> gen_arif_update_sp(gen_arif_DTO objgen_arif);
        Task<List<rpc_gen_arif_DTO>> GetAllJoined_GenArifAsync(Int64 currnet_page, Int64 page_size);


    }
}

