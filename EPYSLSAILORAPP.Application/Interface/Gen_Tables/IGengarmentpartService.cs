using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Application.DTO;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenGarmentPartService
    {
       Task<bool> SaveAsync(gen_garment_part_entity entity);

		Task<bool> UpdateAsync(gen_garment_part_entity entity);

		Task<List<gen_garment_part_entity>> GetAllAsync();

		Task<List<gen_garment_part_entity>> GetAsync(Int64 Id);

        Task<List<gen_garment_part_entity>> GetPagedData(DtParameters filter);
    }
}

