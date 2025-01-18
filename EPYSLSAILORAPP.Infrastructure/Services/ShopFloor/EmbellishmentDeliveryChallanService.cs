
using AutoMapper;
using Dapper;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class EmbellishmentDeliveryChallanService : IEmbellishmentDeliveryChallanService
    {

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public EmbellishmentDeliveryChallanService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<List<tran_service_work_order_DTO>> GetDeliveryChalan_PendingListAsync(Int64 row_index, Int64 page_size, Int64 workOrderId, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_emb_delivery_challan_pendinglist( @row_index,@page_size,@query_type,@p_workorder_id,@p_fiscal_year,@p_event_id,@p_delivery_unit_id)";

                    var dataList = connection.Query<tran_service_work_order_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_workorder_id = workOrderId,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_delivery_unit_id = delivery_unit_id
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

        public async Task<List<tran_embellish_delivery_challan_DTO>> GetTranDeliveryChallanListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_emb_delivery_challan_List(@row_index,@page_size,@action_type,@p_fiscal_year_id,@p_event_id,@p_supplier_id)";

                    var parameters = new DynamicParameters();
                    parameters.Add("row_index", row_index);
                    parameters.Add("page_size", page_size);
                    parameters.Add("action_type", actiontype);
                    parameters.Add("p_fiscal_year_id", fiscal_year_id);
                    parameters.Add("p_event_id", event_id);
                    parameters.Add("p_supplier_id", supplier_id);

                    // int timeoutInSeconds = 300;
                    var result = await connection.QueryAsync<tran_embellish_delivery_challan_DTO>(
                      sqlCommand,
                      parameters
                    // commandTimeout: timeoutInSeconds
                    );

                    return result.OrderBy(x => x.embellish_delivery_challan_id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }





        public async Task<List<tran_cutting_batch_wise_DTO>> GetAll_batch_workOrder_wiseAsync(Int64? p_techpack_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_batch_workOrder_wiseAsync( @p_techpack_id)";

                    var dataList = connection.Query<tran_cutting_batch_wise_DTO>(query,
                          new { p_techpack_id }
                         ).AsList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<tran_bundle_DTO>> GetAll_bundle_batch_wiseAsync(Int64? p_batch_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_bundle_batch_wise_async( @p_batch_id)";

                    var dataList = connection.Query<tran_bundle_DTO>(query,
                          new { p_batch_id }
                         ).AsList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<tran_bundle_DTO>> GetAll_bundle_batch_wiseAsync(Int64? p_batch_id, String p_color_code, Int64? p_gen_garment_part)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_bundle_batch_wise_receive_async( @p_batch_id,@p_color_code,@p_gen_garment_part)";

                    var dataList = connection.Query<tran_bundle_DTO>(query,
                          new { p_batch_id, p_color_code, p_gen_garment_part }
                         ).AsList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<bool> tran_embellish_delivery_challan_insert_sp(tran_embellish_delivery_challan_DTO objEmb)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_embellish_delivery_challan_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_embellish_delivery_challan_no", NpgsqlDbType.Text, objEmb.embellish_delivery_challan_no == null ? DBNull.Value : objEmb.embellish_delivery_challan_no);
                        Command.Parameters.AddWithValue("in_embellish_delivery_challan_date", NpgsqlDbType.Date, objEmb.embellish_delivery_challan_date == null ? DBNull.Value : objEmb.embellish_delivery_challan_date);
                        Command.Parameters.AddWithValue("in_service_work_order_id", NpgsqlDbType.Bigint, objEmb.service_work_order_id == null ? DBNull.Value : objEmb.service_work_order_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objEmb.techpack_id == null ? DBNull.Value : objEmb.techpack_id);
                        Command.Parameters.AddWithValue("in_gen_unit_id", NpgsqlDbType.Bigint, objEmb.gen_unit_id == null ? DBNull.Value : objEmb.gen_unit_id);
                        Command.Parameters.AddWithValue("in_unit_address", NpgsqlDbType.Text, objEmb.unit_address == null ? DBNull.Value : objEmb.unit_address);
                        Command.Parameters.AddWithValue("in_unit_name", NpgsqlDbType.Text, objEmb.unit_name == null ? DBNull.Value : objEmb.unit_name);

                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objEmb.is_submitted == null ? DBNull.Value : objEmb.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objEmb.submitted_by == null ? DBNull.Value : objEmb.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objEmb.is_approved == null ? DBNull.Value : objEmb.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objEmb.approved_by == null ? DBNull.Value : objEmb.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objEmb.approve_date == null ? DBNull.Value : objEmb.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objEmb.approve_remarks == null ? DBNull.Value : objEmb.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objEmb.added_by == null ? DBNull.Value : objEmb.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objEmb.date_added == null ? DBNull.Value : objEmb.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objEmb.updated_by == null ? DBNull.Value : objEmb.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objEmb.date_updated == null ? DBNull.Value : objEmb.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objEmb.fiscal_year_id == null ? DBNull.Value : objEmb.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objEmb.event_id == null ? DBNull.Value : objEmb.event_id);

                        Command.Parameters.AddWithValue("in_embellish_delivery_challan_detail_list", NpgsqlDbType.Text, objEmb.embellish_delivery_challan_detail_list.ToString() == null ? DBNull.Value : objEmb.embellish_delivery_challan_detail_list.ToString());

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


        public async Task<bool> ApprovalProposedAsync(tran_embellish_delivery_challan_DTO request)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {

                    await connection.OpenAsync();
                    //string sqlCommand = "SELECT public.proc_sp_proposed_for_approval_emb_delivery_challan(@p_emb_del_chalan_id, @p_is_submitted)";

                    string sqlCommand = @"UPDATE tran_embellish_delivery_challan
                                         SET is_submitted = 2
                                         WHERE embellish_delivery_challan_id = @p_emb_del_chalan_id;";

                    var parameters = new
                    {
                        p_emb_del_chalan_id = request.embellish_delivery_challan_id,
                        //p_is_submitted = 2
                    };

                    int rowsAffected = await connection.ExecuteAsync(sqlCommand, parameters);

                    if (rowsAffected == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ApprovedAsync(tran_embellish_delivery_challan_DTO request)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT public.proc_sp_approved_for_emb_delivery_challan(@p_emb_del_chalan_id, @p_is_approved)";

                    var parameters = new
                    {
                        p_emb_del_chalan_id = request.embellish_delivery_challan_id,
                        p_is_approved = 1
                    };

                    int rowsAffected = await connection.ExecuteAsync(sqlCommand, parameters);

                    if (rowsAffected < 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<tran_embellish_delivery_challan_DTO> Get_master_detail_tran_emb_delivery_challan_Async(Int64 embellish_delivery_challan_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_embellish_delivery_challan_details( @p_embellish_delivery_challan_id)";

                    var dataList = connection.Query<tran_embellish_delivery_challan_DTO>(query,
                          new
                          {

                              p_embellish_delivery_challan_id = embellish_delivery_challan_id
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

        public async Task<List<tran_service_work_order_DTO>> GetnDeliveryChallanReceivePendingAsync(Int64 row_index, Int64 page_size, Int64 workOrderId, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_emb_delivery_challan_receive_pending_list( @row_index,@page_size,@query_type,@p_workorder_id,@p_fiscal_year,@p_event_id,@p_delivery_unit_id)";

                    var dataList = connection.Query<tran_service_work_order_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_workorder_id = workOrderId,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_delivery_unit_id = delivery_unit_id
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

        public async Task<tran_service_work_order_DTO> GetnDeliveryChallanReceiveAddAsync(Int64 row_index, Int64 page_size, Int64 workOrderId, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_emb_delivery_challan_receive_pending_list( @row_index,@page_size,@query_type,@p_workorder_id,@p_fiscal_year,@p_event_id,@p_delivery_unit_id)";

                    var dataList = connection.Query<tran_service_work_order_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_workorder_id = workOrderId,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_delivery_unit_id = delivery_unit_id
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

        public async Task<List<tran_emb_part_dropdown>> GetAllproc_sp_get_color_wise_part_async(Int64? p_work_order_id, String p_color_code)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_color_wise_part( @p_work_order_id,@p_color_code)";

                    var dataList = connection.Query<tran_emb_part_dropdown>(query,
                          new { p_work_order_id, p_color_code }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_emb_part_dropdown>> GetAllproc_sp_get_part_wise_batch_async(Int64? p_work_order_id, String p_color_code, Int64? p_gen_garment_part)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_delivery_chalan_receive_get_part_wise_batch( @p_work_order_id,@p_color_code,@p_gen_garment_part)";

                    var dataList = connection.Query<tran_emb_part_dropdown>(query,
                          new { p_work_order_id, p_color_code, p_gen_garment_part }
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

