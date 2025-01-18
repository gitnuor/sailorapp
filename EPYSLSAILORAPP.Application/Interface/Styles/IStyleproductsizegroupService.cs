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
using EPYSLSAILORAPP.Application.DTO;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IStyleproductsizegroupService
    {
        Task<bool> SaveAsync(style_product_size_group_DTO entity);

        Task<bool> UpdateAsync(style_product_size_group_DTO entity);

        Task<List<style_product_size_group_entity>> GetAllAsync();

        Task<List<style_product_size_group_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<style_product_size_group_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);

       
    }
}

