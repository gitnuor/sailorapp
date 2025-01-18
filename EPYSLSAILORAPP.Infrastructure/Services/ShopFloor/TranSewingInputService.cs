using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranSewingInputService : ITranSewingInputService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranSewingInputService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_sewing_input_DTO entity)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sewing_input_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_sewing_allocation_plan_id", NpgsqlDbType.Bigint, entity.tran_sewing_allocation_plan_id == null ?
                            DBNull.Value : entity.tran_sewing_allocation_plan_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, entity.techpack_id == null ? DBNull.Value : entity.techpack_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, entity.date_added == null ? DBNull.Value : entity.date_added);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, entity.added_by == null ? DBNull.Value : entity.event_id);
                        Command.Parameters.AddWithValue("in_tran_sewing_input_details", NpgsqlDbType.Text,
                            entity.tran_sewing_input_details == null ? DBNull.Value : JsonConvert.SerializeObject(entity.tran_sewing_input_details).ToString());



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

        public async Task<bool> UpdateAsync(tran_sewing_input_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_sewing_input_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_sewing_input_entity>> GetAllAsync()
        {
            List<tran_sewing_input_entity> list = new List<tran_sewing_input_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_sewing_input_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sewing_input_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_sewing_input_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_sewing_input m
                                           where  m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id

                                                     and 
                                                     case
                                                     when @search_text is null then true
                                                     when @search_text is not null and (
                                                            m. ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_sewing_input_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length,
                            fiscal_year_id=param.fiscal_year_id,
                            event_id=param.event_id
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        public async Task<tran_sewing_input_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_sewing_input m   where m.tran_sewing_input_id=@Id";

                    var dataList = connection.Query<tran_sewing_input_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sewing_input_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_sewing_input_entity { tran_sewing_input_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<bool> tran_sewing_input_update_sp(tran_sewing_input_DTO objtran_sewing_input)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sewing_input_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_sewing_allocation_plan_id", NpgsqlDbType.Bigint, objtran_sewing_input.tran_sewing_allocation_plan_id == null ? DBNull.Value : objtran_sewing_input.tran_sewing_allocation_plan_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_sewing_input.techpack_id == null ? DBNull.Value : objtran_sewing_input.techpack_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_sewing_input.added_by == null ? DBNull.Value : objtran_sewing_input.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_sewing_input.updated_by == null ? DBNull.Value : objtran_sewing_input.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_sewing_input.date_added == null ? DBNull.Value : objtran_sewing_input.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_sewing_input.date_updated == null ? DBNull.Value : objtran_sewing_input.date_updated);


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
        public async Task<List<rpc_tran_sewing_input_DTO>> GetAllPendingSewingInputAsync(Int64 row_index, Int64 page_size, long p_fiscal_year_id, long p_event_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_pending_sewing_input(@p_fiscal_year_id,@p_event_id, @row_index,@page_size)";

                    var dataList = connection.Query<rpc_tran_sewing_input_DTO>(query,
                          new
                          {
                              p_fiscal_year_id = p_fiscal_year_id,
                              p_event_id = p_event_id,
                              row_index = row_index,
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

        public async Task<List<rpc_tran_sewing_input_DTO>> GetAllSewingInputtedAsync(Int64 row_index, Int64 page_size, long p_fiscal_year_id, long p_event_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_sewing_inputted(@p_fiscal_year_id,@p_event_id, @row_index,@page_size)";

                    var dataList = connection.Query<rpc_tran_sewing_input_DTO>(query,
                          new
                          {
                              p_fiscal_year_id = p_fiscal_year_id,
                              p_event_id = p_event_id,
                              row_index = row_index,
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

