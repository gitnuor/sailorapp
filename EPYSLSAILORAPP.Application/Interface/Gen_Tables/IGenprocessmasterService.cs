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
    public interface IGenProcessMasterService
    {
       Task<bool> SaveAsync(gen_process_master_entity entity);

		Task<bool> UpdateAsync(gen_process_master_entity entity);

		Task<List<gen_process_master_entity>> GetAllAsync();

		Task<List<gen_process_master_entity>> GetAsync(Int64 Id);
    }
}

