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
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenChatGroupService : IGenChatGroupService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public GenChatGroupService(IConfiguration configuration, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {

            _mapper = mapper;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<bool> SaveAsync(gen_chat_group_DTO objgen_chat_group)
        {
            try
            {

                objgen_chat_group.group_users =  JsonConvert.SerializeObject(objgen_chat_group.det_list);

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var Command = new NpgsqlCommand("gen_chat_group_insert", connection);

                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.AddWithValue("in_chat_group_name", NpgsqlDbType.Text, objgen_chat_group.chat_group_name == null ? DBNull.Value : objgen_chat_group.chat_group_name);
                            Command.Parameters.AddWithValue("in_group_users", NpgsqlDbType.Text, objgen_chat_group.group_users == null ? DBNull.Value : objgen_chat_group.group_users);
                            Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_chat_group.added_by == null ? DBNull.Value : objgen_chat_group.added_by);
                            Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_chat_group.date_added == null ? DBNull.Value : objgen_chat_group.date_added);
                          
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

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(gen_chat_group_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_chat_group_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_chat_group_entity>> GetAllAsync()
        {
            List<gen_chat_group_entity> list = new List<gen_chat_group_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_chat_group_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_chat_group_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_chat_group_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_chat_group m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.chat_group_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_chat_group_entity>(query,
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



        public async Task<gen_chat_group_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_chat_group m   where m.chat_group_id=@Id";

                    var dataList = connection.Query<gen_chat_group_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_chat_group_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_chat_group_entity { chat_group_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_chat_group_insert_sp(gen_chat_group_DTO objgen_chat_group)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_chat_group_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_chat_group_name", NpgsqlDbType.Text, objgen_chat_group.chat_group_name == null ? DBNull.Value : objgen_chat_group.chat_group_name);
                        Command.Parameters.AddWithValue("in_group_users", NpgsqlDbType.Text, objgen_chat_group.group_users == null ? DBNull.Value : objgen_chat_group.group_users);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_chat_group.added_by == null ? DBNull.Value : objgen_chat_group.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_chat_group.updated_by == null ? DBNull.Value : objgen_chat_group.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_chat_group.date_added == null ? DBNull.Value : objgen_chat_group.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_chat_group.date_updated == null ? DBNull.Value : objgen_chat_group.date_updated);


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
        public async Task<bool> gen_chat_group_update_sp(gen_chat_group_DTO objgen_chat_group)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_chat_group_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_chat_group_name", NpgsqlDbType.Text, objgen_chat_group.chat_group_name == null ? DBNull.Value : objgen_chat_group.chat_group_name);
                        Command.Parameters.AddWithValue("in_group_users", NpgsqlDbType.Text, objgen_chat_group.group_users == null ? DBNull.Value : objgen_chat_group.group_users);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_chat_group.added_by == null ? DBNull.Value : objgen_chat_group.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_chat_group.updated_by == null ? DBNull.Value : objgen_chat_group.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_chat_group.date_added == null ? DBNull.Value : objgen_chat_group.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_chat_group.date_updated == null ? DBNull.Value : objgen_chat_group.date_updated);


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

