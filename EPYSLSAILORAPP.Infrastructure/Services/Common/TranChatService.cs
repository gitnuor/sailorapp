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
using System.Numerics;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranChatService : ITranChatService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranChatService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_chat_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_chat_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<Int64> SaveAndGetNewMessageCount(tran_chat_DTO obj)
        {

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM tran_chat_insert( " +
                        $"@in_date_added,@in_is_group,@in_gen_chat_group_id,@in_gen_chat_group_name, " +
                        $"@in_from_user_name,@in_to_user_name,@in_message,@in_message_json," +
                        "@in_to_chat_group_users)";

                    var objCount = connection.ExecuteScalar<long>(query,
                          new
                          {
                              in_date_added = DateTime.Now,
                              in_from_user_name = obj.from_user_name,
                              in_to_user_name= obj.to_user_name,
                              in_message= obj.message,
                              in_message_json=obj.message_json,
                              in_is_group=obj.is_group,
                              in_gen_chat_group_id=obj.to_chat_group_id,
                              in_gen_chat_group_name= obj.to_chat_group_name,
                              in_to_chat_group_users=obj.to_chat_group_users,

                          }
                         );


                    return objCount;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }



        public async Task<bool> UpdateAsync(tran_chat_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_chat_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_chat_entity>> GetAllAsync()
        {
            List<tran_chat_entity> list = new List<tran_chat_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_chat_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_chat_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_chat_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"       update tran_chat set is_view=1
                                            where  (
                                                   to_user_name=@to_user_name and from_user_name=@from_user_name
                                                 and to_chat_group_id=0
                                                );

                    
                                            WITH cte_saved AS (SELECT m.*
                                           FROM tran_chat m
                                           where (
                                                     (m.to_user_name=@to_user_name and m.from_user_name=@from_user_name
                                                    or m.to_user_name=@from_user_name and m.from_user_name=@to_user_name)
                                                   and m.to_chat_group_id=0
                                                  )
                                             )


                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            ORDER BY cte_saved.tran_chat_id DESC
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_chat_entity>(query,
                        new
                        {
                            to_user_name=param.strSecondID,
                            from_user_name=param.strMasterID,
                           // search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList().OrderBy(p=>p.tran_chat_id).ToList();

                    //foreach(var obj in dataList)
                    //{

                    //}

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_chat_DTO>> GetAllPagedGroupChatDetailsDataAsync(Chat_DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"       update tran_chat set is_view=1
                                            where  (
                                                   to_user_name=@to_user_name and from_user_name=@from_user_name
                                                 and to_chat_group_id=@group_id
                                                );

                    WITH cte_saved AS (SELECT m.*, u.emp_pic
                                                FROM tran_chat m
                                                         inner join owin_user u on u.user_name = m.from_user_name
                                                where  m.to_user_name = @to_user_name 
                                                  and m.to_chat_group_id =@group_id and m.is_group = 1

                                                )

                                            SELECT cte_saved.*,
                                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                            FROM cte_saved
                                            ORDER BY cte_saved.tran_chat_id DESC
                                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_chat_DTO>(query,
                        new
                        {
                            to_user_name = param.to_user_name,
                            from_user_name = param.from_user_name,
                            group_id=param.group_id,
                            // search_text = param.Search.Value,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList().OrderBy(p => p.tran_chat_id).ToList();

                    //foreach(var obj in dataList)
                    //{

                    //}

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_chat_DTO>> GetAllPagedMessageBox(Chat_DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT 0 as to_chat_group_id, m.from_user_name, u.emp_pic
                                    FROM tran_chat m
                                             inner join owin_user u on u.user_name = m.from_user_name
                                    where m.to_user_name = @to_user_name and m.to_chat_group_id=0
                                    group by m.from_user_name, u.emp_pic
                                    union
                                    SELECT m.to_chat_group_id, m.to_chat_group_name, ''
                                    FROM tran_chat m
                                             inner join owin_user u on u.user_name = m.from_user_name
                                    where m.to_user_name =@to_user_name
                                      and m.is_group = 1
                                    group by m.to_chat_group_id, m.to_chat_group_name)

                                         SELECT cte_saved.*,
                                                (
                                                    case
                                                        when cte_saved.to_chat_group_id = 0 then
                                                            (select date_added
                                                             from tran_chat tc
                                                             where tc.to_user_name = @to_user_name
                                                               and (tc.from_user_name = cte_saved.from_user_name or
                                                                    tc.to_chat_group_name = cte_saved.from_user_name)
                                                             and tc.to_chat_group_id=0
                                                             order by tc.tran_chat_id desc
                                                             limit 1)
                                                         when cte_saved.to_chat_group_id > 0 then
                                                            (select date_added
                                                             from tran_chat tc
                                                             where tc.to_user_name = @to_user_name
                                                               and (tc.from_user_name = cte_saved.from_user_name or
                                                                    tc.to_chat_group_name = cte_saved.from_user_name)
                                                             and tc.to_chat_group_id=cte_saved.to_chat_group_id
                                                             order by tc.tran_chat_id desc
                                                             limit 1)

                                                    else null end
                                                    ) as date_added,
                                             (
                                                    case
                                                        when cte_saved.to_chat_group_id = 0 then
                                                            (select message
                                                             from tran_chat tc
                                                             where tc.to_user_name = @to_user_name
                                                               and (tc.from_user_name = cte_saved.from_user_name or
                                                                    tc.to_chat_group_name = cte_saved.from_user_name)
                                                             and tc.to_chat_group_id=0
                                                             order by tc.tran_chat_id desc
                                                             limit 1)
                                                         when cte_saved.to_chat_group_id > 0 then
                                                            (select message
                                                             from tran_chat tc
                                                             where tc.to_user_name = @to_user_name
                                                               and (tc.from_user_name = cte_saved.from_user_name or
                                                                    tc.to_chat_group_name = cte_saved.from_user_name)
                                                             and tc.to_chat_group_id=cte_saved.to_chat_group_id
                                                             order by tc.tran_chat_id desc
                                                             limit 1)

                                                    else null end
                                                    ) as last_message,
                                                (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                                         FROM cte_saved
                                       OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<tran_chat_DTO>(query,
                        new
                        {
                            to_user_name = param.to_user_name,
                            //from_user_name=param.from_user_name,
                            row_index = param.Start,
                            page_size = param.Length
                        }).ToList().OrderBy(p => p.tran_chat_id).ToList();

                    return dataList;

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


        //public async Task<List<tran_chat_DTO>> GetAllPagedMessageBox(DtParameters param)
        //{
        //    try
        //    {

        //        using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
        //        {
        //            connection.Open();

        //            string query = @"WITH cte_saved AS (SELECT m.from_user_name,u.emp_pic
        //                                FROM tran_chat m
        //                                inner join owin_user u on u.user_name=m.from_user_name
        //                                where  m.to_user_name = @to_user_name
        //                                group by m.from_user_name,u.emp_pic  )


        //                                SELECT cte_saved.*,(select date_added from tran_chat tc where tc.to_user_name=@to_user_name
        //                                and tc.from_user_name= cte_saved.from_user_name order by tc.tran_chat_id desc limit 1) date_added,
        //                                    (select message from tran_chat tc where tc.to_user_name=@to_user_name
        //                                and tc.from_user_name= cte_saved.from_user_name order by tc.tran_chat_id desc limit 1) last_message,
        //                                       (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
        //                                FROM cte_saved
        //                               OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

        //            var dataList = connection.Query<tran_chat_DTO>(query,
        //                new
        //                {
        //                    to_user_name = param.strMasterID,
        //                    row_index = param.Start,
        //                    page_size = param.Length
        //                }).ToList().OrderBy(p => p.tran_chat_id).ToList();

        //            return dataList;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //}

        public async Task<tran_chat_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_chat m   where m.tran_chat_id=@Id";

                    var dataList = connection.Query<tran_chat_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_chat_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_chat_entity { tran_chat_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> tran_chat_insert_sp(tran_chat_DTO objtran_chat)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_chat_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_from_user_id", NpgsqlDbType.Bigint, objtran_chat.from_user_id == null ? DBNull.Value : objtran_chat.from_user_id);
                        Command.Parameters.AddWithValue("in_from_user_name", NpgsqlDbType.Text, objtran_chat.from_user_name == null ? DBNull.Value : objtran_chat.from_user_name);
                        Command.Parameters.AddWithValue("in_to_user_id", NpgsqlDbType.Bigint, objtran_chat.to_user_id == null ? DBNull.Value : objtran_chat.to_user_id);
                        Command.Parameters.AddWithValue("in_to_user_name", NpgsqlDbType.Text, objtran_chat.to_user_name == null ? DBNull.Value : objtran_chat.to_user_name);
                        Command.Parameters.AddWithValue("in_message", NpgsqlDbType.Text, objtran_chat.message == null ? DBNull.Value : objtran_chat.message);
                        Command.Parameters.AddWithValue("in_message_json", NpgsqlDbType.Text, objtran_chat.message_json == null ? DBNull.Value : objtran_chat.message_json);
                        Command.Parameters.AddWithValue("in_is_view", NpgsqlDbType.Bigint, objtran_chat.is_view == null ? DBNull.Value : objtran_chat.is_view);
                        Command.Parameters.AddWithValue("in_date_viewed", NpgsqlDbType.Date, objtran_chat.date_viewed == null ? DBNull.Value : objtran_chat.date_viewed);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_chat.date_added == null ? DBNull.Value : objtran_chat.date_added);


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
        public async Task<bool> tran_chat_update_sp(tran_chat_DTO objtran_chat)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_chat_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_from_user_id", NpgsqlDbType.Bigint, objtran_chat.from_user_id == null ? DBNull.Value : objtran_chat.from_user_id);
                        Command.Parameters.AddWithValue("in_from_user_name", NpgsqlDbType.Text, objtran_chat.from_user_name == null ? DBNull.Value : objtran_chat.from_user_name);
                        Command.Parameters.AddWithValue("in_to_user_id", NpgsqlDbType.Bigint, objtran_chat.to_user_id == null ? DBNull.Value : objtran_chat.to_user_id);
                        Command.Parameters.AddWithValue("in_to_user_name", NpgsqlDbType.Text, objtran_chat.to_user_name == null ? DBNull.Value : objtran_chat.to_user_name);
                        Command.Parameters.AddWithValue("in_message", NpgsqlDbType.Text, objtran_chat.message == null ? DBNull.Value : objtran_chat.message);
                        Command.Parameters.AddWithValue("in_message_json", NpgsqlDbType.Text, objtran_chat.message_json == null ? DBNull.Value : objtran_chat.message_json);
                        Command.Parameters.AddWithValue("in_is_view", NpgsqlDbType.Bigint, objtran_chat.is_view == null ? DBNull.Value : objtran_chat.is_view);
                        Command.Parameters.AddWithValue("in_date_viewed", NpgsqlDbType.Date, objtran_chat.date_viewed == null ? DBNull.Value : objtran_chat.date_viewed);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objtran_chat.date_added == null ? DBNull.Value : objtran_chat.date_added);


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

