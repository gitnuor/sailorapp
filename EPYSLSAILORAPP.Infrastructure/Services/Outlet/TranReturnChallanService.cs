using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranReturnChallanService : ITranReturnChallanService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranReturnChallanService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_return_challan_DTO objtran_return_challan)
        {

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_return_challan_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_outlet_challan_id", NpgsqlDbType.Bigint, objtran_return_challan.tran_outlet_challan_id == null ? DBNull.Value : objtran_return_challan.tran_outlet_challan_id);
                        Command.Parameters.AddWithValue("in_tran_outlet_challan_receive_id", NpgsqlDbType.Bigint, objtran_return_challan.tran_outlet_challan_receive_id == null ? DBNull.Value : objtran_return_challan.tran_outlet_challan_receive_id);
                        Command.Parameters.AddWithValue("in_return_date", NpgsqlDbType.Date, objtran_return_challan.return_date == null ? DBNull.Value : objtran_return_challan.return_date);
                        Command.Parameters.AddWithValue("in_transport_id", NpgsqlDbType.Bigint, objtran_return_challan.transport_id == null ? DBNull.Value : objtran_return_challan.transport_id);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_return_challan.added_by == null ? DBNull.Value : objtran_return_challan.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_return_challan.date_added == null ? DBNull.Value : objtran_return_challan.date_added);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_return_challan.fiscal_year_id == null ? DBNull.Value : objtran_return_challan.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_return_challan.event_id == null ? DBNull.Value : objtran_return_challan.event_id);
                        Command.Parameters.AddWithValue("in_return_to", NpgsqlDbType.Bigint, objtran_return_challan.return_to == null ? DBNull.Value : objtran_return_challan.return_to);
                        Command.Parameters.AddWithValue("in_return_from", NpgsqlDbType.Bigint, objtran_return_challan.return_from == null ? DBNull.Value : objtran_return_challan.return_from);
                        Command.Parameters.AddWithValue("in_vehicle_number", NpgsqlDbType.Text, objtran_return_challan.vehicle_number == null ? DBNull.Value : objtran_return_challan.vehicle_number);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_return_challan.driver_name == null ? DBNull.Value : objtran_return_challan.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact_no", NpgsqlDbType.Text, objtran_return_challan.driver_contact_no == null ? DBNull.Value : objtran_return_challan.driver_contact_no);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_return_challan.note == null ? DBNull.Value : objtran_return_challan.note);
                        Command.Parameters.AddWithValue("in_tran_return_challan_details_json", NpgsqlDbType.Text, objtran_return_challan.tran_return_challan_details_json == null ? DBNull.Value : objtran_return_challan.tran_return_challan_details_json.ToString());


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

        public async Task<bool> UpdateAsync(tran_return_challan_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_return_challan_DTO>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_return_challan_DTO>> GetAllAsync()
        {
            List<tran_return_challan_DTO> list = new List<tran_return_challan_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_return_challan_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_return_challan_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_return_challan_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.return_no,
                                                   m.tran_return_challan_id,
                                                   m.return_date,
                                                   m.driver_name,
                                                   ts.transport_type AS transport_type_name,
                                                   m.driver_contact_no
                                           FROM tran_return_challan m
                                     INNER JOIN gen_tran_transport ts ON m.transport_id = ts.transport_id
                                           where m.fiscal_year_id=@p_fiscal_year_id and m.event_id=@p_event_id
                                                     and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.return_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_return_challan_DTO>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length,
                            p_fiscal_year_id = param.fiscal_year_id,
                            p_event_id = param.event_id
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        



        public async Task<tran_return_challan_DTO> GetOutletReceiveData(Int64 p_tran_outlet_receive_note_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_outlet_receive_by_id_for_return_challan( @p_tran_outlet_receive_note_id)";

                    var dataList = connection.Query<tran_return_challan_DTO>(query, new
                    {
                        p_tran_outlet_receive_note_id = p_tran_outlet_receive_note_id
                    }).SingleOrDefault();

                    return dataList;

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<tran_return_challan_DTO> GetOutletChallanReturnData(Int64 p_tran_return_challan_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_outlet_return_by_id(@p_tran_return_challan_id)";

                    var dataList = connection.Query<tran_return_challan_DTO>(query, new
                    {
                        p_tran_return_challan_id = p_tran_return_challan_id
                    }).SingleOrDefault();

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

                    var objToDelete = new tran_return_challan_DTO { tran_return_challan_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_return_challan_insert_sp(tran_return_challan_DTO objtran_return_challan)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_return_challan_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_outlet_challan_id", NpgsqlDbType.Bigint, objtran_return_challan.tran_outlet_challan_id == null ? DBNull.Value : objtran_return_challan.tran_outlet_challan_id);
                        Command.Parameters.AddWithValue("in_tran_outlet_challan_receive_id", NpgsqlDbType.Bigint, objtran_return_challan.tran_outlet_challan_receive_id == null ? DBNull.Value : objtran_return_challan.tran_outlet_challan_receive_id);
                        Command.Parameters.AddWithValue("in_return_no", NpgsqlDbType.Text, objtran_return_challan.return_no == null ? DBNull.Value : objtran_return_challan.return_no);
                        Command.Parameters.AddWithValue("in_return_date", NpgsqlDbType.Date, objtran_return_challan.return_date == null ? DBNull.Value : objtran_return_challan.return_date);
                        Command.Parameters.AddWithValue("in_transport_id", NpgsqlDbType.Bigint, objtran_return_challan.transport_id == null ? DBNull.Value : objtran_return_challan.transport_id);
                        Command.Parameters.AddWithValue("in_vehicle_number", NpgsqlDbType.Text, objtran_return_challan.vehicle_number == null ? DBNull.Value : objtran_return_challan.vehicle_number);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_return_challan.driver_name == null ? DBNull.Value : objtran_return_challan.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact_no", NpgsqlDbType.Text, objtran_return_challan.driver_contact_no == null ? DBNull.Value : objtran_return_challan.driver_contact_no);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_return_challan.note == null ? DBNull.Value : objtran_return_challan.note);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_return_challan.added_by == null ? DBNull.Value : objtran_return_challan.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_return_challan.updated_by == null ? DBNull.Value : objtran_return_challan.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_return_challan.date_added == null ? DBNull.Value : objtran_return_challan.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_return_challan.date_updated == null ? DBNull.Value : objtran_return_challan.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_return_challan.fiscal_year_id == null ? DBNull.Value : objtran_return_challan.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_return_challan.event_id == null ? DBNull.Value : objtran_return_challan.event_id);
                        Command.Parameters.AddWithValue("in_tran_return_challan_details_json", NpgsqlDbType.Text, objtran_return_challan.tran_return_challan_details_json == null ? DBNull.Value : objtran_return_challan.tran_return_challan_details_json);


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
        public async Task<bool> tran_return_challan_update_sp(tran_return_challan_DTO objtran_return_challan)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_return_challan_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_outlet_challan_id", NpgsqlDbType.Bigint, objtran_return_challan.tran_outlet_challan_id == null ? DBNull.Value : objtran_return_challan.tran_outlet_challan_id);
                        Command.Parameters.AddWithValue("in_tran_outlet_challan_receive_id", NpgsqlDbType.Bigint, objtran_return_challan.tran_outlet_challan_receive_id == null ? DBNull.Value : objtran_return_challan.tran_outlet_challan_receive_id);
                        Command.Parameters.AddWithValue("in_return_no", NpgsqlDbType.Text, objtran_return_challan.return_no == null ? DBNull.Value : objtran_return_challan.return_no);
                        Command.Parameters.AddWithValue("in_return_date", NpgsqlDbType.Date, objtran_return_challan.return_date == null ? DBNull.Value : objtran_return_challan.return_date);
                        Command.Parameters.AddWithValue("in_transport_id", NpgsqlDbType.Bigint, objtran_return_challan.transport_id == null ? DBNull.Value : objtran_return_challan.transport_id);
                        Command.Parameters.AddWithValue("in_vehicle_number", NpgsqlDbType.Text, objtran_return_challan.vehicle_number == null ? DBNull.Value : objtran_return_challan.vehicle_number);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_return_challan.driver_name == null ? DBNull.Value : objtran_return_challan.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact_no", NpgsqlDbType.Text, objtran_return_challan.driver_contact_no == null ? DBNull.Value : objtran_return_challan.driver_contact_no);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_return_challan.note == null ? DBNull.Value : objtran_return_challan.note);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_return_challan.added_by == null ? DBNull.Value : objtran_return_challan.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_return_challan.updated_by == null ? DBNull.Value : objtran_return_challan.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_return_challan.date_added == null ? DBNull.Value : objtran_return_challan.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_return_challan.date_updated == null ? DBNull.Value : objtran_return_challan.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_return_challan.fiscal_year_id == null ? DBNull.Value : objtran_return_challan.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_return_challan.event_id == null ? DBNull.Value : objtran_return_challan.event_id);
                        Command.Parameters.AddWithValue("in_tran_return_challan_details_json", NpgsqlDbType.Text, objtran_return_challan.tran_return_challan_details_json == null ? DBNull.Value : objtran_return_challan.tran_return_challan_details_json);


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

        public async Task<List<tran_delivery_outlet_challan_DTO>> GetPrendingReturnData(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (
                                
                                               SELECT outlet_receive_no,
                                                   orn.tran_outlet_receive_note_id,
                                                   orn.outlet_receive_date,
                                                   orn.driver_name,
                                                   ts.transport_type AS transport_type_name,
                                                   orn.driver_contact
                                            FROM public.tran_outlet_receive_note orn
                                            INNER JOIN gen_tran_transport ts ON orn.transport_type = ts.transport_id
                                            LEFT JOIN public.tran_return_challan trc ON trc.tran_outlet_challan_receive_id = orn.tran_outlet_receive_note_id
                                            WHERE 
                                                (
                                                    @search_text IS NULL OR LENGTH(@search_text) = 0
                                                    OR (
                                                        @search_text IS NOT NULL AND LENGTH(@search_text) > 0
                                                        AND orn.outlet_receive_no ILIKE '%' || @search_text || '%'
                                                    )
                                                )
                                                AND trc.tran_return_challan_id IS NULL and
                                                orn.fiscal_year_id=@p_fiscal_year_id and orn.event_id=@p_event_id
                                        )                               


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_delivery_outlet_challan_DTO>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length,
                            p_fiscal_year_id=param.fiscal_year_id,
                            p_event_id=param.event_id,
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    
        //public async Task<List<rpc_tran_return_challan_DTO>> GetAllJoined_TranReturnChallanAsync(Int64 currnet_page, Int64 page_size)
        //{
        //    try
        //    {
        //        using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
        //        {
        //            connection.Open();

        //            string query = $"SELECT * FROM proc_sp_get_data_tran_return_challan( @currnet_page,@page_size)";

        //            var dataList = connection.Query<rpc_tran_return_challan_DTO>(query,
        //                  new
        //                  {
        //                      currnet_page = currnet_page,
        //                      page_size = page_size
        //                  }
        //                 ).AsList();

        //            return dataList;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //}

    }

}

