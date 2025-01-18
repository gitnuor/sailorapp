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
    public interface IOwinRolePermissionService
    {
       Task<bool> SaveAsync(owin_role_permission_entity entity);

		Task<bool> UpdateAsync(owin_role_permission_entity entity);

		Task<List<owin_role_permission_DTO>> GetAllAsync();

        Task<List<owin_role_permission_DTO>> GetByRoleIdAsync(Int64 Id);

        Task<List<owin_role_permission_entity>> GetAllPagedDataAsync(DtParameters request);

		Task<owin_role_permission_DTO> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64 Id);
    }
}

