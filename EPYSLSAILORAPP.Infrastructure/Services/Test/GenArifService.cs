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
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.DTO;
using System.Data;
using NpgsqlTypes;
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenArifService : IGenArifService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenArifService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_arif_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_arif_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_arif_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_arif_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_arif_entity>> GetAllAsync()
        {
            List<gen_arif_entity> list = new List<gen_arif_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_arif_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_arif_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_arif_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM gen_arif_entity m
                                           where case
                                                     when @search_text is null then true
                                                     when @search_text is not null and (
                                                            m.{TextColumn} ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_arif_entity>(query,
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



        public async Task<gen_arif_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_arif_entity m   where m.gen_arif_id=@Id";

                    var dataList = connection.Query<gen_arif_entity>(query,
                        new { Id = Id }).ToList();

                    return dataList.FirstOrDefault();//JsonConvert.DeserializeObject<List<gen_arif_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> DeleteAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new gen_arif_entity { gen_arif_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_arif_insert_sp(gen_arif_DTO objgen_arif)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_arif_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_bank_name", NpgsqlDbType.Text, objgen_arif.bank_name);
                        Command.Parameters.AddWithValue("in_bank_short_name", NpgsqlDbType.Text, objgen_arif.bank_short_name);
                        Command.Parameters.AddWithValue("in_is_used", NpgsqlDbType.Boolean, objgen_arif.is_used);
                        Command.Parameters.AddWithValue("in_is_local", NpgsqlDbType.Boolean, objgen_arif.is_local);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_arif.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_arif.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_arif.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_arif.date_updated);
                        Command.Parameters.AddWithValue("in_arif_details_1", NpgsqlDbType.Text, objgen_arif.arif_details_1);
                        Command.Parameters.AddWithValue("in_arif_details_2", NpgsqlDbType.Text, objgen_arif.arif_details_2);
                        Command.Parameters.AddWithValue("in_unit_id", NpgsqlDbType.Bigint, objgen_arif.unit_id);
                        Command.Parameters.AddWithValue("in_district_id", NpgsqlDbType.Bigint, objgen_arif.district_id);


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
        public async Task<bool> gen_arif_update_sp(gen_arif_DTO objgen_arif)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_arif_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_bank_name", NpgsqlDbType.Text, objgen_arif.bank_name);
                        Command.Parameters.AddWithValue("in_bank_short_name", NpgsqlDbType.Text, objgen_arif.bank_short_name);
                        Command.Parameters.AddWithValue("in_is_used", NpgsqlDbType.Boolean, objgen_arif.is_used);
                        Command.Parameters.AddWithValue("in_is_local", NpgsqlDbType.Boolean, objgen_arif.is_local);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_arif.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_arif.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_arif.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_arif.date_updated);
                        Command.Parameters.AddWithValue("in_arif_details_1", NpgsqlDbType.Text, objgen_arif.arif_details_1);
                        Command.Parameters.AddWithValue("in_arif_details_2", NpgsqlDbType.Text, objgen_arif.arif_details_2);
                        Command.Parameters.AddWithValue("in_unit_id", NpgsqlDbType.Bigint, objgen_arif.unit_id);
                        Command.Parameters.AddWithValue("in_district_id", NpgsqlDbType.Bigint, objgen_arif.district_id);


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
        public async Task<List<rpc_gen_arif_DTO>> GetAllJoined_GenArifAsync(Int64 currnet_page, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_gen_arif( @currnet_page,@page_size)";

                    var dataList = connection.Query<rpc_gen_arif_DTO>(query,
                          new
                          {
                              currnet_page = currnet_page,
                              page_size = page_size
                          }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

    }

}

