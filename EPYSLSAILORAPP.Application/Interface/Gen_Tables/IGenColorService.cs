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
    public interface IGenColorService
    {
        Task<bool> SaveAsync(gen_color_entity entity);

        Task<bool> UpdateAsync(gen_color_entity entity);

        Task<List<gen_color_entity>> GetAllAsync();

        Task<List<gen_color_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_color_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> gen_color_insert_sp(gen_color_DTO objgen_color);
        Task<bool> gen_color_update_sp(gen_color_DTO objgen_color);
      

    }
}

