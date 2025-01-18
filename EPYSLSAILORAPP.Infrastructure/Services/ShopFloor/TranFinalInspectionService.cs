using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranFinalInspectionService : ITranFinalInspectionService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranFinalInspectionService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_final_inspection_DTO objtran_final_inspection)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_final_inspection_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_final_inspection_date", NpgsqlDbType.Date, objtran_final_inspection.final_inspection_date == null ? DBNull.Value : objtran_final_inspection.final_inspection_date);

                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_final_inspection.techpack_id == null ? DBNull.Value : objtran_final_inspection.techpack_id);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_final_inspection.note == null ? DBNull.Value : objtran_final_inspection.note);
                        Command.Parameters.AddWithValue("in_is_draft", NpgsqlDbType.Bigint, objtran_final_inspection.is_draft == null ? DBNull.Value : objtran_final_inspection.is_draft);
                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_final_inspection.style_item_product_id == null ? DBNull.Value : objtran_final_inspection.style_item_product_id);

                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_final_inspection.added_by == null ? DBNull.Value : objtran_final_inspection.added_by);

                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_final_inspection.date_added == null ? DBNull.Value : objtran_final_inspection.date_added);

                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_final_inspection.fiscal_year_id == null ? DBNull.Value : objtran_final_inspection.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_final_inspection.event_id == null ? DBNull.Value : objtran_final_inspection.event_id);
                        Command.Parameters.AddWithValue("in_tran_final_inspection_details", NpgsqlDbType.Text, objtran_final_inspection.tran_final_inspection_details == null ? 
                            DBNull.Value : objtran_final_inspection.tran_final_inspection_details.ToString());


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

        public async Task<bool> UpdateAsync(tran_final_inspection_DTO objtran_final_inspection)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_final_inspection_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_final_inspection_id", NpgsqlDbType.Bigint, objtran_final_inspection.tran_final_inspection_id == null ? DBNull.Value : objtran_final_inspection.tran_final_inspection_id);
                        Command.Parameters.AddWithValue("in_final_inspection_date", NpgsqlDbType.Date, objtran_final_inspection.final_inspection_date == null ? DBNull.Value : objtran_final_inspection.final_inspection_date);
                        
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_final_inspection.updated_by == null ? DBNull.Value : objtran_final_inspection.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_final_inspection.date_updated == null ? DBNull.Value : objtran_final_inspection.date_updated);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_final_inspection.note == null ? DBNull.Value : objtran_final_inspection.note);
                        Command.Parameters.AddWithValue("in_tran_final_inspection_details", NpgsqlDbType.Text, objtran_final_inspection.tran_final_inspection_details == null ? 
                            DBNull.Value : objtran_final_inspection.tran_final_inspection_details.ToString());


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
        public async Task<bool> SendForApproval(tran_final_inspection_DTO objtran_final_inspection)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_final_inspection_send_for_approval", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_final_inspection_id", NpgsqlDbType.Bigint, objtran_final_inspection.tran_final_inspection_id == null ? DBNull.Value : objtran_final_inspection.tran_final_inspection_id);
                        Command.Parameters.AddWithValue("in_final_inspection_date", NpgsqlDbType.Date, objtran_final_inspection.final_inspection_date == null ? DBNull.Value : objtran_final_inspection.final_inspection_date);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_final_inspection.note == null ? DBNull.Value : objtran_final_inspection.note);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_final_inspection.updated_by == null ? DBNull.Value : objtran_final_inspection.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_final_inspection.date_updated == null ? DBNull.Value : objtran_final_inspection.date_updated);
                        Command.Parameters.AddWithValue("in_tran_final_inspection_details", NpgsqlDbType.Text, objtran_final_inspection.tran_final_inspection_details == null ?
                            DBNull.Value : objtran_final_inspection.tran_final_inspection_details.ToString());


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

        public async Task<bool> ApproveInspection(tran_final_inspection_DTO objtran_final_inspection)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_final_inspection_approve_final_inspection", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_final_inspection_id", NpgsqlDbType.Bigint, objtran_final_inspection.tran_final_inspection_id == null ? DBNull.Value : objtran_final_inspection.tran_final_inspection_id);
                        Command.Parameters.AddWithValue("in_final_inspection_date", NpgsqlDbType.Date, objtran_final_inspection.final_inspection_date == null ? DBNull.Value : objtran_final_inspection.final_inspection_date);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_final_inspection.note == null ? DBNull.Value : objtran_final_inspection.note);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_final_inspection.updated_by == null ? DBNull.Value : objtran_final_inspection.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_final_inspection.date_updated == null ? DBNull.Value : objtran_final_inspection.date_updated);
                        Command.Parameters.AddWithValue("in_tran_final_inspection_details", NpgsqlDbType.Text, objtran_final_inspection.tran_final_inspection_details == null ?
                            DBNull.Value : objtran_final_inspection.tran_final_inspection_details.ToString());


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
        public async Task<List<tran_final_inspection_entity>> GetAllAsync()
        {
            List<tran_final_inspection_entity> list = new List<tran_final_inspection_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_final_inspection_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_final_inspection_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_final_inspection_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_final_inspection m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.final_inspection_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_final_inspection_entity>(query,
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



        public async Task<tran_final_inspection_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_final_inspection_by_id( @p_tran_final_inspection_id)";

                    var dataList = connection.Query<tran_final_inspection_DTO>(query,
                          new { p_tran_final_inspection_id = Id }
                         ).SingleOrDefault();


                    return dataList;

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

                    var objToDelete = new tran_final_inspection_entity { tran_final_inspection_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

       
        public async Task<List<rpc_tran_final_inspection_DTO>> GetAllJoined_TranFinalInspectionAsync(Int64 currnet_page, Int64 page_size, long fiscal_year_id, long event_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    //---------------------------------------------------------------------This Procedure is not founded in the database-----------------------------
                    string query = $"SELECT * FROM proc_sp_get_data_tran_final_inspection( @currnet_page,@page_size, @fiscal_year_id, @event_id)";

                    var dataList = connection.Query<rpc_tran_final_inspection_DTO>(query,
                          new
                          {
                              currnet_page = currnet_page,
                              page_size = page_size,
                              fiscal_year_id = fiscal_year_id,
                              event_id = event_id
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
        public async Task<List<rpc_proc_sp_get_production_quantity_for_final_inspection_DTO>> GetProductionQuantityForFinalInspection(Int64? p_techpack_id, String p_color_code)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_production_quantity_for_final_inspection( @p_techpack_id,@p_color_code)";

                    var dataList = connection.Query<rpc_proc_sp_get_production_quantity_for_final_inspection_DTO>(query,
                          new { p_techpack_id, p_color_code }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        [HttpPost]
        public async Task<List<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>> GetTranFinalInspectionDraftedData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_final_inspection_draft( @row_index,@page_size,@p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>(query,
                          new { row_index = row_index, page_size = page_size, p_fiscal_year_id = fiscal_year_id, p_event_id = event_id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        [HttpPost]
        public async Task<List<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>> GetTranFinalInspectionSubmittedData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_final_inspection_pending_approval( @row_index,@page_size,@p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>(query,
                          new { row_index = row_index, page_size = page_size, p_fiscal_year_id = fiscal_year_id, p_event_id = event_id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        [HttpPost]
        public async Task<List<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>> GetTranFinalInspectionApprovedData(Int64 row_index, Int64 page_size, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_final_inspection_approved( @row_index,@page_size,@p_fiscal_year_id,@p_event_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_final_inspection_draft_DTO>(query,
                          new { row_index = row_index, page_size = page_size, p_fiscal_year_id = fiscal_year_id, p_event_id = event_id }
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

