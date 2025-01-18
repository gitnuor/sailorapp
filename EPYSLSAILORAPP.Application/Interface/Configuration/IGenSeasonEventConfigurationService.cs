using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.Interface.BusinessPlanning
{
    public interface IGenSeasonEventConfigurationService
    {

        Task<bool> SaveAsync(GenSeasonEventConfigurationDTO entity);
        Task<bool> UpdateAsync(GenSeasonEventConfigurationDTO entity);
        Task<bool> DeleteAsync(Int64 Id);
        Task<List<GenSeasonEventConfigurationDTO>> GetAllData();
        Task<List<gen_season_event_config_ext>> GetAllPagedData(DtParameters dtparam);
        Task<List<gen_season_event_config_ext>> GetAllPagedDataForCopy(DtParameters dtparam);
        Task<bool> SaveAsyncCopy(gen_season_event_config entity);
    }
}
