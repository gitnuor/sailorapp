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
    public interface IGenDivisionService
    {
        Task<bool> SaveAsync(gen_division_entity entity);

        Task<bool> UpdateAsync(gen_division_entity entity);

        Task<List<gen_division_entity>> GetAllAsync();

        Task<List<gen_division_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_division_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> gen_division_insert_sp(gen_division_DTO objgen_division);
        Task<bool> gen_division_update_sp(gen_division_DTO objgen_division);


    }
}

