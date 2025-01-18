using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranDistributionPlanService : ITranDistributionPlanService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranDistributionPlanService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> UpdateAsync(tran_distribution_plan_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_distribution_plan_DTO>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_distribution_plan_DTO>> GetAllAsync()
        {
            List<tran_distribution_plan_DTO> list = new List<tran_distribution_plan_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_distribution_plan_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_distribution_plan_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_distribution_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_distribution_plan( @row_index,@page_size,@p_fiscal_year_id,@p_event_id,@search_text)";

                    var dataList = connection.Query<rpc_tran_distribution_DTO>(query,
                           new
                           {
                              
                               row_index = param.Start,
                               page_size = param.Length,
                               p_fiscal_year_id=param.fiscal_year_id,
                               p_event_id=param.event_id,
                               search_text=param.Search.Value
                           }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

          
        }



        public async Task<tran_distribution_plan_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_distribution_plan m   where m.tran_distribution_plan_id=@Id";

                    var dataList = connection.Query<tran_distribution_plan_DTO>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_distribution_plan_DTO>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_distribution_plan_DTO { tran_distribution_plan_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> SaveAsync(tran_distribution_plan_DTO objtran_distribution_plan)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_distribution_plan_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;


                        Command.Parameters.AddWithValue("in_distribution_date", NpgsqlDbType.Date, objtran_distribution_plan.distribution_date == null ? DBNull.Value : objtran_distribution_plan.distribution_date);
                        Command.Parameters.AddWithValue("in_distributed_by", NpgsqlDbType.Bigint, objtran_distribution_plan.distributed_by == null ? DBNull.Value : objtran_distribution_plan.distributed_by);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_distribution_plan.note == null ? DBNull.Value : objtran_distribution_plan.note);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_distribution_plan.fiscal_year_id == null ? DBNull.Value : objtran_distribution_plan.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_distribution_plan.event_id == null ? DBNull.Value : objtran_distribution_plan.event_id);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_distribution_plan.is_submitted == null ? DBNull.Value : objtran_distribution_plan.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_distribution_plan.submitted_by == null ? DBNull.Value : objtran_distribution_plan.submitted_by);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_distribution_plan.submitted_date == null ? DBNull.Value : objtran_distribution_plan.submitted_date);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_distribution_plan.added_by == null ? DBNull.Value : objtran_distribution_plan.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_distribution_plan.date_added == null ? DBNull.Value : objtran_distribution_plan.date_added);
                        Command.Parameters.AddWithValue("in_tran_distribution_plan_details_json", NpgsqlDbType.Text, objtran_distribution_plan.tran_distribution_plan_details_json.Count()>0 ? objtran_distribution_plan.tran_distribution_plan_details_json.ToString() : DBNull.Value );


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
        public async Task<bool> tran_distribution_plan_update_sp(tran_distribution_plan_DTO objtran_distribution_plan)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_distribution_plan_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_distribution_no", NpgsqlDbType.Text, objtran_distribution_plan.distribution_no == null ? DBNull.Value : objtran_distribution_plan.distribution_no);
                        Command.Parameters.AddWithValue("in_distribution_date", NpgsqlDbType.Date, objtran_distribution_plan.distribution_date == null ? DBNull.Value : objtran_distribution_plan.distribution_date);
                        Command.Parameters.AddWithValue("in_distributed_by", NpgsqlDbType.Bigint, objtran_distribution_plan.distributed_by == null ? DBNull.Value : objtran_distribution_plan.distributed_by);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_distribution_plan.note == null ? DBNull.Value : objtran_distribution_plan.note);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_distribution_plan.fiscal_year_id == null ? DBNull.Value : objtran_distribution_plan.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_distribution_plan.event_id == null ? DBNull.Value : objtran_distribution_plan.event_id);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_distribution_plan.is_submitted == null ? DBNull.Value : objtran_distribution_plan.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_distribution_plan.submitted_by == null ? DBNull.Value : objtran_distribution_plan.submitted_by);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_distribution_plan.submitted_date == null ? DBNull.Value : objtran_distribution_plan.submitted_date);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_distribution_plan.is_approved == null ? DBNull.Value : objtran_distribution_plan.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_distribution_plan.approved_by == null ? DBNull.Value : objtran_distribution_plan.approved_by);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_distribution_plan.approved_date == null ? DBNull.Value : objtran_distribution_plan.approved_date);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_distribution_plan.added_by == null ? DBNull.Value : objtran_distribution_plan.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_distribution_plan.updated_by == null ? DBNull.Value : objtran_distribution_plan.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_distribution_plan.date_added == null ? DBNull.Value : objtran_distribution_plan.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_distribution_plan.date_updated == null ? DBNull.Value : objtran_distribution_plan.date_updated);
                        Command.Parameters.AddWithValue("in_tran_distribution_plan_details_json", NpgsqlDbType.Text, objtran_distribution_plan.tran_distribution_plan_details_json == null ? DBNull.Value : objtran_distribution_plan.tran_distribution_plan_details_json);


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

        public async Task<List<tran_distribution_plan_details_DTO>> GetDistributionByTechpack(long p_techpack_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_for_distribution_plan_by_techpack_id( @p_techpack_id)";

                    var dataList = connection.Query<tran_distribution_plan_details_DTO>
                        (query, new { p_techpack_id = p_techpack_id }).AsList();


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

