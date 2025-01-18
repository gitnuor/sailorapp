using EPYSLSAILORAPP.Application.DTO.BusinessPlanning;
using EPYSLSAILORAPP.Application.Interface.BusinessPlanning;
using EPYSLSAILORAPP.Domain.Entity.BusinessPlanning;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using static Postgrest.Constants;
using AutoMapper;
using EPYSLSAILORAPP.Application.DTO.Season;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Domain.Entity.GenTables;
using static Postgrest.QueryOptions;
using Postgrest;
using Microsoft.Extensions.Logging;
using Npgsql;
using Dapper.Contrib.Extensions;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.GenTables;
using NUnit.Framework.Internal.Execution;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenTeamDepartmentGroupMembersService : IGenTeamDepartmentGroupMembersService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenTeamDepartmentGroupMembersService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_team_department_group_members_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_team_department_group_members_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Insert(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(gen_team_department_group_members_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_team_department_group_members_entity>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Update(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_team_department_group_members_entity>> GetAllAsync()
        {
            List<gen_team_department_group_members_entity> list = new List<gen_team_department_group_members_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_team_department_group_members_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_team_department_group_members_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_team_department_group_members_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_team_department_group_members m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.member_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_team_department_group_members_entity>(query,
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



        public async Task<gen_team_department_group_members_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_team_department_group_members m   where m.gen_team_department_group_members_id=@Id";

                    var dataList = connection.Query<gen_team_department_group_members_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_team_department_group_members_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_team_department_group_members_entity { gen_team_department_group_members_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_team_department_group_members_insert_sp(gen_team_department_group_members_DTO objgen_team_department_group_members)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_team_department_group_members_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_gen_team_department_group_id", NpgsqlDbType.Bigint, objgen_team_department_group_members.group_id == null ? DBNull.Value : objgen_team_department_group_members.group_id);
                        Command.Parameters.AddWithValue("in_member_name", NpgsqlDbType.Text, objgen_team_department_group_members.member_name == null ? DBNull.Value : objgen_team_department_group_members.member_name);
                        Command.Parameters.AddWithValue("in_member_employee_id", NpgsqlDbType.Text, objgen_team_department_group_members.member_employee_id == null ? DBNull.Value : objgen_team_department_group_members.member_employee_id);
                        Command.Parameters.AddWithValue("in_member_user_id", NpgsqlDbType.Bigint, objgen_team_department_group_members.member_user_id == null ? DBNull.Value : objgen_team_department_group_members.member_user_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_team_department_group_members.added_by == null ? DBNull.Value : objgen_team_department_group_members.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_team_department_group_members.updated_by == null ? DBNull.Value : objgen_team_department_group_members.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_team_department_group_members.date_added == null ? DBNull.Value : objgen_team_department_group_members.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_team_department_group_members.date_updated == null ? DBNull.Value : objgen_team_department_group_members.date_updated);
                        Command.Parameters.AddWithValue("in_is_active", NpgsqlDbType.Bigint, objgen_team_department_group_members.is_active == null ? DBNull.Value : objgen_team_department_group_members.is_active);


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
        public async Task<bool> gen_team_department_group_members_update_sp(gen_team_department_group_members_DTO objgen_team_department_group_members)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_team_department_group_members_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_gen_team_department_group_id", NpgsqlDbType.Bigint, objgen_team_department_group_members.group_id == null ? DBNull.Value : objgen_team_department_group_members.group_id);
                        Command.Parameters.AddWithValue("in_member_name", NpgsqlDbType.Text, objgen_team_department_group_members.member_name == null ? DBNull.Value : objgen_team_department_group_members.member_name);
                        Command.Parameters.AddWithValue("in_member_employee_id", NpgsqlDbType.Text, objgen_team_department_group_members.member_employee_id == null ? DBNull.Value : objgen_team_department_group_members.member_employee_id);
                        Command.Parameters.AddWithValue("in_member_user_id", NpgsqlDbType.Bigint, objgen_team_department_group_members.member_user_id == null ? DBNull.Value : objgen_team_department_group_members.member_user_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_team_department_group_members.added_by == null ? DBNull.Value : objgen_team_department_group_members.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_team_department_group_members.updated_by == null ? DBNull.Value : objgen_team_department_group_members.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_team_department_group_members.date_added == null ? DBNull.Value : objgen_team_department_group_members.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_team_department_group_members.date_updated == null ? DBNull.Value : objgen_team_department_group_members.date_updated);
                        Command.Parameters.AddWithValue("in_is_active", NpgsqlDbType.Bigint, objgen_team_department_group_members.is_active == null ? DBNull.Value : objgen_team_department_group_members.is_active);


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

