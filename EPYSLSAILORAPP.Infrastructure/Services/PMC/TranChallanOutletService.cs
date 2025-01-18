using AutoMapper;
using Dapper;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.DTO.RPC;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.RPC;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Infrastructure.Services.PMC
{
    public class TranPackingListService : ITranChallanOutletService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranPackingListService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<rpc_tran_outlet_challan_request_DTO>> GetOutletDetailList(Int64 techpack_id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_outlet_challan_by_techpack( @p_techpack_id)";

                    var dataList = connection.Query<rpc_tran_outlet_challan_request_DTO>(query,
                          new { p_techpack_id = techpack_id }
                         ).AsList();

                    return dataList;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_delivery_outlet_challan_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.tran_delivery_outlet_challan_id,
                                                           m.delivery_outlet_challan_no,
	                                                       m.delivery_outlet_challan_date,
	                                                       m.delivery_from,
	                                                       o.outlet_name,
	                                                       m.delivery_address,
	                                                       m.transport_number,
	                                                       m.driver_name
                                                        FROM tran_delivery_outlet_challan m
	                                                         INNER JOIN gen_outlet o ON 
                                                             o.outlet_id = m.delivery_to 
                                                              and m.is_submitted=1
                                                             --and m.is_approved=1
                                                         --left join tran_outlet_receive_note ort on 
			                                             --ort.del_challan_id = m.tran_delivery_outlet_challan_id
			                                               --where ort.del_challan_id is null
                                                            where m.fiscal_year_id=@p_fiscal_year_id and m.event_id=@p_event_id
                                           and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.delivery_outlet_challan_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


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

        public async Task<List<tran_delivery_outlet_challan_DTO>> GetAllPagedDataPendingAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.tran_delivery_outlet_challan_id,
                                                           m.delivery_outlet_challan_no,
	                                                       m.delivery_outlet_challan_date,
	                                                       m.delivery_from,
	                                                       o.outlet_name,
	                                                       m.delivery_address,
	                                                       m.transport_number,
	                                                       m.driver_name
                                                        FROM tran_delivery_outlet_challan m
	                                                         INNER JOIN gen_outlet o ON 
                                                             o.outlet_id = m.delivery_to 
                                                              and m.is_submitted=2
                                                             and m.is_approved=1
                                                         left join tran_outlet_receive_note ort on 
			                                             ort.del_challan_id = m.tran_delivery_outlet_challan_id
			                                              where ort.del_challan_id is null and
                                                          m.fiscal_year_id=@p_fiscal_year_id and m.event_id=@p_event_id
                                                            
                                           and case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.delivery_outlet_challan_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


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
        public async Task<bool> tran_delivery_outlet_challan_insert_sp(tran_delivery_outlet_challan_DTO objtran_delivery_outlet_challan)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_delivery_outlet_challan_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_delivery_outlet_challan_no", NpgsqlDbType.Text, objtran_delivery_outlet_challan.delivery_outlet_challan_no == null ? DBNull.Value : objtran_delivery_outlet_challan.delivery_outlet_challan_no);
                        Command.Parameters.AddWithValue("in_delivery_outlet_challan_date", NpgsqlDbType.Date, objtran_delivery_outlet_challan.delivery_outlet_challan_date == null ? DBNull.Value : objtran_delivery_outlet_challan.delivery_outlet_challan_date);
                        Command.Parameters.AddWithValue("in_del_challan_no", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.del_challan_no == null ? DBNull.Value : objtran_delivery_outlet_challan.del_challan_no);
                        Command.Parameters.AddWithValue("in_del_challan_date", NpgsqlDbType.Date, objtran_delivery_outlet_challan.del_challan_date == null ? DBNull.Value : objtran_delivery_outlet_challan.del_challan_date);
                        Command.Parameters.AddWithValue("in_delivery_from", NpgsqlDbType.Text, objtran_delivery_outlet_challan.delivery_from == null ? DBNull.Value : objtran_delivery_outlet_challan.delivery_from);
                        Command.Parameters.AddWithValue("in_delivery_to", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.delivery_to == null ? DBNull.Value : objtran_delivery_outlet_challan.delivery_to);
                        Command.Parameters.AddWithValue("in_delivery_address", NpgsqlDbType.Text, objtran_delivery_outlet_challan.delivery_address == null ? DBNull.Value : objtran_delivery_outlet_challan.delivery_address);
                        Command.Parameters.AddWithValue("in_transport_type", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.transport_type == null ? DBNull.Value : objtran_delivery_outlet_challan.transport_type);
                        Command.Parameters.AddWithValue("in_transport_number", NpgsqlDbType.Text, objtran_delivery_outlet_challan.transport_number == null ? DBNull.Value : objtran_delivery_outlet_challan.transport_number);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_delivery_outlet_challan.driver_name == null ? DBNull.Value : objtran_delivery_outlet_challan.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact", NpgsqlDbType.Text, objtran_delivery_outlet_challan.driver_contact == null ? DBNull.Value : objtran_delivery_outlet_challan.driver_contact);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.fiscal_year_id == null ? DBNull.Value : objtran_delivery_outlet_challan.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.event_id == null ? DBNull.Value : objtran_delivery_outlet_challan.event_id);
                        Command.Parameters.AddWithValue("in_is_draft", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.is_draft == null ? DBNull.Value : objtran_delivery_outlet_challan.is_draft);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.is_submitted == null ? DBNull.Value : objtran_delivery_outlet_challan.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.submitted_by == null ? DBNull.Value : objtran_delivery_outlet_challan.submitted_by);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_delivery_outlet_challan.submitted_date == null ? DBNull.Value : objtran_delivery_outlet_challan.submitted_date);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.is_approved == null ? DBNull.Value : objtran_delivery_outlet_challan.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.approved_by == null ? DBNull.Value : objtran_delivery_outlet_challan.approved_by);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_delivery_outlet_challan.approved_date == null ? DBNull.Value : objtran_delivery_outlet_challan.approved_date);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.added_by == null ? DBNull.Value : objtran_delivery_outlet_challan.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_delivery_outlet_challan.updated_by == null ? DBNull.Value : objtran_delivery_outlet_challan.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_delivery_outlet_challan.date_added == null ? DBNull.Value : objtran_delivery_outlet_challan.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_delivery_outlet_challan.date_updated == null ? DBNull.Value : objtran_delivery_outlet_challan.date_updated);
                        Command.Parameters.AddWithValue("in_tran_delivery_outlet_challan_id_detail_json", NpgsqlDbType.Text, objtran_delivery_outlet_challan.tran_delivery_outlet_challan_id_detail_json.ToString() == null ? DBNull.Value : objtran_delivery_outlet_challan.tran_delivery_outlet_challan_id_detail_json.ToString());
                        Command.Parameters.AddWithValue("in_remarks", NpgsqlDbType.Text, objtran_delivery_outlet_challan.remarks == null ? DBNull.Value : objtran_delivery_outlet_challan.remarks);


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

        public async Task<List<tran_delivery_outlet_challan_DTO>> GetTranServiceoutlet_challan_landing_approval_data(Int64 row_index, Int64 page_size, Int64 actionType, Int64 fiscal_year_id, Int64 event_id, string search)
        {
            try
            {

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = "SELECT * FROM proc_sp_get_data_tran_outlet_challan_landing_data(@row_index,@page_size,@action_type,@p_fiscal_year_id,@p_event_id,@search_text)";

                    var data = connection.Query<tran_delivery_outlet_challan_DTO>(query, new
                    {
                        row_index = row_index,
                        page_size = page_size,
                        action_type = actionType,
                        p_fiscal_year_id = fiscal_year_id,
                        p_event_id = event_id,
                        search_text=search

                    }).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_delivery_outlet_challan_DTO> Get_data_tran_delivery_outlet_challan_Async(Int64 outlet_challan_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_delivery_outlet_challan( @p_outlet_challan_id)";

                    var dataList = connection.Query<tran_delivery_outlet_challan_DTO>(query,
                          new
                          {

                              p_outlet_challan_id = outlet_challan_id
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

        public async Task<bool> ApproveAsync(tran_delivery_outlet_challan_DTO request)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @" UPDATE tran_delivery_outlet_challan
                                        SET is_approved = 1
                                        WHERE tran_delivery_outlet_challan_id = @p_tran_delivery_outlet_challan_id";

                    await connection.ExecuteAsync(query, new
                    {
                        p_tran_delivery_outlet_challan_id = request.tran_delivery_outlet_challan_id
                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> ProposedAsync(tran_delivery_outlet_challan_DTO request)
        {

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    await connection.OpenAsync();

                    string query = @" UPDATE tran_delivery_outlet_challan
                                        SET is_submitted = 2
                                        WHERE tran_delivery_outlet_challan_id = @p_tran_delivery_outlet_challan_id";

                    await connection.ExecuteAsync(query, new
                    {
                        p_tran_delivery_outlet_challan_id = request.tran_delivery_outlet_challan_id
                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }



    }

  }

