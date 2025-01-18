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
    public interface IGenProductionLineService
    {
        Task<bool> SaveAsync(gen_production_line_entity entity);

        Task<bool> UpdateAsync(gen_production_line_entity entity);

        Task<List<gen_production_line_entity>> GetAllAsync();

        Task<List<gen_production_line_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_production_line_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> gen_production_line_insert_sp(gen_production_line_DTO objgen_production_line);
        Task<bool> gen_production_line_update_sp(gen_production_line_DTO objgen_production_line);
        //Task<List<rpc_gen_production_line_DTO>> GetAllJoined_GenProductionLineAsync(Int64 currnet_page,Int64 page_size);

        List<gen_production_line_entity> GetSingleLineByAsync(Int64 Id);
    }
}

