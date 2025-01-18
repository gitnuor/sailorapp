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
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranFinishingProductionProcessService : IFinishingProductionProcessService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranFinishingProductionProcessService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<rpc_tran_finishing_production_DTO>> GetnFinishingProductionPendingListAsync(Int64 row_index, Int64 page_size, Int64 finishing_receive_id, string actionType, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_finishing_production_pending_list( @row_index,@page_size,@query_type,@p_finishing_receive_id,@p_fiscal_year,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_finishing_production_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_finishing_receive_id = finishing_receive_id,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id
                              
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

        
        public async Task<List<rpc_tran_finishing_production_DTO>> GetnFinishingProductionPendingListByTechpackAsync(Int64 row_index, Int64 page_size, Int64 techpackId, Int64 activeTag, string actionType, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_finish_product_pending_list_by_teckpack( @row_index,@page_size,@query_type,@p_techpack_id,@p_active_tag,@p_fiscal_year,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_finishing_production_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,               
                              p_techpack_id = techpackId,
                              p_active_tag = activeTag,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                             

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

        public async Task<tran_finishing_production_process_DTO> GetnFinishingProductionReceiveListByTechpacAsync(Int64 row_index, Int64 page_size, Int64 techpackId, string actionType, Int64 fiscal_year_id, Int64 event_id, Int64 gen_finishing_process_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_finish_product_second_list_by_teckpack( @row_index,@page_size,@query_type,@p_techpack_id,@p_fiscal_year,@p_event_id , @p_gen_finishing_process_id)";

                    var dataList = connection.Query<tran_finishing_production_process_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_techpack_id = techpackId,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_gen_finishing_process_id= gen_finishing_process_id


                          }
                        ).FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public async Task<rpc_tran_finishing_production_DTO> GetnFinishingProductionListByIdAsync(Int64 row_index, Int64 page_size, Int64 finishing_techpack_id, string actionType, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_finishing_production_by_techpackId_list( @row_index,@page_size,@query_type,@p_finishing_receive_id,@p_fiscal_year,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_finishing_production_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_finishing_receive_id = finishing_techpack_id,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id

                          }
                         ).FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async  Task<List<tran_finishing_production_process_DTO>> GetnFinishingProductionListView(Int64 techpack_id , string active_tag)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_finishing_production_process_View(@p_techpack_id , @p_active_tag)";

                    var dataList = connection.Query<tran_finishing_production_process_DTO>(query,
                          new
                          {
                              // query_type = actionType,
                              p_techpack_id = techpack_id,
                              p_active_tag = active_tag
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

        public async Task<List<tran_finishing_production_detail_process_DTO>> GetDetaiilsProductionIdWise(Int64 finishing_production_process_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_finis_product_detail_by_finish_proces_Id(@p_finishing_production_process_details_id)";

                    var dataList = connection.Query<tran_finishing_production_detail_process_DTO>(query,
                          new
                          {
                              // query_type = actionType,
                              p_finishing_production_process_details_id = finishing_production_process_id
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

        public List<rpc_tran_finishing_production_DTO> GetAllproc_sp_get_colors_by_techpack_Id(Int64? p_techpack_Id)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_colors_by_product_finish_techpack_Id( @p_techpack_Id)";

                    var dataList = connection.Query<rpc_tran_finishing_production_DTO>(query,
                          new { p_techpack_Id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public List<rpc_proc_finishing_process_type_DTO> GetAllFinishingProcessType()
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_all_FinishingProcessType()";

                    var dataList = connection.Query<rpc_proc_finishing_process_type_DTO>(query).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_finishing_production_DTO>> GetAll_finishing_production_techpack_color_wiseAsync(Int64? p_techpack_id, String p_color_code, Int64? P_finishingProcessId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_finishing_production_techpack_color_wiseAsync( @p_techpack_id,@p_color_code ,@P_finishingProcessId)";

                    var dataList = connection.Query<rpc_tran_finishing_production_DTO>(query,
                          new { p_techpack_id, p_color_code , P_finishingProcessId }
                         ).AsList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<rpc_tran_finishing_receive_DTO> GetnFinishingProductionProcessTab(Int64 finishing_techpack_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_finishing_production_process_type(@p_techpack_id)";

                    var dataList = connection.Query<rpc_tran_finishing_receive_DTO>(query,
                          new
                          {
                              // query_type = actionType,
                              p_techpack_id = finishing_techpack_id
                          }
                         ).FirstOrDefault();

                    return dataList;
                    //return JsonConvert.DeserializeObject<rpc_tran_finishing_receive_DTO>(JsonConvert.SerializeObject(dataList));

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<bool> tran_finishing_production_process_insert_sp(tran_finishing_production_process_DTO objtran_finishing_production_process)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_finishing_production_process_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_finish_receive_date", NpgsqlDbType.Date, objtran_finishing_production_process.tran_finish_receive_date == null ? DBNull.Value : objtran_finishing_production_process.tran_finish_receive_date);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_finishing_production_process.techpack_id == null ? DBNull.Value : objtran_finishing_production_process.techpack_id);
                        Command.Parameters.AddWithValue("in_style_item_product_category", NpgsqlDbType.Text, objtran_finishing_production_process.style_item_product_category == null ? DBNull.Value : objtran_finishing_production_process.style_item_product_category);
                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_finishing_production_process.style_item_product_id == null ? DBNull.Value : objtran_finishing_production_process.style_item_product_id);
                        Command.Parameters.AddWithValue("in_gen_finishing_process_id", NpgsqlDbType.Bigint, objtran_finishing_production_process.gen_finishing_process_id == null ? DBNull.Value : objtran_finishing_production_process.gen_finishing_process_id);
                        Command.Parameters.AddWithValue("in_finishing_process_name", NpgsqlDbType.Text, objtran_finishing_production_process.finishing_process_name == null ? DBNull.Value : objtran_finishing_production_process.finishing_process_name);
                        Command.Parameters.AddWithValue("in_is_iron", NpgsqlDbType.Bigint, objtran_finishing_production_process.is_iron == null ? DBNull.Value : objtran_finishing_production_process.is_iron);
                        Command.Parameters.AddWithValue("in_is_hang_tag", NpgsqlDbType.Bigint, objtran_finishing_production_process.is_hang_tag == null ? DBNull.Value : objtran_finishing_production_process.is_hang_tag);
                        Command.Parameters.AddWithValue("in_is_folding", NpgsqlDbType.Bigint, objtran_finishing_production_process.is_folding == null ? DBNull.Value : objtran_finishing_production_process.is_folding);
                        Command.Parameters.AddWithValue("in_is_poly", NpgsqlDbType.Bigint, objtran_finishing_production_process.is_poly == null ? DBNull.Value : objtran_finishing_production_process.is_poly);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_finishing_production_process.fiscal_year_id == null ? DBNull.Value : objtran_finishing_production_process.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_finishing_production_process.event_id == null ? DBNull.Value : objtran_finishing_production_process.event_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_finishing_production_process.added_by == null ? DBNull.Value : objtran_finishing_production_process.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_finishing_production_process.updated_by == null ? DBNull.Value : objtran_finishing_production_process.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_finishing_production_process.date_added == null ? DBNull.Value : objtran_finishing_production_process.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_finishing_production_process.date_updated == null ? DBNull.Value : objtran_finishing_production_process.date_updated);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_finishing_production_process.is_submitted == null ? DBNull.Value : objtran_finishing_production_process.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_finishing_production_process.submitted_by == null ? DBNull.Value : objtran_finishing_production_process.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_finishing_production_process.is_approved == null ? DBNull.Value : objtran_finishing_production_process.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_finishing_production_process.approved_by == null ? DBNull.Value : objtran_finishing_production_process.approved_by);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_finishing_production_process.note == null ? DBNull.Value : objtran_finishing_production_process.note);
                        Command.Parameters.AddWithValue("in_tran_finishing_production_process_details", NpgsqlDbType.Text, objtran_finishing_production_process.tran_finishing_production_process_details.ToString() == null ? DBNull.Value : objtran_finishing_production_process.tran_finishing_production_process_details.ToString());
                       // Command.Parameters.AddWithValue("in_tran_production_process_date", NpgsqlDbType.Date, Convert.ToDateTime(objtran_finishing_production_process.production_process_date) == null ? DBNull.Value : Convert.ToDateTime(objtran_finishing_production_process.production_process_date));
                       

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

        
        public List<rpc_tran_finishing_production_DTO> GetAllTechpackList()
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_all_techpack_for_production_process()";

                    var dataList = connection.Query<rpc_tran_finishing_production_DTO>(query).AsList();

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

