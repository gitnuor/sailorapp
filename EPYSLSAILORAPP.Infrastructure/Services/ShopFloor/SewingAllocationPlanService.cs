using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Drawing.Printing;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class SewingAllocationPlanService : ISewingAllocationPlanService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SewingAllocationPlanService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(sewing_plan_insert_dto entity)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sewing_allocation_plan_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;


                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint,
                            entity.techpack_id == null ? DBNull.Value : entity.techpack_id);
                        Command.Parameters.AddWithValue("in_sewing_allocation_date", NpgsqlDbType.Date,
                            entity.sewing_allocation_date == null ? DBNull.Value : entity.sewing_allocation_date);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint,
                            entity.merchandiser_id == null ? DBNull.Value : entity.merchandiser_id);
                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint,
                            entity.style_item_product_id == null ? DBNull.Value : entity.style_item_product_id);

                        Command.Parameters.AddWithValue("in_style_item_product_category", NpgsqlDbType.Text,
                            entity.style_item_product_category == null ? DBNull.Value : entity.style_item_product_category);

                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint,
                            entity.added_by == null ? DBNull.Value : entity.added_by);

                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint,
                            entity.fiscal_year_id == null ? DBNull.Value : entity.fiscal_year_id);

                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint,
                            entity.event_id == null ? DBNull.Value : entity.event_id);

                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date,
                            entity.date_added == null ? DBNull.Value : entity.date_added);

                        Command.Parameters.AddWithValue("in_tran_sewing_allocation_plan_details", NpgsqlDbType.Text,
                            entity.tran_sewing_allocation_plan_details == null ? DBNull.Value : JsonConvert.SerializeObject(entity.tran_sewing_allocation_plan_details).ToString());

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

        public async Task<bool> UpdateAsync(tran_sewing_allocation_plan_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_sewing_allocation_plan_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_sewing_allocation_plan_entity>> GetAllAsync()
        {
            List<tran_sewing_allocation_plan_entity> list = new List<tran_sewing_allocation_plan_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_sewing_allocation_plan_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sewing_allocation_plan_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_sewing_allocation_plan_DTO>> GetAllPagedDataAsync(Int64 fiscal_year_id, Int64 event_id, Int64 currnet_page, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_sewing_allocation_plan( @row_index,@page_size,@p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_sewing_allocation_plan_DTO>(query,
                          new
                          {
                              p_fiscal_year_id = fiscal_year_id,
                              p_event_id = event_id,
                              row_index = currnet_page,
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


        public async Task<rpc_sewing_allocation_data_DTO> GetSewingInputDataByAllocationId(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_sewing_input_data_by_sewing_allocation_id( @p_sewing_allocation_plan_id)";

                    var dataList = connection.Query<rpc_sewing_allocation_data_DTO>(query,
                          new
                          {
                              p_sewing_allocation_plan_id = Id

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
        public async Task<List<rpc_sewing_line_wise_deatail_DTO>> AddLine(long tran_sewing_allocation_plan_id, long production_line_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_sewing_input_by_sewing_plan( @p_sewing_plan_id,@p_line_id)";

                    var dataList = connection.Query<rpc_sewing_line_wise_deatail_DTO>(query,
                          new
                          {
                              p_sewing_plan_id = tran_sewing_allocation_plan_id,
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
        public async Task<tran_sewing_allocation_plan_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_sewing_allocation_plan m   where m.tran_sewing_allocation_plan_id=@Id";

                    var dataList = connection.Query<tran_sewing_allocation_plan_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_sewing_allocation_plan_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_sewing_allocation_plan_entity { tran_sewing_allocation_plan_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_sewing_allocation_plan_insert_sp(tran_sewing_allocation_plan_DTO objtran_sewing_allocation_plan)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sewing_allocation_plan_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_sewing_allocation_number", NpgsqlDbType.Text, objtran_sewing_allocation_plan.sewing_allocation_number == null ? DBNull.Value : objtran_sewing_allocation_plan.sewing_allocation_number);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.techpack_id == null ? DBNull.Value : objtran_sewing_allocation_plan.techpack_id);
                        Command.Parameters.AddWithValue("in_sewing_allocation_date", NpgsqlDbType.Date, objtran_sewing_allocation_plan.sewing_allocation_date == null ? DBNull.Value : objtran_sewing_allocation_plan.sewing_allocation_date);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.merchandiser_id == null ? DBNull.Value : objtran_sewing_allocation_plan.merchandiser_id);
                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.style_item_product_id == null ? DBNull.Value : objtran_sewing_allocation_plan.style_item_product_id);
                        Command.Parameters.AddWithValue("in_style_item_product_category", NpgsqlDbType.Text, objtran_sewing_allocation_plan.style_item_product_category == null ? DBNull.Value : objtran_sewing_allocation_plan.style_item_product_category);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.added_by == null ? DBNull.Value : objtran_sewing_allocation_plan.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.updated_by == null ? DBNull.Value : objtran_sewing_allocation_plan.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_sewing_allocation_plan.date_added == null ? DBNull.Value : objtran_sewing_allocation_plan.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_sewing_allocation_plan.date_updated == null ? DBNull.Value : objtran_sewing_allocation_plan.date_updated);


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
        public async Task<bool> tran_sewing_allocation_plan_update_sp(tran_sewing_allocation_plan_DTO objtran_sewing_allocation_plan)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sewing_allocation_plan_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_sewing_allocation_number", NpgsqlDbType.Text, objtran_sewing_allocation_plan.sewing_allocation_number == null ? DBNull.Value : objtran_sewing_allocation_plan.sewing_allocation_number);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.techpack_id == null ? DBNull.Value : objtran_sewing_allocation_plan.techpack_id);
                        Command.Parameters.AddWithValue("in_sewing_allocation_date", NpgsqlDbType.Date, objtran_sewing_allocation_plan.sewing_allocation_date == null ? DBNull.Value : objtran_sewing_allocation_plan.sewing_allocation_date);
                        Command.Parameters.AddWithValue("in_merchandiser_id", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.merchandiser_id == null ? DBNull.Value : objtran_sewing_allocation_plan.merchandiser_id);
                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.style_item_product_id == null ? DBNull.Value : objtran_sewing_allocation_plan.style_item_product_id);
                        Command.Parameters.AddWithValue("in_style_item_product_category", NpgsqlDbType.Text, objtran_sewing_allocation_plan.style_item_product_category == null ? DBNull.Value : objtran_sewing_allocation_plan.style_item_product_category);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.added_by == null ? DBNull.Value : objtran_sewing_allocation_plan.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_sewing_allocation_plan.updated_by == null ? DBNull.Value : objtran_sewing_allocation_plan.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_sewing_allocation_plan.date_added == null ? DBNull.Value : objtran_sewing_allocation_plan.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_sewing_allocation_plan.date_updated == null ? DBNull.Value : objtran_sewing_allocation_plan.date_updated);


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

