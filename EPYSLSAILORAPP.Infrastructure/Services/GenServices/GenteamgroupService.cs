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
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Application.Interface;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenTeamGroupService : IGenTeamGroupService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenTeamGroupService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_team_group_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_team_group_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_team_group_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_team_group_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_team_group_entity>> GetAllAsync()
        {
            List<gen_team_group_entity> list = new List<gen_team_group_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_team_group_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_team_group_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_team_group_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_team_group m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.team_group_name ilike '%' || @search_text || '%'  or
                                                            m.team_group_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_team_group_entity>(query,
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



        public async Task<gen_team_group_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_team_group m   where m.gen_team_group_id=@Id";

                    var dataList = connection.Query<gen_team_group_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_team_group_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_team_group_entity { gen_team_group_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_team_group_insert_sp(gen_team_group_DTO objgen_team_group)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_team_group_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_department_id", NpgsqlDbType.Bigint, objgen_team_group.department_id == null ? DBNull.Value : objgen_team_group.department_id);
                        Command.Parameters.AddWithValue("in_team_group_name", NpgsqlDbType.Text, objgen_team_group.team_group_name == null ? DBNull.Value : objgen_team_group.team_group_name);
                        Command.Parameters.AddWithValue("in_team_head_name", NpgsqlDbType.Text, objgen_team_group.team_head_name == null ? DBNull.Value : objgen_team_group.team_head_name);
                        Command.Parameters.AddWithValue("in_team_head_id_number", NpgsqlDbType.Text, objgen_team_group.team_head_id_number == null ? DBNull.Value : objgen_team_group.team_head_id_number);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_team_group.added_by == null ? DBNull.Value : objgen_team_group.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_team_group.updated_by == null ? DBNull.Value : objgen_team_group.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_team_group.date_added == null ? DBNull.Value : objgen_team_group.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_team_group.date_updated == null ? DBNull.Value : objgen_team_group.date_updated);


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
        public async Task<bool> gen_team_group_update_sp(gen_team_group_DTO objgen_team_group)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_team_group_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_department_id", NpgsqlDbType.Bigint, objgen_team_group.department_id == null ? DBNull.Value : objgen_team_group.department_id);
                        Command.Parameters.AddWithValue("in_team_group_name", NpgsqlDbType.Text, objgen_team_group.team_group_name == null ? DBNull.Value : objgen_team_group.team_group_name);
                        Command.Parameters.AddWithValue("in_team_head_name", NpgsqlDbType.Text, objgen_team_group.team_head_name == null ? DBNull.Value : objgen_team_group.team_head_name);
                        Command.Parameters.AddWithValue("in_team_head_id_number", NpgsqlDbType.Text, objgen_team_group.team_head_id_number == null ? DBNull.Value : objgen_team_group.team_head_id_number);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_team_group.added_by == null ? DBNull.Value : objgen_team_group.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_team_group.updated_by == null ? DBNull.Value : objgen_team_group.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_team_group.date_added == null ? DBNull.Value : objgen_team_group.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_team_group.date_updated == null ? DBNull.Value : objgen_team_group.date_updated);


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

