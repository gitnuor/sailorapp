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
    public interface IMenuActionService
    {
       Task<bool> SaveAsync(menu_action_entity entity);

		Task<bool> UpdateAsync(menu_action_entity entity);

		Task<List<menu_action_entity>> GetAllAsync();

        Task<List<menu_action_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<menu_action_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

