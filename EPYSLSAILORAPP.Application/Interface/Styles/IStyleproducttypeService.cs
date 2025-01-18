using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IStyleproducttypeService
    {
       Task<bool> SaveAsync(style_product_type_entity entity);

		Task<bool> UpdateAsync(style_product_type_entity entity);

		Task<List<style_product_type_entity>> GetAllAsync();

		Task<List<style_product_type_entity>> GetAsync(Int64 Id);
    }
}

