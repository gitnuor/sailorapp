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
    public interface ITranvaproductembellishmentmappingService
    {
       Task<bool> SaveAsync(tran_va_product_embellishment_mapping_entity entity);

		Task<bool> UpdateAsync(tran_va_product_embellishment_mapping_entity entity);

		Task<List<tran_va_product_embellishment_mapping_entity>> GetAllAsync();

		Task<List<tran_va_product_embellishment_mapping_entity>> GetAsync(Int64 Id);
    }
}

