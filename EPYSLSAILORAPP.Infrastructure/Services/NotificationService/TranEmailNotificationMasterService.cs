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
using EPYSLSAILORAPP.Domain;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class TranEmailNotificationMasterService : ITranEmailNotificationMasterService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TranEmailNotificationMasterService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(tran_email_notification_master_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_email_notification_master_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(tran_email_notification_master_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<tran_email_notification_master_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<tran_email_notification_master_entity>> GetAllAsync()
        {
            List<tran_email_notification_master_entity> list = new List<tran_email_notification_master_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<tran_email_notification_master_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_email_notification_master_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<tran_email_notification_master_entity>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.*
                                           FROM tran_email_notification_master m
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

                    var dataList = connection.Query<tran_email_notification_master_entity>(query,
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



        public async Task<tran_email_notification_master_entity> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM tran_email_notification_master m   where m.tran_email_notification_master_id=@Id";

                    var dataList = connection.Query<tran_email_notification_master_entity>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<tran_email_notification_master_entity>>(JsonConvert.SerializeObject(dataList));
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

                    var objToDelete = new tran_email_notification_master_entity { tran_email_notification_master_id = Id.Value };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool>  SendNotificationEmail(string to_email,string to_name, string subject,  string attachment, Int64? initiated_by)
        {
            tran_email_notification_DTO tran_email_notification =new tran_email_notification_DTO();
            tran_email_notification.to_email = to_email;
            tran_email_notification.to_name = to_name;
            tran_email_notification.subject = subject;
            tran_email_notification.initiated_by = initiated_by.Value;
            tran_email_notification.initiated_date = DateTime.Now;

            tran_email_notification_master_DTO tran_email_notification_master = new tran_email_notification_master_DTO();
            tran_email_notification_master.subject = subject;
            
            tran_email_notification_master.initiated_date = DateTime.Now;
            tran_email_notification_master.initiated_by = initiated_by.Value;
            tran_email_notification_master.email_attchement1 = attachment;
            tran_email_notification_master.email_template_id = Convert.ToInt64(Enum_Email_Template.Account_Created);
            tran_email_notification_master.TranEmailNotification_List = new List<tran_email_notification_DTO>();
            tran_email_notification_master.TranEmailNotification_List.Add(tran_email_notification);

            return await tran_email_notification_master_insert_sp(tran_email_notification_master);

        }

        public async Task<bool> SendNotificationEmail(List<tran_email_notification_DTO> to_email_list,  string subject, string attachment, Int64? initiated_by,Int64? email_template_id)
        {
            var list_det = new List<tran_email_notification_DTO>();

            foreach (var tran_email_notification in to_email_list)
            {
                tran_email_notification.subject = subject;
                tran_email_notification.initiated_by = initiated_by.Value;
                tran_email_notification.initiated_date = DateTime.Now;
                list_det.Add(tran_email_notification);
            }

            tran_email_notification_master_DTO tran_email_notification_master = new tran_email_notification_master_DTO();
            tran_email_notification_master.subject = subject;
            tran_email_notification_master.initiated_date = DateTime.Now;
            tran_email_notification_master.initiated_by = initiated_by.Value;
            tran_email_notification_master.email_attchement1 = attachment;
            tran_email_notification_master.email_template_id = email_template_id;
            
            tran_email_notification_master.TranEmailNotification_List = list_det;
            
            return await tran_email_notification_master_insert_sp(tran_email_notification_master);

        }
        public async Task<bool> tran_email_notification_master_insert_sp(tran_email_notification_master_DTO objtran_email_notification_master)
        {
            if(objtran_email_notification_master.TranEmailNotification_List.Count>0)
            {
                objtran_email_notification_master.detl_list = JsonConvert.SerializeObject(objtran_email_notification_master.TranEmailNotification_List);
            }

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_email_notification_master_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_initiated_by", NpgsqlDbType.Bigint, objtran_email_notification_master.initiated_by == null ? DBNull.Value : objtran_email_notification_master.initiated_by);
                        Command.Parameters.AddWithValue("in_email_template_id", NpgsqlDbType.Bigint, objtran_email_notification_master.email_template_id == null ? DBNull.Value : objtran_email_notification_master.email_template_id);
                        Command.Parameters.AddWithValue("in_initiated_date", NpgsqlDbType.Date, objtran_email_notification_master.initiated_date == null ? DBNull.Value : objtran_email_notification_master.initiated_date);

                        Command.Parameters.AddWithValue("in_sender_email", NpgsqlDbType.Text, objtran_email_notification_master.sender_email == null ? DBNull.Value : objtran_email_notification_master.sender_email);
                        Command.Parameters.AddWithValue("in_to_email", NpgsqlDbType.Text, objtran_email_notification_master.to_email == null ? DBNull.Value : objtran_email_notification_master.to_email);
                       
                        Command.Parameters.AddWithValue("in_cc_email", NpgsqlDbType.Text, objtran_email_notification_master.cc_email == null ? DBNull.Value : objtran_email_notification_master.cc_email);
                        Command.Parameters.AddWithValue("in_subject", NpgsqlDbType.Text, objtran_email_notification_master.subject == null ? DBNull.Value : objtran_email_notification_master.subject);
                        //Command.Parameters.AddWithValue("in_email_body", NpgsqlDbType.Text, objtran_email_notification_master.email_body == null ? DBNull.Value : objtran_email_notification_master.email_body);
                        Command.Parameters.AddWithValue("in_email_attchement1", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement1 == null ? DBNull.Value : objtran_email_notification_master.email_attchement1);
                        Command.Parameters.AddWithValue("in_email_attchement2", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement2 == null ? DBNull.Value : objtran_email_notification_master.email_attchement2);
                        Command.Parameters.AddWithValue("in_email_attchement3", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement3 == null ? DBNull.Value : objtran_email_notification_master.email_attchement3);
                        Command.Parameters.AddWithValue("in_email_attchement4", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement4 == null ? DBNull.Value : objtran_email_notification_master.email_attchement4);
                        Command.Parameters.AddWithValue("in_email_attchement5", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement5 == null ? DBNull.Value : objtran_email_notification_master.email_attchement5);
                        Command.Parameters.AddWithValue("in_email_attchement6", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement6 == null ? DBNull.Value : objtran_email_notification_master.email_attchement6);
                        Command.Parameters.AddWithValue("in_email_attchement7", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement7 == null ? DBNull.Value : objtran_email_notification_master.email_attchement7);
                        Command.Parameters.AddWithValue("in_email_attchement8", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement8 == null ? DBNull.Value : objtran_email_notification_master.email_attchement8);
                        Command.Parameters.AddWithValue("in_email_attchement9", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement9 == null ? DBNull.Value : objtran_email_notification_master.email_attchement9);
                        Command.Parameters.AddWithValue("in_email_attchement10", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement10 == null ? DBNull.Value : objtran_email_notification_master.email_attchement10);
                        Command.Parameters.AddWithValue("in_detl_list", NpgsqlDbType.Text, objtran_email_notification_master.detl_list == null ? DBNull.Value : objtran_email_notification_master.detl_list);


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
        public async Task<bool> tran_email_notification_master_update_sp(tran_email_notification_master_DTO objtran_email_notification_master)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("tran_email_notification_master_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_sender_email", NpgsqlDbType.Text, objtran_email_notification_master.sender_email == null ? DBNull.Value : objtran_email_notification_master.sender_email);
                        Command.Parameters.AddWithValue("in_to_email", NpgsqlDbType.Text, objtran_email_notification_master.to_email == null ? DBNull.Value : objtran_email_notification_master.to_email);
                        Command.Parameters.AddWithValue("in_cc_email", NpgsqlDbType.Text, objtran_email_notification_master.cc_email == null ? DBNull.Value : objtran_email_notification_master.cc_email);
                        Command.Parameters.AddWithValue("in_subject", NpgsqlDbType.Text, objtran_email_notification_master.subject == null ? DBNull.Value : objtran_email_notification_master.subject);
                        Command.Parameters.AddWithValue("in_email_body", NpgsqlDbType.Text, objtran_email_notification_master.email_body == null ? DBNull.Value : objtran_email_notification_master.email_body);
                        Command.Parameters.AddWithValue("in_email_attchement1", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement1 == null ? DBNull.Value : objtran_email_notification_master.email_attchement1);
                        Command.Parameters.AddWithValue("in_email_attchement2", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement2 == null ? DBNull.Value : objtran_email_notification_master.email_attchement2);
                        Command.Parameters.AddWithValue("in_email_attchement3", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement3 == null ? DBNull.Value : objtran_email_notification_master.email_attchement3);
                        Command.Parameters.AddWithValue("in_email_attchement4", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement4 == null ? DBNull.Value : objtran_email_notification_master.email_attchement4);
                        Command.Parameters.AddWithValue("in_email_attchement5", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement5 == null ? DBNull.Value : objtran_email_notification_master.email_attchement5);
                        Command.Parameters.AddWithValue("in_email_attchement6", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement6 == null ? DBNull.Value : objtran_email_notification_master.email_attchement6);
                        Command.Parameters.AddWithValue("in_email_attchement7", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement7 == null ? DBNull.Value : objtran_email_notification_master.email_attchement7);
                        Command.Parameters.AddWithValue("in_email_attchement8", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement8 == null ? DBNull.Value : objtran_email_notification_master.email_attchement8);
                        Command.Parameters.AddWithValue("in_email_attchement9", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement9 == null ? DBNull.Value : objtran_email_notification_master.email_attchement9);
                        Command.Parameters.AddWithValue("in_email_attchement10", NpgsqlDbType.Text, objtran_email_notification_master.email_attchement10 == null ? DBNull.Value : objtran_email_notification_master.email_attchement10);
                        Command.Parameters.AddWithValue("in_initiated_by", NpgsqlDbType.Bigint, objtran_email_notification_master.initiated_by == null ? DBNull.Value : objtran_email_notification_master.initiated_by);
                        Command.Parameters.AddWithValue("in_initiated_date", NpgsqlDbType.Date, objtran_email_notification_master.initiated_date == null ? DBNull.Value : objtran_email_notification_master.initiated_date);
                        Command.Parameters.AddWithValue("in_detl_list", NpgsqlDbType.Text, objtran_email_notification_master.detl_list == null ? DBNull.Value : objtran_email_notification_master.detl_list);


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

