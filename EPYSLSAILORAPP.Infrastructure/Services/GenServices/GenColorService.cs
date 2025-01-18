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

    public class GenColorService : IGenColorService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenColorService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_color_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_color_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_color_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_color_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_color_entity>> GetAllAsync()
        {
            List<gen_color_entity> list = new List<gen_color_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_color_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_color_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_color_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_color m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.color_name ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_color_entity>(query,
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



        public async Task<gen_color_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_color m   where m.gen_color_id=@Id";

                    var dataList = connection.Query<gen_color_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_color_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_color_entity { gen_color_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_color_insert_sp(gen_color_DTO objgen_color)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_color_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_color_name", NpgsqlDbType.Text, objgen_color.color_name == null ? DBNull.Value : objgen_color.color_name);
                        Command.Parameters.AddWithValue("in_color_code", NpgsqlDbType.Text, objgen_color.color_code == null ? DBNull.Value : objgen_color.color_code);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_color.added_by == null ? DBNull.Value : objgen_color.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_color.updated_by == null ? DBNull.Value : objgen_color.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_color.date_added == null ? DBNull.Value : objgen_color.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_color.date_updated == null ? DBNull.Value : objgen_color.date_updated);


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
        public async Task<bool> gen_color_update_sp(gen_color_DTO objgen_color)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_color_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_color_name", NpgsqlDbType.Text, objgen_color.color_name == null ? DBNull.Value : objgen_color.color_name);
                        Command.Parameters.AddWithValue("in_color_code", NpgsqlDbType.Text, objgen_color.color_code == null ? DBNull.Value : objgen_color.color_code);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_color.added_by == null ? DBNull.Value : objgen_color.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_color.updated_by == null ? DBNull.Value : objgen_color.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_color.date_added == null ? DBNull.Value : objgen_color.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_color.date_updated == null ? DBNull.Value : objgen_color.date_updated);


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

