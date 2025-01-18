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
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.Entity;
using EPYSLSAILORAPP.Domain.DTO;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranEmailNotificationService : ITranEmailNotificationService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranEmailNotificationService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_email_notification_entity entity)
        {
            try
            {
               
                var objEntity = JsonConvert.DeserializeObject<tran_email_notification_entity>(JsonConvert.SerializeObject(entity));

                string SenderEmail = _configuration.GetValue<string>("SenderEmail");

                objEntity.sender_email = SenderEmail;

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

        public async Task<bool> UpdateAsync(tran_email_notification_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_email_notification_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_email_notification_entity>> GetAllAsync()
        {
            List<tran_email_notification_entity> list = new List<tran_email_notification_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_email_notification_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_email_notification_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_email_notification_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_email_notification m
                                           where case
                                                     when @search_text is null or length(@search_text)=0 then true
                                                     when @search_text is not null and length(@search_text)>0 and (
                                                            m.sender_email ilike '%' || @search_text || '%'
                                                         ) then true
                                                     else false end)


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_email_notification_entity>(query,
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



        public async Task<tran_email_notification_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_email_notification m   where m.tran_email_notification_id=@Id";

                    var dataList = connection.Query<tran_email_notification_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_email_notification_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_email_notification_entity { tran_email_notification_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_email_notification_insert_sp(tran_email_notification_DTO objtran_email_notification)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_email_notification_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_sender_email", NpgsqlDbType.Text, objtran_email_notification.sender_email == null ? DBNull.Value : objtran_email_notification.sender_email);
                        Command.Parameters.AddWithValue("in_to_email", NpgsqlDbType.Text, objtran_email_notification.to_email == null ? DBNull.Value : objtran_email_notification.to_email);
                        Command.Parameters.AddWithValue("in_cc_email", NpgsqlDbType.Text, objtran_email_notification.cc_email == null ? DBNull.Value : objtran_email_notification.cc_email);
                        Command.Parameters.AddWithValue("in_subject", NpgsqlDbType.Text, objtran_email_notification.subject == null ? DBNull.Value : objtran_email_notification.subject);
                        Command.Parameters.AddWithValue("in_email_body", NpgsqlDbType.Text, objtran_email_notification.email_body == null ? DBNull.Value : objtran_email_notification.email_body);
                        Command.Parameters.AddWithValue("in_initiated_by", NpgsqlDbType.Bigint, objtran_email_notification.initiated_by == null ? DBNull.Value : objtran_email_notification.initiated_by);
                        Command.Parameters.AddWithValue("in_initiated_date", NpgsqlDbType.Date, objtran_email_notification.initiated_date == null ? DBNull.Value : objtran_email_notification.initiated_date);
                        Command.Parameters.AddWithValue("in_is_sent", NpgsqlDbType.Bigint, objtran_email_notification.is_sent == null ? DBNull.Value : objtran_email_notification.is_sent);
                        Command.Parameters.AddWithValue("in_sent_status", NpgsqlDbType.Text, objtran_email_notification.sent_status == null ? DBNull.Value : objtran_email_notification.sent_status);
                        Command.Parameters.AddWithValue("in_sent_time", NpgsqlDbType.Date, objtran_email_notification.sent_time == null ? DBNull.Value : objtran_email_notification.sent_time);
                        Command.Parameters.AddWithValue("in_attempt_count", NpgsqlDbType.Bigint, objtran_email_notification.attempt_count == null ? DBNull.Value : objtran_email_notification.attempt_count);


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
        public async Task<bool> tran_email_notification_update_sp(tran_email_notification_DTO objtran_email_notification)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_email_notification_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_sender_email", NpgsqlDbType.Text, objtran_email_notification.sender_email == null ? DBNull.Value : objtran_email_notification.sender_email);
                        Command.Parameters.AddWithValue("in_to_email", NpgsqlDbType.Text, objtran_email_notification.to_email == null ? DBNull.Value : objtran_email_notification.to_email);
                        Command.Parameters.AddWithValue("in_cc_email", NpgsqlDbType.Text, objtran_email_notification.cc_email == null ? DBNull.Value : objtran_email_notification.cc_email);
                        Command.Parameters.AddWithValue("in_subject", NpgsqlDbType.Text, objtran_email_notification.subject == null ? DBNull.Value : objtran_email_notification.subject);
                        Command.Parameters.AddWithValue("in_email_body", NpgsqlDbType.Text, objtran_email_notification.email_body == null ? DBNull.Value : objtran_email_notification.email_body);
                        Command.Parameters.AddWithValue("in_initiated_by", NpgsqlDbType.Bigint, objtran_email_notification.initiated_by == null ? DBNull.Value : objtran_email_notification.initiated_by);
                        Command.Parameters.AddWithValue("in_initiated_date", NpgsqlDbType.Date, objtran_email_notification.initiated_date == null ? DBNull.Value : objtran_email_notification.initiated_date);
                        Command.Parameters.AddWithValue("in_is_sent", NpgsqlDbType.Bigint, objtran_email_notification.is_sent == null ? DBNull.Value : objtran_email_notification.is_sent);
                        Command.Parameters.AddWithValue("in_sent_status", NpgsqlDbType.Text, objtran_email_notification.sent_status == null ? DBNull.Value : objtran_email_notification.sent_status);
                        Command.Parameters.AddWithValue("in_sent_time", NpgsqlDbType.Date, objtran_email_notification.sent_time == null ? DBNull.Value : objtran_email_notification.sent_time);
                        Command.Parameters.AddWithValue("in_attempt_count", NpgsqlDbType.Bigint, objtran_email_notification.attempt_count == null ? DBNull.Value : objtran_email_notification.attempt_count);


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

    }

}

