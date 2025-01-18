using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranReturnChallanReceivedService : ITranReturnChallanReceivedService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranReturnChallanReceivedService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_return_challan_received_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_return_challan_received_DTO>(JsonConvert.SerializeObject(entity));

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();
                    connection.Insert(objEntity);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> UpdateAsync(tran_return_challan_received_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_return_challan_received_DTO>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_return_challan_received_DTO>> GetAllAsync()
        {
            List<tran_return_challan_received_DTO> list = new List<tran_return_challan_received_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_return_challan_received_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_return_challan_received_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_return_challan_received_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_return_challan_received m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.return_receive_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_return_challan_received_DTO>(query,
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



        public async Task<tran_return_challan_received_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_return_challan_received_by_id(@p_tran_return_challan_received_id)";

                    var dataList = connection.Query<tran_return_challan_received_DTO>(query,
                        new { p_tran_return_challan_received_id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_return_challan_received_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_return_challan_received_DTO> GetReturnChallanData(Int64 p_tran_return_challan_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_outlet_return_by_id(@p_tran_return_challan_id)";

                    var dataList = connection.Query<tran_return_challan_received_DTO>(query, new
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

                    var objToDelete = new tran_return_challan_received_DTO { tran_return_challan_received_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_return_challan_received_insert_sp(tran_return_challan_received_DTO objtran_return_challan_received)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_return_challan_received_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_return_challan_id", NpgsqlDbType.Bigint, objtran_return_challan_received.tran_return_challan_id == null ? DBNull.Value : objtran_return_challan_received.tran_return_challan_id);
                        Command.Parameters.AddWithValue("in_return_receive_no", NpgsqlDbType.Text, objtran_return_challan_received.return_receive_no == null ? DBNull.Value : objtran_return_challan_received.return_receive_no);
                        Command.Parameters.AddWithValue("in_return_receive_date", NpgsqlDbType.Date, objtran_return_challan_received.return_receive_date == null ? DBNull.Value : objtran_return_challan_received.return_receive_date);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_return_challan_received.added_by == null ? DBNull.Value : objtran_return_challan_received.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_return_challan_received.updated_by == null ? DBNull.Value : objtran_return_challan_received.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_return_challan_received.date_added == null ? DBNull.Value : objtran_return_challan_received.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_return_challan_received.date_updated == null ? DBNull.Value : objtran_return_challan_received.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_return_challan_received.fiscal_year_id == null ? DBNull.Value : objtran_return_challan_received.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_return_challan_received.event_id == null ? DBNull.Value : objtran_return_challan_received.event_id);
                        Command.Parameters.AddWithValue("in_tran_return_challan_receive_details_json", NpgsqlDbType.Text, objtran_return_challan_received.tran_return_challan_receive_details_json == null ? DBNull.Value : objtran_return_challan_received.tran_return_challan_receive_details_json);


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
        public async Task<bool> tran_return_challan_received_update_sp(tran_return_challan_received_DTO objtran_return_challan_received)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_return_challan_received_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_tran_return_challan_id", NpgsqlDbType.Bigint, objtran_return_challan_received.tran_return_challan_id == null ? DBNull.Value : objtran_return_challan_received.tran_return_challan_id);
                        Command.Parameters.AddWithValue("in_return_receive_no", NpgsqlDbType.Text, objtran_return_challan_received.return_receive_no == null ? DBNull.Value : objtran_return_challan_received.return_receive_no);
                        Command.Parameters.AddWithValue("in_return_receive_date", NpgsqlDbType.Date, objtran_return_challan_received.return_receive_date == null ? DBNull.Value : objtran_return_challan_received.return_receive_date);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_return_challan_received.added_by == null ? DBNull.Value : objtran_return_challan_received.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_return_challan_received.updated_by == null ? DBNull.Value : objtran_return_challan_received.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_return_challan_received.date_added == null ? DBNull.Value : objtran_return_challan_received.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_return_challan_received.date_updated == null ? DBNull.Value : objtran_return_challan_received.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_return_challan_received.fiscal_year_id == null ? DBNull.Value : objtran_return_challan_received.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_return_challan_received.event_id == null ? DBNull.Value : objtran_return_challan_received.event_id);
                        Command.Parameters.AddWithValue("in_tran_return_challan_receive_details_json", NpgsqlDbType.Text, objtran_return_challan_received.tran_return_challan_receive_details_json == null ? DBNull.Value : objtran_return_challan_received.tran_return_challan_receive_details_json);


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
        //public async Task<List<rpc_tran_return_challan_received_DTO>> GetAllJoined_TranReturnChallanReceivedAsync(Int64 currnet_page, Int64 page_size)
        //{
        //    try
        //    {
        //        using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
        //        {
        //            connection.Open();

        //            string query = $"SELECT * FROM proc_sp_get_data_tran_return_challan_received( @currnet_page,@page_size)";

        //            var dataList = connection.Query<rpc_tran_return_challan_received_DTO>(query,
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



        public async Task<List<tran_return_challan_received_DTO>> GetAllPendingReturnChallanReceivedAsync(long p_fiscal_year_id, long p_event_id, Int64 row_index, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_pending_return_challan(@p_fiscal_year_id,@p_event_id, @row_index,@page_size)";

                    var dataList = connection.Query<tran_return_challan_received_DTO>(query,
                          new
                          {
                              p_fiscal_year_id = p_fiscal_year_id,
                              p_event_id = p_event_id,
                              row_index = row_index,
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

        public async Task<List<tran_return_challan_received_DTO>> GetAllReturnChallanReceivedAsync(long p_fiscal_year_id, long p_event_id, Int64 row_index, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_return_challan_receive(@p_fiscal_year_id,@p_event_id, @row_index,@page_size)";

                    var dataList = connection.Query<tran_return_challan_received_DTO>(query,
                          new
                          {
                              p_fiscal_year_id = p_fiscal_year_id,
                              p_event_id = p_event_id,
                              row_index = row_index,
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



    }

}

