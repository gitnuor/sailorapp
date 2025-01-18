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
    public interface IStylePatternService
    {
       Task<bool> SaveAsync(style_pattern_entity entity);

		Task<bool> UpdateAsync(style_pattern_entity entity);

		Task<List<style_pattern_DTO>> GetAllAsync();

        Task<List<style_pattern_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<style_pattern_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

