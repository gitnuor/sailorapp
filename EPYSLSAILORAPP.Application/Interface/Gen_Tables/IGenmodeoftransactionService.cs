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
    public interface IGenModeOfTransactionService
    {
       Task<bool> SaveAsync(gen_mode_of_transaction_entity entity);

		Task<bool> UpdateAsync(gen_mode_of_transaction_entity entity);

		Task<List<gen_mode_of_transaction_entity>> GetAllAsync();

		Task<List<gen_mode_of_transaction_entity>> GetAsync(Int64 Id);
    }
}

