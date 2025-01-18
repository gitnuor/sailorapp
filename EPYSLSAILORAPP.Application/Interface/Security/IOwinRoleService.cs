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
    public interface IOwinRoleService
    {
       Task<bool> SaveAsync(owin_role_DTO entity);

		Task<bool> UpdateAsync(owin_role_DTO entity);

		Task<List<owin_role_DTO>> GetAllAsync();

       
        Task<owin_role_DTO> GetAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

