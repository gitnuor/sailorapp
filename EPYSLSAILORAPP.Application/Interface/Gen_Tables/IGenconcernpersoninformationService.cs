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
    public interface IGenConcernPersonInformationService
    {
       Task<bool> SaveAsync(gen_concern_person_information_entity entity);

		Task<bool> UpdateAsync(gen_concern_person_information_entity entity);

		Task<List<gen_concern_person_information_entity>> GetAllAsync();

		Task<List<gen_concern_person_information_entity>> GetAsync(Int64 Id);
    }
}

