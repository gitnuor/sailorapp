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
    public interface IGenDistrictService
    {
        Task<bool> SaveAsync(gen_district_entity entity);

        Task<bool> UpdateAsync(gen_district_entity entity);

        Task<List<gen_district_entity>> GetAllAsync();

        Task<List<gen_district_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_district_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> gen_district_insert_sp(gen_district_DTO objgen_district);
        Task<bool> gen_district_update_sp(gen_district_DTO objgen_district);
       

    }
}

