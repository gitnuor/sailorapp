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
using EPYSLSAILORAPP.Domain.RPC;

namespace EPYSLSAILORAPP.Infrastructure.Services
{

    public class GenEmailTemplateService : IGenEmailTemplateService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GenEmailTemplateService(IConfiguration configuration, IMapper mapper)
        {

            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> SaveAsync(gen_email_template_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_email_template_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<bool> UpdateAsync(gen_email_template_entity entity)
        {
            try
            {
                var objEntity = JsonConvert.DeserializeObject<gen_email_template_entity>(JsonConvert.SerializeObject(entity));

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

        public async Task<List<gen_email_template_entity>> GetAllAsync()
        {
            List<gen_email_template_entity> list = new List<gen_email_template_entity>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var dataList = connection.GetAll<gen_email_template_entity>().ToList();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_email_template_DTO>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<List<gen_email_template_DTO>> GetAllPagedDataAsync(DtParameters param)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"WITH cte_saved AS (SELECT m.gen_email_template_id, m.email_template_html
                   FROM gen_email_template m
                   
                   where case
                             when @search_text is null or length(@search_text) = 0 then true
                             when @search_text is not null and length(@search_text) > 0 and (
                                 m.email_template_name ilike '%' || @search_text || '%'
                                 ) then true
                             else false end)


                            SELECT cte_saved.*,
                                   (COUNT(cte_saved.*) OVER ())::bigint AS total_rows
                            FROM cte_saved
                            order by cte_saved.gen_email_template_id
                            OFFSET @row_index ROWS FETCH NEXT @page_size ROWS ONLY;";

                    var dataList = connection.Query<gen_email_template_DTO>(query,
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



        public async Task<gen_email_template_DTO> GetSingleAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = @"SELECT m.*  FROM gen_email_template m
            
            where m.gen_email_template_id=@Id";

                    var dataList = connection.Query<gen_email_template_DTO>(query,
                        new { @Id = Id }).ToList().FirstOrDefault();

                    return dataList;//JsonConvert.DeserializeObject<List<gen_email_template_entity>>(JsonConvert.SerializeObject(dataList));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> DeleteAsync(Int64 Id)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    var objToDelete = new gen_email_template_entity { gen_email_template_id = Id };

                    bool deleted = connection.Delete(objToDelete);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public async Task<bool> gen_email_template_insert_sp(gen_email_template_DTO objgen_email_template)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_email_template_insert", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_email_template_html", NpgsqlDbType.Text, objgen_email_template.email_template_html == null ? DBNull.Value : objgen_email_template.email_template_html);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_email_template.added_by == null ? DBNull.Value : objgen_email_template.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_email_template.date_added == null ? DBNull.Value : objgen_email_template.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_email_template.updated_by == null ? DBNull.Value : objgen_email_template.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_email_template.date_updated == null ? DBNull.Value : objgen_email_template.date_updated);
                        Command.Parameters.AddWithValue("in_gen_email_template_category_id", NpgsqlDbType.Bigint, objgen_email_template.gen_email_template_category_id == null ? DBNull.Value : objgen_email_template.gen_email_template_category_id);


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
        public async Task<bool> gen_email_template_update_sp(gen_email_template_DTO objgen_email_template)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var Command = new NpgsqlCommand("gen_email_template_update", connection);

                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("in_email_template_html", NpgsqlDbType.Text, objgen_email_template.email_template_html == null ? DBNull.Value : objgen_email_template.email_template_html);
                        Command.Parameters.AddWithValue("in_added_by", NpgsqlDbType.Bigint, objgen_email_template.added_by == null ? DBNull.Value : objgen_email_template.added_by);
                        Command.Parameters.AddWithValue("in_date_added", NpgsqlDbType.Date, objgen_email_template.date_added == null ? DBNull.Value : objgen_email_template.date_added);
                        Command.Parameters.AddWithValue("in_updated_by", NpgsqlDbType.Bigint, objgen_email_template.updated_by == null ? DBNull.Value : objgen_email_template.updated_by);
                        Command.Parameters.AddWithValue("in_date_updated", NpgsqlDbType.Date, objgen_email_template.date_updated == null ? DBNull.Value : objgen_email_template.date_updated);
                        Command.Parameters.AddWithValue("in_gen_email_template_category_id", NpgsqlDbType.Bigint, objgen_email_template.gen_email_template_category_id == null ? DBNull.Value : objgen_email_template.gen_email_template_category_id);


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
        public async Task<List<rpc_gen_email_template_DTO>> GetAllJoined_GenEmailTemplateAsync(Int64 currnet_page, Int64 page_size)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString("PGConnectionString")))
                {
                    connection.Open();

                    string query = $"SELECT * FROM proc_sp_get_data_gen_email_template( @currnet_page,@page_size)";

                    var dataList = connection.Query<rpc_gen_email_template_DTO>(query,
                          new
                          {
                              currnet_page = currnet_page,
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

