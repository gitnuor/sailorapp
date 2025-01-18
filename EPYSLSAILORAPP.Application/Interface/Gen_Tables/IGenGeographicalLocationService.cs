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

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenGeographicalLocationService
    {
       Task<bool> SaveAsync(gen_geographical_location_entity entity);

		Task<bool> UpdateAsync(gen_geographical_location_entity entity);

		Task<List<gen_geographical_location_DTO>> GetAllAsync();

        Task<List<gen_geographical_location_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<gen_geographical_location_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

