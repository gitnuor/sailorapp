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
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.RPC;
using EPYSLSAILORAPP.Application.Interface;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranOutletReceiveNoteService : ITranOutletReceiveNoteService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranOutletReceiveNoteService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_outlet_receive_note_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_outlet_receive_note_DTO>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_outlet_receive_note_DTO entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_outlet_receive_note_DTO>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_outlet_receive_note_DTO>> GetAllAsync()
        {
            List<tran_outlet_receive_note_DTO> list = new List<tran_outlet_receive_note_DTO>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_outlet_receive_note_DTO>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_outlet_receive_note_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_outlet_receive_note_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.tran_outlet_receive_note_id,
                                                           m.outlet_receive_no,
	                                                       m.outlet_receive_date,
	                                                       m.delivery_from,
	                                                       o.outlet_name,
	                                                       m.delivery_address,
	                                                       m.transport_number,
	                                                       m.driver_name
                                                        FROM tran_outlet_receive_note m
	                                                         INNER JOIN gen_outlet o ON 
                                                             o.outlet_id = m.delivery_to and m.is_submitted=1
                                           where     m.fiscal_year_id=@p_fiscal_year_id and m.event_id=@p_event_id and
                                                     case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.outlet_receive_no ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_outlet_receive_note_DTO>(query,
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



        public async Task<tran_outlet_receive_note_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_outlet_receive_note m   where m.tran_outlet_receive_note_id=@Id";

                    var dataList = connection.Query<tran_outlet_receive_note_DTO>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_outlet_receive_note_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_outlet_receive_note_DTO { tran_outlet_receive_note_id = (long)Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_outlet_receive_note_insert_sp(tran_outlet_receive_note_DTO objtran_outlet_receive_note)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_outlet_receive_note_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_outlet_receive_no", NpgsqlDbType.Text, objtran_outlet_receive_note.outlet_receive_no == null ? DBNull.Value : objtran_outlet_receive_note.outlet_receive_no);
                        Command.Parameters.AddWithValue("in_outlet_receive_date", NpgsqlDbType.Date, objtran_outlet_receive_note.outlet_receive_date == null ? DBNull.Value : objtran_outlet_receive_note.outlet_receive_date);
                        Command.Parameters.AddWithValue("in_del_challan_id", NpgsqlDbType.Bigint, objtran_outlet_receive_note.del_challan_id == null ? DBNull.Value : objtran_outlet_receive_note.del_challan_id);
                        Command.Parameters.AddWithValue("in_del_challan_date", NpgsqlDbType.Date, objtran_outlet_receive_note.del_challan_date == null ? DBNull.Value : objtran_outlet_receive_note.del_challan_date);
                        Command.Parameters.AddWithValue("in_delivery_from", NpgsqlDbType.Text, objtran_outlet_receive_note.delivery_from == null ? DBNull.Value : objtran_outlet_receive_note.delivery_from);
                        Command.Parameters.AddWithValue("in_delivery_to", NpgsqlDbType.Bigint, objtran_outlet_receive_note.delivery_to == null ? DBNull.Value : objtran_outlet_receive_note.delivery_to);
                        Command.Parameters.AddWithValue("in_delivery_address", NpgsqlDbType.Text, objtran_outlet_receive_note.delivery_address == null ? DBNull.Value : objtran_outlet_receive_note.delivery_address);
                        Command.Parameters.AddWithValue("in_transport_type", NpgsqlDbType.Bigint, objtran_outlet_receive_note.transport_type == null ? DBNull.Value : objtran_outlet_receive_note.transport_type);
                        Command.Parameters.AddWithValue("in_transport_number", NpgsqlDbType.Text, objtran_outlet_receive_note.transport_number == null ? DBNull.Value : objtran_outlet_receive_note.transport_number);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_outlet_receive_note.driver_name == null ? DBNull.Value : objtran_outlet_receive_note.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact", NpgsqlDbType.Text, objtran_outlet_receive_note.driver_contact == null ? DBNull.Value : objtran_outlet_receive_note.driver_contact);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_outlet_receive_note.fiscal_year_id == null ? DBNull.Value : objtran_outlet_receive_note.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_outlet_receive_note.event_id == null ? DBNull.Value : objtran_outlet_receive_note.event_id);
                        Command.Parameters.AddWithValue("in_is_draft", NpgsqlDbType.Bigint, objtran_outlet_receive_note.is_draft == null ? DBNull.Value : objtran_outlet_receive_note.is_draft);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_outlet_receive_note.is_submitted == null ? DBNull.Value : objtran_outlet_receive_note.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_outlet_receive_note.submitted_by == null ? DBNull.Value : objtran_outlet_receive_note.submitted_by);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_outlet_receive_note.submitted_date == null ? DBNull.Value : objtran_outlet_receive_note.submitted_date);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_outlet_receive_note.is_approved == null ? DBNull.Value : objtran_outlet_receive_note.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_outlet_receive_note.approved_by == null ? DBNull.Value : objtran_outlet_receive_note.approved_by);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_outlet_receive_note.approved_date == null ? DBNull.Value : objtran_outlet_receive_note.approved_date);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_outlet_receive_note.added_by == null ? DBNull.Value : objtran_outlet_receive_note.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_outlet_receive_note.updated_by == null ? DBNull.Value : objtran_outlet_receive_note.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_outlet_receive_note.date_added == null ? DBNull.Value : objtran_outlet_receive_note.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_outlet_receive_note.date_updated == null ? DBNull.Value : objtran_outlet_receive_note.date_updated);
                        Command.Parameters.AddWithValue("in_tran_outlet_receive_note_detail_json", NpgsqlDbType.Text, objtran_outlet_receive_note.tran_outlet_receive_note_detail_json.ToString() == null ? DBNull.Value : objtran_outlet_receive_note.tran_outlet_receive_note_detail_json.ToString());
                        Command.Parameters.AddWithValue("in_barcode", NpgsqlDbType.Text, objtran_outlet_receive_note.barcode == null ? DBNull.Value : objtran_outlet_receive_note.barcode);


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
        public async Task<bool> tran_outlet_receive_note_update_sp(tran_outlet_receive_note_DTO objtran_outlet_receive_note)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_outlet_receive_note_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_outlet_receive_no", NpgsqlDbType.Text, objtran_outlet_receive_note.outlet_receive_no == null ? DBNull.Value : objtran_outlet_receive_note.outlet_receive_no);
                        Command.Parameters.AddWithValue("in_outlet_receive_date", NpgsqlDbType.Date, objtran_outlet_receive_note.outlet_receive_date == null ? DBNull.Value : objtran_outlet_receive_note.outlet_receive_date);
                        Command.Parameters.AddWithValue("in_del_challan_no", NpgsqlDbType.Bigint, objtran_outlet_receive_note.del_challan_id == null ? DBNull.Value : objtran_outlet_receive_note.del_challan_id);
                        Command.Parameters.AddWithValue("in_del_challan_date", NpgsqlDbType.Date, objtran_outlet_receive_note.del_challan_date == null ? DBNull.Value : objtran_outlet_receive_note.del_challan_date);
                        Command.Parameters.AddWithValue("in_delivery_from", NpgsqlDbType.Text, objtran_outlet_receive_note.delivery_from == null ? DBNull.Value : objtran_outlet_receive_note.delivery_from);
                        Command.Parameters.AddWithValue("in_delivery_to", NpgsqlDbType.Bigint, objtran_outlet_receive_note.delivery_to == null ? DBNull.Value : objtran_outlet_receive_note.delivery_to);
                        Command.Parameters.AddWithValue("in_delivery_address", NpgsqlDbType.Text, objtran_outlet_receive_note.delivery_address == null ? DBNull.Value : objtran_outlet_receive_note.delivery_address);
                        Command.Parameters.AddWithValue("in_transport_type", NpgsqlDbType.Bigint, objtran_outlet_receive_note.transport_type == null ? DBNull.Value : objtran_outlet_receive_note.transport_type);
                        Command.Parameters.AddWithValue("in_transport_number", NpgsqlDbType.Text, objtran_outlet_receive_note.transport_number == null ? DBNull.Value : objtran_outlet_receive_note.transport_number);
                        Command.Parameters.AddWithValue("in_driver_name", NpgsqlDbType.Text, objtran_outlet_receive_note.driver_name == null ? DBNull.Value : objtran_outlet_receive_note.driver_name);
                        Command.Parameters.AddWithValue("in_driver_contact", NpgsqlDbType.Text, objtran_outlet_receive_note.driver_contact == null ? DBNull.Value : objtran_outlet_receive_note.driver_contact);
                        Command.Parameters.AddWithValue("in_fiscal_year_id", NpgsqlDbType.Bigint, objtran_outlet_receive_note.fiscal_year_id == null ? DBNull.Value : objtran_outlet_receive_note.fiscal_year_id);
                        Command.Parameters.AddWithValue("in_event_id", NpgsqlDbType.Bigint, objtran_outlet_receive_note.event_id == null ? DBNull.Value : objtran_outlet_receive_note.event_id);
                        Command.Parameters.AddWithValue("in_is_draft", NpgsqlDbType.Bigint, objtran_outlet_receive_note.is_draft == null ? DBNull.Value : objtran_outlet_receive_note.is_draft);
                        Command.Parameters.AddWithValue("in_is_submitted", NpgsqlDbType.Bigint, objtran_outlet_receive_note.is_submitted == null ? DBNull.Value : objtran_outlet_receive_note.is_submitted);
                        Command.Parameters.AddWithValue("in_submitted_by", NpgsqlDbType.Bigint, objtran_outlet_receive_note.submitted_by == null ? DBNull.Value : objtran_outlet_receive_note.submitted_by);
                        Command.Parameters.AddWithValue("in_submitted_date", NpgsqlDbType.Date, objtran_outlet_receive_note.submitted_date == null ? DBNull.Value : objtran_outlet_receive_note.submitted_date);
                        Command.Parameters.AddWithValue("in_is_approved", NpgsqlDbType.Bigint, objtran_outlet_receive_note.is_approved == null ? DBNull.Value : objtran_outlet_receive_note.is_approved);
                        Command.Parameters.AddWithValue("in_approved_by", NpgsqlDbType.Bigint, objtran_outlet_receive_note.approved_by == null ? DBNull.Value : objtran_outlet_receive_note.approved_by);
                        Command.Parameters.AddWithValue("in_approved_date", NpgsqlDbType.Date, objtran_outlet_receive_note.approved_date == null ? DBNull.Value : objtran_outlet_receive_note.approved_date);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objtran_outlet_receive_note.added_by == null ? DBNull.Value : objtran_outlet_receive_note.added_by);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objtran_outlet_receive_note.updated_by == null ? DBNull.Value : objtran_outlet_receive_note.updated_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_outlet_receive_note.date_added == null ? DBNull.Value : objtran_outlet_receive_note.date_added);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objtran_outlet_receive_note.date_updated == null ? DBNull.Value : objtran_outlet_receive_note.date_updated);
                        Command.Parameters.AddWithValue("in_tran_outlet_receive_note_detail_json", NpgsqlDbType.Text, objtran_outlet_receive_note.tran_outlet_receive_note_detail_json == null ? DBNull.Value : objtran_outlet_receive_note.tran_outlet_receive_note_detail_json);
                        Command.Parameters.AddWithValue("in_barcode", NpgsqlDbType.Text, objtran_outlet_receive_note.barcode == null ? DBNull.Value : objtran_outlet_receive_note.barcode);


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
        public async Task<tran_outlet_receive_note_DTO> GetAllJoined_TranOutletReceiveNoteAsync(Int64 outlet_challan_receive_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_tran_outlet_receive_note( @p_outlet_receive_note_id)";

                    var dataList = connection.Query<tran_outlet_receive_note_DTO>(query,
                          new
                          {

                              p_outlet_receive_note_id = outlet_challan_receive_id
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


    }

}

