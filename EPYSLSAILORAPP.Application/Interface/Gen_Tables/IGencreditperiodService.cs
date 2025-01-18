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
    public interface IGenCreditPeriodService
    {
        Task<bool> SaveAsync(gen_credit_period_entity entity);

        Task<bool> UpdateAsync(gen_credit_period_entity entity);

        Task<List<gen_credit_period_entity>> GetAllAsync();

        Task<List<gen_credit_period_entity>> GetAsync(long Id);
    }
}

