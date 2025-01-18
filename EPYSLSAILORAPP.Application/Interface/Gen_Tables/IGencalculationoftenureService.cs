using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.Entity.GenTables;

namespace EPYSLSAILORAPP.Application.Interface.Gen_Tables
{
    public interface IGenCalculationOfTenureService
    {
        Task<bool> SaveAsync(gen_calculation_of_tenure_entity entity);

        Task<bool> UpdateAsync(gen_calculation_of_tenure_entity entity);

        Task<List<gen_calculation_of_tenure_entity>> GetAllAsync();

        Task<List<gen_calculation_of_tenure_entity>> GetAsync(long Id);
    }
}

