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
    public interface IGenDesignationService
    {
        Task<bool> SaveAsync(gen_designation_entity entity);

        Task<bool> UpdateAsync(gen_designation_entity entity);

        Task<List<gen_designation_entity>> GetAllAsync();

        Task<List<gen_designation_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_designation_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> gen_designation_insert_sp(gen_designation_DTO objgen_designation);
        Task<bool> gen_designation_update_sp(gen_designation_DTO objgen_designation);


    }
}

