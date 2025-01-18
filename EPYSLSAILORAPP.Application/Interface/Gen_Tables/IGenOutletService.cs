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
    public interface IGenOutletService
    {
       Task<bool> SaveAsync(gen_outlet_entity entity);

		Task<bool> UpdateAsync(gen_outlet_entity entity);

		Task<List<gen_outlet_DTO>> GetAllAsync();

		Task<gen_outlet_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);


    }
}

