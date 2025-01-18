using AutoMapper;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranFinishingReceiveService : ITranFinishingReceiveService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranFinishingReceiveService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<rpc_tran_finishing_receive_DTO>> GetnFinishingReceivePendingListAsync(Int64 row_index, Int64 page_size, Int64 finishing_receive_id, string actionType, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_emb_finishing_receive_pending_list( @row_index,@page_size,@query_type,@p_finishing_receive_id,@p_fiscal_year,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_finishing_receive_DTO>(query,
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


        public async Task<List<tran_finishing_receive_DTO>> GetFinishingReceive_DraftListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_finishing_receive_list(@row_index,@page_size,@action_type,@p_fiscal_year_id,@p_event_id)";

                    var parameters = new DynamicParameters();
                    parameters.Add("row_index", row_index);
                    parameters.Add("page_size", page_size);
                    parameters.Add("action_type", actiontype);
                    parameters.Add("p_fiscal_year_id", fiscal_year_id);
                    parameters.Add("p_event_id", event_id);



                    var result = await connection.QueryAsync<tran_finishing_receive_DTO>(
                      sqlCommand,
                      parameters

                    );

                    return result.OrderBy(x => x.tran_finish_receive_id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }





        public async Task<rpc_tran_finishing_receive_DTO> GetnFinishingReceiveListByIdAsync(Int64 row_index, Int64 page_size, Int64 finishing_receive_id, string actionType, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_emb_finishing_receive_pending_list( @row_index,@page_size,@query_type,@p_finishing_receive_id,@p_fiscal_year,@p_event_id)";

                    var dataList = connection.Query<rpc_tran_finishing_receive_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_finishing_receive_id = finishing_receive_id,
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

        public List<rpc_tran_finishing_receive_DTO> GetAllproc_sp_get_colors_by_sewing_output_Id(Int64? p_tech_pack_id)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_colors_by_sewingoutputId( @p_tech_pack_id)";

                    var dataList = connection.Query<rpc_tran_finishing_receive_DTO>(query,
                          new { p_tech_pack_id }
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

        public async Task<List<rpc_tran_finishing_receive_DTO>> GetAll_finishing_receive_techpack_wiseAsync(Int64? p_techpack_id, String p_color_code)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_finishing_receive_techpack_wiseAsync( @p_techpack_id,@p_color_code)";

                    var dataList = connection.Query<rpc_tran_finishing_receive_DTO>(query,
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

        public async Task<bool> tran_finishing_receive_insert_sp(tran_finishing_receive_DTO objtran_finishing_receive)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_finishing_receive_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_finish_receive_no", NpgsqlDbType.Text, objtran_finishing_receive.tran_finish_receive_no == null ? DBNull.Value : objtran_finishing_receive.tran_finish_receive_no);
                        Command.Parameters.AddWithValue("in_tran_finish_receive_date", NpgsqlDbType.Date, objtran_finishing_receive.tran_finish_receive_date == null ? DBNull.Value : objtran_finishing_receive.tran_finish_receive_date);
                        Command.Parameters.AddWithValue("in_tran_sewing_output_id", NpgsqlDbType.Bigint, objtran_finishing_receive.tran_sewing_output_id == null ? DBNull.Value : objtran_finishing_receive.tran_sewing_output_id);
                        Command.Parameters.AddWithValue("in_tran_sewing_allocation_plan_id", NpgsqlDbType.Bigint, objtran_finishing_receive.tran_sewing_allocation_plan_id == null ? DBNull.Value : objtran_finishing_receive.tran_sewing_allocation_plan_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_finishing_receive.techpack_id == null ? DBNull.Value : objtran_finishing_receive.techpack_id);
                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_finishing_receive.style_item_product_id == null ? DBNull.Value : objtran_finishing_receive.style_item_product_id);
                        Command.Parameters.AddWithValue("in_style_item_product_category", NpgsqlDbType.Text, objtran_finishing_receive.style_item_product_category == null ? DBNull.Value : objtran_finishing_receive.style_item_product_category);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_finishing_receive.fiscal_year_id == null ? DBNull.Value : objtran_finishing_receive.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_finishing_receive.event_id == null ? DBNull.Value : objtran_finishing_receive.event_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_finishing_receive.added_by == null ? DBNull.Value : objtran_finishing_receive.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_finishing_receive.updated_by == null ? DBNull.Value : objtran_finishing_receive.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_finishing_receive.date_added == null ? DBNull.Value : objtran_finishing_receive.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_finishing_receive.date_updated == null ? DBNull.Value : objtran_finishing_receive.date_updated);



                        Command.Parameters.AddWithValue("in_tran_finish_receive_details", NpgsqlDbType.Text, objtran_finishing_receive.tran_finish_receive_details.ToString() == null ? DBNull.Value : objtran_finishing_receive.tran_finish_receive_details.ToString());
                        Command.Parameters.AddWithValue("in_tran_finish_process_details", NpgsqlDbType.Text, objtran_finishing_receive.finishing_process_name.ToString() == null ? DBNull.Value : objtran_finishing_receive.finishing_process_name.ToString());

                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_finishing_receive.is_submitted == null ? DBNull.Value : objtran_finishing_receive.is_submitted);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_finishing_receive.is_approved == null ? DBNull.Value : objtran_finishing_receive.is_approved);

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

        public async Task<tran_finishing_receive_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*,
       ttp.techpack_number,

       (select jsonb_agg(
                       jsonb_build_object(
                               'tran_finish_receive_details_id',tdprd.tran_finish_receive_details_id,
                               'tran_finish_receive_id', tdprd.tran_finish_receive_id,
                               'tran_sewing_output_id', tdprd.tran_sewing_output_id,
                               'tran_sewing_allocation_plan_id', tdprd.tran_sewing_allocation_plan_id,
                               'style_product_size_group_detid', tdprd.style_product_size_group_detid,
                               'color_code', tdprd.color_code,
                               'color_quantity', tdprd.color_quantity,
                               'size_name',tdprd.size_name,
                               'qc_pass_quantity', tdprd.qc_pass_quantity,
                               'finish_receive_qty', tdprd.finish_receive_qty


                       )
               )
        from tran_finishing_receive_details tdprd where
        tdprd.tran_finish_receive_id=m.tran_finish_receive_id ) as finish_receive_details


FROM tran_finishing_receive m

INNER JOIN tran_tech_pack ttp ON
    ttp.tran_techpack_id=m.techpack_id

where m.tran_finish_receive_id=@Id";

                    var dataList = connection.Query<tran_finishing_receive_DTO>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

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

