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

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenIncoTermService
    {
       Task<bool> SaveAsync(gen_inco_term_entity entity);

		Task<bool> UpdateAsync(gen_inco_term_entity entity);

		Task<List<gen_inco_term_DTO>> GetAllAsync();

		Task<gen_inco_term_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

