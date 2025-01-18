using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;

using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Security;
using EPYSLSAILORAPP.Domain.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Application.Interface.Security
{
    public interface IOwinUserService
    {
        Task<ResultEntity> CheckUserForLogin(owin_user_DTO model);
        Task UserLogOff();
        Task<bool> SaveAsync(owin_user_DTO entity);
        Task<bool> UpdateAsync(owin_user_DTO entity);

        Task<bool> UpdatePasswordAsync(owin_user_DTO entity);

        Task<List<owin_user_entity>> GetAll();

        Task<List<owin_user_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<List<owin_user_entity_Ext>> GetAllPagedDataForSelect2(DtParameters param);

        Task<owin_user_DTO> GetSingleAsync(Int64 Id);
      
        Task<string>  RoleById(long? roleid);


        Task<List<owin_user_DTO>> GetAllUserByNavigateUrl(string navigate_url);

        Task<List<owin_user_DTO>> GetMemberListByTeamID(Int64 gen_team_group_id);

        Task<owin_user_DTO> GetSingleAsyncByROle(string username);

    }
}
