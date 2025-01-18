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
    public interface IStyleProductCompositionService
    {
       Task<bool> SaveAsync(style_product_composition_entity entity);

		Task<bool> UpdateAsync(style_product_composition_entity entity);

		Task<List<style_product_composition_DTO>> GetAllAsync();

        Task<List<style_product_composition_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<style_product_composition_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

