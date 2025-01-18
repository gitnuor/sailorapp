using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenTeamDepartmentGroupService : IGenTeamDepartmentGroupService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenTeamDepartmentGroupService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_team_department_group_DTO entity)
        {
            try
            {
                entity.detl_list = entity.GenTeamDepartmentGroupMembers_List == null ? string.Empty :
                          JsonConvert.SerializeObject(entity.GenTeamDepartmentGroupMembers_List);


                return await gen_team_department_group_insert_sp(entity);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(gen_team_department_group_DTO entity)
        {
            try
            {
                entity.detl_list = entity.GenTeamDepartmentGroupMembers_List == null ? string.Empty :
                          JsonConvert.SerializeObject(entity.GenTeamDepartmentGroupMembers_List);

                return await gen_team_department_group_update_sp(entity);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_team_department_group_entity>> GetAllAsync()
        {
            List<gen_team_department_group_entity> list = new List<gen_team_department_group_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_team_department_group_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_team_department_group_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_team_department_group_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_team_department_group m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.team_group_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_team_department_group_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<string> CheckMemberByUserID(Int64 UserID, Int64 groupid = 0)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = "";

                    if (groupid == 0)
                    {
                        query = @"select m.* from gen_team_department_group_members d
                                             inner join gen_team_department_group m on m.group_id=d.group_id
                                    where d.member_user_id=@member_user_id and d.is_active=1";
                       
                        var dataList = connection.Query<gen_team_department_group_DTO>(query,
                          new
                          {
                              member_user_id = UserID,
                              group_id = groupid
                          }).ToList();

                        return dataList.FirstOrDefault() != null ? dataList.FirstOrDefault().team_group_name : string.Empty;


                    }
                    else
                    {
                        query = @"select m.* from gen_team_department_group_members d
                                             inner join gen_team_department_group m on m.group_id=d.group_id
                                    where d.member_user_id=@member_user_id and d.is_active=1 and m.group_id!=@group_id";

                        var dataList = connection.Query<gen_team_department_group_DTO>(query,
                       new
                       {
                           member_user_id = UserID,
                           group_id = groupid
                       }).ToList();

                        return dataList.FirstOrDefault() != null ? dataList.FirstOrDefault().team_group_name : string.Empty;

                    }

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<gen_team_department_group_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*,

                                   (select jsonb_agg(
                                                   jsonb_build_object(
                                                           'group_members_id', gtdgm.group_members_id,
                                                           'group_id', gtdgm.group_id,
                                                           'member_name', gtdgm.member_name,
                                                           'member_employee_id', gtdgm.member_employee_id,
                                                           'member_user_id', gtdgm.member_user_id,
                                                           'is_active', gtdgm.is_active,
                                                           'current_state', 2, 
                                                           'emp_pic', u.emp_pic,
                                                           'designation_name', gd.designation_name,
                                                           'employee_code', u.employee_code,
                                                           'name', u.name
                                                   )
                                           )
                                     from gen_team_department_group_members gtdgm
                                     inner join owin_user u on u.userid = gtdgm.member_user_id
                                     inner join gen_designation gd on u.designation_id = gd.designation_id
                                     where gtdgm.group_id = m.group_id) as detl_list

                                 FROM gen_team_department_group m

                                 where m.group_id=@Id";

                    var dataList = connection.Query<gen_team_department_group_DTO>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    dataList.GenTeamDepartmentGroupMembers_List = JsonConvert.DeserializeObject<List<gen_team_department_group_members_DTO>>(dataList.detl_list);

                    return dataList;//JsonConvert.DeserializeObject<List<gen_team_department_group_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> DeleteAsync(Int64? Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new gen_team_department_group_entity { group_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_team_department_group_insert_sp(gen_team_department_group_DTO objgen_team_department_group)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_team_department_group_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;
                        Command.CommandTimeout = 100;

                        Command.Parameters.AddWithValue("in_team_head_user_id", NpgsqlDbType.Bigint, objgen_team_department_group.team_head_user_id == null ? DBNull.Value : objgen_team_department_group.team_head_user_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_team_department_group.added_by == null ? DBNull.Value : objgen_team_department_group.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_team_department_group.date_added == null ? DBNull.Value : objgen_team_department_group.date_added);
                        Command.Parameters.AddWithValue("in_team_group_id", NpgsqlDbType.Bigint, objgen_team_department_group.team_group_id == null ? DBNull.Value : objgen_team_department_group.team_group_id);
                        Command.Parameters.AddWithValue("in_team_group_name", NpgsqlDbType.Text, objgen_team_department_group.team_group_name == null ? DBNull.Value : objgen_team_department_group.team_group_name);
                        Command.Parameters.AddWithValue("in_team_head_name", NpgsqlDbType.Text, objgen_team_department_group.team_head_name == null ? DBNull.Value : objgen_team_department_group.team_head_name);
                        Command.Parameters.AddWithValue("in_team_head_id_number", NpgsqlDbType.Text, objgen_team_department_group.team_head_id_number == null ? DBNull.Value : objgen_team_department_group.team_head_id_number);
                        Command.Parameters.AddWithValue("in_team_group", NpgsqlDbType.Text, objgen_team_department_group.team_group == null ? DBNull.Value : objgen_team_department_group.team_group);
                        Command.Parameters.AddWithValue("in_detl_list", NpgsqlDbType.Text, objgen_team_department_group.detl_list);

                        Command.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public async Task<bool> gen_team_department_group_update_sp(gen_team_department_group_DTO objgen_team_department_group)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_team_department_group_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_gen_team_group_id", NpgsqlDbType.Bigint, objgen_team_department_group.group_id == null ? DBNull.Value : objgen_team_department_group.group_id);

                        Command.Parameters.AddWithValue("in_team_head_user_id", NpgsqlDbType.Bigint, objgen_team_department_group.team_head_user_id == null ? DBNull.Value : objgen_team_department_group.team_head_user_id);

                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_team_department_group.added_by == null ? DBNull.Value : objgen_team_department_group.added_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_team_department_group.date_added == null ? DBNull.Value : objgen_team_department_group.date_added);

                        Command.Parameters.AddWithValue("in_team_group_id", NpgsqlDbType.Bigint, objgen_team_department_group.team_group_id == null ? DBNull.Value : objgen_team_department_group.team_group_id);

                        Command.Parameters.AddWithValue("in_team_group_name", NpgsqlDbType.Text, objgen_team_department_group.team_group_name == null ? DBNull.Value : objgen_team_department_group.team_group_name);

                        Command.Parameters.AddWithValue("in_team_head_name", NpgsqlDbType.Text, objgen_team_department_group.team_head_name == null ? DBNull.Value : objgen_team_department_group.team_head_name);
                        Command.Parameters.AddWithValue("in_team_head_id_number", NpgsqlDbType.Text, objgen_team_department_group.team_head_id_number == null ? DBNull.Value : objgen_team_department_group.team_head_id_number);

                        Command.Parameters.AddWithValue("in_team_group", NpgsqlDbType.Text, objgen_team_department_group.team_group == null ? DBNull.Value : objgen_team_department_group.team_group);

                        Command.Parameters.AddWithValue("in_detl_list", NpgsqlDbType.Text, objgen_team_department_group.detl_list);

                        Command.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }


    }

}

