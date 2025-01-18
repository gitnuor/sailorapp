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

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranEmbellishReceiveService : ITranEmbellishReceiveService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranEmbellishReceiveService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_embellish_receive_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_embellish_receive_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_embellish_receive_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_embellish_receive_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_embellish_receive_entity>> GetAllAsync()
        {
            List<tran_embellish_receive_entity> list = new List<tran_embellish_receive_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_embellish_receive_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_embellish_receive_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_embellish_receive_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_embellish_receive m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.embellish_receive_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_embellish_receive_entity>(query,
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



        public async Task<tran_embellish_receive_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_embellish_receive m   where m.embellish_receive_id=@Id";

                    var dataList = connection.Query<tran_embellish_receive_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_embellish_receive_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_embellish_receive_entity { embellish_receive_id = (long)Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_embellish_receive_insert_sp(tran_embellish_receive_DTO objtran_embellish_receive)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_embellish_receive_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_embellish_receive_no", NpgsqlDbType.Text, objtran_embellish_receive.embellish_receive_no == null ? DBNull.Value : objtran_embellish_receive.embellish_receive_no);
                        Command.Parameters.AddWithValue("in_embellish_receive_date", NpgsqlDbType.Date, objtran_embellish_receive.embellish_receive_date == null ? DBNull.Value : objtran_embellish_receive.embellish_receive_date);
                        Command.Parameters.AddWithValue("in_challan_no_receive", NpgsqlDbType.Text, objtran_embellish_receive.challan_no_receive == null ? DBNull.Value : objtran_embellish_receive.challan_no_receive);
                        Command.Parameters.AddWithValue("in_challan_receive_date", NpgsqlDbType.Date, objtran_embellish_receive.challan_receive_date == null ? DBNull.Value : objtran_embellish_receive.challan_receive_date);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_embellish_receive.note == null ? DBNull.Value : objtran_embellish_receive.note);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_embellish_receive.supplier_id == null ? DBNull.Value : objtran_embellish_receive.supplier_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_embellish_receive.techpack_id == null ? DBNull.Value : objtran_embellish_receive.techpack_id);
                        Command.Parameters.AddWithValue("in_service_work_order_id", NpgsqlDbType.Bigint, objtran_embellish_receive.service_work_order_id == null ? DBNull.Value : objtran_embellish_receive.service_work_order_id);
                        Command.Parameters.AddWithValue("in_gen_process_master_id", NpgsqlDbType.Bigint, objtran_embellish_receive.gen_process_master_id == null ? DBNull.Value : objtran_embellish_receive.gen_process_master_id);
                        Command.Parameters.AddWithValue("in_total_qty", NpgsqlDbType.Numeric, objtran_embellish_receive.total_qty == null ? DBNull.Value : objtran_embellish_receive.total_qty);
                        Command.Parameters.AddWithValue("in_total_no_bundle", NpgsqlDbType.Bigint, objtran_embellish_receive.total_no_bundle == null ? DBNull.Value : objtran_embellish_receive.total_no_bundle);
                        Command.Parameters.AddWithValue("in_embellish_receive_detail_list", NpgsqlDbType.Text, objtran_embellish_receive.embellish_receive_detail_list.ToString() == null ? DBNull.Value : objtran_embellish_receive.embellish_receive_detail_list.ToString());
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_embellish_receive.is_submitted == null ? DBNull.Value : objtran_embellish_receive.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_embellish_receive.submitted_by == null ? DBNull.Value : objtran_embellish_receive.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_embellish_receive.is_approved == null ? DBNull.Value : objtran_embellish_receive.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_embellish_receive.approved_by == null ? DBNull.Value : objtran_embellish_receive.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_embellish_receive.approve_date == null ? DBNull.Value : objtran_embellish_receive.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_embellish_receive.approve_remarks == null ? DBNull.Value : objtran_embellish_receive.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_embellish_receive.added_by == null ? DBNull.Value : objtran_embellish_receive.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_embellish_receive.date_added == null ? DBNull.Value : objtran_embellish_receive.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_embellish_receive.updated_by == null ? DBNull.Value : objtran_embellish_receive.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_embellish_receive.date_updated == null ? DBNull.Value : objtran_embellish_receive.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_embellish_receive.fiscal_year_id == null ? DBNull.Value : objtran_embellish_receive.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_embellish_receive.event_id == null ? DBNull.Value : objtran_embellish_receive.event_id);


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

        public async Task<List<tran_embellish_receive_DTO>> GetTranReceivedChallanAllListAsync(Int64 row_index, Int64 page_size, Int64 actiontype, Int64 fiscal_year_id, Int64 event_id, Int64 supplier_id)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT * FROM proc_sp_get_data_tran_emb_receive_challan_all_list(@row_index,@page_size,@action_type,@p_fiscal_year_id,@p_event_id,@p_supplier_id)";

                    var parameters = new DynamicParameters();
                    parameters.Add("row_index", row_index);
                    parameters.Add("page_size", page_size);
                    parameters.Add("action_type", actiontype);
                    parameters.Add("p_fiscal_year_id", fiscal_year_id);
                    parameters.Add("p_event_id", event_id);
                    parameters.Add("p_supplier_id", supplier_id);

                    // int timeoutInSeconds = 300;
                    var result = await connection.QueryAsync<tran_embellish_receive_DTO>(
                      sqlCommand,
                      parameters
                    // commandTimeout: timeoutInSeconds
                    );

                    return result.OrderBy(x => x.embellish_receive_id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_embellish_receive_update_sp(tran_embellish_receive_DTO objtran_embellish_receive)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_embellish_receive_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_embellish_receive_no", NpgsqlDbType.Text, objtran_embellish_receive.embellish_receive_no == null ? DBNull.Value : objtran_embellish_receive.embellish_receive_no);
                        Command.Parameters.AddWithValue("in_embellish_receive_date", NpgsqlDbType.Date, objtran_embellish_receive.embellish_receive_date == null ? DBNull.Value : objtran_embellish_receive.embellish_receive_date);
                        Command.Parameters.AddWithValue("in_challan_no_receive", NpgsqlDbType.Text, objtran_embellish_receive.challan_no_receive == null ? DBNull.Value : objtran_embellish_receive.challan_no_receive);
                        Command.Parameters.AddWithValue("in_challan_receive_date", NpgsqlDbType.Date, objtran_embellish_receive.challan_receive_date == null ? DBNull.Value : objtran_embellish_receive.challan_receive_date);
                        Command.Parameters.AddWithValue("in_note", NpgsqlDbType.Text, objtran_embellish_receive.note == null ? DBNull.Value : objtran_embellish_receive.note);
                        Command.Parameters.AddWithValue("in_supplier_id", NpgsqlDbType.Bigint, objtran_embellish_receive.supplier_id == null ? DBNull.Value : objtran_embellish_receive.supplier_id);
                        Command.Parameters.AddWithValue("in_techpack_id", NpgsqlDbType.Bigint, objtran_embellish_receive.techpack_id == null ? DBNull.Value : objtran_embellish_receive.techpack_id);
                        Command.Parameters.AddWithValue("in_service_work_order_id", NpgsqlDbType.Bigint, objtran_embellish_receive.service_work_order_id == null ? DBNull.Value : objtran_embellish_receive.service_work_order_id);
                        Command.Parameters.AddWithValue("in_gen_process_master_id", NpgsqlDbType.Bigint, objtran_embellish_receive.gen_process_master_id == null ? DBNull.Value : objtran_embellish_receive.gen_process_master_id);
                        Command.Parameters.AddWithValue("in_total_qty", NpgsqlDbType.Numeric, objtran_embellish_receive.total_qty == null ? DBNull.Value : objtran_embellish_receive.total_qty);
                        Command.Parameters.AddWithValue("in_total_no_bundle", NpgsqlDbType.Bigint, objtran_embellish_receive.total_no_bundle == null ? DBNull.Value : objtran_embellish_receive.total_no_bundle);
                        Command.Parameters.AddWithValue("in_embellish_receive_detail_list", NpgsqlDbType.Text, objtran_embellish_receive.embellish_receive_detail_list == null ? DBNull.Value : objtran_embellish_receive.embellish_receive_detail_list);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_embellish_receive.is_submitted == null ? DBNull.Value : objtran_embellish_receive.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_embellish_receive.submitted_by == null ? DBNull.Value : objtran_embellish_receive.submitted_by);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_embellish_receive.is_approved == null ? DBNull.Value : objtran_embellish_receive.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_embellish_receive.approved_by == null ? DBNull.Value : objtran_embellish_receive.approved_by);
                        Command.Parameters.AddWithValue("in_approve_date", NpgsqlDbType.Date, objtran_embellish_receive.approve_date == null ? DBNull.Value : objtran_embellish_receive.approve_date);
                        Command.Parameters.AddWithValue("in_approve_remarks", NpgsqlDbType.Text, objtran_embellish_receive.approve_remarks == null ? DBNull.Value : objtran_embellish_receive.approve_remarks);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_embellish_receive.added_by == null ? DBNull.Value : objtran_embellish_receive.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_embellish_receive.date_added == null ? DBNull.Value : objtran_embellish_receive.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_embellish_receive.updated_by == null ? DBNull.Value : objtran_embellish_receive.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_embellish_receive.date_updated == null ? DBNull.Value : objtran_embellish_receive.date_updated);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_embellish_receive.fiscal_year_id == null ? DBNull.Value : objtran_embellish_receive.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_embellish_receive.event_id == null ? DBNull.Value : objtran_embellish_receive.event_id);


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

        public async Task<tran_embellish_receive_DTO> Get_master_detail_tran_emb_delivery_challan_Receive_Async(Int64 embellish_receive_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_embellish_receive_details( @p_embellish_receive_id)";

                    var dataList = connection.Query<tran_embellish_receive_DTO>(query,
                          new
                          {

                              p_embellish_receive_id = embellish_receive_id
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

        public async Task<bool> ApprovalProposedAsync(tran_embellish_receive_DTO request)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {

                    await connection.OpenAsync();
                    //string sqlCommand = "SELECT public.proc_sp_proposed_for_approval_emb_challan_receive(@p_embellish_receive_id, @p_is_submitted)";

                    string sqlCommand = @"UPDATE tran_embellish_receive
                                          SET is_submitted = 2
                                          WHERE embellish_receive_id = @p_embellish_receive_id;";

                    var parameters = new
                    {
                        p_embellish_receive_id = request.embellish_receive_id,
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

        public async Task<bool> ApprovedAsync(tran_embellish_receive_DTO request)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();
                    string sqlCommand = "SELECT public.proc_sp_approved_for_emb_challan_receive(@p_embellish_receive_id, @p_is_approved)";

                    var parameters = new
                    {
                        p_embellish_receive_id = request.embellish_receive_id,
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


    }

}

