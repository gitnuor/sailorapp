using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using EPYSLSAILORAPP.Application.DTO;
using EPYSLSAILORAPP.Application.Interface;
using EPYSLSAILORAPP.Domain.DTO;
using EPYSLSAILORAPP.Domain.Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranNotificationService : ITranNotificationService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
      
        public TranNotificationService(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_notification_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_notification_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task UpdateAsync(Int64? notification_id)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"Update tran_notification set is_view=1 where tran_notification_id=@tran_notification_id";

                    var dataList = connection.Execute(query,
                        new
                        {
                            tran_notification_id = notification_id
                        });
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_notification_entity>> GetAllAsync()
        {
            List<tran_notification_entity> list = new List<tran_notification_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_notification_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_notification_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_notification_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.* FROM tran_notification m where m.to_user_name=@to_user_name and m.is_view=0)

                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            order by cte_saved.tran_notification_id desc
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_notification_entity>(query,
                        new
                        {
                            to_user_name=param.strMasterID,
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



        public async Task<tran_notification_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_notification m   where m.tran_notification_id=@Id";

                    var dataList = connection.Query<tran_notification_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_notification_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_notification_entity { tran_notification_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<tran_notification_DTO> tran_notification_insert_sp(tran_notification_DTO objtran_notification)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_notification_insert( " +
                        $"@in_to_user_id,@in_is_view,@in_date_added,@in_message,@in_to_user_name,@in_notifacation_link)";

                    var objData = connection.Query<tran_notification_DTO>(query,
                          new
                          {
                              in_to_user_id = objtran_notification.to_user_id,
                              in_is_view = 0,
                              in_date_added=DateTime.Now,   
                              in_to_user_name = objtran_notification.to_user_name,
                              in_message = objtran_notification.message,
                              //in_message_json = objtran_notification.message_json,
                              in_notifacation_link = objtran_notification.notifacation_link
                          }
                         );


                    return objData.FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
           
        }
        public async Task<bool> tran_notification_update_sp(tran_notification_DTO objtran_notification)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_notification_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_to_user_id", NpgsqlDbType.Bigint, objtran_notification.to_user_id == null ? DBNull.Value : objtran_notification.to_user_id);
                        Command.Parameters.AddWithValue("in_to_user_name", NpgsqlDbType.Text, objtran_notification.to_user_name == null ? DBNull.Value : objtran_notification.to_user_name);
                        Command.Parameters.AddWithValue("in_message", NpgsqlDbType.Text, objtran_notification.message == null ? DBNull.Value : objtran_notification.message);
                        Command.Parameters.AddWithValue("in_message_json", NpgsqlDbType.Text, objtran_notification.message_json == null ? DBNull.Value : objtran_notification.message_json);
                        Command.Parameters.AddWithValue("in_is_view", NpgsqlDbType.Bigint, objtran_notification.is_view == null ? DBNull.Value : objtran_notification.is_view);
                        Command.Parameters.AddWithValue("in_date_viewed", NpgsqlDbType.Date, objtran_notification.date_viewed == null ? DBNull.Value : objtran_notification.date_viewed);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_notification.date_added == null ? DBNull.Value : objtran_notification.date_added);
                        Command.Parameters.AddWithValue("in_module_id", NpgsqlDbType.Bigint, objtran_notification.module_id == null ? DBNull.Value : objtran_notification.module_id);
                        Command.Parameters.AddWithValue("in_notifacation_link", NpgsqlDbType.Text, objtran_notification.notifacation_link == null ? DBNull.Value : objtran_notification.notifacation_link);


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

