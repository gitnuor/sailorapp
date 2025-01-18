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
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenSeasonEventConfigService : IGenSeasonEventConfigService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenSeasonEventConfigService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_season_event_config_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_season_event_config_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_season_event_config_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_season_event_config_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_season_event_config_entity>> GetAllAsync()
        {
            List<gen_season_event_config_entity> list = new List<gen_season_event_config_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_season_event_config_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_season_event_config_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_season_event_config_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.event_id,
                                      f.year_name,
                                      s.season_name,
                                      m.event_title,
                                      m.start_date,
                                      m.end_date
                                      FROM gen_season_event_config m


                                     inner join gen_fiscal_year f on f.fiscal_year_id=m.fiscal_year_id
                                     inner join  gen_season s on s.season_id=m.season_id
                                                   where case
                                                             when @search_text is null or length(@search_text)=0 then true
                                                             when @search_text is not null and length(@search_text)>0 and (
                                                                    m.event_title ilike '%' || @search_text || '%'
                                                                 ) then true
                                                             else false end)


                                                    SELECT cte_saved.*,
                                                           (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                                    FROM cte_saved
                                                    order by cte_saved.event_id desc
                                                    OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_season_event_config_DTO>(query,
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



        public async Task<gen_season_event_config_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_season_event_config m   where m.event_id=@Id";

                    var dataList = connection.Query<gen_season_event_config_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_season_event_config_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new gen_season_event_config_entity { event_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_season_event_config_insert_sp(gen_season_event_config_DTO objgen_season_event_config)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_season_event_config_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_season_id", NpgsqlDbType.Bigint, objgen_season_event_config.season_id == null ? DBNull.Value : objgen_season_event_config.season_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objgen_season_event_config.fiscal_year_id == null ? DBNull.Value : objgen_season_event_config.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_start_date", NpgsqlDbType.Date, objgen_season_event_config.start_date == null ? DBNull.Value : objgen_season_event_config.start_date);
                        Command.Parameters.AddWithValue("in_start_month_id", NpgsqlDbType.Bigint, objgen_season_event_config.start_month_id == null ? DBNull.Value : objgen_season_event_config.start_month_id);
                        Command.Parameters.AddWithValue("in_end_month_id", NpgsqlDbType.Bigint, objgen_season_event_config.end_month_id == null ? DBNull.Value : objgen_season_event_config.end_month_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_season_event_config.added_by == null ? DBNull.Value : objgen_season_event_config.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_season_event_config.updated_by == null ? DBNull.Value : objgen_season_event_config.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_season_event_config.date_added == null ? DBNull.Value : objgen_season_event_config.date_added);
                        Command.Parameters.AddWithValue("in_event_title", NpgsqlDbType.Text, objgen_season_event_config.event_title == null ? DBNull.Value : objgen_season_event_config.event_title);
                        Command.Parameters.AddWithValue("in_is_active", NpgsqlDbType.Boolean, objgen_season_event_config.is_active == null ? DBNull.Value : objgen_season_event_config.is_active);
                        Command.Parameters.AddWithValue("in_event_sequence", NpgsqlDbType.Bigint, objgen_season_event_config.event_sequence == null ? DBNull.Value : objgen_season_event_config.event_sequence);
                        Command.Parameters.AddWithValue("in_end_date", NpgsqlDbType.Date, objgen_season_event_config.end_date == null ? DBNull.Value : objgen_season_event_config.end_date);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_season_event_config.date_updated == null ? DBNull.Value : objgen_season_event_config.date_updated);


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
        public async Task<bool> gen_season_event_config_update_sp(gen_season_event_config_DTO objgen_season_event_config)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_season_event_config_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_season_id", NpgsqlDbType.Bigint, objgen_season_event_config.season_id == null ? DBNull.Value : objgen_season_event_config.season_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objgen_season_event_config.fiscal_year_id == null ? DBNull.Value : objgen_season_event_config.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_start_date", NpgsqlDbType.Date, objgen_season_event_config.start_date == null ? DBNull.Value : objgen_season_event_config.start_date);
                        Command.Parameters.AddWithValue("in_start_month_id", NpgsqlDbType.Bigint, objgen_season_event_config.start_month_id == null ? DBNull.Value : objgen_season_event_config.start_month_id);
                        Command.Parameters.AddWithValue("in_end_month_id", NpgsqlDbType.Bigint, objgen_season_event_config.end_month_id == null ? DBNull.Value : objgen_season_event_config.end_month_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_season_event_config.added_by == null ? DBNull.Value : objgen_season_event_config.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_season_event_config.updated_by == null ? DBNull.Value : objgen_season_event_config.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_season_event_config.date_added == null ? DBNull.Value : objgen_season_event_config.date_added);
                        Command.Parameters.AddWithValue("in_event_title", NpgsqlDbType.Text, objgen_season_event_config.event_title == null ? DBNull.Value : objgen_season_event_config.event_title);
                        Command.Parameters.AddWithValue("in_is_active", NpgsqlDbType.Boolean, objgen_season_event_config.is_active == null ? DBNull.Value : objgen_season_event_config.is_active);
                        Command.Parameters.AddWithValue("in_event_sequence", NpgsqlDbType.Bigint, objgen_season_event_config.event_sequence == null ? DBNull.Value : objgen_season_event_config.event_sequence);
                        Command.Parameters.AddWithValue("in_end_date", NpgsqlDbType.Date, objgen_season_event_config.end_date == null ? DBNull.Value : objgen_season_event_config.end_date);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_season_event_config.date_updated == null ? DBNull.Value : objgen_season_event_config.date_updated);


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
        public async Task<List<rpc_gen_season_event_config_DTO>> GetAllJoined_GenSeasonEventConfigAsync(Int64 currnet_page, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_gen_season_event_config( @currnet_page,@page_size)";

                    var dataList = connection.Query<rpc_gen_season_event_config_DTO>(query,
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

