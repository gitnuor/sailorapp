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
    public interface IGenSeasonEventConfigService
    {
        Task<bool> SaveAsync(gen_season_event_config_entity entity);

        Task<bool> UpdateAsync(gen_season_event_config_entity entity);

        Task<List<gen_season_event_config_entity>> GetAllAsync();

        Task<List<gen_season_event_config_DTO>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_season_event_config_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);

        Task<bool> gen_season_event_config_insert_sp(gen_season_event_config_DTO objgen_season_event_config);
        Task<bool> gen_season_event_config_update_sp(gen_season_event_config_DTO objgen_season_event_config);
        Task<List<rpc_gen_season_event_config_DTO>> GetAllJoined_GenSeasonEventConfigAsync(Int64 currnet_page, Int64 page_size);


    }
}

