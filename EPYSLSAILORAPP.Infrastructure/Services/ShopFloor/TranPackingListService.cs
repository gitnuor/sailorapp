using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranPackingListService : ITranPackingListService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranPackingListService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_packing_list_DTO objtran_packing_list)
        {

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_packing_list_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_packing_list_date", NpgsqlDbType.Date, objtran_packing_list.packing_list_date == null ? DBNull.Value : objtran_packing_list.packing_list_date);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_packing_list.note == null ? DBNull.Value : objtran_packing_list.note);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_packing_list.added_by == null ? DBNull.Value : objtran_packing_list.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_packing_list.date_added == null ? DBNull.Value : objtran_packing_list.date_added);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_packing_list.fiscal_year_id == null ? DBNull.Value : objtran_packing_list.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_packing_list.event_id == null ? DBNull.Value : objtran_packing_list.event_id);
                        Command.Parameters.AddWithValue("in_tran_packing_list_details_json", NpgsqlDbType.Text, objtran_packing_list.tran_packing_list_details == null ?
                            DBNull.Value : JArray.Parse(JsonConvert.SerializeObject(objtran_packing_list.tran_packing_list_details)).ToString());


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

        public async Task<bool> UpdateAsync(tran_packing_list_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_packing_list_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_packing_list_entity>> GetAllAsync()
        {
            List<tran_packing_list_entity> list = new List<tran_packing_list_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_packing_list_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_packing_list_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_packing_list_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_packing_list m
                                           where m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id

                                                    and
                                                    is_draft = 1 and 
                                                    case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.packing_list_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_packing_list_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length,
                            fiscal_year_id = param.fiscal_year_id,
                            event_id = param.event_id

                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<List<tran_packing_list_entity>> GetAllSubmittedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_packing_list m
                                            
                                              where m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id

                                                    and
                                             is_submitted = 2
                                             and is_approved is null and
                                                    case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.packing_list_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_packing_list_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length,
                            fiscal_year_id = param.fiscal_year_id,
                            event_id = param.event_id
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<List<tran_packing_list_entity>> GetAllApprovedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_packing_list m
                                           where  m.fiscal_year_id=@fiscal_year_id and m.event_id=@event_id

                                                    and
                                             is_submitted = 2
                                             and is_approved =2 and
                                                    case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.packing_list_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_packing_list_entity>(query,
                        new
                        {
                            search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length,
                            fiscal_year_id = param.fiscal_year_id,
                            event_id = param.event_id
                        }).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        public async Task<rpc_proc_sp_get_data_tran_packing_list_by_id_DTO> GetSingleAsync(Int64 p_tran_packing_list_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_packing_list_by_id( @p_tran_packing_list_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_packing_list_by_id_DTO>(query,
                          new { p_tran_packing_list_id = p_tran_packing_list_id }
                         ).SingleOrDefault();


                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public async Task<rpc_proc_sp_get_data_tran_packing_list_by_id_DTO> GetPackingListForPMC(Int64 p_tran_packing_list_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_packing_list_by_id_for_pmc_receive( @p_tran_packing_list_id)";

                    var dataList = connection.Query<rpc_proc_sp_get_data_tran_packing_list_by_id_DTO>(query,
                          new { p_tran_packing_list_id = p_tran_packing_list_id }
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

                    var objToDelete = new tran_packing_list_entity { tran_packing_list_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> SendForApproval(Int64? p_tran_packing_list_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();


                    string query = $"Update  tran_packing_list set is_draft=2, is_submitted =2 where tran_packing_list_id=@p_tran_packing_list_id ";

                    var dataList = connection.Execute(query, new { p_tran_packing_list_id = p_tran_packing_list_id });
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);

            }

        }
        public async Task<bool> Approve(tran_packing_list_DTO entity)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();


                    string query = $"Update  tran_packing_list set is_draft=2, is_submitted =2, is_approved=2,approved_by=@p_approved_by,approved_date=@p_approved_date where tran_packing_list_id=@p_tran_packing_list_id ";

                    var dataList = connection.Execute(query, new
                    {
                        p_tran_packing_list_id = entity.tran_packing_list_id,
                        p_approved_by = entity.approved_by,
                        p_approved_date = entity.approved_date,
                    });
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);

            }

        }
        public async Task<bool> tran_packing_list_update_sp(tran_packing_list_DTO objtran_packing_list)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_packing_list_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_packing_list_no", NpgsqlDbType.Text, objtran_packing_list.packing_list_no == null ? DBNull.Value : objtran_packing_list.packing_list_no);
                        Command.Parameters.AddWithValue("in_packing_list_date", NpgsqlDbType.Date, objtran_packing_list.packing_list_date == null ? DBNull.Value : objtran_packing_list.packing_list_date);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_packing_list.note == null ? DBNull.Value : objtran_packing_list.note);
                        Command.Parameters.AddWithValue("in_is_draft", NpgsqlDbType.Bigint, objtran_packing_list.is_draft == null ? DBNull.Value : objtran_packing_list.is_draft);
                        Command.Parameters.AddWithValue("in_draft_date", NpgsqlDbType.Date, objtran_packing_list.draft_date == null ? DBNull.Value : objtran_packing_list.draft_date);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_packing_list.is_submitted == null ? DBNull.Value : objtran_packing_list.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_packing_list.submitted_date == null ? DBNull.Value : objtran_packing_list.submitted_date);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_packing_list.submitted_by == null ? DBNull.Value : objtran_packing_list.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_packing_list.is_approved == null ? DBNull.Value : objtran_packing_list.is_approved);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_packing_list.approved_date == null ? DBNull.Value : objtran_packing_list.approved_date);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_packing_list.approved_by == null ? DBNull.Value : objtran_packing_list.approved_by);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_packing_list.added_by == null ? DBNull.Value : objtran_packing_list.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_packing_list.updated_by == null ? DBNull.Value : objtran_packing_list.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_packing_list.date_added == null ? DBNull.Value : objtran_packing_list.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_packing_list.date_updated == null ? DBNull.Value : objtran_packing_list.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_packing_list.fiscal_year_id == null ? DBNull.Value : objtran_packing_list.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_packing_list.event_id == null ? DBNull.Value : objtran_packing_list.event_id);
                        Command.Parameters.AddWithValue("in_tran_packing_list_details_json", NpgsqlDbType.Text, objtran_packing_list.tran_packing_list_details_json == null ? DBNull.Value : objtran_packing_list.tran_packing_list_details_json);


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
        public async Task<List<rpc_tran_packing_list_DTO>> GetAllJoined_TranPackingListAsync(Int64 currnet_page, Int64 page_size, Int64 fiscal_year_id, Int64 event_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_packing_list( @currnet_page,@page_size,@fiscal_year_id,@event_id)";

                    var dataList = connection.Query<rpc_tran_packing_list_DTO>(query,
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


    }

}

