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
    public interface IGenEmailTemplateService
    {
        Task<bool> SaveAsync(gen_email_template_entity entity);

        Task<bool> UpdateAsync(gen_email_template_entity entity);

        Task<List<gen_email_template_entity>> GetAllAsync();

        Task<List<gen_email_template_DTO>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_email_template_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);

        Task<bool> gen_email_template_insert_sp(gen_email_template_DTO objgen_email_template);
        Task<bool> gen_email_template_update_sp(gen_email_template_DTO objgen_email_template);
        Task<List<rpc_gen_email_template_DTO>> GetAllJoined_GenEmailTemplateAsync(Int64 currnet_page, Int64 page_size);


    }
}

