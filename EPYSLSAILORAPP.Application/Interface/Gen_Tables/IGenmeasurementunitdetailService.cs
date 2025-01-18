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
    public interface IGenMeasurementUnitDetailService
    {
       Task<bool> SaveAsync(gen_measurement_unit_detail_entity entity);

		Task<bool> UpdateAsync(gen_measurement_unit_detail_entity entity);

		Task<List<gen_measurement_unit_detail_entity>> GetAllAsync();

		Task<gen_measurement_unit_detail_entity> GetAsync(Int64 Id);
    }
}

