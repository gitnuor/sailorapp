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
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Application.Interface
{
    public interface IGenTeamDepartmentGroupMembersService
    {
        Task<bool> SaveAsync(gen_team_department_group_members_entity entity);

        Task<bool> UpdateAsync(gen_team_department_group_members_entity entity);

        Task<List<gen_team_department_group_members_entity>> GetAllAsync();

        Task<List<gen_team_department_group_members_entity>> GetAllPagedDataAsync(DtParameters request);

        Task<gen_team_department_group_members_entity> GetSingleAsync(Int64 Id);

        Task<bool> DeleteAsync(Int64? Id);

        Task<bool> gen_team_department_group_members_insert_sp(gen_team_department_group_members_DTO objgen_team_department_group_members);
        Task<bool> gen_team_department_group_members_update_sp(gen_team_department_group_members_DTO objgen_team_department_group_members);

    }
}

