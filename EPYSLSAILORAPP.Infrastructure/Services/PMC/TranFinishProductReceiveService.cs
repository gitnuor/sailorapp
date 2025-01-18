using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.Tran_DesignMgt;
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

    public class TranFinishProductReceiveService : ITranFinishProductReceiveService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranFinishProductReceiveService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }



        public async Task<bool> UpdateAsync(tran_finish_product_receive_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_finish_product_receive_DTO>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_finish_product_receive_DTO>> GetAllAsync()
        {
            List<tran_finish_product_receive_DTO> list = new List<tran_finish_product_receive_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_finish_product_receive_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_finish_product_receive_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_finish_product_receive_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.tran_finish_product_receive_id,

                                            m.finish_product_receive_no,
                                            m.finish_product_receive_date,

                                            m.vehicle_number,
                                            m.driver_name
                                              FROM tran_finish_product_receive m
                                           where m.fiscal_year_id=@p_fiscal_year_id and m.event_id=@p_event_id and
                                                     case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.finish_product_receive_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_finish_product_receive_DTO>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length,
                            p_fiscal_year_id=param.fiscal_year_id,
                            p_event_id=param.event_id
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        public async Task<tran_finish_product_receive_DTO> GetSingleAsync(Int64 p_tran_finish_product_receive_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                   // string query = $"SELECT * FROM proc_sp_get_data_tran_finish_product_receive_by_id( @p_tran_finish_product_receive_id)";

                    string query = @"SELECT m.*,
       gsi.packing_list_no,
       ttp.transport_type,

       (select jsonb_agg(
                       jsonb_build_object(
                               'tran_finish_product_receive_details_id', tdprd.tran_finish_product_receive_details_id,
                               'tran_finish_product_receive_id', tdprd.tran_finish_product_receive_id,
                               'techpack_id', tdprd.techpack_id,
                               'style_code', tdprd.style_code,
                               'color_code', tdprd.color_code,
                               'style_product_size_group_detid', tdprd.style_product_size_group_detid,
                               'style_product_size', tdprd.style_product_size,
                               'barcode', tdprd.barcode,
                               'style_product_unit_id', tdprd.style_product_unit_id,
                               'mrp', tdprd.mrp,
                               'order_quantity', tdprd.order_quantity,
                               'packing_quantity', tdprd.packing_quantity,
                               'receive_quantity', tdprd.receive_quantity,
                               'reject_quantity', tdprd.reject_quantity,
                               'note', tdprd.note,
                               'total_mrp_value', tdprd.total_mrp_value,
                               'style_product_unit', tdprd.style_product_unit,
                               'is_distributed', tdprd.is_distributed,
                               'techpack_number', ttp.techpack_number
                       )
               )
        from tran_finish_product_receive_details tdprd
        inner join tran_tech_pack ttp
                                    on tdprd.techpack_id = ttp.tran_techpack_id
        where tdprd.tran_finish_product_receive_id = m.tran_finish_product_receive_id) as finish_details
FROM tran_finish_product_receive m
         INNER JOIN tran_packing_list gsi
                    ON gsi.tran_packing_list_id = m.tran_packing_list_id
         INNER JOIN gen_tran_transport ttp ON
    ttp.transport_id = m.transport_id

where m.tran_finish_product_receive_id =@Id";

                    var dataList = connection.Query<tran_finish_product_receive_DTO>(query,
                          new { @Id = p_tran_finish_product_receive_id }
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

                    var objToDelete = new tran_finish_product_receive_DTO { tran_finish_product_receive_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> SaveAsync(tran_finish_product_receive_DTO objtran_finish_product_receive)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_finish_product_receive_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_packing_list_id", NpgsqlDbType.Bigint, objtran_finish_product_receive.tran_packing_list_id == null ? DBNull.Value : objtran_finish_product_receive.tran_packing_list_id);
                        Command.Parameters.AddWithValue("in_finish_product_receive_date", NpgsqlDbType.Date, objtran_finish_product_receive.finish_product_receive_date == null ? DBNull.Value : objtran_finish_product_receive.finish_product_receive_date);
                        Command.Parameters.AddWithValue("in_transport_id", NpgsqlDbType.Bigint, objtran_finish_product_receive.transport_id == null ? DBNull.Value : objtran_finish_product_receive.transport_id);
                        Command.Parameters.AddWithValue("in_vehicle_number", NpgsqlDbType.Text, objtran_finish_product_receive.vehicle_number == null ? DBNull.Value : objtran_finish_product_receive.vehicle_number);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_finish_product_receive.driver_name == null ? DBNull.Value : objtran_finish_product_receive.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact_no", NpgsqlDbType.Text, objtran_finish_product_receive.driver_contact_no == null ? DBNull.Value : objtran_finish_product_receive.driver_contact_no);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_finish_product_receive.note == null ? DBNull.Value : objtran_finish_product_receive.note);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_finish_product_receive.added_by == null ? DBNull.Value : objtran_finish_product_receive.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_finish_product_receive.date_added == null ? DBNull.Value : objtran_finish_product_receive.date_added);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_finish_product_receive.fiscal_year_id == null ? DBNull.Value : objtran_finish_product_receive.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_finish_product_receive.event_id == null ? DBNull.Value : objtran_finish_product_receive.event_id);
                        Command.Parameters.AddWithValue("in_tran_finish_product_receive_details_json", NpgsqlDbType.Text, objtran_finish_product_receive.tran_finish_product_receive_details_json == null ? DBNull.Value : objtran_finish_product_receive.tran_finish_product_receive_details_json.ToString());


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
        public async Task<bool> tran_finish_product_receive_update_sp(tran_finish_product_receive_DTO objtran_finish_product_receive)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_finish_product_receive_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_packing_list_id", NpgsqlDbType.Bigint, objtran_finish_product_receive.tran_packing_list_id == null ? DBNull.Value : objtran_finish_product_receive.tran_packing_list_id);
                        Command.Parameters.AddWithValue("in_finish_product_receive_no", NpgsqlDbType.Text, objtran_finish_product_receive.finish_product_receive_no == null ? DBNull.Value : objtran_finish_product_receive.finish_product_receive_no);
                        Command.Parameters.AddWithValue("in_finish_product_receive_date", NpgsqlDbType.Date, objtran_finish_product_receive.finish_product_receive_date == null ? DBNull.Value : objtran_finish_product_receive.finish_product_receive_date);
                        Command.Parameters.AddWithValue("in_transport_id", NpgsqlDbType.Text, objtran_finish_product_receive.transport_id == null ? DBNull.Value : objtran_finish_product_receive.transport_id);
                        Command.Parameters.AddWithValue("in_vehicle_number", NpgsqlDbType.Text, objtran_finish_product_receive.vehicle_number == null ? DBNull.Value : objtran_finish_product_receive.vehicle_number);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_finish_product_receive.driver_name == null ? DBNull.Value : objtran_finish_product_receive.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact_no", NpgsqlDbType.Text, objtran_finish_product_receive.driver_contact_no == null ? DBNull.Value : objtran_finish_product_receive.driver_contact_no);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_finish_product_receive.note == null ? DBNull.Value : objtran_finish_product_receive.note);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_finish_product_receive.added_by == null ? DBNull.Value : objtran_finish_product_receive.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_finish_product_receive.updated_by == null ? DBNull.Value : objtran_finish_product_receive.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_finish_product_receive.date_added == null ? DBNull.Value : objtran_finish_product_receive.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_finish_product_receive.date_updated == null ? DBNull.Value : objtran_finish_product_receive.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_finish_product_receive.fiscal_year_id == null ? DBNull.Value : objtran_finish_product_receive.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_finish_product_receive.event_id == null ? DBNull.Value : objtran_finish_product_receive.event_id);
                        Command.Parameters.AddWithValue("in_tran_finish_product_receive_details_json", NpgsqlDbType.Text, objtran_finish_product_receive.tran_finish_product_receive_details_json == null ? DBNull.Value : objtran_finish_product_receive.tran_finish_product_receive_details_json);


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
        public async Task<List<rpc_tran_finish_product_receive_DTO>> GetAllJoined_TranFinishProductReceiveAsync(Int64 currnet_page, Int64 page_size, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_finish_product_receive( @currnet_page,@page_size,@p_fiscal_year_id, @p_event_id)";

                    var dataList = connection.Query<rpc_tran_finish_product_receive_DTO>(query,
                          new
                          {
                              currnet_page = currnet_page,
                              page_size = page_size,
                              p_fiscal_year_id=fiscal_year_id,
                              p_event_id= event_id

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

