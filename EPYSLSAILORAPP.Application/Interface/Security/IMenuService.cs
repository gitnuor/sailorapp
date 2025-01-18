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
using EPYSLSAILORAPP.Domain.Security;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IMenuService
    {
  //     Task<bool> SaveAsync(menu_entity entity);

		//Task<bool> UpdateAsync(menu_entity entity);

        Task<List<Menu>> GetMenusAsync(int userId, int applicationId, int companyId);

        Task<List<menu_entity>> GetAllAsync();

        //Task<List<menu_DTO>> GetAllPagedDataAsync(DtParameters request);

		//Task<menu_DTO> GetSingleAsync(Int64 Id);

        //Task<bool> DeleteAsync(Int64 Id);
    }
}

