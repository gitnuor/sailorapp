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
    public interface IGenTermAndConditionsService
    {
        Task<bool> SaveAsync(gen_term_and_conditions_entity entity);

        Task<bool> UpdateAsync(gen_term_and_conditions_entity entity);

        Task<List<gen_term_and_conditions_DTO>> GetAllAsync();

        Task<List<gen_term_and_conditions_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_term_and_conditions_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> gen_term_and_conditions_insert_sp(gen_term_and_conditions_DTO objgen_term_and_conditions);
        Task<bool> gen_term_and_conditions_update_sp(gen_term_and_conditions_DTO objgen_term_and_conditions);
        Task<List<rpc_gen_term_and_conditions_DTO>> GetAllJoined_GenTermAndConditionsAsync(Int64 currnet_page, Int64 page_size);

        Task<List<gen_term_and_conditions_details_DTO>> GetTermsandConditionDetail(Int64 Id);


    }
}

