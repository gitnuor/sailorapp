using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranSewingOutputService : ITranSewingOutputService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranSewingOutputService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

       

        public async Task<bool> UpdateAsync(tran_sewing_output_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_sewing_output_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_sewing_output_entity>> GetAllAsync()
        {
            List<tran_sewing_output_entity> list = new List<tran_sewing_output_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_sewing_output_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sewing_output_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_sewing_input_data_for_output_DTO> GetSewingOutputDataByInputId(long Id)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_sewing_output_data_by_sewing_input_id( @p_sewing_input_id)";

                    var dataList = connection.Query<rpc_sewing_input_data_for_output_DTO>(query,
                          new
                          {
                              p_sewing_input_id = Id

                          }
                         ).SingleOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_sewing_line_wise_input_deatail_DTO>> AddLine(long p_sewing_input_id, long production_line_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_sewing_output_by_sewing_input(@p_sewing_input_id,@p_line_id)";

                    var dataList = connection.Query<rpc_sewing_line_wise_input_deatail_DTO>(query,
                          new
                          {
                              p_sewing_input_id = p_sewing_input_id,
                              p_line_id = production_line_id
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

        public async Task<tran_sewing_output_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_sewing_output m   where m.tran_sewing_output_id=@Id";

                    var dataList = connection.Query<tran_sewing_output_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sewing_output_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_sewing_output_entity { tran_sewing_output_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> SaveAsync(tran_sewing_output_DTO objtran_sewing_output)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sewing_output_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_sewing_input_id", NpgsqlDbType.Bigint, objtran_sewing_output.tran_sewing_input_id == null ? DBNull.Value : objtran_sewing_output.tran_sewing_input_id);
                        Command.Parameters.AddWithValue("in_tran_sewing_allocation_plan_id", NpgsqlDbType.Bigint, objtran_sewing_output.tran_sewing_allocation_plan_id == null ? DBNull.Value : objtran_sewing_output.tran_sewing_allocation_plan_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_sewing_output.techpack_id == null ? DBNull.Value : objtran_sewing_output.techpack_id);
                        Command.Parameters.AddWithValue("in_output_date", NpgsqlDbType.Date, objtran_sewing_output.output_date == null ? DBNull.Value : objtran_sewing_output.output_date);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_sewing_output.note == null ? DBNull.Value : objtran_sewing_output.note);
                        Command.Parameters.AddWithValue("in_hour_output", NpgsqlDbType.Text, objtran_sewing_output.hour_output == null ? DBNull.Value : objtran_sewing_output.hour_output);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_sewing_output.fiscal_year_id == null ? DBNull.Value : objtran_sewing_output.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_sewing_output.event_id == null ? DBNull.Value : objtran_sewing_output.event_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_sewing_output.added_by == null ? DBNull.Value : objtran_sewing_output.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_sewing_output.date_added == null ? DBNull.Value : objtran_sewing_output.date_added);
                       Command.Parameters.AddWithValue("in_tran_sewing_output_details", NpgsqlDbType.Text, objtran_sewing_output.tran_sewing_output_details == null ? DBNull.Value : JsonConvert.SerializeObject(objtran_sewing_output.tran_sewing_output_details));


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
        public async Task<bool> tran_sewing_output_update_sp(tran_sewing_output_DTO objtran_sewing_output)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sewing_output_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_sewing_input_id", NpgsqlDbType.Bigint, objtran_sewing_output.tran_sewing_input_id == null ? DBNull.Value : objtran_sewing_output.tran_sewing_input_id);
                        Command.Parameters.AddWithValue("in_tran_sewing_allocation_plan_id", NpgsqlDbType.Bigint, objtran_sewing_output.tran_sewing_allocation_plan_id == null ? DBNull.Value : objtran_sewing_output.tran_sewing_allocation_plan_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_sewing_output.techpack_id == null ? DBNull.Value : objtran_sewing_output.techpack_id);
                        Command.Parameters.AddWithValue("in_output_date", NpgsqlDbType.Date, objtran_sewing_output.output_date == null ? DBNull.Value : objtran_sewing_output.output_date);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_sewing_output.note == null ? DBNull.Value : objtran_sewing_output.note);
                        Command.Parameters.AddWithValue("in_hour_output", NpgsqlDbType.Text, objtran_sewing_output.hour_output == null ? DBNull.Value : objtran_sewing_output.hour_output);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_sewing_output.fiscal_year_id == null ? DBNull.Value : objtran_sewing_output.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_sewing_output.event_id == null ? DBNull.Value : objtran_sewing_output.event_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_sewing_output.added_by == null ? DBNull.Value : objtran_sewing_output.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_sewing_output.updated_by == null ? DBNull.Value : objtran_sewing_output.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_sewing_output.date_added == null ? DBNull.Value : objtran_sewing_output.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_sewing_output.date_updated == null ? DBNull.Value : objtran_sewing_output.date_updated);
                        Command.Parameters.AddWithValue("in_tran_sewing_output_details", NpgsqlDbType.Text, objtran_sewing_output.tran_sewing_output_details == null ? DBNull.Value : objtran_sewing_output.tran_sewing_output_details);


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
        public async Task<List<rpc_tran_sewing_output_DTO>> GetTranSewingOutputtedData(long row_index, long page_size, long p_fiscal_year_id, long p_event_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_sewing_Outputed( @p_fiscal_year_id,@p_event_id, @row_index,@page_size)";

                    var dataList = connection.Query<rpc_tran_sewing_output_DTO>(query,
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
        public async Task<List<qc_failed_details_DTO>> GetAllQCParam()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT gs.param_name qc_parameter_name,0 as qc_failed_quantity FROM gen_satic_list gs   where gs.list_name='qc_parameters'";

                    var dataList = connection.Query<qc_failed_details_DTO>(query).ToList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
   
        public async Task<List<SelectListItem>> GetAllHourList()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT gs.param_name Text,gs.param_name as Value FROM gen_satic_list gs   where gs.list_name='sewing_hour_output'";

                    var dataList = connection.Query<SelectListItem>(query).ToList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

           
        }
        public async Task<List<rpc_tran_sewing_input_DTO>> GetAllSewingOutputPendingAsync(Int64 row_index, Int64 page_size, long p_fiscal_year_id, long p_event_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_sewing_output_pending(@p_fiscal_year_id,@p_event_id, @row_index,@page_size)";

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

