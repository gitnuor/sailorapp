
using AutoMapper;
using Dapper;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using static Dapper.SqlMapper;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class ProductionProcessDefineService : IProductionProcessDefineService
    {

        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;

        public ProductionProcessDefineService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<List<rpc_tran_production_process_DTO>> GetProductionProcess_PendingListAsync(Int64 row_index, Int64 page_size, Int64 tran_techpack_id, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_production_process_pendinglist( @row_index,@page_size,@query_type,@p_tran_techpack_id,@p_fiscal_year,@p_event_id,@p_delivery_unit_id)";

                    var dataList = connection.Query<rpc_tran_production_process_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
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

        public async Task<List<tran_production_process_define_DTO>> GetProductionProcess_DraftListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_production_process_define_list(@row_index,@page_size,@action_type,@p_fiscal_year_id,@p_event_id,@p_supplier_id)";

                    var parameters = new DynamicParameters();
                    parameters.Add("row_index", row_index);
                    parameters.Add("page_size", page_size);
                    parameters.Add("action_type", actiontype);
                    parameters.Add("p_fiscal_year_id", fiscal_year_id);
                    parameters.Add("p_event_id", event_id);
                    parameters.Add("p_supplier_id", supplier_id);


                    var result = await connection.QueryAsync<tran_production_process_define_DTO>(
                      sqlCommand,
                      parameters

                    );

                    return result.OrderBy(x => x.tran_production_process_id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<rpc_tran_production_process_DTO> GetTechPackInfoForProductionProcess(Int64 row_index, Int64 page_size, Int64 tran_techpack_id, string actionType, Int64 fiscal_year_id, Int64 event_id, long delivery_unit_id)
        {
            try
            {


                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_production_process_pendinglist ( @row_index,@page_size,@query_type,@p_tran_techpack_id,@p_fiscal_year,@p_event_id,@p_delivery_unit_id)";

                    var dataList = connection.Query<rpc_tran_production_process_DTO>(query,
                          new
                          {
                              row_index,
                              page_size,
                              query_type = actionType,
                              p_tran_techpack_id = tran_techpack_id,
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

        public async Task<rpc_tran_production_process_DTO> GetAll_production_techpack_wiseAsync(Int64? p_techpack_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_product_process_by_techpack( @p_techpack_id)";

                    var dataList = connection.Query<rpc_tran_production_process_DTO>(query,
                          new { p_techpack_id }
                         ).AsList().FirstOrDefault();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public async Task<rpc_tran_production_process_DTO> GetAll_production_techpack_wiseAsync(Int64? p_techpack_id, string in_color_code)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_product_process_by_techpack( @p_techpack_id,@in_color_code)";

                    var dataList = connection.Query<rpc_tran_production_process_DTO>(query,
                          new { p_techpack_id, in_color_code }
                         ).AsList().FirstOrDefault();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<bool> tran_production_process_define_insert_sp(tran_production_process_define_DTO objtran_production_process_define)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_production_process_define_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_production_process_date", NpgsqlDbType.Date, objtran_production_process_define.tran_production_process_date == null ? DBNull.Value : objtran_production_process_define.tran_production_process_date);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_production_process_define.techpack_id == null ? DBNull.Value : objtran_production_process_define.techpack_id);
                        Command.Parameters.AddWithValue("in_techpack_date", NpgsqlDbType.Bigint, objtran_production_process_define.techpack_date == null ? DBNull.Value : objtran_production_process_define.techpack_date);
                        Command.Parameters.AddWithValue("in_style_item_product_category", NpgsqlDbType.Text, objtran_production_process_define.style_item_product_category == null ? DBNull.Value : objtran_production_process_define.style_item_product_category);
                        Command.Parameters.AddWithValue("in_style_item_product_id", NpgsqlDbType.Bigint, objtran_production_process_define.style_item_product_id == null ? DBNull.Value : objtran_production_process_define.style_item_product_id);
                        Command.Parameters.AddWithValue("in_merchandiser_name", NpgsqlDbType.Text, objtran_production_process_define.merchandiser_name == null ? DBNull.Value : objtran_production_process_define.merchandiser_name);
                        Command.Parameters.AddWithValue("in_designer_name", NpgsqlDbType.Text, objtran_production_process_define.designer_name == null ? DBNull.Value : objtran_production_process_define.designer_name);
                        Command.Parameters.AddWithValue("in_style_qty", NpgsqlDbType.Numeric, objtran_production_process_define.style_qty == null ? DBNull.Value : objtran_production_process_define.style_qty);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_production_process_define.supplier_id == null ? DBNull.Value : objtran_production_process_define.supplier_id);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_production_process_define.fiscal_year_id == null ? DBNull.Value : objtran_production_process_define.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_production_process_define.event_id == null ? DBNull.Value : objtran_production_process_define.event_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_production_process_define.added_by == null ? DBNull.Value : objtran_production_process_define.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_production_process_define.updated_by == null ? DBNull.Value : objtran_production_process_define.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_production_process_define.date_added == null ? DBNull.Value : objtran_production_process_define.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_production_process_define.date_updated == null ? DBNull.Value : objtran_production_process_define.date_updated);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_production_process_define.is_submitted == null ? DBNull.Value : objtran_production_process_define.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_production_process_define.submitted_by == null ? DBNull.Value : objtran_production_process_define.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_production_process_define.is_approved == null ? DBNull.Value : objtran_production_process_define.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_production_process_define.approved_by == null ? DBNull.Value : objtran_production_process_define.approved_by);
                        Command.Parameters.AddWithValue("in_tran_production_process_details", NpgsqlDbType.Text, objtran_production_process_define.tran_production_process_details.ToString() == null ? DBNull.Value : objtran_production_process_define.tran_production_process_details.ToString());


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

        public async Task<tran_production_process_define_DTO_exc> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*,
       gsi.name     supplier_name,ttp.techpack_number,

       (select jsonb_agg(
                       jsonb_build_object(
                               'tran_production_process_details_id',tdprd.tran_production_process_details_id,
                               'tran_production_process_id', tdprd.tran_production_process_id,
                               'color_code', tdprd.color_code,
                               'color_qty', tdprd.color_qty,
                               'production_process_name', tdprd.production_process_name,
                               'production_process_uit_id', tdprd.production_process_uit_id,
                               'production_process_uit_name', tdprd.production_process_uit_name,
                               'remarks', tdprd.remarks

                       )
               )
        from tran_production_process_define_details tdprd where
        tdprd.tran_production_process_id=m.tran_production_process_id ) as production_process_details
FROM tran_production_process_define m
    INNER JOIN gen_supplier_information gsi
ON gsi.gen_supplier_information_id = m.supplier_id
INNER JOIN tran_tech_pack ttp ON
    ttp.tran_techpack_id=m.techpack_id

where m.tran_production_process_id=@Id";

                    var dataList = connection.Query<tran_production_process_define_DTO_exc>(query,
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

