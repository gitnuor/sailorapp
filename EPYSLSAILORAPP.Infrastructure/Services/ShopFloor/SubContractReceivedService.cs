
using AutoMapper;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class SubContractReceivedService : ISubContractReceivedService
    {

        private readonly IConfiguration _configuration;
       
        private readonly IMapper _mapper;

        public SubContractReceivedService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
          
        }

        public async Task<List<rpc_tran_sub_contract_received_DTO>> GetSubContractReceivedPendingListAsync(Int64 row_index, Int64 page_size, Int64 tran_techpack_id, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_sub_conract_received_pendinglist( @row_index,@page_size,@query_type,@p_tran_techpack_id,@p_fiscal_year,@p_event_id,@p_delivery_unit_id)";

                    var dataList = connection.Query<rpc_tran_sub_contract_received_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type= actionType,
                              p_tran_techpack_id = tran_techpack_id,                          
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



        public async Task<List<tran_sub_contract_received_DTO>> GetSubContractReceived_DraftListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_sub_contract_received_list(@row_index,@page_size,@action_type,@p_fiscal_year_id,@p_event_id,@p_supplier_id)";

                    var parameters = new DynamicParameters();
                    parameters.Add("row_index", row_index);
                    parameters.Add("page_size", page_size);
                    parameters.Add("action_type", actiontype);
                    parameters.Add("p_fiscal_year_id", fiscal_year_id);
                    parameters.Add("p_event_id", event_id);
                    parameters.Add("p_supplier_id", supplier_id);


                    var result = await connection.QueryAsync<tran_sub_contract_received_DTO>(
                      sqlCommand,
                      parameters

                    );

                    return result.OrderBy(x => x.tran_sub_contract_request_id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<rpc_tran_sub_contract_received_DTO> GetTechPackInfoForSubContractReceived(Int64 row_index, Int64 page_size, Int64 tran_production_process_id, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_sub_conract_received_pendinglist ( @row_index,@page_size,@query_type,@p_tran_techpack_id,@p_fiscal_year,@p_event_id,@p_delivery_unit_id)";

                    var dataList = connection.Query<rpc_tran_sub_contract_received_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_tran_techpack_id = tran_production_process_id,
                              p_fiscal_year = fiscal_year_id,
                              p_event_id = event_id,
                              p_delivery_unit_id = delivery_unit_id
                          }
                         ).AsList().FirstOrDefault();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_sub_contract_received_DTO>> GetAllSupplier(Int64 p_tran_sub_contract_request_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.supplier_id,s.name,s.factory_address,s.office_address,s.contact_person  FROM tran_sub_contract_request m
                                     inner join gen_supplier_information s on s.gen_supplier_information_id=m.supplier_id
                                      where m.tran_sub_contract_request_id=@p_tran_sub_contract_request_id";

                    var dataList = connection.Query<tran_supplier_info_ext>(query,
                        new { @p_tran_sub_contract_request_id = p_tran_sub_contract_request_id }).ToList();

                    return JsonConvert.DeserializeObject<List<rpc_tran_sub_contract_received_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

      
        public List<rpc_tran_sub_contract_received_DTO> GetAll_subcontract_color_Async(Int64? tran_sub_contract_request_id)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                   // string query = $"SELECT * FROM proc_sp_get_colors_by_sewingoutputId( @p_sewing_output_Id)";
                    string query = @"SELECT 	pd.color_code	FROM tran_sub_contract_request_details pd
			                         inner JOIN tran_sub_contract_request p ON
			                         pd.tran_sub_contract_request_id=p.tran_sub_contract_request_id
		                             where p.tran_sub_contract_request_id=@tran_sub_contract_request_id
		                             group by pd.color_code";

                    var dataList = connection.Query<rpc_tran_sub_contract_received_DTO>(query,
                          new { @tran_sub_contract_request_id = tran_sub_contract_request_id }
                         ).AsList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<rpc_tran_sub_contract_received_DTO>> GetAll_subcontract_techpack_color_wiseAsync(Int64? p_techpack_id, String p_color_code)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();


                    string query = $"SELECT * FROM proc_sp_get_data_subcontract_received_color_wiseasync( @p_techpack_id,@p_color_code)";

                    var dataList = connection.Query<rpc_tran_sub_contract_received_DTO>(query,
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
        public async Task<rpc_tran_production_process_DTO> GetAll_production_techpack_wiseAsync(Int64? p_techpack_id , string in_color_code)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_product_process_by_techpack( @p_techpack_id,@in_color_code)";

                    var dataList = connection.Query<rpc_tran_production_process_DTO>(query,
                          new { p_techpack_id , in_color_code }
                         ).AsList().FirstOrDefault();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<bool> tran_sub_contract_received_insert_sp(tran_sub_contract_received_DTO objtran_sub_contract_received)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_sub_contract_received_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_sub_contract_received_date", NpgsqlDbType.Date, objtran_sub_contract_received.tran_sub_contract_received_date == null ? DBNull.Value : objtran_sub_contract_received.tran_sub_contract_received_date);
                        Command.Parameters.AddWithValue("in_tran_sub_contract_request_id", NpgsqlDbType.Bigint, objtran_sub_contract_received.tran_sub_contract_request_id == null ? DBNull.Value : objtran_sub_contract_received.tran_sub_contract_request_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_sub_contract_received.techpack_id == null ? DBNull.Value : objtran_sub_contract_received.techpack_id);
                        Command.Parameters.AddWithValue("in_techpack_date", NpgsqlDbType.Date, objtran_sub_contract_received.techpack_date == null ? DBNull.Value : objtran_sub_contract_received.techpack_date);
                        Command.Parameters.AddWithValue("in_style_item_product_category", NpgsqlDbType.Text, objtran_sub_contract_received.style_item_product_category == null ? DBNull.Value : objtran_sub_contract_received.style_item_product_category);
                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_sub_contract_received.style_item_product_id == null ? DBNull.Value : objtran_sub_contract_received.style_item_product_id);
                        Command.Parameters.AddWithValue("in_merchandiser_name", NpgsqlDbType.Text, objtran_sub_contract_received.merchandiser_name == null ? DBNull.Value : objtran_sub_contract_received.merchandiser_name);
                        Command.Parameters.AddWithValue("in_designer_name", NpgsqlDbType.Text, objtran_sub_contract_received.designer_name == null ? DBNull.Value : objtran_sub_contract_received.designer_name);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_sub_contract_received.supplier_id == null ? DBNull.Value : objtran_sub_contract_received.supplier_id);
                        Command.Parameters.AddWithValue("in_supplier_concern_person", NpgsqlDbType.Text, objtran_sub_contract_received.supplier_concern_person == null ? DBNull.Value : objtran_sub_contract_received.supplier_concern_person);
                        Command.Parameters.AddWithValue("in_supplier_address", NpgsqlDbType.Text, objtran_sub_contract_received.supplier_address == null ? DBNull.Value : objtran_sub_contract_received.supplier_address);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_sub_contract_received.fiscal_year_id == null ? DBNull.Value : objtran_sub_contract_received.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_sub_contract_received.event_id == null ? DBNull.Value : objtran_sub_contract_received.event_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_sub_contract_received.added_by == null ? DBNull.Value : objtran_sub_contract_received.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_sub_contract_received.updated_by == null ? DBNull.Value : objtran_sub_contract_received.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_sub_contract_received.date_added == null ? DBNull.Value : objtran_sub_contract_received.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_sub_contract_received.date_updated == null ? DBNull.Value : objtran_sub_contract_received.date_updated);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_sub_contract_received.is_submitted == null ? DBNull.Value : objtran_sub_contract_received.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_sub_contract_received.submitted_by == null ? DBNull.Value : objtran_sub_contract_received.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_sub_contract_received.is_approved == null ? DBNull.Value : objtran_sub_contract_received.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_sub_contract_received.approved_by == null ? DBNull.Value : objtran_sub_contract_received.approved_by);
                        Command.Parameters.AddWithValue("in_tran_sub_contract_received_details", NpgsqlDbType.Text, objtran_sub_contract_received.tran_sub_contract_received_details.ToString() == null ? DBNull.Value : objtran_sub_contract_received.tran_sub_contract_received_details.ToString());


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


        public async Task<tran_sub_contract_received_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*,
       gsi.name                                                                       supplier_name,
       ttp.techpack_number,

       (select jsonb_agg(
                       jsonb_build_object(
                               'tran_sub_contract_received_details_id', tdprd.tran_sub_contract_received_details_id,
                               'tran_sub_contract_received_id', tdprd.tran_sub_contract_received_id,
                               'color_code', tdprd.color_code,
                               'color_qty', tdprd.color_qty,
                               'production_process_name', tdprd.production_process_name,
                               'sub_contract_challan_qty', tdprd.sub_contract_challan_qty,
                               'sub_contract_receive_qty', tdprd.sub_contract_receive_qty,
                               'bal_qty', tdprd.bal_qty,
                               'remarks', tdprd.remarks
                       )
               )
        from tran_sub_contract_received_details tdprd
        where tdprd.tran_sub_contract_received_id = m.tran_sub_contract_received_id) as sub_contract_received_details
FROM tran_sub_contract_received m
         INNER JOIN gen_supplier_information gsi
                    ON gsi.gen_supplier_information_id = m.supplier_id
         INNER JOIN tran_tech_pack ttp ON
    ttp.tran_techpack_id = m.techpack_id

where m.tran_sub_contract_received_id = @Id";

                    var dataList = connection.Query<tran_sub_contract_received_DTO>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> ApprovalProposedAsync(tran_sub_contract_received_DTO request)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {

                    await connection.OpenAsync();

                    string sqlCommand = @"UPDATE tran_sub_contract_received
                                          SET is_submitted = 2
                                          WHERE tran_sub_contract_received_id = @p_tran_sub_contract_receive_id;";

                    var parameters = new
                    {
                        p_tran_sub_contract_receive_id = request.tran_sub_contract_received_id

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

        public async Task<bool> ApprovedAsync(tran_sub_contract_received_DTO request)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = @"UPDATE tran_sub_contract_received  
                                          SET is_approved = 1                                        
                                          WHERE tran_sub_contract_received_id = @p_tran_sub_contract_receive_id;";

                    var parameters = new
                    {
                        p_tran_sub_contract_receive_id = request.tran_sub_contract_received_id,

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


    }

}

